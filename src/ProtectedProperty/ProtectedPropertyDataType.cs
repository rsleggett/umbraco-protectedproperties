using System;
using System.Linq;
using System.Reflection;
using System.Web;
using umbraco.BusinessLogic;
using umbraco.cms.businesslogic.datatype;
using umbraco.interfaces;

namespace Rob.Umbraco.DataTypes.ProtectedProperty
{
    public class ProtectedPropertyDataType : BaseDataType,IDataType
    {
        private ProtectedPropertyPrevalueEditor _prevalueEditor;
        private IData _data;
        private IDataType _internal;
        private IDataEditor _editor;

        public override IDataPrevalue PrevalueEditor
        {
            get
            {
                if (_prevalueEditor == null)
                    _prevalueEditor = new ProtectedPropertyPrevalueEditor(this);
                return _prevalueEditor;
            }
        }

        public IDataType Internal
        {
            get
            {
                if(_internal == null)
                {
                    if (!string.IsNullOrEmpty(((ProtectedPropertyPrevalueEditor) PrevalueEditor).Configuration))
                    {
                        string[] config = ((ProtectedPropertyPrevalueEditor) PrevalueEditor).Configuration.Split('|');
                        var def = GetDataTypeDefinitionFromConfig(config[0]);

                        if (def != null)
                        {
                            _internal = def.DataType;
                        }
                    }
                }
                return _internal;
            }
        }

        public override IData Data
        {
            get 
            {
                if (_data == null)
                {
                    _data = Internal.Data;
                }
                return _data;
            }
        }

        public override string DataTypeName
        {
            get { return "Protected Property v0.2"; }
        }

        public override IDataEditor DataEditor
        {
            get
            {
                if (_editor == null)
                {
                    if (!string.IsNullOrEmpty(((ProtectedPropertyPrevalueEditor)PrevalueEditor).Configuration))
                    {
                        string[] config = ((ProtectedPropertyPrevalueEditor)PrevalueEditor).Configuration.Split('|');
                        IProtectedPropertyAccessCheck check = LoadTypeFromConfig(config[1]);
                        bool disabled = !check.UserHasAccess(GetPageId(), User.GetCurrent());

                        if (disabled)
                            return new DisabledPropertyDataEditor(check.ProtectionMessage);

                        _editor = Internal.DataEditor;
                    }
                    
                }
                return _editor;
            }
        }

        public override Guid Id
        {
            get { return new Guid("df6da481-38e7-4827-a19a-dc1f1dbf3ed2"); }
        }

        private DataTypeDefinition GetDataTypeDefinitionFromConfig(string dataTypeDefinitionId)
        {
            DataTypeDefinition[] defs = DataTypeDefinition.GetAll();
            Guid dataTypeGuid = new Guid(dataTypeDefinitionId);
            return (from d in defs
                       where d.UniqueId.CompareTo(dataTypeGuid) == 0
                       select d).SingleOrDefault();
        }

        private int GetPageId()
        {
            return int.Parse(HttpContext.Current.Request["id"]);
        }

        private IProtectedPropertyAccessCheck LoadTypeFromConfig(string protectionTypeName)
        {
            if (string.IsNullOrEmpty(protectionTypeName))
                return new AdminOnlyAccessCheck();

            Type t = Type.GetType(protectionTypeName);
            if (t != null && t.GetInterface(typeof(IProtectedPropertyAccessCheck).Name) != null)
            {
                ConstructorInfo info = t.GetConstructor(new Type[0]);
                object created = info.Invoke(new object[0]);
                return created as IProtectedPropertyAccessCheck;
            }
            return new AdminOnlyAccessCheck();
        }
    }
}
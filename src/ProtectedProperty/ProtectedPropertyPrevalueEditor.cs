using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using umbraco.cms.businesslogic.datatype;
using umbraco.interfaces;
using umbraco.DataLayer;
using umbraco.BusinessLogic;

namespace MagneticNorth.Umbraco.DataTypes.ProtectedProperty
{
    public class ProtectedPropertyPrevalueEditor : PlaceHolder, IDataPrevalue
    {
        private BaseDataType _datatype;

        private DropDownList _lstDataTypes;
        private DropDownList _lstTypes;

        public ProtectedPropertyPrevalueEditor(BaseDataType DataType)
        {
            _datatype = DataType;
            SetupChildControls();
        }

        private void SetupChildControls()
        {
            _lstDataTypes = new DropDownList();
            _lstDataTypes.ID = "lstDataTypes";
            _lstDataTypes.CssClass = "umbEditorDropdown";
            _lstDataTypes.DataTextField = "Text";
            _lstDataTypes.DataValueField = "UniqueId";
            
            _lstTypes = new DropDownList();
            _lstTypes.ID = "lstProtectionTypes";
            _lstTypes.CssClass = "umbEditorDropdown";
            _lstTypes.DataTextField = "Name";
            _lstTypes.DataValueField = "AssemblyQualifiedName";

            Controls.Add(_lstDataTypes);
            Controls.Add(_lstTypes);
        }

        private List<DataTypeDefinition> GetExistingDatatypes()
        {
            return DataTypeDefinition.GetAll().ToList();
        }

        private List<Type> GetProtectionTypes()
        {
            return ReflectionHelper.GetAllClassesThatImplementProtectedPropertyAccessCheck().ToList();
        }

        protected override void OnLoad(EventArgs e)
        {

            base.OnLoad(e);
            if (!Page.IsPostBack)
            {
                _lstDataTypes.DataSource = GetExistingDatatypes();
                _lstDataTypes.DataBind();

                _lstTypes.DataSource = GetProtectionTypes();
                _lstTypes.DataBind();

                if (Configuration.Length > 0)
                {
                    string dataType = Configuration.Split('|')[0];
                    string protectionType = Configuration.Split('|')[1];
                    _lstDataTypes.SelectedValue = dataType;
                    _lstTypes.SelectedValue = protectionType;
                }
            }
        }

         protected override void Render(HtmlTextWriter writer)
        {
            writer.WriteLine("<table>");
            writer.Write("<tr><th>Protected Datatype:</th><td>");
            _lstDataTypes.RenderControl(writer);
            writer.Write("</td></tr>");
            writer.Write("<tr><th>Protection type:</th><td>");
             _lstTypes.RenderControl(writer);
            writer.Write("</td></tr>");
            writer.Write("</table>");
        }

        public System.Web.UI.Control Editor
        {
            get
            {
                return this;
            }
        }

        public void Save()
        {            
            _datatype.DBType = (DBTypes)Enum.Parse(typeof(DBTypes), DBTypes.Ntext.ToString(), true);

            string dataType = _lstDataTypes.SelectedValue;
            string protectionType = _lstTypes.SelectedValue;

            string data = dataType + "|" + protectionType;

            SqlHelper.ExecuteNonQuery("delete from cmsDataTypePreValues where datatypenodeid = @dtdefid", 
                SqlHelper.CreateParameter("@dtdefid", _datatype.DataTypeDefinitionId));

            SqlHelper.ExecuteNonQuery("insert into cmsDataTypePreValues (datatypenodeid,[value],sortorder,alias) values (@dtdefid,@value,0,'')", 
                SqlHelper.CreateParameter("@dtdefid", _datatype.DataTypeDefinitionId), SqlHelper.CreateParameter("@value", data));

        }

        public ISqlHelper SqlHelper
        {
            get
            {
                return Application.SqlHelper;
            }
        }

        public string Configuration
        {
            get
            {
                object conf = SqlHelper.ExecuteScalar<object>(
                    "select value from cmsDataTypePreValues where datatypenodeid = @datatypenodeid",
                    SqlHelper.CreateParameter("@datatypenodeid", _datatype.DataTypeDefinitionId));

                if (conf != null)
                    return conf.ToString();
                
                return "";
            }
        }
    }
}
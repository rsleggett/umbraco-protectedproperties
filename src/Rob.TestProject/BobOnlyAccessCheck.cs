using System;
using Rob.Umbraco.DataTypes.ProtectedProperty;
using umbraco.BusinessLogic;

namespace Rob.TestProject
{
    public class BobOnlyAccessCheck : IProtectedPropertyAccessCheck
    {
        #region Implementation of IProtectedPropertyAccessCheck

        public bool UserHasAccess(int nodeId, User user)
        {
            return user.Name == "bob";
        }

        #endregion

        #region Implementation of IProtectedPropertyAccessCheck

        public string Name
        {
            get { return "Bob only"; }
        }

        public string ProtectionMessage
        {
            get { return "Only Bob can access this field"; }
        }

        #endregion
    }
}

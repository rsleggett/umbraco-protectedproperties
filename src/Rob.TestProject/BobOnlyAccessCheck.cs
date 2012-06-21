using System;
using Rob.Umbraco.DataTypes.ProtectedProperty;
using umbraco.BusinessLogic;

namespace Rob.TestProject
{
    /// <summary>
    /// Example implementation which protects a property unless the user's name is "bob"
    /// </summary>
    public class BobOnlyAccessCheck : IProtectedPropertyAccessCheck
    {
        public bool UserHasAccess(int nodeId, User user)
        {
            //Can do anything here.
            return user.Name == "bob";
        }

        public string Name
        {
            get { return "Bob only"; }
        }

        public string ProtectionMessage
        {
            //This is the message displayed in the Umbraco GUI
            get { return "Only Bob can access this field"; }
        }
    }
}

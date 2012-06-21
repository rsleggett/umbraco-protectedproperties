namespace Rob.Umbraco.DataTypes.ProtectedProperty
{
    public class AdminOnlyAccessCheck : IProtectedPropertyAccessCheck
    {
        public bool UserHasAccess(int nodeId, umbraco.BusinessLogic.User user)
        {
            return user.IsAdmin();
        }

        public string Name
        {
            get { return "Admin only"; }
        }


        public string ProtectionMessage
        {
            get { return "This property is only available to admins."; }
        }
    }
}
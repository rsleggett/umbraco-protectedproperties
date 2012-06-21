using umbraco.BusinessLogic;

namespace MagneticNorth.Umbraco.DataTypes.ProtectedProperty
{
    public interface IProtectedPropertyAccessCheck
    {
        bool UserHasAccess(int nodeId, User user);
        string Name { get; }
        string ProtectionMessage { get; }
    }
}

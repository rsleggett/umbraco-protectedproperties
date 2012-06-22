Umbraco Protected Properties
===========================

Protected Properties Datatype for Umbraco

This package provides a framework to allow the creation of data types which are only 
available depending on certain conditions.

Concepts:

The idea is to wrap existing datatypes with the protected one. So for example say you had an existing datatype called "Banner Locations" which you wanted only available to Administrators you would create a second DataType called "Protected Banner Locations" and set the datatype to "Banner Locations" and then select the class from the other drop down.

The package ships with an AdminOnly implementation but an interface allows easy extension with arbitrary conditions.

For example:

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
    
To install this, just place the compiled DLL Rob.Umbraco.DataTypes.ProtectedProperty in the bin directory. Then you can go to the developer tab and create a new datatype.

Special thanks to Sanj and Steve at magneticNorth (http://mnatwork.com) for finding the code.
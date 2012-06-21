using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Configuration;

namespace MagneticNorth.Umbraco.DataTypes.ProtectedProperty
{
    public static class ReflectionHelper
    {
        public static List<Type> GetAllClassesThatImplementProtectedPropertyAccessCheck()
        {
            var type = typeof(IProtectedPropertyAccessCheck);
            var types = AppDomain.CurrentDomain.GetAssemblies().ToList()
                .Where(x => !x.FullName.StartsWith("System."))
                .SelectMany(s => s.GetTypes())
                .Where(p => type.IsAssignableFrom(p) && p.IsClass);

            return types.ToList();
            //var instances = new List<IProtectedPropertyAccessCheck>();

            //foreach (Type t in types)
            //{
            //    ConstructorInfo info = t.GetConstructor(new Type[0]);
            //    object created = info.Invoke(new object[0]);

            //    instances.Add(created as IProtectedPropertyAccessCheck);

            //}

            
            //TODO: Get the reflection working


            //List<Type> types = new List<Type>();
            //foreach (var typeName in ConfigurationManager.AppSettings["ProtectedPropertyAccessCheckTypeList"].Split('|'))
            //{
            //    Type type = Type.GetType(typeName);
            //    if (type != null)
            //        types.Add(type);
            //}

            //return types;
        }
    }
}

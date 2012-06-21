using System;
using System.Collections.Generic;
using System.Linq;

namespace Rob.Umbraco.DataTypes.ProtectedProperty
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
        }
    }
}

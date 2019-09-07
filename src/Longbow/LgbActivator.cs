using System;
using System.Reflection;

namespace Longbow
{
    /// <summary>
    /// Contains methods to create types of objects locally or remotely, or obtain references to existing remote objects. This class cannot be inherited.
    /// </summary>
    public static class LgbActivator
    {
        /// <summary>
        /// Creates an instance of the type whose name is specified, using the named assembly and default constructor.
        /// </summary>
        /// <typeparam name="T">The type of object to create</typeparam>
        /// <param name="assemblyName">The name of the assembly where the type named typeName is sought. If assemblyName is a null reference (Nothing in Visual Basic), the executing assembly is searched.</param>
        /// <param name="typeName">The name of the preferred type.</param>
        /// <param name="args">An array of arguments that match in number, order, and type the parameters of the constructor to invoke. If args is an empty array or a null reference, the constructor that takes no parameters (the default constructor) is invoked.</param>
        /// <returns></returns>
        public static T CreateInstance<T>(string assemblyName = null, string typeName = null, params object[] args) where T : class
        {
            T ret = default;
            if (!string.IsNullOrEmpty(assemblyName))
            {
                if (string.IsNullOrEmpty(typeName)) throw new ArgumentNullException(nameof(typeName));
                var assembly = Assembly.Load(assemblyName);
                ret = Activator.CreateInstance(assembly.GetType(typeName, true, true), args) as T;
            }
            else
            {
                ret = Activator.CreateInstance<T>();
            }
            return ret;
        }
    }
}

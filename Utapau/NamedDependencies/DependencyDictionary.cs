using System;
using System.Collections.Generic;

namespace Utapau.NamedDependencies
{
    internal static class DependencyDictionary
    {
        private static readonly IDictionary<Type, IDictionary<string, Type>> TypesDictionary;

        static DependencyDictionary()
        {
            TypesDictionary = new Dictionary<Type, IDictionary<string, Type>>();
        }

        public static void Register<TInterface, TImplementation>(string name) where TImplementation : class
        {
            var interfaceType = typeof(TInterface);
            var implementationType = typeof(TImplementation);
            
            if (!TypesDictionary.ContainsKey(interfaceType))
            {
                TypesDictionary[interfaceType] = new Dictionary<string, Type>();
            }
            else if (TypesDictionary[interfaceType].ContainsKey(name))
            {
                throw new InvalidOperationException($"Service {name} has been already registered");
            }
            
            TypesDictionary[interfaceType][name] = implementationType;
        }
        
        public static Type GetTypeByName<TInterface>(string name)
        {
            var interfaceType = typeof(TInterface);
            
            if (!TypesDictionary.ContainsKey(interfaceType))
            {
                throw new KeyNotFoundException(interfaceType.Name);
            }
            
            if (!TypesDictionary[interfaceType].ContainsKey(name))
            {
                throw new KeyNotFoundException(name);
            }

            return TypesDictionary[interfaceType][name];
        }

        public static void Clear()
        {
            TypesDictionary.Clear();
        }
    }
}
using System;
using System.Collections.Generic;

namespace Utapau
{
    internal static class DependencyDictionary
    {
        private static readonly IDictionary<Type, IDictionary<string, Type>> Dictionary;

        static DependencyDictionary()
        {
            Dictionary = new Dictionary<Type, IDictionary<string, Type>>();
        }

        public static void Register<TInterface, TImplementation>(string name) where TImplementation : class
        {
            var interfaceType = typeof(TInterface);
            var implementationType = typeof(TImplementation);
            
            if (!Dictionary.ContainsKey(interfaceType))
            {
                Dictionary[interfaceType] = new Dictionary<string, Type>();
            }
            else if (Dictionary[interfaceType].ContainsKey(name))
            {
                throw new InvalidOperationException($"Service {name} has been already registered");
            }
            
            Dictionary[interfaceType][name] = implementationType;
        }
        
        public static Type GetTypeByName<TInterface>(string name)
        {
            var interfaceType = typeof(TInterface);
            
            if (!Dictionary.ContainsKey(interfaceType))
            {
                throw new KeyNotFoundException(nameof(interfaceType));
            }
            
            if (!Dictionary[interfaceType].ContainsKey(name))
            {
                throw new KeyNotFoundException(nameof(name));
            }

            return Dictionary[interfaceType][name];
        }

        public static void Clear()
        {
            Dictionary.Clear();
        }
    }
}
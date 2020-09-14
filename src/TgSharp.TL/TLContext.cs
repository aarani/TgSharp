using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TgSharp.Common
{
    public static class TLContext
    {
        private static Dictionary<int, Type> Types;
        private static Dictionary<Type, int> ReverseTypes;

        static TLContext()
        {
            Types = new Dictionary<int, Type>();
            ReverseTypes = new Dictionary<Type, int>();
            Types = (from t in Assembly.GetExecutingAssembly().GetTypes()
                     where t.IsClass && t.Namespace != null && t.Namespace.StartsWith(typeof(TLContext).Namespace)
                     where t.IsSubclassOf(typeof(TLObject))
                     where t.GetCustomAttribute(typeof(TLObjectAttribute)) != null
                     select t).ToDictionary(x => ((TLObjectAttribute)x.GetCustomAttribute(typeof(TLObjectAttribute))).Constructor, x => x);
            ReverseTypes = (from t in Assembly.GetExecutingAssembly().GetTypes()
                            where t.IsClass && t.Namespace != null && t.Namespace.StartsWith(typeof(TLContext).Namespace)
                            where t.IsSubclassOf(typeof(TLObject))
                            where t.GetCustomAttribute(typeof(TLObjectAttribute)) != null
                            select t).ToDictionary(x => x, x => ((TLObjectAttribute)x.GetCustomAttribute(typeof(TLObjectAttribute))).Constructor);

            var vectorTypeId = 481674261;
            var genericVectorType = typeof(TLVector<>);

            Type type;
            if (Types.TryGetValue(vectorTypeId, out type))
            {
                if (type != genericVectorType && type != typeof(TLVector))
                {
                    throw new InvalidOperationException($"Type {vectorTypeId} should have been a TLVector type but was {type}");
                }
            }
            else
            {
                Types[vectorTypeId] = genericVectorType;
            }
        }

        public static Type GetType(int Constructor)
        {
            return Types[Constructor];
        }

        public static int GetConstuctor(Type type)
        {
            return ReverseTypes[type];
        }

    }
}

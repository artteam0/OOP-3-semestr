using System;
using System.Reflection;

namespace lab11
{
    public static class Reflector
    {
        public static string Assembly(Type cl)
        {
            return "\nИнформация о сборке: " + cl.Assembly.ToString() + "\n";
        }
        public static string PubConstr(Type cl)
        {
            bool flag = false;
            foreach (ConstructorInfo i in cl.GetConstructors(BindingFlags.Public | BindingFlags.Instance))
            {
                flag = true;
            }
            return "\nНаличие публичных конструкторов: " + flag + "\n";
        }
        public static IEnumerable<string>Methods(Type cl)
        {
            return cl.GetMethods(BindingFlags.Public | BindingFlags.Instance).Select(x => x.Name).Distinct();
        }
        public static IEnumerable<string>Fields(Type cl)
        {
            return cl.GetFields().Select(x => x.Name).Distinct();
        }
        public static IEnumerable<string>Interfaces(Type cl)
        {
            return cl.GetProperties().Select(x => x.Name).Distinct();
        }
        
        public static void Invoke(object obj, string[] parameters, MethodInfo methodName)
        {
            methodName.Invoke(obj, parameters);
        }
        public static void writeInfo(string cl)
        {
            File.WriteAllText("text.txt", cl);
        }
    }
}
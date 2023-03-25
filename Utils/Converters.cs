using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace fileImporter.Utils
{
    public static class Converters
    {
        public static int ToInt32(object valor)
            => valor == null ? 0 : Convert.ToInt32(valor);

        public static string[] GetDescriptionsFromEnum<T>() where T : Enum
        {
            List<string> descriptions = new List<string>();

            foreach (FieldInfo field in typeof(T).GetFields(
                    BindingFlags.Static | BindingFlags.GetField | BindingFlags.Public))
            {
                var description = field.GetCustomAttributes(typeof(System.ComponentModel.DescriptionAttribute), true);

                descriptions.Add(((System.ComponentModel.DescriptionAttribute)(description[0])).Description);
            }

            return descriptions.ToArray();
        }

        public static IEnumerable<T> GetValues<T>() 
            => Enum.GetValues(typeof(T)).Cast<T>();
        

        public static T GetValueFromDescription<T>(string description) where T : Enum
        {
            foreach(var field in typeof(T).GetFields())
            {
                if (Attribute.GetCustomAttribute(field,
                typeof(DescriptionAttribute)) is DescriptionAttribute attribute)
                {
                    if (attribute.Description == description)
                        return (T)field.GetValue(null);
                }
                else
                {
                    if (field.Name == description)
                        return (T)field.GetValue(null);
                }
            }

            throw new ArgumentException("Not found.", nameof(description));
            // Or return default(T);
        }
    }
}
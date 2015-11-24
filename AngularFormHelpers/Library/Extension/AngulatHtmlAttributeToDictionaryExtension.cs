using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using AngularFormHelpers.Library.Helpers.Form;

namespace AngularFormHelpers.Library.Extension
{
    public static class AngulatHtmlAttributeToDictionaryExtension
    {
        public static IDictionary<string, object> ToDictionary<TModel, TProperty>(this AngularHtmlAttributes attributes)
        {
            var result = new Dictionary<string, object>();

            var propertyList =
                attributes.GetType()
                    .GetProperties()
                    .Where(r => r.GetCustomAttribute<DataMemberAttribute>() != null);

            foreach (var p in propertyList)
            {
                var propertyName = p.GetCustomAttribute<DataMemberAttribute>().Name ?? p.Name.ToLower();
                var value = p.GetValue(attributes);

                if(value == null) continue;

                result.Add(propertyName, value);
            }

            return result;
        }
    }
}
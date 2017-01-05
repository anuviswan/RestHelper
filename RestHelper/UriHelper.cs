using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace EcSolvo
{
    public static class UrlHelpers
    {
        public static bool IsPrimitiveType(object t)
        {
            return (t.GetType().GetTypeInfo().IsPrimitive 
                || t == typeof(Decimal) 
                || t == typeof(String) 
                || t == typeof(DateTime) 
                || t == typeof(Int32)
                || t == typeof(Boolean));
        }



        /// <summary>
        /// To the query string. http://ole.michelsen.dk/blog/serialize-object-into-a-query-string-with-reflection.html 
        /// </summary>
        /// <param name="request"> The request. </param>
        /// <param name="separator"> The separator. </param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException"> request </exception>
        public static string ToQueryString(this object request, string innerPropertyName = null, string separator = ",")
        {
            if (request == null)
            {
                throw new ArgumentNullException("request");
            }

            StringBuilder propertyQuery = new StringBuilder();

            // Get all primitive properties on the object 
            var properties = request.GetType().GetRuntimeProperties()
                .Where(x => x.CanRead)
                .Where(x => x.GetValue(request, null) != null)
                .Where(x=> IsPrimitiveType(x.PropertyType))
                .ToDictionary(x => x.Name, x => x.GetValue(request, null));
            //.Where(x => !x.GetType().GetTypeInfo().IsClass || (x.GetType().GetTypeInfo().IsClass && x.PropertyType.FullName == "System.String"))




            foreach (KeyValuePair<string, object> kvp in properties)
            {
                if (string.IsNullOrEmpty(innerPropertyName))
                {
                    propertyQuery.AppendFormat("{0}={1}", Uri.EscapeDataString(kvp.Key), Uri.EscapeDataString(kvp.Value.ToString()));
                }
                else
                {
                    propertyQuery.AppendFormat("{0}.{1}={2}", Uri.EscapeDataString(innerPropertyName), Uri.EscapeDataString(kvp.Key), Uri.EscapeDataString(kvp.Value.ToString()));
                }
                propertyQuery.AppendFormat("&");
            }

            var innerClass = request.GetType().GetRuntimeProperties()
                .Where(x => x.CanRead)
                .Where(x => x.GetValue(request, null) != null)
                .Where(x => !IsPrimitiveType(x.PropertyType))
                .ToDictionary(x => x.Name, x => x.GetValue(request, null));

            // Get names for all IEnumerable properties (excl. string) 
            var propertyCollectionNames = request.GetType().GetRuntimeProperties()
                .Where(x => x.CanRead)
                .Where(x => x.GetValue(request, null) != null)
                .ToDictionary(x => x.Name, x => x.GetValue(request, null))
                .Where(x => !(x.Value is string) && x.Value is IEnumerable)
                .ToDictionary(x => x.Key, x => x.Value);

            // Concat all IEnumerable properties into a comma separated string 
            foreach (var kvp in propertyCollectionNames)
            {
                var valueType = kvp.Value.GetType();
                var valueElemType = valueType.GetTypeInfo().IsGenericType
                                        ? valueType.GetType().GetGenericTypeDefinition()
                                        : valueType.GetElementType();
                if (valueElemType.GetTypeInfo().IsPrimitive || valueElemType == typeof(string)) // List of primitive value type or string
                {
                    var enumerable = kvp.Value as IEnumerable;
                    int count = 0;
                    foreach (object obj in enumerable)
                    {
                        if (string.IsNullOrEmpty(innerPropertyName))
                        {
                            propertyQuery.AppendFormat("{0}[{1}]={2}", Uri.EscapeDataString(kvp.Key), count, Uri.EscapeDataString(obj.ToString()));
                        }
                        else
                        {
                            propertyQuery.AppendFormat("{0}.{1}[{2}]={3}", Uri.EscapeDataString(innerPropertyName), Uri.EscapeDataString(kvp.Key), count, Uri.EscapeDataString(obj.ToString()));
                        }
                        count++;
                        propertyQuery.AppendFormat("&");
                    }
                }
                else if (!IsPrimitiveType(valueElemType)) // list of class Objects
                {
                    int count = 0;
                    foreach (var className in kvp.Value as IEnumerable)
                    {
                        string queryKey = string.Format("{0}[{1}]", kvp.Key, count);
                        propertyQuery.AppendFormat(ToQueryString(className, queryKey));
                        count++;
                    }
                }
            }

            foreach (var className in innerClass)
            {
                propertyQuery.AppendFormat(ToQueryString(className.Value, className.Key));
            }

            return propertyQuery.ToString().Remove(propertyQuery.ToString().Length-1);
        }
    }
}

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
        /// <summary>
        /// Checks if the DataType is Primitive
        /// </summary>
        /// <returns>A Boolean Value indicating if the data type if primitive or not</returns>
        internal static bool IsPrimitiveType(this Type DataType)
        {
            return (DataType.GetTypeInfo().IsPrimitive
                || DataType == typeof(Byte)
                || DataType == typeof(SByte)
                || DataType == typeof(Int32)
                || DataType == typeof(UInt32)
                || DataType == typeof(Int16)
                || DataType == typeof(UInt16)
                || DataType == typeof(Int64)
                || DataType == typeof(UInt64)
                || DataType == typeof(Single)
                || DataType == typeof(Double)
                || DataType == typeof(Char)
                || DataType == typeof(Boolean)
                || DataType == typeof(Object)
                || DataType == typeof(String)
                || DataType == typeof(Decimal)
                || DataType == typeof(DateTime)
                || DataType == typeof(TimeSpan)
                );
        }



        /// <summary>
        /// Convert the object to Query String format.
        /// A Spin off based on the code written by Ole Michelsen
        /// </summary>
        /// <param name="RequestObject"> The request. </param>
        /// <param name="separator"> The separator. </param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException"> request </exception>
        internal static string ToQueryString(this object RequestObject, string InnerPropertyName = null)
        {
            if (RequestObject == null)
            {
                throw new ArgumentNullException("Request Object Null");
            }

            StringBuilder propertyQuery = new StringBuilder();

            // Get all primitive properties on the object 
            var properties = RequestObject.GetType().GetRuntimeProperties()
                                          .Where(x => x.CanRead)
                                          .Where(x => x.GetValue(RequestObject, null) != null)
                                          .Where(x=> x.PropertyType.IsPrimitiveType())
                                          .ToDictionary(x => x.Name, x => x.GetValue(RequestObject, null));

            // Form the Query String for Primitive Types
            foreach (KeyValuePair<string, object> kvp in properties)
            {
                if (string.IsNullOrEmpty(InnerPropertyName))
                    propertyQuery.AppendFormat("{0}={1}", Uri.EscapeDataString(kvp.Key), Uri.EscapeDataString(kvp.Value.ToString()));
                else
                    propertyQuery.AppendFormat("{0}.{1}={2}", Uri.EscapeDataString(InnerPropertyName), Uri.EscapeDataString(kvp.Key), Uri.EscapeDataString(kvp.Value.ToString()));

                propertyQuery.AppendFormat("&");
            }

            // Get all Inner Classes on the object
            var innerClass = RequestObject.GetType().GetRuntimeProperties()
                                        .Where(x => x.CanRead)
                                        .Where(x => x.GetValue(RequestObject, null) != null)
                                        .Where(x => !x.PropertyType.IsPrimitiveType())
                                        .ToDictionary(x => x.Name, x => x.GetValue(RequestObject, null));

            // Get names for all IEnumerable properties (excl. string) 
            var propertyCollectionNames = RequestObject.GetType().GetRuntimeProperties()
                                        .Where(x => x.CanRead)
                                        .Where(x => x.GetValue(RequestObject, null) != null)
                                        .ToDictionary(x => x.Name, x => x.GetValue(RequestObject, null))
                                        .Where(x => !(x.Value is string) && x.Value is IEnumerable)
                                        .ToDictionary(x => x.Key, x => x.Value);

            // Concat all IEnumerable properties into a comma separated string 
            foreach (var kvp in propertyCollectionNames)
            {
                var valueType = kvp.Value.GetType();
                var valueElemType = valueType.GetTypeInfo().IsGenericType
                                        ? valueType.GetType().GetGenericTypeDefinition()
                                        : valueType.GetElementType();
                if (valueElemType.GetType().IsPrimitiveType() || valueElemType == typeof(string)) // List of primitive value type or string
                {
                    var enumerable = kvp.Value as IEnumerable;
                    int count = 0;
                    foreach (object obj in enumerable)
                    {
                        if (string.IsNullOrEmpty(InnerPropertyName))
                        {
                            propertyQuery.AppendFormat("{0}[{1}]={2}", Uri.EscapeDataString(kvp.Key), count, Uri.EscapeDataString(obj.ToString()));
                        }
                        else
                        {
                            propertyQuery.AppendFormat("{0}.{1}[{2}]={3}", Uri.EscapeDataString(InnerPropertyName), Uri.EscapeDataString(kvp.Key), count, Uri.EscapeDataString(obj.ToString()));
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


            if(propertyQuery.ToString()[propertyQuery.ToString().Length - 1]=='&')
                return propertyQuery.ToString().Remove(propertyQuery.ToString().Length - 1);
            else
                return propertyQuery.ToString();
        }
    }
}


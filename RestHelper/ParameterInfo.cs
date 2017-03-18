using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace EcSolvo
{
    /// <summary>
    /// This class tracks and maintains the Parameters for the query
    /// Update 001: Remove Parameter as a separate class to adhere to SRP
    /// </summary>
    internal class ParameterInfo
    {
        #region Private Variables
        private Dictionary<string, object> _URLParameterDictionary;
        private object _MessageBodyParameter;
        #endregion

        #region Internal Methods

        internal void AssignMessageBodyParameter<T>(T Value)
        {
            this._MessageBodyParameter = Value;
        }
       

        /// <summary>
        /// Adds a new parameter to the list
        /// </summary>
        /// <param name="Key">Name representing the parameter</param>
        /// <param name="Value">Value of the parameter</param>
        internal void AddURLParameter<PValue>(string Key, PValue Value)
        {
            if (_URLParameterDictionary == null)
                _URLParameterDictionary = new Dictionary<string, dynamic>();

            _URLParameterDictionary.Add(Key, Value);
        }

        
        /// <summary>
        /// Get QueryString from the Parameter List
        /// </summary>
        /// <returns>Returns a string representing Query String</returns>
        internal string GetQueryString()
        {
            if (HasParameter)
            {
                List<string> parameters = new List<string>();
                foreach (var param in _URLParameterDictionary)
                {
                    // If it is primitive type
                    if (param.Value.GetType().IsPrimitiveType())
                        parameters.Add(string.Format("{0}={1}", param.Key, param.Value));
                    // If it is an array of Primitive types
                    else if(param.Value.GetType().IsArray && param.Value.GetType().GetElementType().IsPrimitiveType())
                    {
                        foreach (var item in ((IEnumerable)param.Value))
                            parameters.Add(string.Format("{0}={1}", param.Key, item));
                    }
                    // If it is a complex type
                    else
                        parameters.Add(param.Value.ToQueryString());
                }

                return string.Join("&",parameters.ToArray());
            }
            else
                    return string.Empty;
        }

        /// <summary>
        /// Clear Message Body Parameter
        /// </summary>
        internal void ClearMessageBodyParameter()
        {
            this._MessageBodyParameter = null;
        }

        /// <summary>
        /// Clear URL Parameters
        /// </summary>
        internal void ClearURLParameters()
        {
            this._URLParameterDictionary?.Clear();
        }

        /// <summary>
        /// Get From Body Parameter
        /// </summary>
        /// <returns>Parameter as HTTP Content</returns>
        internal StringContent GetHTTPRequestContent()
        {
            try
            {
                var json = JsonConvert.SerializeObject( _MessageBodyParameter);
                return new System.Net.Http.StringContent(json, Encoding.UTF8, "application/json");
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Checks if there are Parameter in the List
        /// </summary>
        private bool HasParameter
        {
            get
            {
                if (_URLParameterDictionary == null)
                    return false;
                else
                    return _URLParameterDictionary.Count > 0;
            }
        }
        #endregion

    }
}

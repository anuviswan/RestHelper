﻿using Newtonsoft.Json;
using System;
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
        private dynamic _MessageBodyParameter;
        #endregion

        #region Internal Methods

        internal void AssignMessageBodyParameter(dynamic Value)
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
                    if (param.Value.GetType().IsPrimitiveType())
                        parameters.Add(string.Format("{0}={1}", param.Key, param.Value));
                    else
                        parameters.Add(param.Value.ToQueryString());
                }

                return string.Join("&",parameters.ToArray());
            }
            else
                    return string.Empty;
        }

        /// <summary>
        /// Get From Body Parameter
        /// </summary>
        /// <returns>Parameter as HTTP Content</returns>
        internal StringContent GetHTTPRequestContent()
        {
            return  new StringContent(JsonConvert.SerializeObject(_MessageBodyParameter), Encoding.UTF8, "application/json");
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

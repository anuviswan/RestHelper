﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace RestHelper
{
    /// <summary>
    /// This class tracks and maintains the Parameters for the query
    /// Update 001: Remove Parameter as a separate class to adhere to SRP
    /// </summary>
    internal class ParameterInfo
    {
        #region Private Variables
        private Dictionary<string, object> _QueryStringParameterDictionary;
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
        internal void AddQueryStringParameter(string Key,dynamic Value)
        {
            if (_QueryStringParameterDictionary == null)
                _QueryStringParameterDictionary = new Dictionary<string, dynamic>();

            _QueryStringParameterDictionary.Add(Key, Value);
        }

        
        /// <summary>
        /// Get QueryString from the Parameter List
        /// </summary>
        /// <returns>Returns a string representing Query String</returns>
        internal string GetQueryString()
        {
            if (HasParameter)
            {
                var queryString = String.Join("&", _QueryStringParameterDictionary
                                    .Select(kvp => String.Format("{0}={1}",
                                    System.Net.WebUtility.UrlEncode(kvp.Key),
                                    System.Net.WebUtility.UrlEncode(Convert.ToString(kvp.Value)))).ToArray());

                return queryString;
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
                if (_QueryStringParameterDictionary == null)
                    return false;
                else
                    return _QueryStringParameterDictionary.Count > 0;
            }
        }
        #endregion

    }
}

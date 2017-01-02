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
        private Dictionary<string, object> _ParameterDictionary;
        private HttpMethod _MethodType;
        #endregion

        #region Ctor
        public ParameterInfo(HttpMethod MethodType)
        {
            _MethodType = MethodType;
        }

        public ParameterInfo(HttpMethod MethodType, Dictionary<string,object> ParameterList):this(MethodType)
        {
            if(ParameterList!=null)
                _ParameterDictionary = new Dictionary<string, object>(ParameterList);

        }
        #endregion

        #region Internal Methods

        /// <summary>
        /// Adds a new parameter to the list
        /// </summary>
        /// <param name="Key">Name representing the parameter</param>
        /// <param name="Value">Value of the parameter</param>
        internal void AddParameter(string Key,object Value)
        {
            if (_ParameterDictionary == null)
                _ParameterDictionary = new Dictionary<string, object>();

            _ParameterDictionary.Add(Key, Value);
        }

        /// <summary>
        /// Checks if there are Parameter in the List
        /// </summary>
        internal bool HasParameter
        {
            get
            {
                if (_ParameterDictionary == null)
                    return false;
                else
                    return _ParameterDictionary.Count > 0;
            }
        }
        
        /// <summary>
        /// Get QueryString from the Parameter List
        /// </summary>
        /// <returns>Returns a string representing Query String</returns>
        internal string GetQueryString()
        {
            if (HasParameter)
            {
                var queryString = String.Join("&", _ParameterDictionary
                                    .Select(kvp => String.Format("{0}={1}",
                                    System.Net.WebUtility.UrlEncode(kvp.Key),
                                    System.Net.WebUtility.UrlEncode(Convert.ToString(kvp.Value)))).ToArray());

                return queryString;
            }
            else
                    return string.Empty;
        }
        #endregion
    }
}

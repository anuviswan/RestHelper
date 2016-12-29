using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;


namespace RestHelper
{
    public class RestHelper
    {
        #region Private Properties
        private Uri _BaseUri;
        private HttpClient _HttpClient;
        private Dictionary<string,object> _ParameterDictionary;
        #endregion

        #region Public Attributes

        /// <summary>
        /// String representing Base URL
        /// </summary>
        public Uri BaseUri
        {
            get
            {
                return _BaseUri;
            }
            
        }
        #endregion

        #region Ctor
        /// <summary>
        /// Constructor accepting the base URL as parameter
        /// </summary>
        /// <param name="BaseUri"></param>
        public RestHelper(string BaseUri)
        {
            this._BaseUri = new Uri(BaseUri);
            this._HttpClient = new HttpClient();
            this._HttpClient.MaxResponseContentBufferSize = 256000;
        }
        #endregion

        /// <summary>
        /// Add Paramter
        /// </summary>
        /// <param name="Key">Paramter Name</param>
        /// <param name="Value">Parameter Value</param>
        public void AddParameter(string Key,object Value)
        {
            if(_ParameterDictionary==null)
                _ParameterDictionary = new Dictionary<string, object>();

            _ParameterDictionary.Add(Key, Value);
        }

        /// <summary>
        /// Invoke the WebAPI mentioned using Base URL and Resource URL, with the parameters 
        /// in parameter list.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ResourceURL">URL to the Resource to be invoked</param>
        /// <returns></returns>
        public async Task<T> ExecuteAsync<T>(string ResourceURL, HttpMethod RequestType) 
        {

            if (HttpMethod.Get == RequestType)
            {
                return await ExecuteGet<T>(ResourceURL);
            }
            
            return default(T);
        }

        #region Private Methods

        private bool HasParamter
        {
            get { return _ParameterDictionary?.Count() > 0; }
        }


        /// <summary>
        /// Builds the URI based on BaseURI and ResourceURI
        /// </summary>
        /// <param name="resourceURL"></param>
        /// <returns>Resultant URI</returns>
        private Uri BuildURI(string resourceURL)
        {
            if(HasParamter)
            {
                var parameterString = String.Join("&", _ParameterDictionary
                        .Select(kvp => String.Format("{0}={1}",
                        System.Net.WebUtility.UrlEncode(kvp.Key),
                        System.Net.WebUtility.UrlEncode(Convert.ToString(kvp.Value)))).ToArray());

                return new Uri(_BaseUri, string.Format("{0}?{1}", resourceURL, parameterString));
            }
            else
                return new Uri(_BaseUri, resourceURL);
        }



        /// <summary>
        /// Execute a Get Request at the API
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ResourceUri">Resource URI</param>
        /// <returns>Return Value from the API</returns>
        private async Task<T> ExecuteGet<T>(string ResourceURI)
        {
            T result;
            try
            {
                var response = await _HttpClient.GetAsync(BuildURI(ResourceURI));

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    result = JsonConvert.DeserializeObject<T>(content);
                    return result;
                }
            }
            catch (Exception Ex)
            {
                string s = Ex.Message;
                throw;
            }

            return default(T);
        }


       
   

    #endregion

}
}

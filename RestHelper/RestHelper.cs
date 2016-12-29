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
        private ParameterList _ParameterList;
        private string _ResourceURI;
        private HttpMethod _HttpMethodType;
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
            this._ParameterList = new ParameterList();
        }
        #endregion

        /// <summary>
        /// Add Paramter
        /// </summary>
        /// <param name="Key">Paramter Name</param>
        /// <param name="Value">Parameter Value</param>
        public void AddParameter(string Key,object Value)
        {
            _ParameterList.AddParameter(Key, Value);
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
            this._ResourceURI = ResourceURL;
            this._HttpMethodType = RequestType;

            if (HttpMethod.Get == RequestType)
            {
                return await ExecuteGetAsync<T>();
            }
            
            return default(T);
        }

        #region Private Methods
        

        /// <summary>
        /// Builds the URI based on BaseURI and ResourceURI
        /// </summary>
        /// <returns>Resultant URI</returns>
        private Uri BuildURI()
        {
            if(_HttpMethodType == HttpMethod.Get && _ParameterList.HasParameter)
                return new Uri(_BaseUri, string.Format("{0}?{1}", _ResourceURI , _ParameterList.GetQueryString() ));
            else
                return new Uri(_BaseUri, _ResourceURI);
        }



        /// <summary>
        /// Execute a Get Request at the API
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns>Return Value from the API</returns>
        private async Task<T> ExecuteGetAsync<T>()
        {
            T result;
            try
            {
                var response = await _HttpClient.GetAsync(BuildURI());

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

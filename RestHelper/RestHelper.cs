using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace RestHelper
{
    public class RestHelper
    {
        #region Private Properties
        private Uri _BaseUri;
        private HttpClient _HttpClient;
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
        /// Invoke the WebAPI mentioned using Base URL and Resource URL, with the parameters 
        /// in parameter list.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ResourceURL">URL to the Resource to be invoked</param>
        /// <returns></returns>
        public async Task<T> ExecuteAsync<T>(string ResourceURL, HttpMethod RequestType) 
        {
            var apiUri = BuildURI(ResourceURL);

            if (HttpMethod.Get == RequestType)
            {
                return await ExecuteGet<T>(apiUri);
            }
            
            return default(T);
        }

        #region Private Methods
        /// <summary>
        /// Builds the URI based on BaseURI and ResourceURI
        /// </summary>
        /// <param name="resourceURL"></param>
        /// <returns>Resultant URI</returns>
        private Uri BuildURI(string resourceURL)
        {
            return new Uri(_BaseUri, resourceURL);
        }


        /// <summary>
        /// Execute a Get Request at the API
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="APIUri">Complete URI to the API</param>
        /// <returns>Return Value from the API</returns>
        private async Task<T> ExecuteGet<T>(Uri APIUri)
        {
            T result;
            try
            {
                var response = await _HttpClient.GetAsync(APIUri);
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

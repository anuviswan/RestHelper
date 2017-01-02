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
    /// <summary>
    /// RestHelper Class aids in Querying REST API's.
    /// </summary>
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
            this._HttpClient.MaxResponseContentBufferSize = int.MaxValue;
        }

        public async Task<T> ExecuteGetAsync<T>(string ResourceURI,string Key,object Value)
        {
            T result;
            try
            {
                var Parameter = new ParameterInfo(HttpMethod.Get);
                Parameter.AddParameter(Key, Value);
                var completeURI = BuildURI(HttpMethod.Get, ResourceURI, Parameter);
                result = await ExecuteGetAsync<T>(completeURI);
                return result;
            }
            catch (Exception Ex)
            {
                string s = Ex.Message;
                throw;
            }
            return default(T);
        }

        /// <summary>
        /// Execute a GET Request on the Server
        /// </summary>
        /// <typeparam name="T">The Expected Return Type</typeparam>
        /// <param name="ResourceURI">URI of the Resource</param>
        /// <param name="Parameters">List of Parameters</param>
        /// <returns>Value returned by the Web API</returns>
        public async Task<T> ExecuteGetAsync<T>(string ResourceURI, Dictionary<string, object> Parameters = null)
        {
            T result;
            try
            {
                var completeURI = Parameters == null ? BuildURI(HttpMethod.Get,ResourceURI) : BuildURI(HttpMethod.Get,ResourceURI, new ParameterInfo(HttpMethod.Get,Parameters));
                result = await ExecuteGetAsync<T>(completeURI);
                return result;
            }
            catch (Exception Ex)
            {
                string s = Ex.Message;
                throw;
            }

            return default(T);
        }

        #endregion


        #region Private Methods

        private async Task<T> ExecuteGetAsync<T>(Uri CompleteURI)
        {
            T result;
            try
            {
                var response = await _HttpClient.GetAsync(CompleteURI);

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
        

        /// <summary>
        /// Builds Complete URI From BaseURI,ResourceURI and ParameterList
        /// </summary>
        /// <param name="Parameter"></param>
        /// <param name="ResourceURI"></param>
        /// <param name="MethodType"></param>
        /// <returns></returns>
        private Uri BuildURI(HttpMethod MethodType,string ResourceURI, ParameterInfo Parameter=null)
        {
            if(MethodType == HttpMethod.Get && Parameter !=null && Parameter.HasParameter)
                return new Uri(_BaseUri, string.Format("{0}?{1}", ResourceURI, Parameter.GetQueryString() ));
            else
                return new Uri(_BaseUri, ResourceURI);
        }


        /// <summary>
        /// Execute a Post Request at the API
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns>Return value from the API</returns>
        private Task<T> ExecutePostAsync<T,K>(K item)
        {
            //if(_ParameterList.HasParameter)
            //{
            //    var contend = new StringContent(JsonConvert.SerializeObject(item), Encoding.UTF8, "application/json");
            //}
            return null;
        }


       
 

    #endregion

    }
}


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
        private ParameterInfo _ParameterInfo;
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
            this._ParameterInfo = new ParameterInfo();
        }
        #endregion

        public void AddQueryStringParameters(string Key,dynamic Value)
        {
            this._ParameterInfo.AddQueryStringParameter(Key, Value);
        }

        public void AssignMessageBodyParameter(dynamic Value)
        {
            _ParameterInfo.AssignMessageBodyParameter(Value);
        }


        public async Task<TReturnValue> ExecuteAsync<TReturnValue>(HttpMethod MethodType,string ResourceURI)
        {
            TReturnValue result;
            var completeURI = BuildURI(ResourceURI);
            if (MethodType == HttpMethod.Get)
            {
                result = await ExecuteGetAsync<TReturnValue>(completeURI);
                return result;
            }
            else if(MethodType == HttpMethod.Post)
            {
                result = await ExecutePostAsync<TReturnValue>(completeURI);
                return result;
            }
            return default(TReturnValue);
        }
        

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
        

        private async Task<T> ExecutePostAsync<T>(Uri CompleteURI)
        {
            T result;
            try
            {
                var response = await _HttpClient.PostAsync(CompleteURI,_ParameterInfo.GetHTTPRequestContent());

                if(response.IsSuccessStatusCode)
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
        /// <param name="ResourceURI"></param>
        /// <returns></returns>
        private Uri BuildURI(string ResourceURI)
        {
            string queryString = _ParameterInfo.GetQueryString();

            if(string.IsNullOrEmpty(queryString))
                return new Uri(_BaseUri, ResourceURI);
            else
                return new Uri(_BaseUri, string.Format("{0}?{1}", ResourceURI, queryString));
        }
        #endregion

    }
}


using Microsoft.Owin.Hosting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestHelper.UnitTest.OWINServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace RestHelper.UnitTest
{
    public class BaseTest
    {
        internal const string _BaseAddress = "http://localhost:9388/";


        public async Task<bool> CallNoParamAPI_ServerRunning_GetResponseTrue(string ResourceURL,HttpMethod MethodType)
        {

            #region Arrange
            var restHelper = new RestHelper(_BaseAddress);
            bool result;
            #endregion

            #region Act
            using (WebApp.Start<WebApiStartup>(_BaseAddress))
            {
                
                result = await restHelper.ExecuteGetAsync<bool>(ResourceURL,null);
            }
            #endregion

            return result; 

        }


        public async Task<bool> CallSingleParamAPI_ServerRunning_GetResponseWithParamaterNameValueAppended(string ResourceURL, HttpMethod MethodType)
        {

            var restHelper = new RestHelper(_BaseAddress);
            string ParameterKey = "VariableStr";
            string ParameterValue = "DummyString";
            string result;

            using (WebApp.Start<WebApiStartup>(_BaseAddress))
            {
                result = await restHelper.ExecuteGetAsync<string>(ResourceURL, ParameterKey,ParameterValue);
            }

            return (string.Format("{0}={1}", ParameterKey, ParameterValue) == result);

        }


        public async Task<bool> CallMultipleParamAPI_ServerRunning_GetResponseWithParamatersNameValueAppended(string ResourceURL, HttpMethod MethodType)
        {
            var restHelper = new RestHelper(_BaseAddress);
            string ParameterKey1 = "VariableStr1";
            string ParameterValue1 = "DummyStr1";
            string ParameterKey2 = "VariableStr2";
            string ParameterValue2 = "DummyStr2";
            string result;

            var Parameters = new Dictionary<string, object>();
            Parameters.Add(ParameterKey1, ParameterValue1);
            Parameters.Add(ParameterKey2, ParameterValue2);
            using (WebApp.Start<WebApiStartup>(_BaseAddress))
            {
                result = await restHelper.ExecuteGetAsync<string>(ResourceURL, Parameters);
            }
            return (string.Format("{0}={1}&{2}={3}",
                            ParameterKey1, ParameterValue1,
                            ParameterKey2, ParameterValue2) == result);
        }


        public async Task<bool> CallMultipleTypeParamAPI_ServerRunning_GetResponseWithParamatersNameValueAppended(string ResourceURL, HttpMethod MethodType)
        {
            
            var restHelper = new RestHelper(_BaseAddress);
            string ParameterKey1 = "VariableStr";
            string ParameterValue1 = "Jia";
            string ParameterKey2 = "VariableInt";
            int ParameterValue2 = 1;
            string ParameterKey3 = "VariableBool";
            bool ParameterValue3 = true;
            string result;
            

            var Parameters = new Dictionary<string, object>();
            Parameters.Add(ParameterKey1, ParameterValue1);
            Parameters.Add(ParameterKey2, ParameterValue2);
            Parameters.Add(ParameterKey3, ParameterValue3);

            using (WebApp.Start<WebApiStartup>(_BaseAddress))
            {
                result = await restHelper.ExecuteGetAsync<string>(ResourceURL, Parameters);
            }
            return (string.Format("{0}={1}&{2}={3}&{4}={5}",
                                        ParameterKey1, ParameterValue1,
                                        ParameterKey2, ParameterValue2,
                                        ParameterKey3, ParameterValue3) == result);

        }


        public async Task<bool> CallDateTimeParamAPI_ServerRunning_GetResponseWithParamaterNameValueAppended(string ResourceURL, HttpMethod MethodType)
        {

            var restHelper = new RestHelper(_BaseAddress);
            string ParameterKey = "VariableDate";
            DateTime ParameterValue = DateTime.Now;
            string result;

            var Parameters = new Dictionary<string, object>();
            Parameters.Add(ParameterKey, ParameterValue);

            using (WebApp.Start<WebApiStartup>(_BaseAddress))
            {
                result = await restHelper.ExecuteGetAsync<string>(ResourceURL, Parameters);
            }
            return (string.Format("{0}={1}", ParameterKey, ParameterValue) == result);

        }


        public async Task<bool> CallComplexRefTypeParamAPI_ServerRunning_GetResponseWithParamatersNameValueAppended(string ResourceURL, HttpMethod MethodType)
        {

            var restHelper = new RestHelper(_BaseAddress);

            string ParameterKey1 = "ParameterComplexRefType";
            string ParameterValueStr = "Jia";

            string ParameterKey2 = "VariableInt";
            int ParameterValueInt = 1;

            string ParameterKey3 = "VariableBool";
            bool ParameterValueBool = true;

            string ParameterKey4 = "VariableDateTime";
            DateTime ParameterValueDateTime = DateTime.Now;

            ComplexRefType ParameterComplexRefType = new ComplexRefType()
            {
                VariableBool = ParameterValueBool,
                VariableDateTime = ParameterValueDateTime,
                VariableInt = ParameterValueInt,
                VariableStr = ParameterValueStr
            };
            string result;
            var Parameters = new Dictionary<string, object>();
            Parameters.Add("ParameterComplexRefType", ParameterComplexRefType);

            using (WebApp.Start<WebApiStartup>(_BaseAddress))
            {
                result = await restHelper.ExecuteGetAsync<string>(ResourceURL, Parameters);
            }

            return (string.Format("{0}={1}&{2}={3}&{4}={5}&{6}={7}",
                                        ParameterKey1, ParameterValueStr,
                                        ParameterKey2, ParameterValueInt,
                                        ParameterKey3, ParameterValueBool,
                                        ParameterKey4, ParameterValueDateTime) == result);

        }
    }
}

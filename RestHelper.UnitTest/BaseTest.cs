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

        
        public async Task<bool> CallSingleParamAPI_ServerRunning_GetResponseWithParamaterNameValueAppended(string ResourceURL, HttpMethod MethodType)
        {

            var restHelper = new RestHelper(_BaseAddress);
            string ParameterKey = "VariableStr";
            string ParameterValue = "DummyString";
            string result;

            using (WebApp.Start<WebApiStartup>(_BaseAddress))
            {
                if (MethodType == HttpMethod.Get)
                    restHelper.AddQueryStringParameters(ParameterKey, ParameterValue);
                else
                    restHelper.AssignMessageBodyParameter(ParameterValue);

                result = await restHelper.ExecuteAsync<string>(MethodType, ResourceURL);
            }

            if(MethodType == HttpMethod.Get)
                return (string.Format("{0}={1}", ParameterKey, ParameterValue) == result);
            else
                return (ParameterValue == result);
        }



        public async Task<bool> CallComplexRefTypeParamAPI_ServerRunning_GetResponseWithParamatersNameValueAppended(string ResourceURL, HttpMethod MethodType)
        {

            var restHelper = new RestHelper(_BaseAddress);

            string ParameterKey1 = "VariableStr";
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
            

            using (WebApp.Start<WebApiStartup>(_BaseAddress))
            {
                if(MethodType == HttpMethod.Get)
                    restHelper.AddQueryStringParameters("ParameterComplexRefType", ParameterComplexRefType);
                else
                    restHelper.AssignMessageBodyParameter(ParameterComplexRefType);
                result = await restHelper.ExecuteAsync<string>(MethodType,ResourceURL);
            }

            return (string.Format("{0}={1}&{2}={3}&{4}={5}&{6}={7}",
                                        ParameterKey1, ParameterValueStr,
                                        ParameterKey2, ParameterValueInt,
                                        ParameterKey3, ParameterValueBool,
                                        ParameterKey4, ParameterValueDateTime) == result);

        }
    }
}

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
                result = await restHelper.ExecuteAsync<bool>(ResourceURL, MethodType);
            }
            #endregion

            return result;

        }


        public async Task<bool> CallSingleParamAPI_ServerRunning_GetResponseWithParamaterNameValueAppended(string ResourceURL, HttpMethod MethodType)
        {

            #region Arrange
            var restHelper = new RestHelper(_BaseAddress);
            string ParameterKey = "VariableStr";
            string ParameterValue = "DummyString";
            string result;
            #endregion

            #region Act
            restHelper.AddParameter(ParameterKey, ParameterValue);
            using (WebApp.Start<WebApiStartup>(_BaseAddress))
            {
                result = await restHelper.ExecuteAsync<string>(ResourceURL, MethodType);
            }
            #endregion

            return (string.Format("{0}={1}", ParameterKey, ParameterValue) == result);
            #region Assert
            //Assert.AreEqual<string>(string.Format("{0}1={1}", ParameterKey, ParameterValue), result);
            #endregion

        }


        public async Task<bool> CallMultipleParamAPI_ServerRunning_GetResponseWithParamatersNameValueAppended(string ResourceURL, HttpMethod MethodType)
        {

            var restHelper = new RestHelper(_BaseAddress);
            string ParameterKey1 = "VariableStr1";
            string ParameterValue1 = "DummyStr1";
            string ParameterKey2 = "VariableStr2";
            string ParameterValue2 = "DummyStr2";
            string result;

            restHelper.AddParameter(ParameterKey1, ParameterValue1);
            restHelper.AddParameter(ParameterKey2, ParameterValue2);

            using (WebApp.Start<WebApiStartup>(_BaseAddress))
            {
                result = await restHelper.ExecuteAsync<string>(ResourceURL, MethodType);
            }
            return (string.Format("{0}={1}&{2}={3}",
                                        ParameterKey1, ParameterValue1,
                                        ParameterKey2, ParameterValue2) == result);

        }


        public async Task<bool> CallMultipleTypeParamAPI_ServerRunning_GetResponseWithParamatersNameValueAppended(string ResourceURL, HttpMethod MethodType)
        {

            #region Arrange
            var restHelper = new RestHelper(_BaseAddress);
            string ParameterKey1 = "VariableStr";
            string ParameterValue1 = "Jia";
            string ParameterKey2 = "VariableInt";
            int ParameterValue2 = 1;
            string ParameterKey3 = "VariableBool";
            bool ParameterValue3 = true;
            string result;
            #endregion

            #region Act
            restHelper.AddParameter(ParameterKey1, ParameterValue1);
            restHelper.AddParameter(ParameterKey2, ParameterValue2);
            restHelper.AddParameter(ParameterKey3, ParameterValue3);
            using (WebApp.Start<WebApiStartup>(_BaseAddress))
            {
                result = await restHelper.ExecuteAsync<string>(ResourceURL, MethodType);
            }
            #endregion

            #region Assert
            return (string.Format("{0}={1}&{2}={3}&{4}={5}",
                                        ParameterKey1, ParameterValue1,
                                        ParameterKey2, ParameterValue2,
                                        ParameterKey3, ParameterValue3) == result);
            #endregion

        }


        public async Task<bool> CallDateTimeParamAPI_ServerRunning_GetResponseWithParamaterNameValueAppended(string ResourceURL, HttpMethod MethodType)
        {

            #region Arrange
            var restHelper = new RestHelper(_BaseAddress);
            string ParameterKey = "VariableDate";
            DateTime ParameterValue = DateTime.Now;
            string result;
            #endregion

            #region Act
            restHelper.AddParameter(ParameterKey, ParameterValue);
            using (WebApp.Start<WebApiStartup>(_BaseAddress))
            {
                result = await restHelper.ExecuteAsync<string>(ResourceURL, MethodType);
            }
            #endregion

            #region Assert
            return (string.Format("{0}={1}", ParameterKey, ParameterValue) == result);
            #endregion

        }


        public async Task<bool> CallComplexRefTypeParamAPI_ServerRunning_GetResponseWithParamatersNameValueAppended(string ResourceURL, HttpMethod MethodType)
        {

            #region Arrange
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
            #endregion

            #region Act
            restHelper.AddParameter("ParameterComplexRefType", ParameterComplexRefType);
            using (WebApp.Start<WebApiStartup>(_BaseAddress))
            {
                result = await restHelper.ExecuteAsync<string>(ResourceURL, MethodType);
            }
            #endregion

            #region Assert
            return (string.Format("{0}={1}&{2}={3}&{4}={5}&{6}={7}",
                                        ParameterKey1, ParameterValueStr,
                                        ParameterKey2, ParameterValueInt,
                                        ParameterKey3, ParameterValueBool,
                                        ParameterKey4, ParameterValueDateTime) == result);
            #endregion

        }
    }
}

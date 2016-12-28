using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Owin.Hosting;
using RestHelper.UnitTest.OWINServer;
using System.Threading.Tasks;
using System.Net.Http;

namespace RestHelper.UnitTest
{
    [TestClass]
    public class RestHelperGetRequestTest
    {
        private const string _BaseAddress = "http://localhost:9388/";

        [TestMethod]
        public async Task CallNoParamAPI_ServerRunning_GetResponseTrue()
        {

            #region Arrange
            
            var resourceURL = "api/user/SingleParamBooleanResponse";
            var restHelper = new RestHelper(_BaseAddress);
            bool result;
            #endregion

            #region Act
            using (WebApp.Start<WebApiStartup>(_BaseAddress))
            {
                result = await restHelper.ExecuteAsync<bool>(resourceURL,HttpMethod.Get);
            }
            #endregion

            #region Assert
            Assert.IsTrue(result);
            #endregion

        }


        [TestMethod]
        public async Task CallSingleParamAPI_ServerRunning_GetResponseWithParamaterNameValueAppended()
        {

            #region Arrange

            var resourceURL = "api/user/SingleParamStringResponse";
            var restHelper = new RestHelper(_BaseAddress);
            string ParameterKey = "UserName";
            string ParameterValue = "Jia";
            string result;
            #endregion

            #region Act
            restHelper.AddParameter(ParameterKey, ParameterValue);
            using (WebApp.Start<WebApiStartup>(_BaseAddress))
            {
                result = await restHelper.ExecuteAsync<string>(resourceURL,HttpMethod.Get);
            }
            #endregion

            #region Assert
            Assert.AreEqual<string>(string.Format("{0}={1}",ParameterKey,ParameterValue),result);
            #endregion

        }

        [TestMethod]
        public async Task CallMultipleParamAPI_ServerRunning_GetResponseWithParamatersNameValueAppended()
        {

            #region Arrange

            var resourceURL = "api/user/MultipleParamStringResponse";
            var restHelper = new RestHelper(_BaseAddress);
            string ParameterKey1 = "UserName";
            string ParameterValue1 = "Jia";
            string ParameterKey2 = "Password";
            string ParameterValue2 = "Anu";
            string result;
            #endregion

            #region Act
            restHelper.AddParameter(ParameterKey1, ParameterValue1);
            restHelper.AddParameter(ParameterKey2, ParameterValue2);
            using (WebApp.Start<WebApiStartup>(_BaseAddress))
            {
                result = await restHelper.ExecuteAsync<string>(resourceURL, HttpMethod.Get);
            }
            #endregion

            #region Assert
            Assert.AreEqual<string>(string.Format("{0}={1}&{2}={3}", 
                                        ParameterKey1, ParameterValue1,
                                        ParameterKey2, ParameterValue2), result);
            #endregion

        }

        [TestMethod]
        public async Task CallMultipleTypeParamAPI_ServerRunning_GetResponseWithParamatersNameValueAppended()
        {

            #region Arrange

            var resourceURL = "api/user/MultipleTypeParamStringResponse";
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
                result = await restHelper.ExecuteAsync<string>(resourceURL, HttpMethod.Get);
            }
            #endregion

            #region Assert
            Assert.AreEqual<string>(string.Format("{0}={1}&{2}={3}&{4}={5}",
                                        ParameterKey1, ParameterValue1,
                                        ParameterKey2, ParameterValue2,
                                        ParameterKey3, ParameterValue3), result);
            #endregion

        }
    }
}

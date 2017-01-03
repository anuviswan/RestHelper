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
        private string _BaseAddress = "http://localhost:8888/";
        [TestMethod]
        public async Task CallNoParamAPI_Get_GetResponseTrue()
        {

            #region Arrange
            var resourceURL = "api/user/WithoutParamBooleanResponse";
            var restHelper = new RestHelper(_BaseAddress);
            bool result;
            #endregion

            #region Act
            using (WebApp.Start<WebApiStartup>(_BaseAddress))
            {
                result = await restHelper.ExecuteAsync<bool>(HttpMethod.Get, resourceURL);
            }
            #endregion

            #region Assert
            Assert.IsTrue(result);
            #endregion
        }

        [TestMethod]
        public async Task CallSingleParamAPI_Get_GetResponseWithParamaterNameValueAppended()
        {

            #region Arrange
            var resourceURL = "api/user/SingleParamStringResponse";
            var restHelper = new RestHelper(_BaseAddress);
            string ParameterKey = "VariableStr";
            string ParameterValue = "DummyString";
            string result;
            #endregion

            #region Act
            using (WebApp.Start<WebApiStartup>(_BaseAddress))
            {
                restHelper.AddQueryStringParameters(ParameterKey, ParameterValue);
                result = await restHelper.ExecuteAsync<string>(HttpMethod.Get, resourceURL);
            }
            #endregion

            #region Assert
            Assert.AreEqual<string>(string.Format("{0}={1}", ParameterKey, ParameterValue), result);
            #endregion

        }

        
        
        [TestMethod]
        public async Task CallComplexRefTypeParamAPI_Get_GetResponseWithParamatersNameValueAppended()
        {

            #region Arrange
            var resourceURL = "api/user/ComplexReferenceTypeParamStringResponse";
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
            using (WebApp.Start<WebApiStartup>(_BaseAddress))
            {
                restHelper.AddQueryStringParameters("ParameterComplexRefType", ParameterComplexRefType);
                result = await restHelper.ExecuteAsync<string>(HttpMethod.Get, resourceURL);
            }
            #endregion

            #region Assert
            Assert.AreEqual<string>(string.Format("{0}={1}&{2}={3}&{4}={5}&{6}={7}",
                                        ParameterKey1, ParameterValueStr,
                                        ParameterKey2, ParameterValueInt,
                                        ParameterKey3, ParameterValueBool,
                                        ParameterKey4, ParameterValueDateTime), result);
            #endregion
            
        }
    }
}

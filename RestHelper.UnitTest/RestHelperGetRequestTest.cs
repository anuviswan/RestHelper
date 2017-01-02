using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Owin.Hosting;
using RestHelper.UnitTest.OWINServer;
using System.Threading.Tasks;
using System.Net.Http;

namespace RestHelper.UnitTest
{
    [TestClass]
    public class RestHelperGetRequestTest:BaseTest
    {

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
            var resourceURL = "api/user/SingleParamStringResponse";
            var result = await base.CallSingleParamAPI_ServerRunning_GetResponseWithParamaterNameValueAppended(resourceURL, HttpMethod.Get);
            Assert.IsTrue(result);
        }

        
        
        [TestMethod]
        public async Task CallComplexRefTypeParamAPI_Get_GetResponseWithParamatersNameValueAppended()
        {

            var resourceURL = "api/user/ComplexReferenceTypeParamStringResponse";
            var result = await base.CallComplexRefTypeParamAPI_ServerRunning_GetResponseWithParamatersNameValueAppended(resourceURL, HttpMethod.Get);
            Assert.IsTrue(result);
        }
    }
}

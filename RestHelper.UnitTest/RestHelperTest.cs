using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Owin.Hosting;
using RestHelper.UnitTest.OWINServer;
using System.Threading.Tasks;
using System.Net.Http;

namespace RestHelper.UnitTest
{
    [TestClass]
    public class RestHelperTest
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
        public async Task CallSingleParamAPI_ServerRunning_GetResponseWithString1Appended()
        {

            #region Arrange

            var resourceURL = "api/user/SingleParamBooleanResponse";
            var restHelper = new RestHelper(_BaseAddress);
            var parameter = "jia";
            string result;
            #endregion

            #region Act
            using (WebApp.Start<WebApiStartup>(_BaseAddress))
            {
                result = await restHelper.ExecuteAsync<string>(resourceURL,HttpMethod.Get);
            }
            #endregion

            #region Assert
            Assert.AreEqual<string>(string.Format("{0}{1}",parameter,1),result);
            #endregion

        }
    }
}

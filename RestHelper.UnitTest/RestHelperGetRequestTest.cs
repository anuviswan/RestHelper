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
            var resourceURL = "api/user/WithoutParamBooleanResponse";
            var result = await base.CallNoParamAPI_ServerRunning_GetResponseTrue(resourceURL, HttpMethod.Get);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public async Task CallSingleParamAPI_Get_GetResponseWithParamaterNameValueAppended()
        {
            var resourceURL = "api/user/SingleParamStringResponse";
            var result = await base.CallSingleParamAPI_ServerRunning_GetResponseWithParamaterNameValueAppended(resourceURL, HttpMethod.Get);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public async Task CallMultipleParamAPI_Get_GetResponseWithParamatersNameValueAppended()
        {
            var resourceURL = "api/user/MultipleParamStringResponse";
            var result = await base.CallMultipleParamAPI_ServerRunning_GetResponseWithParamatersNameValueAppended(resourceURL, HttpMethod.Get);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public async Task CallMultipleTypeParamAPI_Get_GetResponseWithParamatersNameValueAppended()
        {

            var resourceURL = "api/user/MultipleTypeParamStringResponse";
            var result = await base.CallMultipleTypeParamAPI_ServerRunning_GetResponseWithParamatersNameValueAppended(resourceURL, HttpMethod.Get);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public async Task CallDateTimeParamAPI_Get_GetResponseWithParamaterNameValueAppended()
        {

            var resourceURL = "api/user/DateTimeParamStringResponse";
            var result = await base.CallDateTimeParamAPI_ServerRunning_GetResponseWithParamaterNameValueAppended(resourceURL, HttpMethod.Get);
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

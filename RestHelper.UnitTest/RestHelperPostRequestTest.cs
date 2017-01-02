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
    [TestClass]
    public class RestHelperPostRequestTest : BaseTest
    {
        [TestMethod]
        public async Task CallSingleParamAPI_Post_GetResponseWithParamaterNameValueAppended()
        {
            var resourceURL = "api/user/PostSingleParamStringResponse";
            var result = await base.CallSingleParamAPI_ServerRunning_GetResponseWithParamaterNameValueAppended(resourceURL, HttpMethod.Post);
            Assert.IsTrue(result);
        }

       

        [TestMethod]
        public async Task CallComplexRefTypeParamAPI_Post_GetResponseWithParamatersNameValueAppended()
        {

            var resourceURL = "api/user/PostComplexReferenceTypeParamStringResponse";
            var result = await base.CallComplexRefTypeParamAPI_ServerRunning_GetResponseWithParamatersNameValueAppended(resourceURL, HttpMethod.Post);
            Assert.IsTrue(result);
        }
    }
}

using Microsoft.Owin.Hosting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EcSolvoRestHelper.UnitTest.OWINServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace EcSolvoRestHelper.UnitTest
{
    [TestClass]
    public class RestHelperPostRequestTest 
    {
        private string _BaseAddress = "http://localhost:8888/";
        [TestMethod]
        public async Task CallSingleParamAPI_Post_GetResponseWithParamaterNameValueAppended()
        {

            #region Arrange
            var resourceURL = "api/user/PostSingleParamStringResponse";
            var restHelper = new EcSolvo.RestHelper(_BaseAddress);
            string ParameterValue = "DummyString";
            string result;
            #endregion

            #region Act
            using (WebApp.Start<WebApiStartup>(_BaseAddress))
            {
                restHelper.AssignMessageBodyParameter(ParameterValue);
                result = await restHelper.ExecuteAsync<string>(HttpMethod.Post, resourceURL);
            }
            #endregion

            #region Assert
            Assert.AreEqual<string>(ParameterValue, result);
            #endregion
        }



        [TestMethod]
        public async Task CallComplexRefTypeParamAPI_Post_GetResponseWithParamatersNameValueAppended()
        {



            #region Arrange
            var resourceURL = "api/user/PostComplexReferenceTypeParamStringResponse";
            var restHelper = new EcSolvo.RestHelper(_BaseAddress);

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
                restHelper.AssignMessageBodyParameter(ParameterComplexRefType);
                result = await restHelper.ExecuteAsync<string>(HttpMethod.Post, resourceURL);
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

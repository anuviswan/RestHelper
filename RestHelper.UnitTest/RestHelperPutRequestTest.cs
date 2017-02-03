using EcSolvoRestHelper.UnitTest.OWINServer;
using Microsoft.Owin.Hosting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace RestHelper.UnitTest
{
    [TestClass]
    public class RestHelperPutRequestTest
    {
        private string _BaseAddress = "http://localhost:8888/";

        [TestMethod]
        public async Task CallSingleParamAPI_Put_PutStringParamStringResponse()
        {

            #region Arrange
            var resourceURL = "api/user/PutStringParamStringResponse";
            var restHelper = new EcSolvo.RestHelper(_BaseAddress);
            string ParameterKey = "VariableStr";
            string ParameterValue = "DummyString";
            string result;
            #endregion

            #region Act
            using (WebApp.Start<WebApiStartup>(_BaseAddress))
            {
                restHelper.AddURLParameters(ParameterKey, ParameterValue);
                result = await restHelper.ExecuteAsync<string>(HttpMethod.Put, resourceURL);
            }
            #endregion

            #region Assert
            Assert.AreEqual<string>(string.Format("{0}={1}", ParameterKey, ParameterValue), result);
            #endregion

        }

        [TestMethod]
        public async Task CallMultipleParamAPI_Put_PutMultipleParamStringResponse()
        {

            #region Arrange
            var resourceURL = "api/user/MultipleParamStringResponse";
            var restHelper = new EcSolvo.RestHelper(_BaseAddress);
            string ParameterKey1 = "VariableStr";
            string ParameterValue1 = "DummyString";
            string ParameterKey2 = "VariableInt";
            int ParameterValue2 = 12;
            string ParameterKey3 = "VariableDateTime";
            DateTime ParameterValue3 = DateTime.Now;
            string result;
            #endregion

            #region Act
            using (WebApp.Start<WebApiStartup>(_BaseAddress))
            {
                restHelper.AddURLParameters(ParameterKey1, ParameterValue1);
                restHelper.AddURLParameters(ParameterKey2, ParameterValue2);
                restHelper.AddURLParameters(ParameterKey3, ParameterValue3);
                result = await restHelper.ExecuteAsync<string>(HttpMethod.Put, resourceURL);
            }
            #endregion

            #region Assert
            Assert.AreEqual<string>(string.Format("{0}={1}&{2}={3}", ParameterKey1, ParameterValue1,
                                                                     ParameterKey2, ParameterValue2), result);
            #endregion

        }

        [TestMethod]
        public async Task CallComplexParamAPI_Put_PutComplexParamStringResponse()
        {

            #region Arrange
            var resourceURL = "api/user/PutComplexReferenceTypeParamStringResponse";
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
                restHelper.AssignMessageBodyParameter( ParameterComplexRefType);
                result = await restHelper.ExecuteAsync<string>(HttpMethod.Put, resourceURL);
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

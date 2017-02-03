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
            var resourceURL = "api/user/PostDateTimeParamStringResponse";
            var restHelper = new EcSolvo.RestHelper(_BaseAddress);
            string Parameter = "Dummy";
            string result;
            #endregion

            #region Act
            using (WebApp.Start<WebApiStartup>(_BaseAddress))
            {
                restHelper.AssignMessageBodyParameter(Parameter);
                result = await restHelper.ExecuteAsync<string>(HttpMethod.Post, resourceURL);
            }
            #endregion

            #region Assert
            Assert.AreEqual<string>(Parameter, result);
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

        [TestMethod]
        public async Task CallNestedComplexRefTypeParamAPI_Post_GetResponseWithParamatersNameValueAppended()
        {

            #region Arrange
            var resourceURL = "api/user/PostNestedComplexReferenceTypeParamStringResponse";
            var restHelper = new EcSolvo.RestHelper(_BaseAddress);

            string ParameterKey1 = "VariableStr";
            string ParameterValueStr = "Jia";

            string ParameterKey2 = "VariableInt";
            int ParameterValueInt = 1;

            string ParameterKey3 = "VariableBool";
            bool ParameterValueBool = true;

            string ParameterKey4 = "VariableDateTime";
            DateTime ParameterValueDateTime = DateTime.Now;

            string ParameterKey5 = "OuterVariableStr";
            string OuterParameterValueStr = "Outer";

            ComplexRefType ParameterComplexRefType = new ComplexRefType()
            {
                VariableBool = ParameterValueBool,
                VariableDateTime = ParameterValueDateTime,
                VariableInt = ParameterValueInt,
                VariableStr = ParameterValueStr
            };

            NestedComplexRefType ParameterNestedComplexRefType = new NestedComplexRefType()
            {
                InnerVariableComplex = ParameterComplexRefType,
                OuterVariableStr = OuterParameterValueStr
            };
            string result;
            #endregion

            #region Act
            using (WebApp.Start<WebApiStartup>(_BaseAddress))
            {
                restHelper.AssignMessageBodyParameter(ParameterNestedComplexRefType);
                result = await restHelper.ExecuteAsync<string>(HttpMethod.Post, resourceURL);
            }
            #endregion

            #region Assert
            Assert.AreEqual<string>(string.Format("{0}={1}&{2}={3}&{4}={5}&{6}={7}&{8}={9}",
                                    ParameterKey5,OuterParameterValueStr,
                                    ParameterKey1,ParameterValueStr,
                                    ParameterKey2,ParameterValueInt,
                                    ParameterKey3,ParameterValueBool,
                                    ParameterKey4,ParameterValueDateTime), result);
            #endregion
        }

        [TestMethod]
        public async Task CallIntArrayParamAPI_Post_GetResponseWithParamatersNameValueAppended()
        {

            #region Arrange

            var resourceURL = @"/api/user/PostIntArrayStringResponse";
            var restHelper = new EcSolvo.RestHelper(_BaseAddress);

            var ParameterKey = "ArrayObject";
            var ParameterArray = new int[] { 2, 3, 4 };

            string result;
            #endregion

            #region Act
            using (WebApp.Start<WebApiStartup>(_BaseAddress))
            {
                restHelper.AssignMessageBodyParameter(ParameterArray);
                result = await restHelper.ExecuteAsync<string>(HttpMethod.Post, resourceURL);
            }


            #endregion

            #region Assert
            Assert.AreEqual<string>(string.Format("{0}-{1},{2},{3}",
                                    ParameterKey, ParameterArray[0],
                                    ParameterArray[1], ParameterArray[2]
                                    ), result);
            #endregion

        }


        [TestMethod]
        public async Task CallMultipleParamAPI_Post_PostMultipleTypeParamStringResponse()
        {

            #region Arrange
            var resourceURL = @"/api/user/PostMultipleTypeParamStringResponse";
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
                result = await restHelper.ExecuteAsync<string>(HttpMethod.Post, resourceURL);
            }
            #endregion

            #region Assert
            Assert.AreEqual<string>(string.Format("{0}={1}&{2}={3}", ParameterKey1, ParameterValue1,
                                                                     ParameterKey2, ParameterValue2), result);
            #endregion

        }

        [TestMethod]
        public async Task CallComplexArrayParamAPI_Post_GetResponseWithParamatersNameValueAppended()
        {

            #region Arrange

            var resourceURL = @"/api/user/PostComplexArrayStringResponse";
            var restHelper = new EcSolvo.RestHelper(_BaseAddress);

            var ParameterKey = "ArrayObject";
            bool ParameterBool = true;
            int ParameterInt = 2;
            DateTime ParameterDateTime = DateTime.Now;
            string ParameterStr = "Jia";
            var ParameterArray = new ComplexRefType[] {
                new ComplexRefType() {VariableBool= ParameterBool,VariableDateTime=ParameterDateTime,VariableInt=ParameterInt,VariableStr=ParameterStr},
                new ComplexRefType() {VariableBool= ParameterBool,VariableDateTime=ParameterDateTime,VariableInt=ParameterInt,VariableStr=ParameterStr},
                new ComplexRefType() {VariableBool= ParameterBool,VariableDateTime=ParameterDateTime,VariableInt=ParameterInt,VariableStr=ParameterStr}
                                        };

            string result;
            #endregion

            #region Act
            using (WebApp.Start<WebApiStartup>(_BaseAddress))
            {
                restHelper.AssignMessageBodyParameter(ParameterArray);
                result = await restHelper.ExecuteAsync<string>(HttpMethod.Post, resourceURL);
            }


            #endregion

            #region Assert
            Assert.AreEqual<string>(string.Format("{0}-{1},{2},{3}",
                                    ParameterKey, ParameterArray[0].VariableBool,
                                    ParameterArray[1].VariableBool, ParameterArray[2].VariableBool
                                    ), result);
            #endregion

        }
    }
}

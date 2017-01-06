using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Owin.Hosting;
using EcSolvoRestHelper.UnitTest.OWINServer;
using System.Threading.Tasks;
using System.Net.Http;

namespace EcSolvoRestHelper.UnitTest
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
            var restHelper = new EcSolvo.RestHelper(_BaseAddress);
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
            var restHelper = new EcSolvo.RestHelper(_BaseAddress);
            string ParameterKey = "VariableStr";
            string ParameterValue = "DummyString";
            string result;
            #endregion

            #region Act
            using (WebApp.Start<WebApiStartup>(_BaseAddress))
            {
                restHelper.AddURLParameters(ParameterKey, ParameterValue);
                result = await restHelper.ExecuteAsync<string>(HttpMethod.Get, resourceURL);
            }
            #endregion

            #region Assert
            Assert.AreEqual<string>(string.Format("{0}={1}", ParameterKey, ParameterValue), result);
            #endregion

        }

        [TestMethod]
        public async Task CallMultipleStringParamAPI_Get_GetResponseWithParamaterNameValueAppended()
        {

            #region Arrange
            var resourceURL = "api/user/GetMultipleParamStringResponse";
            var restHelper = new EcSolvo.RestHelper(_BaseAddress);
            string ParameterKey1 = "VariableStr1";
            string ParameterValue1 = "DummyString1";
            string ParameterKey2 = "VariableStr2";
            string ParameterValue2 = "DummyString2";
            string result;
            #endregion

            #region Act
            using (WebApp.Start<WebApiStartup>(_BaseAddress))
            {
                restHelper.AddURLParameters(ParameterKey1, ParameterValue1);
                restHelper.AddURLParameters(ParameterKey2, ParameterValue2);
                result = await restHelper.ExecuteAsync<string>(HttpMethod.Get, resourceURL);
            }
            #endregion

            #region Assert
            Assert.AreEqual<string>(string.Format("{0}={1}&{2}={3}", ParameterKey1, ParameterValue1,
                                                                     ParameterKey2,ParameterValue2), result);
            #endregion

        }

        [TestMethod]
        public async Task CallMultipleTypeParamAPI_Get_GetResponseWithParamaterNameValueAppended()
        {

            #region Arrange
            var resourceURL = "api/user/GetMultipleTypeParamStringResponse";
            var restHelper = new EcSolvo.RestHelper(_BaseAddress);
            string ParameterKey1 = "VariableStr";
            string ParameterValue1 = "DummyString1";
            string ParameterKey2 = "VariableInt";
            int ParameterValue2 = 2;
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
                result = await restHelper.ExecuteAsync<string>(HttpMethod.Get, resourceURL);
            }
            #endregion

            #region Assert
            Assert.AreEqual<string>(string.Format("{0}={1}&{2}={3}&{4}={5}", ParameterKey1, ParameterValue1,
                                                                     ParameterKey2, ParameterValue2,
                                                                     ParameterKey3,ParameterValue3), result);
            #endregion

        }



        [TestMethod]
        public async Task CallComplexRefTypeParamAPI_Get_GetResponseWithParamatersNameValueAppended()
        {

            #region Arrange
            var resourceURL = @"/api/user/GetComplexReferenceTypeParamStringResponse";
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
                restHelper.AddURLParameters("VariableComplexRef", ParameterComplexRefType);
                restHelper.AddURLParameters("DummyStr", "DummyStr");
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

        [TestMethod]
        public async Task CallNestedComplexRefTypeParamAPI_Get_GetResponseWithParamatersNameValueAppended()
        {

            #region Arrange
            
            var resourceURL = @"/api/user/GetNestedComplexReferenceTypeParamStringResponse";
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
                restHelper.AddURLParameters("VariableNestedComplexRef", ParameterNestedComplexRefType);
                restHelper.AddURLParameters("DummyStr1", "DummyStr");
                restHelper.AddURLParameters("DummyStr2", "DummyStr");
                result = await restHelper.ExecuteAsync<string>(HttpMethod.Get, resourceURL);
            }


            #endregion

            #region Assert
            Assert.AreEqual<string>(string.Format("{0}={1}&{2}={3}&{4}={5}&{6}={7}&{8}={9}",
                                    ParameterKey5, OuterParameterValueStr,
                                    ParameterKey1, ParameterValueStr,
                                    ParameterKey2, ParameterValueInt,
                                    ParameterKey3, ParameterValueBool,
                                    ParameterKey4, ParameterValueDateTime), result);
            #endregion

        }

        [TestMethod]
        public async Task CallIntArrayParamAPI_Get_GetResponseWithParamatersNameValueAppended()
        {

            #region Arrange

            var resourceURL = @"/api/user/GetIntArrayStringResponse";
            var restHelper = new EcSolvo.RestHelper(_BaseAddress);

            var ParameterKey = "ArrayObject";
            var ParameterArray = new int[] { 2,3,4};

            string result;
            #endregion

            #region Act
            using (WebApp.Start<WebApiStartup>(_BaseAddress))
            {
                restHelper.AddURLParameters(ParameterKey, ParameterArray);
                result = await restHelper.ExecuteAsync<string>(HttpMethod.Get, resourceURL);
            }


            #endregion

            #region Assert
            Assert.AreEqual<string>(string.Format("{0}-{1},{2},{3}",
                                    ParameterKey,ParameterArray[0],
                                    ParameterArray[1], ParameterArray[2]
                                    ), result);
            #endregion

        }

        [TestMethod]
        public async Task CallComplexArrayParamAPI_Get_GetResponseWithParamatersNameValueAppended()
        {

            #region Arrange

            var resourceURL = @"/api/user/GetComplexArrayStringResponse";
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
                restHelper.AddURLParameters(ParameterKey, ParameterArray);
                result = await restHelper.ExecuteAsync<string>(HttpMethod.Get, resourceURL);
            }


            #endregion

            #region Assert
            Assert.AreEqual<string>(string.Format("ArrayObjectBool-{0},{1},{2}",
                ParameterArray[0].VariableBool,
                ParameterArray[1].VariableBool,
                ParameterArray[2].VariableBool), result);
            #endregion

        }
    }
}


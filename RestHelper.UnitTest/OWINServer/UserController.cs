using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace EcSolvoRestHelper.UnitTest.OWINServer
{
    [RoutePrefix("api/user")]
    public class UserController : ApiController
    {
        #region Get Requests

        [HttpGet]
        [Route("")]
        public bool WithoutParamBooleanResponse()
        {
            return true;
        }

        [HttpGet]
        [Route("")]
        public string SingleParamStringResponse(string VariableStr)
        {
            return string.Format("{0}={1}", "VariableStr", VariableStr);
        }

        public string GetMultipleParamStringResponse(string VariableStr1, string VariableStr2)
        {
            return string.Format("{0}={1}&{2}={3}", "VariableStr1", VariableStr1, "VariableStr2", VariableStr2);
        }

        [Route("api/user/MultipleParamStringResponse")]
        public string GetMultipleTypeParamStringResponse(string VariableStr, int VariableInt, DateTime VariableDateTime)
        {
            return string.Format("VariableStr={0}&VariableInt={1}&VariableDateTime={2}", VariableStr, VariableInt, VariableDateTime);
        }

       
        [Route("api/user/GetComplexReferenceTypeParamStringResponse")]
        public string GetComplexReferenceTypeParamStringResponse([FromUri]ComplexRefType VariableComplexRef, string DummyStr)
        {
            return string.Format("VariableStr={0}&VariableInt={1}&VariableBool={2}&VariableDateTime={3}",
                                    VariableComplexRef.VariableStr,
                                    VariableComplexRef.VariableInt,
                                    VariableComplexRef.VariableBool,
                                    VariableComplexRef.VariableDateTime);
        }

        [Route("api/user/GetNestedComplexReferenceTypeParamStringResponse")]
        public string GetNestedComplexReferenceTypeParamStringResponse([FromUri]NestedComplexRefType VariableNestedComplexRef, string DummyStr1,string DummyStr2)
        {
            return string.Format("OuterVariableStr={0}&VariableStr={1}&VariableInt={2}&VariableBool={3}&VariableDateTime={4}",
                                    VariableNestedComplexRef.OuterVariableStr,
                                    VariableNestedComplexRef.InnerVariableComplex.VariableStr,
                                    VariableNestedComplexRef.InnerVariableComplex.VariableInt,
                                    VariableNestedComplexRef.InnerVariableComplex.VariableBool,
                                    VariableNestedComplexRef.InnerVariableComplex.VariableDateTime);
        }

        [Route("api/user/GetIntArrayStringResponse")]
        public string GetIntArrayStringResponse([FromUri] int[] ArrayObject)
        {
            return string.Format("ArrayObject-{0}{1}{2}",ArrayObject[0], ArrayObject[1], ArrayObject[2]);
        }

        #endregion

        #region Post Requests

        [HttpPost]
        [Route("api/user/PostDateTimeParamStringResponse")]
        public string PostDateTimeParamStringResponse([FromBody]string VariableStr)
        {
            return VariableStr;
        }

        [HttpPost]
        [Route("api/user/PostMultipleParamStringResponse")]
        public string PostMultipleParamStringResponse(string VariableStr1, string VariableStr2)
        {
            return string.Format("{0}={1}&{2}={3}", "VariableStr1", VariableStr1, "VariableStr2", VariableStr2);
        }

        [HttpPost]
        [Route("")]
        public string PostMultipleTypeParamStringResponse(string VariableStr, int VariableInt, bool VariableBool)
        {
            return string.Format("VariableStr={0}&VariableInt={1}&VariableBool={2}", VariableStr, VariableInt, VariableBool);
        }

        [HttpPost]
        [Route("api/user/PostComplexReferenceTypeParamStringResponse")]
        public string PostComplexReferenceTypeParamStringResponse(ComplexRefType VariableComplexRef)
        {
            return string.Format("VariableStr={0}&VariableInt={1}&VariableBool={2}&VariableDateTime={3}",
                                    VariableComplexRef.VariableStr,
                                    VariableComplexRef.VariableInt,
                                    VariableComplexRef.VariableBool,
                                    VariableComplexRef.VariableDateTime);
        }


        [HttpPost]
        [Route("api/user/PostNestedComplexReferenceTypeParamStringResponse")]
        public string PostNestedComplexReferenceTypeParamStringResponse(NestedComplexRefType VariableNestedComplexRef)
        {
            return string.Format("OuterVariableStr={0}&VariableStr={1}&VariableInt={2}&VariableBool={3}&VariableDateTime={4}",
                                    VariableNestedComplexRef.OuterVariableStr,
                                    VariableNestedComplexRef.InnerVariableComplex.VariableStr,
                                    VariableNestedComplexRef.InnerVariableComplex.VariableInt,
                                    VariableNestedComplexRef.InnerVariableComplex.VariableBool,
                                    VariableNestedComplexRef.InnerVariableComplex.VariableDateTime);
        }

        [HttpPost]
        [Route("api/user/PostIntArrayStringResponse")]
        public string PostIntArrayStringResponse([FromBody]int[] ArrayObject)
        {
            return string.Format("ArrayObject-{0}{1}{2}", ArrayObject[0], ArrayObject[1], ArrayObject[2]);
        }
        #endregion
    }
}

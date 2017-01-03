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

        [HttpGet]
        [Route("")]
        public string DateTimeParamStringResponse(DateTime VariableDate)
        {
            return string.Format("{0}={1}", "VariableDate", VariableDate);
        }

        [HttpGet]
        [Route("")]
        public string MultipleParamStringResponse(string VariableStr1, string VariableStr2)
        {
            return string.Format("{0}={1}&{2}={3}", "VariableStr1", VariableStr1, "VariableStr2", VariableStr2);
        }

        [HttpGet]
        [Route("")]
        public string MultipleTypeParamStringResponse(string VariableStr, int VariableInt, DateTime VariableDateTime)
        {
            return string.Format("VariableStr={0}&VariableInt={1}&VariableDateTime={2}", VariableStr,VariableInt, VariableDateTime);
        }

        [HttpGet]
        [Route("")]
        public string ComplexReferenceTypeParamStringResponse([FromUri]ComplexRefType VariableComplexRef,string DummyParam)
        {
            return string.Format("VariableStr={0}&VariableInt={1}&VariableBool={2}&VariableDateTime={3}", 
                                    VariableComplexRef.VariableStr, 
                                    VariableComplexRef.VariableInt, 
                                    VariableComplexRef.VariableBool,
                                    VariableComplexRef.VariableDateTime);
        }

        #endregion

        #region Post Requests

        [HttpPost]
        [Route("")]
        public string PostDateTimeParamStringResponse(DateTime VariableDate)
        {
            return string.Format("{0}={1}", "VariableDate", VariableDate);
        }

        [HttpPost]
        [Route("")]
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
        [Route("")]
        public string PostComplexReferenceTypeParamStringResponse(ComplexRefType VariableComplexRef)
        {
            return string.Format("VariableStr={0}&VariableInt={1}&VariableBool={2}&VariableDateTime={3}",
                                    VariableComplexRef.VariableStr,
                                    VariableComplexRef.VariableInt,
                                    VariableComplexRef.VariableBool,
                                    VariableComplexRef.VariableDateTime);
        }

        #endregion
    }
}

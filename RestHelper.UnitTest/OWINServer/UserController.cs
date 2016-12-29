using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace RestHelper.UnitTest.OWINServer
{
    [RoutePrefix("api/user")]
    public class UserController : ApiController
    {


        [HttpGet]
        [Route("api/user/SingleParamStringResponse")]
        public string SingleParamStringResponse(string VariableStr)
        {
            return string.Format("{0}={1}", "VariableStr", VariableStr);
        }

        [HttpGet]
        [Route("api/user/DateTimeParamStringResponse")]
        public string DateTimeParamStringResponse(DateTime VariableDate)
        {
            return string.Format("{0}={1}", "VariableDate", VariableDate);
        }

        [HttpGet]
        [Route("api/user/MultipleParamStringResponse")]
        public string MultipleParamStringResponse(string VariableStr1, string VariableStr2)
        {
            return string.Format("{0}={1}&{2}={3}", "VariableStr1", VariableStr1, "VariableStr2", VariableStr2);
        }

        [HttpGet]
        [Route("api/user/MultipleTypeParamStringResponse")]
        public string MultipleTypeParamStringResponse(string VariableStr, int VariableInt, bool VariableBool)
        {
            return string.Format("VariableStr={0}&VariableInt={1}&VariableBool={2}", VariableStr,VariableInt,VariableBool);
        }

        [HttpGet]
        [Route("api/user/ComplexReferenceTypeParamStringResponse")]
        public string ComplexReferenceTypeParamStringResponse([FromUri]ComplexRefType VariableComplexRef,string DummyParam)
        {
            return string.Format("VariableStr={0}&VariableInt={1}&VariableBool={2}&VariableDateTime={3}", 
                                    VariableComplexRef.VariableStr, 
                                    VariableComplexRef.VariableInt, 
                                    VariableComplexRef.VariableBool,
                                    VariableComplexRef.VariableDateTime);
        }

        [HttpGet]
        [Route("api/user/WithoutParamBooleanResponse")]
        public bool WithoutParamBooleanResponse()
        {
            return true;
        }

    }
}

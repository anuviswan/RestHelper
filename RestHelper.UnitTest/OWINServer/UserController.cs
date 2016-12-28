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
        [Route("")]
        public string SingleParamStringResponse(string UserName)
        {
            return string.Format("{0}={1}", "UserName", UserName);
        }

        [HttpGet]
        [Route("")]
        public string MultipleParamStringResponse(string UserName,string Password)
        {
            return string.Format("{0}={1}&{2}={3}", "UserName", UserName,"Password",Password);
        }

        [HttpGet]
        [Route("")]
        public string MultipleTypeParamStringResponse(string VariableStr, int VariableInt, bool VariableBool)
        {
            return string.Format("VariableStr={0}&VariableInt={1}&VariableBool={2}", VariableStr,VariableInt,VariableBool);
        }

        [HttpGet]
        [Route("")]
        public bool NoParamBooleanResponse()
        {
            return true;
        }
    }
}

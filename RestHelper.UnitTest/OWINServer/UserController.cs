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
        public bool SingleParamBooleanResponse(string UserName)
        {
            return true;
        }

        [HttpGet]
        [Route("")]
        public bool NoParamBooleanResponse()
        {
            return true;
        }
    }
}

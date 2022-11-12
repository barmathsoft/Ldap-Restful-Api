using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Web.Http;
using Bar.LDAP.RestApi.ExtraMethods;
using Bar.LDAP.RestApi.Models;


namespace Bar.LDAP.RestApi.Controllers
{
    [Route("LoginController")]
    public class LoginController : ApiController
    {
        [HttpPost]
        [Route("Login/UserCheck/")]
        public IHttpActionResult UserCheck(string userName, string passWord)
        {
            DirectoryEntry user = new DirectoryEntry("server", userName, passWord);
            bool isLogin = false;

            if (user.Properties.Count > 0)
                isLogin = true;
            return Ok(Messages.MSG_LGN_SUCCESS);
        }
    }
}

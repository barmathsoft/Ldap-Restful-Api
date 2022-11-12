using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Bar.LDAP.RestApi.ExtraMethods;

namespace Bar.LDAP.RestApi.Controllers
{
    [Route("GroupController")]
    public class GroupController : ApiController
    {
        Manager manager = new Manager();

        [HttpGet]
        [Route("Groups/GetGroups/")]
        public IHttpActionResult GetGroups(string username, string password)
        {
            var groupList = manager.GroupList(username,password);
           
            return Ok(groupList);
        }
    }
}

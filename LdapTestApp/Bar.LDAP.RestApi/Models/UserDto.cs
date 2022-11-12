using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bar.LDAP.RestApi.Models
{
    public class UserDto
    {
        public string UserName { get; set; }
        public string AccountName { get; set; }
        public string Email { get; set; }
        public DateTime? PwdSetDate { get; set; }
        public DateTime? LastLogon { get; set; }
        public string UsageTime { get; set; }
    }
}
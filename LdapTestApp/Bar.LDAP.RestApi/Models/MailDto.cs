using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bar.LDAP.RestApi.Models
{
    public class MailDto
    {
        public string ToName { get; set; }
        public string ToMail { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
    }
}
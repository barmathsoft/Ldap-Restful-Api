using System;
using System.Collections.Generic;
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
    [Route("MailController")]
    public class MailController : ApiController
    {
        MailDto mailDto = new MailDto();
        string fromMail = "mailAddress";
        string password = "mailPassword";
        string host = "mailHost";
        int port = 587;
        public MailController()
        {
            mailDto.Subject = Messages.MSG_MAIL_SUBJECT;
            mailDto.Message = Messages.MSG_MAIL_CONTENT;
        }

        [HttpPost]
        [Route("Mail/SendMail/")]
        public IHttpActionResult SendMail(string ToMail)
        {
            Regex chars = new Regex(@"^[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,4}$", RegexOptions.IgnoreCase);
            var isMail = chars.IsMatch(ToMail);            

            if (isMail)
            {

                SmtpClient server = new SmtpClient(host, port);
                NetworkCredential accoutnInfo = new NetworkCredential(fromMail, password);
                MailAddress from = new MailAddress(fromMail);
                MailAddress to = new MailAddress(ToMail);
                MailMessage mail = new MailMessage(from, to);
                //mail
                mail.Subject = mailDto.Subject;
                mail.SubjectEncoding = System.Text.Encoding.UTF8;
                mail.Body = mailDto.Message;
                mail.BodyEncoding = System.Text.Encoding.UTF8;
                mail.IsBodyHtml = false;
                //smtp
                server.EnableSsl = true;
                server.UseDefaultCredentials = false;
                server.DeliveryMethod = SmtpDeliveryMethod.Network;
                server.Credentials = accoutnInfo;
                server.Send(mail);
                return Ok(Messages.MSG_MAIL_SUCCESS);
            }
            else
            {
                return BadRequest(Messages.MSG_MAIL_ERROR);
            }
        }
    }
}

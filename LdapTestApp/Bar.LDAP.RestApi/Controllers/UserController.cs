using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Bar.LDAP.RestApi.ExtraMethods;
using Bar.LDAP.RestApi.Models;


namespace Bar.LDAP.RestApi.Controllers
{
    [Route("UserController")]
    public class UserController : ApiController
    {
        List<UserDto> userList = new List<UserDto>();
        MailController mail = new MailController();
        Manager manager = new Manager();
       
        [HttpGet]
        [Route("Users/GetByAccountName/")]
        public IHttpActionResult GetByAccountName(string accountName)
        {
            var users = manager.UserList(accountName);
            
            foreach (var user in users)
            {
                userList.Add(new UserDto
                {
                    UserName = user.DisplayName,
                    AccountName = user.SamAccountName,
                    Email = user.EmailAddress,
                    PwdSetDate = user.LastPasswordSet,
                    LastLogon = user.LastLogon,
                    UsageTime = "Kullandığınız gün sayısı: " + Convert.ToString((DateTime.Now - user.LastPasswordSet.Value).Days)
                });
            }
            return Ok(userList);
        }
        [HttpPost]
        [Route("Users/UpdatePassword/")]
        public IHttpActionResult UpdatePassword(string accountName,string currentPwd, string newPwd)
        {
            PrincipalContext context = new PrincipalContext(ContextType.Domain);

            UserPrincipal queryByUser = new UserPrincipal(context);
            queryByUser.SamAccountName = accountName;

            PrincipalSearcher search = new PrincipalSearcher(queryByUser);
            var user = search.FindOne();

            try
            {
                UserPrincipal admin = UserPrincipal.FindByIdentity(context, IdentityType.SamAccountName, user.SamAccountName);
                admin.ChangePassword(currentPwd, newPwd);
                admin.Save();
                return Ok(Messages.MSG_USER_PWD_SUCCESS);
            }
            catch (Exception ex)
            {
                return BadRequest(Messages.MSG_USER_PWD_ERROR + ex.Message);
            }
        }
        [HttpPost]
        [Route("Users/UpdatePassWordByDate/")]
        public IHttpActionResult UpdatePassWordByDate(string accountName)
        {
            var users = manager.UserList(accountName);
            foreach (var user in users)
            {
                userList.Add(new UserDto
                {
                    UserName = user.DisplayName,
                    AccountName = user.SamAccountName,
                    Email = user.EmailAddress,
                    PwdSetDate = user.LastPasswordSet,
                    LastLogon = user.LastLogon
                });
            }
            var account = userList.Where(x => x.AccountName == accountName).FirstOrDefault();   
            var lastDays = DiffOfDates(accountName);

            if (lastDays >= 20)
            {
                mail.SendMail(account.Email);
                return Ok(Messages.MSG_USER_MAIL_CHECK);
            }
            else
            {
                return BadRequest(Messages.MSG_USER_PWD_KEEPGOIN);
            }

        }
    
        public int DiffOfDates(string accountName)
        {
            int lastUsageTime = 0;
            var account = userList.Where(u => u.AccountName == accountName).FirstOrDefault();
            var today = DateTime.Now;
            var expiredDate = account.PwdSetDate.Value;
            var diffOfDates = today - expiredDate;
            lastUsageTime = diffOfDates.Days;

            return lastUsageTime;
        }
    }
}

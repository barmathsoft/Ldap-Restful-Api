using System;
using System.Collections.Generic;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bar.LDAP.RestApi.Models;

namespace Bar.LDAP.RestApi
{
    public interface IManager
    {
        IEnumerable<UserPrincipal> UserList(string accountName);
        List<GroupDto> GroupList(string username,string password);
    }
}

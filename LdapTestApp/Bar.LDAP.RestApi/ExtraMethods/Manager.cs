using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Web;
using Bar.LDAP.RestApi.Models;

namespace Bar.LDAP.RestApi.ExtraMethods
{
    public class Manager : IManager
    {
        public List<GroupDto> GroupList(string username, string password)
        {
            List<GroupDto> groupList = new List<GroupDto>();
            DirectoryEntry directoryEntry = new DirectoryEntry("server", username, password);

            if (directoryEntry.Properties.Count > 0)
            {
                SearchResultCollection searchResult;
                DirectorySearcher directorySearcher = null;

                directorySearcher = new DirectorySearcher(directoryEntry);
                directorySearcher.Sort = new SortOption("name", SortDirection.Ascending);
                directorySearcher.PropertiesToLoad.Add("name");
                directorySearcher.PropertiesToLoad.Add("memberof");
                directorySearcher.PropertiesToLoad.Add("member");
                directorySearcher.Filter = "(&(objectCategory=Group))";

                searchResult = directorySearcher.FindAll();

                foreach (SearchResult result in searchResult)
                {
                    if (result.Properties["name"].Count > 0)
                    {
                        groupList.Add(new GroupDto
                        {
                            GroupName = result.Properties["name"][0].ToString(),
                            Path = result.Path
                        });
                    }
                }
            }
            return groupList;
        }

        public IEnumerable<UserPrincipal> UserList(string accountName)
        {
            PrincipalContext context = new PrincipalContext(ContextType.Domain);

            UserPrincipal queryByUser = new UserPrincipal(context);
            queryByUser.SamAccountName = accountName;
            

            PrincipalSearcher search = new PrincipalSearcher(queryByUser);

            var users = search.FindAll().Cast<UserPrincipal>();
            
            return users;
        }
    }
}
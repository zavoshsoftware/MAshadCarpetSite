
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using MashadCarpet.Models;


 

namespace eShop.Classes
{
    public class eShopRoleProvider : RoleProvider
    {
        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override string ApplicationName
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] GetRolesForUser(string username)
        { 

            string[] a = new string[1];
            Guid userID = new Guid(username);
            using (MashadCarpetEntities db = new MashadCarpetEntities())
            {
                var m = (from us in db.Users
                         where us.UserID == userID
                         select us).FirstOrDefault();

                var o = (from b in db.Roles
                         where b.RoleID == m.fk_RoleID
                         select b).FirstOrDefault();

                a[0] = o.RoleName;
 

            } 
            return a;
        }
        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }
    }
}
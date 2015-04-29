using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KarateIsere.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace KarateIsere.DataAccess {
    public partial class Role {
        private RoleStore<IdentityRole> roleStore;

        private UserStore<ApplicationUser> userStore;

        private RoleManager<IdentityRole> roleManager;

        private UserManager<ApplicationUser> userManager;

        protected ApplicationDbContext context = new ApplicationDbContext();

        private RoleStore<IdentityRole> RoleStore {
            get {
                if (roleStore == null) {
                    roleStore = new RoleStore<IdentityRole>(context);
                }

                return roleStore;
            }
        }

        private RoleManager<IdentityRole> RoleManager {
            get {
                if (roleManager == null) {
                    roleManager = new RoleManager<IdentityRole>(RoleStore);
                }

                return roleManager;
            }
        }

        private UserStore<ApplicationUser> UserStore {
            get {
                if (userStore == null) {
                    userStore = new UserStore<ApplicationUser>(context);
                }

                return userStore;
            }
        }


        private UserManager<ApplicationUser> UserManager {
            get {
                if (userManager == null) {
                    userManager = new UserManager<ApplicationUser>(UserStore);
                }

                return userManager;
            }
        }


        /// <summary>
        /// Gets the list of roles
        /// </summary>
        /// <returns>the IdentityRole list</returns>
        public List<IdentityRole> Get() {
            return RoleManager.Roles.ToList();
        }

        /// <summary>
        /// Gets the list of user having a role 
        /// </summary>
        /// <param name="roleName">Role name</param>
        /// <returns>List of user having a role</returns>
        public List<ApplicationUser> GetUsers() {
            Contract.Requires(!string.IsNullOrWhiteSpace(Name));

            IdentityRole r = RoleManager.FindByName(Name);
            List<IdentityUserRole> usersRoles = r.Users.ToList();
            List<string> userIds = usersRoles.Select(d => d.UserId).ToList();
            return UserManager.Users.Where(d => userIds.Contains(d.Id)).ToList();
        }

        /// <summary>
        /// Create a new role
        /// </summary>
        public void Create() {
            Contract.Requires(!string.IsNullOrWhiteSpace(Name));

            var res = RoleManager.Roles.Where(d => d.Name.ToUpper() == Name.ToUpper()).SingleOrDefault();
            if (res == null) {
                IdentityRole ir = new IdentityRole(Name);
                RoleManager.Create(ir);
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Delete a new role
        /// </summary>
        /// <param name="name">Role name</param>
        public void Delete() {
            Contract.Requires(!string.IsNullOrWhiteSpace(Name));

            if (RoleManager.RoleExists(Name)) {
                var res = RoleManager.FindByName(Name);
                RoleManager.Delete(res);
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Grant a role to a user by its name
        /// </summary>
        /// <param name="role"></param>
        /// <param name="user"></param>
        public void Grant(string userId) {
            Contract.Requires(!string.IsNullOrWhiteSpace(Name));
            Contract.Requires(!string.IsNullOrWhiteSpace(userId));

            UserManager.AddToRole(userId, Name);
        }

        public void UnGrant(string userId) {
            Contract.Requires(!string.IsNullOrWhiteSpace(Name));
            Contract.Requires(!string.IsNullOrWhiteSpace(userId));

            UserManager.RemoveFromRole(userId, Name);
        }
    }
}

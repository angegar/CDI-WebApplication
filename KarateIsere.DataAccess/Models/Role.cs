using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;

namespace KarateIsere.DataAccess {
    public partial class Role {

        public string Id { get; set; }

        public string Name { get; private set; }

        public Role() { }

        public Role(string roleName) {
            Name = roleName;
            GetRoleByName();
        }

        /// <summary>
        /// Check if role already exists and complete the role object if
        /// it already exists.
        /// </summary>
        private void GetRoleByName() {
            IdentityRole tmp = RoleManager.Roles.Where(d => d.Name == Name).SingleOrDefault();
            if (tmp != null) this.Id = tmp.Id;
        }
    }
}

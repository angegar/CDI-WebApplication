using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KarateIsere.DataAccess {
    public partial class Role {

        public int Id { get; set; }

        public string Name { get; private set; }

        public Role() { }
        public Role(string roleName) {
            Name = roleName;
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LimeSystems.DAL
{
    public class Role
    {
        [Key]
        public int RoleId { get; set; }

        [Required, MaxLength(20)]
        public string Name { get; set; }

        public virtual ICollection<User> Users { get; set; }

        public Role() {
            Users = new HashSet<User>();
        }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LimeSystems.DAL
{
    public class User
    {
        [Key, Required, MaxLength(25)]
        public string Login { get; set; }

        [MaxLength(25)]
        public string Name { get; set; }

        [MaxLength(25)]
        public string Surname { get; set; }

        [Required, MaxLength(32)]
        public string Password { get; set; }

        [Required]
        public bool Status { get; set; }

        [Required]
        public Role UserRole { get; set; }

        public virtual ICollection<Project> Projects { get; set; }

        public User()
        {
            Projects = new HashSet<Project>();
        }

    }
}
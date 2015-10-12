using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LimeSystems.DAL
{
    public class Project
    {
        [Key]
        public int ProjectCode { get; set; }

        [Required, MaxLength(30)]
        public string Name { get; set; }

        [Required]
        public bool Status { get; set; }

        public virtual ICollection<User> Employees { get; set; }

        public virtual ICollection<Task> Tasks { get; set; }

        public Project() {
            Employees = new HashSet<User>();
            Tasks = new HashSet<Task>();
        }
    }
}
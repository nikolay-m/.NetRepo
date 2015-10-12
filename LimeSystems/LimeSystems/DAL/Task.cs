using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LimeSystems.DAL
{
    public class Task
    {
        [Key]
        public int TaskCode { get; set; }

        [Required, MaxLength(25)]
        public string Name { get; set; }

        [Column(TypeName="text")] 
        public string Description { get; set; }

        [Required]
        public Project Project { get; set; }

        public User Employee { get; set; }

        [Column(TypeName = "text")] 
        public string EmployeeInfo { get; set; }

        [Required]
        public bool Status { get; set; }

        [Required]
        public DateTime OpenDate { get; set; }

        public DateTime? CloseDate { get; set; }

        [Required]
        public DateTime DeadLine { get; set; }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace LimeSystems.DAL
{
    public class Storage: DbContext
    {
        public Storage() : this("Storage") { }

        public Storage(string constr) : base(constr) { }

        public DbSet<User> Users {get; set;}
        public DbSet<Project> Projects { get; set; }
        public DbSet<Task> Tasks { get; set; }
        public DbSet<Role> Roles { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Malenko_new.Models
{
    public class DataContext : DbContext
    {
        public DataContext() : base("Userdb")
        {
        }

        public DbSet<User> Users { get; set; }
    }
}
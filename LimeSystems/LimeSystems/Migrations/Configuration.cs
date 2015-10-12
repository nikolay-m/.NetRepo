namespace LimeSystems.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using LimeSystems.DAL;

    internal sealed class Configuration : DbMigrationsConfiguration<LimeSystems.DAL.Storage>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(LimeSystems.DAL.Storage context)
        {
            Role Admin = new Role() { Name = "Admin" };
            Role User = new Role() { Name = "User" };

            User AdminUser = new User();

            AdminUser.Login = "Admin";
            AdminUser.Password = "e3afed0047b08059d0fada10f400c1e5";
            AdminUser.Status = true;
            AdminUser.UserRole = Admin;

            User CommonUser = new User();

            CommonUser.Login = "User";
            CommonUser.Password = "8f9bfe9d1345237cb3b2b205864da075";
            CommonUser.Status = true;
            CommonUser.UserRole = User;

            context.Users.AddOrUpdate(AdminUser, CommonUser);
        }
    }
}

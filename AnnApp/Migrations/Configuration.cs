namespace AnnApp.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using AnnApp.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<AnnApp.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(AnnApp.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
            AddUsers(context);
        }


        // help - http://stackoverflow.com/questions/19280527/mvc-5-seed-users-and-roles
        void AddUsers(AnnApp.Models.ApplicationDbContext context)
        {
            var storeP = new RoleStore<IdentityRole>(context);
            RoleManager<IdentityRole> managerP = new RoleManager<IdentityRole>(storeP);
            var storeS = new RoleStore<IdentityRole>(context);
            RoleManager<IdentityRole> managerS = new RoleManager<IdentityRole>(storeS);
            if (!context.Roles.Any(r => r.Name == "Professor"))
            {
                var role = new IdentityRole { Name = "Professor" };

                managerP.Create(role);
            }

            if (!context.Roles.Any(r => r.Name == "Student"))
            {
                var role = new IdentityRole { Name = "Student" };

                managerS.Create(role);
            }

            var um = new UserManager<ApplicationUser>(
                    new UserStore<ApplicationUser>(context));

            if (!context.Users.Any(u => u.UserName == "Lecturer1@email.com"))
            {
                var p1 = new ApplicationUser { UserName = "Lecturer1@email.com" };
                var result1 = um.Create(p1, "password");
                if (result1.Succeeded == false) { throw new Exception(result1.Errors.First()); }
                // I know username may not be unique, but I will make it in this case.
                um.AddToRole(p1.Id, "Professor");
            }

            if (!context.Users.Any(u => u.UserName == "Student1@email.com"))
            {
                var s1 = new ApplicationUser { UserName = "Student1@email.com" };
                var result2 = um.Create(s1, "password");
                if (result2.Succeeded == false) { throw new Exception(result2.Errors.First()); }
                um.AddToRole(s1.Id, "Student");
            }
            if (!context.Users.Any(u => u.UserName == "Student2@email.com"))
            {
                var s2 = new ApplicationUser { UserName = "Student2@email.com" };
                var result2 = um.Create(s2, "password");
                if (result2.Succeeded == false) { throw new Exception(result2.Errors.First()); }
                um.AddToRole(s2.Id, "Student");
            }
            if (!context.Users.Any(u => u.UserName == "Student3@email.com"))
            {
                var s3 = new ApplicationUser { UserName = "Student3@email.com" };
                var result2 = um.Create(s3, "password");
                if (result2.Succeeded == false) { throw new Exception(result2.Errors.First()); }
                um.AddToRole(s3.Id, "Student");
            }
            if (!context.Users.Any(u => u.UserName == "Student4@email.com"))
            {
                var s4 = new ApplicationUser { UserName = "Student4@email.com" };
                var result2 = um.Create(s4, "password");
                if (result2.Succeeded == false) { throw new Exception(result2.Errors.First()); }
                um.AddToRole(s4.Id, "Student");
            }
            if (!context.Users.Any(u => u.UserName == "Student5@email.com"))
            {
                var s5 = new ApplicationUser { UserName = "Student5@email.com" };
                var result2 = um.Create(s5, "password");
                if (result2.Succeeded == false) { throw new Exception(result2.Errors.First()); }
                um.AddToRole(s5.Id, "Student");
            }
        }
    }
}

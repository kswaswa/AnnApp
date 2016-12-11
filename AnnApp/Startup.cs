using AnnApp.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AnnApp.Startup))]
namespace AnnApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            CreateRolesAndUsers();
        }

        private void CreateRolesAndUsers()
        {
            ApplicationDbContext context = new ApplicationDbContext();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            if (!roleManager.RoleExists("Professor"))
            {
                // Create prof role
                var role = new IdentityRole();
                role.Name = "Professor";
                roleManager.Create(role);
            }

            if (!roleManager.RoleExists("Student"))
            {
                // Create prof role
                var role = new IdentityRole();
                role.Name = "Student";
                roleManager.Create(role);
            }
        }
    }
}

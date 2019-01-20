using FormtechS.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FormtechS.Startup))]
namespace FormtechS
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            CreateRolesAndUsers();
        }
        public void CreateRolesAndUsers()
        {
            ApplicationDbContext context = new ApplicationDbContext();
            var RoleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            if(!RoleManager.RoleExists("Admin"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Admin";
                RoleManager.Create(role);
                
                //var user = new ApplicationUser();
                //user.Email = "yolandapardo1990@gmail.com";
                //user.UserName = "yolanda";
                //string userP = "A@Z200711*";
                //var chUser = UserManager.Create(user, userP);
                //if(chUser.Succeeded)
                //{
                //    var result1 = UserManager.AddToRole(user.Id, "Admin");
                //}
            }
            if(!RoleManager.RoleExists("Employee"))
            {
                var role = new IdentityRole();
                role.Name = "Employee";
                RoleManager.Create(role);

            }
            if (!RoleManager.RoleExists("Viewer"))
            {
                var role = new IdentityRole();
                role.Name = "Viewer";
                RoleManager.Create(role);

            }
        }
    }
}

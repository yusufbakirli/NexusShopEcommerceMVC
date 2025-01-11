using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Tez.MvcWebUI.Entity;

namespace Tez.MvcWebUI.Identity
{
    public class IdentityInitializer : CreateDatabaseIfNotExists<IdentityDataContext>
    {
        protected override void Seed(IdentityDataContext context)
        {
            if (!context.Roles.Any(i => i.Name == "admin"))
            {
                var store = new RoleStore<ApplicationRole>(context);
                var manager = new RoleManager<ApplicationRole>(store);

                var role = new ApplicationRole() { Name = "admin", Description = "yönetici rolü" };

                manager.Create(role);
            }

            if (!context.Roles.Any(i => i.Name == "user"))
            {
                var store = new RoleStore<ApplicationRole>(context);
                var manager = new RoleManager<ApplicationRole>(store);

                var role = new ApplicationRole() { Name = "user", Description = "kullanıcı rolü" };

                manager.Create(role);
            }

            if (!context.Users.Any(i => i.Name == "ysfbkrl"))
            {
                var store = new UserStore<ApplicationUser>(context);
                var manager = new UserManager<ApplicationUser>(store);

                var user = new ApplicationUser() { Name = "Yusuf", Surname = "Bakırlı", UserName = "ysfbkrl", PhoneNumber = "5511338504", Email = "ysfbkrl@gmail.com" };

                manager.Create(user, "yusuf864");
                manager.AddToRole(user.Id, "admin");
                manager.AddToRole(user.Id, "user");
            }

            if (!context.Users.Any(i => i.Name == "krmbkrl"))
            {
                var store = new UserStore<ApplicationUser>(context);
                var manager = new UserManager<ApplicationUser>(store);

                var user = new ApplicationUser() { Name = "Kerem", Surname = "Bakırlı", UserName = "krmbkrl", PhoneNumber = "5434039295", Email = "krmbkrl233@gmail.com" };

                manager.Create(user, "kerem233");
                manager.AddToRole(user.Id, "user");
            }


            base.Seed(context);
        }
    }
}
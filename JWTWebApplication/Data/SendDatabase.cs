using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JWTWebApplication.Data
{
    public class SendDatabase
    {

        public static void Initialize(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetRequiredService<ApplicationDbContext>();
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplcationUser>>();
            context.Database.EnsureCreated();
            if (!context.Users.Any())
            {
                ApplcationUser user = new ApplcationUser()
                {
                    Email="a@b.com",
                    SecurityStamp=Guid.NewGuid().ToString(),
                    UserName="Ram"
                };

                userManager.CreateAsync(user, "Password@123");
            }
        }

    }
}

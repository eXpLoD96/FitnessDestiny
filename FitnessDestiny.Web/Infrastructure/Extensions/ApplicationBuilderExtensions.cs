﻿namespace FitnessDestiny.Web.Infrastructure.Extensions
{
    using FitnessDestiny.Data;
    using FitnessDestiny.Data.Models;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using System;
    using System.Threading.Tasks;

    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseDatabaseMigration(this IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                serviceScope.ServiceProvider.GetService<FitnessDestinyDbContext>().Database.Migrate();
                var userManager = serviceScope.ServiceProvider.GetService<UserManager<User>>();
                var roleManager = serviceScope.ServiceProvider.GetService<RoleManager<IdentityRole>>();

                Task
                    .Run(async () =>
                    {
                        var adminName = WebConstants.Administrator;
                        var roles = new[]
                        {
                            adminName,
                            WebConstants.BlogAuthor,
                            WebConstants.VipUser
                        };

                        foreach (var role in roles)
                        {

                            var roleExists = await roleManager.RoleExistsAsync(role);

                            if (!roleExists)
                            {
                                await roleManager.CreateAsync(new IdentityRole
                                {
                                    Name = role
                                });
                            }
                        }

                        var adminEmail = "admin@abv.bg";
                        var adminUser = await userManager.FindByEmailAsync(adminEmail);

                        if (adminUser == null)
                        {
                            adminUser = new User
                            {
                                Email = adminEmail,
                                UserName = adminName,
                                Birthdate = DateTime.UtcNow
                            };

                            await userManager.CreateAsync(adminUser, "admin123");
                            await userManager.AddToRoleAsync(adminUser, adminName);
                        }
                    })
                    .Wait();
            }
            return app;
        }
    }
}

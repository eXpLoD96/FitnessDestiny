namespace FitnessDestiny.Web.Infrastructure.Extensions
{
    using FitnessDestiny.Data;
    using FitnessDestiny.Data.Models;
    using FitnessDestiny.Data.Models.Enums;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseDatabaseMigration(this IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                serviceScope.ServiceProvider.GetService<FitnessDestinyDbContext>().Database.Migrate();
                FitnessDestinyDbContext db = serviceScope.ServiceProvider.GetService<FitnessDestinyDbContext>();
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
                            WebConstants.VipUser,
                            WebConstants.Trainer
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
                    
                
                if (!db.Articles.Any())
                {
                    Task
                        .Run(async () =>
                            {

                                var adminEmail = "admin@abv.bg";
                                var adminUser = await userManager.FindByEmailAsync(adminEmail);
                                var adminId = await userManager.GetUserIdAsync(adminUser);

                                await db.Articles.AddRangeAsync(
                                    new Article
                                    {
                                        Title = "Can High Rep Lifting Replace Cardio For Lifters?",
                                        Content = @"Cardiovascular training can have a host of benefits.  The benefits that get the most press are health-related, for good reason: cardiorespiratory fitness is a strong predictor of all-cause mortality (2).  However, cardiorespiratory fitness may have some benefits for strength athletes as well.  Higher levels of cardiorespiratory fitness may influence how well you can recover between sets, and thus how much volume you can tolerate in a training session.
                                 Unfortunately, it’s almost universally true that strength athletes don’t particularly like doing cardio.If you chose a sport that revolves around lifting heavy objects exactly one time,
                                 then it makes sense that cardio may not click with you.As such, many people wonder if they can get the benefits of cardiovascular training by simply training with high reps, or training with short rest periods.You’re going to be winded after a high rep set of squats or deadlifts, so high - rep training must be a good cardiovascular conditioning tool, the thinking goes.
                                 It’s also been proposed that using high - rep work as your cardiovascular training may help mitigate the interference effect – the fact that people generally gain less muscle mass and strength when doing both strength and aerobic training versus doing exclusively strength training(3).
                                This study set out to test these ideas.  Over 8 weeks, a group of competitive powerlifters and strongmen did two sessions of high intensity interval training(HIIT) per week, using either squats and deadlifts with 60 % of 1RM or cycle sprints as the exercise stimulus.  Predicted VO2max(a measure of cardiorespiratory conditioning) and predicted 1RM knee extension improved in both groups.  Strength increases were similar between groups, but the group performing cycle sprints improved their predicted VO2max more, indicating that cycle sprints are likely a more effective conditioning tool than high-rep squats and deadlifts.",
                                        Author = adminUser,
                                        AuthorId = adminId,
                                        PublishDate = DateTime.UtcNow
                                    },
                                    new Article
                                    {
                                        Title = "Implementing Flexible Training Templates: Why and How?",
                                        Content = @"There’s always a day or two during a training cycle (or even every week) when we just don’t feel great; however, the work still needs to get done. Often, we decide to force ourselves to try to perform the prescribed work, which just ends in frustration from missed reps. For example, 5X5 @80% following only 4 hours of sleep might not be that easy. Similarly, 5X5 @80% at 5 a.m. or after a 12-hour workday is quite difficult. The coach in all of us knows performing a difficult session under any of these circumstances is a bad idea, but the athlete in us can’t resist. So, we take a bunch of caffeine and do the session anyways, though we might miss reps or have to significantly lighten the load. After the session, we spend the next 48 hours thinking about how terrible everything is and obsessing over how weak we have instantly become.
                                  Well, there is a pretty easy way to avoid this issue, which is by using a flexible training template. In short, a flexible training template is having all of your weekly or monthly sessions laid out and then choosing which one you are going to do based upon your energy levels or daily “readiness” prior to the training session, rather than having to perform those sessions in a fixed order.
                                  The premise behind the theory is this: If you try to perform a session when you are not ready, you might miss reps or have to decrease load, which will compromise volume and intensity performed, in turn decreasing the rate of long-term hypertrophy and strength adaptions. Indeed, data has shown a flexible template to be more effective than a fixed weekly training order for strength (1) and adherence to training (2). However, there are two main methodological differences between the two studies that exist on this topic. So even though using flexibility in training can be effective, there are questions that remain about the best way to implement this form of autoregulation. With that in mind, let’s briefly break down these two studies and then point out the two methodological differences to determine a way forward.",
                                        Author = adminUser,
                                        AuthorId = adminId,
                                        PublishDate = DateTime.UtcNow
                                    }
                                );
                                await db.SaveChangesAsync();
                            }).Wait();
                }

                if (!db.Supplements.Any())
                {
                    Task
                        .Run(async () =>
                        {
                            await db.Supplements.AddRangeAsync(
                                new Supplement
                                {
                                    Name = "Whey protein powder",
                                    Description = @"Whey protein is a mixture of globular proteins isolated from whey, the liquid material created as a by-product of cheese production.",
                                    Brand = "On nutrition",
                                    ImageUrl = "http://www.gnc.com/dw/image/v2/BBLB_PRD/on/demandware.static/-/Sites-master-catalog-gnc/default/dwda4f9cec/hi-res/350260_1.jpg?sw=2000&sh=2000&sm=fit",
                                    Price = 60,
                                    Quantity = 10,
                                    SupplementType = SupplementType.Protein
                                },
                                new Supplement
                                {
                                    Name = "Creatine",
                                    Description = @"99.9% Pure Unflavored Creatine with No Fillers or Additives to Support Strength and Power",
                                    Brand = "On nutrition",
                                    ImageUrl = "https://www.supplementworld.co.za/media/catalog/product/cache/1/image/9df78eab33525d08d6e5fb8d27136e95/o/p/optimum-nutrition-micronised-creatine-powder.jpg",
                                    Price = 20,
                                    Quantity = 18,
                                    SupplementType = SupplementType.Creatine
                                },
                                new Supplement
                                {
                                    Name = "Preworkout",
                                    Description = "An Advanced Pre-Workout Formulated for Anyone Seeking Increased Energy and Focus",
                                    Brand = "Cellucor",
                                    ImageUrl = "https://store.bbcomcdn.com/images/store/skuimage/sku_CELLU4620197/image_skuCELLU4620197_largeImage_X_450_white.jpg",
                                    Price = 40,
                                    Quantity = 8,
                                    SupplementType = SupplementType.Preworkout
                                },
                                new Supplement
                                {
                                    Name = "True-Mass Gainer",
                                    Description = "Ultra Premium Muscle Mass Gainer",
                                    Brand = "BSN",
                                    ImageUrl = "https://store.bbcomcdn.com/images/store/skuimage/sku_BSN030/image_skuBSN030_largeImage_X_450_white.jpg",
                                    Price = 36,
                                    Quantity = 3,
                                    SupplementType = SupplementType.Gainer
                                }
                                );
                            await db.SaveChangesAsync();
                        }).Wait();
                }
                
            }
            return app;
        }
    }
}


namespace FitnessDestiny.Web
{
    using AutoMapper;
    using FitnessDestiny.Data;
    using FitnessDestiny.Data.Models;
    using FitnessDestiny.Services;
    using FitnessDestiny.Services.Implementations;
    using FitnessDestiny.Web.Infrastructure.Extensions;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {

            services.AddDbContext<FitnessDestinyDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<User, IdentityRole>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredLength = 5;
            })
                .AddEntityFrameworkStores<FitnessDestinyDbContext>()
                .AddDefaultTokenProviders();

            services.AddAuthentication().AddFacebook(facebookOptions =>
            {
                facebookOptions.AppId = "133498467338842";
                facebookOptions.AppSecret = "d99092f77500f68a934512bb8d8de75e";
            })
            .AddTwitter(twitterOptions =>
            {
                twitterOptions.ConsumerKey = "gNtJX30u9oLjmlP9RDF8UT7Eq";
                twitterOptions.ConsumerSecret = "A53egx9Gskc9YTvypfsatLB02SJSxpaX46MZxqT7MeKDpuouaM";
            })
            .AddGoogle(googleOptions =>
            {
                googleOptions.ClientId = "101188243251-mlo7sjasjkjgi1m4qtc0ejak5upfnv3g.apps.googleusercontent.com";
                googleOptions.ClientSecret = "RxbCYcyuwFmMKBM7JRpDDRNH";
            })
            .AddMicrosoftAccount(microsoftOptions =>
            {
                microsoftOptions.ClientId = "640ec936-81bb-4243-a818-be84911106dd";
                microsoftOptions.ClientSecret = "yvkcohmPMT51434}VUNZ%}]";
            });


            services.AddAutoMapper();
            services.AddDomainServices();
            services.AddSingleton<IShoppingCartManager, ShoppingCartManager>();
            services.AddRouting(routing => routing.LowercaseUrls = true);
            services.AddSession();

            services.AddMvc(options =>
            {
                options.Filters.Add<AutoValidateAntiforgeryTokenAttribute>();
            });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseDatabaseMigration();
            //var options = new RewriteOptions().AddRedirectToHttps();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseSession();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "profile",
                    template: "users/{username}",
                    defaults: new { controller = "Users", action = "Profile" });

                routes.MapRoute(
                    name: "areas",
                    template: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
                 );

                routes.MapRoute(
                    name: "blog",
                    template: "blog/articles/{id}/{title}",
                    defaults: new { area = "Blog", controller = "Articles", action = "Details" });

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}

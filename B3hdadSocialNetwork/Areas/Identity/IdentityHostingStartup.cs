using System;
using B3hdadSocialNetwork.Areas.Identity.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(B3hdadSocialNetwork.Areas.Identity.IdentityHostingStartup))]
namespace B3hdadSocialNetwork.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<ApplicationContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("ApplicationContextConnection")));

                services.AddDefaultIdentity<ApplicationUser>()
                    .AddRoles<IdentityRole>()
                    .AddEntityFrameworkStores<ApplicationContext>();
                //this section is added for password conditions
                services.Configure<IdentityOptions>(x =>
                {
                    x.Password.RequireDigit = true;
                    x.Password.RequireLowercase = true;
                    x.Password.RequireUppercase = true;
                    x.Password.RequireNonAlphanumeric = true;
                    x.Password.RequiredUniqueChars = 1;
                    x.Password.RequiredLength = 8;
                });

                //Google authentication
                services.AddAuthentication().AddGoogle(x =>
                {
                    x.ClientId = "40309208063-6n8gv92u2mv17gnqife7he7avdo24996.apps.googleusercontent.com";
                    x.ClientSecret = "Removed because of security reasons."; //add ClientSecret !!!!!
                                                                             //x.Scope.Add("https://www.googleapis.com/auth/plus.login");
                });
            });

        }
    }
}
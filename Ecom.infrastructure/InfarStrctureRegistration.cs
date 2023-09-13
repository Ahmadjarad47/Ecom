using Ecom.Core.Entities;
using Ecom.Core.Interfaces;
using Ecom.infrastructure.Data;
using Ecom.infrastructure.Data.Config;
using Ecom.infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.infrastructure
{
    public static class InfarStrctureRegistration
    {
        
        public static  IServiceCollection infarStrctureregistration(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddScoped(typeof(IGenericRepositrie<>), typeof(GenericReposetroy<>));
            /*  services.AddScoped<ICategoryRepoetroy, CategoryRepositories>();*/
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IBraketRepositry, BasketRepositry>();
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });
          //
            services.AddIdentity<AppUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();
            services.AddMemoryCache();
            services.AddAuthentication(op =>
            {
                op.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            });
            return services;
        }
        public static async void infraStructurConfigMidlleware(this IApplicationBuilder builder)
        {
            using (var scop = builder.ApplicationServices.CreateScope())
            {
                var usermaneher = scop.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
                await IdentityConfiguration.SeedUserAsync(usermaneher);
            }
        }
    }
}

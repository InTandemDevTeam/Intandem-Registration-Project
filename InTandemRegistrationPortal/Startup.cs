using InTandemRegistrationPortal.Authorization;
using InTandemRegistrationPortal.Data;
using InTandemRegistrationPortal.Models;
using InTandemRegistrationPortal.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
namespace InTandemRegistrationPortal
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<InTandemUser, IdentityRole>(config =>
            {
                config.SignIn.RequireConfirmedEmail = false;
            })
                .AddDefaultUI(UIFramework.Bootstrap4)
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();
            services.AddTransient<IEmailSender, EmailSender>();
            services.Configure<AuthMessageSenderOptions>(Configuration);

            services.AddMvc(config =>
            {
                var policy = new AuthorizationPolicyBuilder()
                                 .RequireAuthenticatedUser()
                                 .Build();
                config.Filters.Add(new AuthorizeFilter(policy));
            }).AddRazorPagesOptions(options =>
            {
                options.Conventions.AddPageRoute("/Events/Index", "");
            });

            services.AddScoped<IAuthorizationHandler, RegisterAuthorizationHandler>();
            services.AddScoped<IAuthorizationHandler, ManagerAuthorizationHandler>();

            services.AddScoped<UserService>();

            //.SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            /*.AddRazorPagesOptions(options =>
            {
                options.Conventions.AuthorizePage("/Privacy", "RequireStokerRole");
            });*/
            /*services.AddAuthorization(options =>
            {
                options.AddPolicy("RequireStokerRole",
                    policy => policy.RequireRole("Stoker"));
            })*/
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            CreateRoles(serviceProvider).Wait();

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseAuthentication();

            app.UseMvc();
        }
        private async Task CreateRoles(IServiceProvider serviceProvider)
        {
            var RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            //var UserManager = serviceProvider.GetRequiredService<UserManager<InTandemUser>>();
            string[] roleNames = {"Administrator", "Captain", "Stoker", "Volunteer"};
            IdentityResult result;
            foreach (string roleName in roleNames)
            {
                var roleExist = await RoleManager.RoleExistsAsync(roleName);
                IdentityRole role = new IdentityRole(roleName);
                if (!roleExist)
                {
                    result = await RoleManager.CreateAsync(role);
                }
            }
            //Task<InTandemUser> adminUser = UserManager.FindByEmailAsync(email);
            /*var poweruser = new InTandemUser
            {
                UserName = Configuration["AppSettings:Email"],
                Email = Configuration["AppSettings:Email"]
            };
            string userPWD = Configuration["AppSettings:Password"];
            var _user = await UserManager.FindByEmailAsync(Configuration["AdminCredentials:Email"]);
            if (_user == null)
            {
                var createPowerUser = await UserManager.CreateAsync(poweruser, userPWD);
                if (createPowerUser.Succeeded)
                {
                    //here we tie the new user to the role
                    await UserManager.AddToRoleAsync(poweruser, "Administrator");

                }
            }*/
        }
    }
}

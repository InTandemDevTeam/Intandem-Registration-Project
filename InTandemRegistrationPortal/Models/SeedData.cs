using InTandemRegistrationPortal.Authorization;
using InTandemRegistrationPortal.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace InTandemRegistrationPortal.Models
{
    public static class SeedData
    {
        public static async Task Initialize(IServiceProvider serviceProvider, string testUserPw)
        {
            using (var context = new ApplicationDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
            {
                // For sample purposes seed both with the same password.
                // Password is set with the following:
                // dotnet user-secrets set SeedUserPW <pw>
                // The admin user can do anything

                var adminID = await EnsureUser(serviceProvider, testUserPw, "Admin", "User", "adminUser", "admin@intandembike.org");
                await EnsureRole(serviceProvider, adminID.Id, Constants.AdministratorsRole);
                
                var captainID = await EnsureUser(serviceProvider, testUserPw, "Captain", "User", "captainUser", "captain@intandembike.org");
                await EnsureRole(serviceProvider, captainID.Id, Constants.CaptainsRole);

                var stokerID = await EnsureUser(serviceProvider, testUserPw, "Stoker", "User", "stokerUser", "stoker@intandembike.org");
                await EnsureRole(serviceProvider, stokerID.Id, Constants.StokersRole);

                var volID = await EnsureUser(serviceProvider, testUserPw, "Volunteer", "User", "volUser", "volunteer@intandembike.org");
                await EnsureRole(serviceProvider, volID.Id, Constants.VolunteersRole);
                
                if (!context.RideEvent.Any())
                {
                    var cpr = new RideEvent
                    {
                        EventDate = DateTime.Today.AddDays(30),
                        Description = "Description for Central Park Ride in 1 month's time",
                        Location = "Central Park NYC",
                        EventName = "Central Park Ride",
                        EventRatio = "1:2",
                        IsActive = true
                    };
                    var fb = new RideEvent
                    {
                        EventDate = DateTime.Today.AddDays(60),
                        Description = "Description for 5 Borough Ride in 2 month's time",
                        Location = "All 5 Boroughs!",
                        EventName = "5 Borough Ride",
                        EventRatio = "1:1",
                        IsActive = true
                    };
                    var se = new RideEvent
                    {
                        EventDate = DateTime.Today.AddDays(1),
                        Description = "Description for Social Event tomorrow",
                        Location = "Brooklyn Bridge Park",
                        EventName = "Social Picnic",
                        EventRatio = "N/A",
                        IsActive = true
                    };

                    context.RideEvent.AddRange(cpr, fb, se);
                    context.SaveChanges();
                }

                if (!context.RideLeaderAssignment.Any())
                {
                    var evt = context.RideEvent
                        .AsNoTracking()
                        .FirstOrDefault(x => x.EventName == "Central Park Ride");

                    var evtSp = context.RideEvent
                        .AsNoTracking()
                        .FirstOrDefault(x => x.EventName == "Social Picnic");

                    context.RideLeaderAssignment.AddRange(
                        new RideLeaderAssignment { RideEventID = evtSp.ID, InTandemUserID = volID.Id },
                        new RideLeaderAssignment { RideEventID = evtSp.ID, InTandemUserID = captainID.Id },
                        new RideLeaderAssignment { RideEventID = evt.ID, InTandemUserID = captainID.Id });

                    context.SaveChanges();
                }
            }
        }

        private static async Task<InTandemUser> EnsureUser(IServiceProvider serviceProvider,
            string testUserPw, string firstName, string lastName, string userName, string emailAddress)
        {
            var userManager = serviceProvider.GetService<UserManager<InTandemUser>>();

            var user = await userManager.FindByEmailAsync(emailAddress);
            if (user == null)
            {
                user = new InTandemUser { FirstName = firstName, LastName = lastName, UserName = userName, Email = emailAddress };
                var result = await userManager.CreateAsync(user, testUserPw);
            }

            return user;
        }

        private static async Task<IdentityResult> EnsureRole(IServiceProvider serviceProvider,
                                                                      string uid, string role)
        {
            var roleManager = serviceProvider.GetService<RoleManager<IdentityRole>>();

            if (roleManager == null)
            {
                throw new Exception("roleManager null");
            }

            IdentityResult IR;
            if (!await roleManager.RoleExistsAsync(role))
            {
                IR = await roleManager.CreateAsync(new IdentityRole(role));
            }

            var userManager = serviceProvider.GetService<UserManager<InTandemUser>>();

            var user = await userManager.FindByIdAsync(uid);

            if (user == null)
            {
                throw new Exception("The testUserPw password was probably not strong enough!");
            }

            IR = await userManager.AddToRoleAsync(user, role);

            return IR;
        }
    }
}

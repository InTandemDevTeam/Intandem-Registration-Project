using InTandemRegistrationPortal.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(InTandemRegistrationPortal.Areas.Identity.IdentityHostingStartup))]
namespace InTandemRegistrationPortal.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            //builder.ConfigureServices((context, services) => {
            //    services.AddDbContext<InTandemRegistrationPortalContext>(options =>
            //        options.UseSqlServer(
            //            context.Configuration.GetConnectionString("InTandemRegistrationPortalContextConnection")));

            //    /*services.AddDefaultIdentity<InTandemUser>()
            //        .AddEntityFrameworkStores<InTandemRegistrationPortalContext>();*/
            //});
        }
    }
}
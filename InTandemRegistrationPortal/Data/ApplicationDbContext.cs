using InTandemRegistrationPortal.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace InTandemRegistrationPortal.Data
{
    public class ApplicationDbContext : IdentityDbContext<InTandemUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<RideEvents> RideEvents { get; set; }
        public DbSet<RideRegistration> RideRegistrations { get; set; }
        public DbSet<ManagerAssignment> ManagerAssignments { get; set; }
    }
}

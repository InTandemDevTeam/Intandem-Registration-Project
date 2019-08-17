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
        public DbSet<RideEvent> RideEvent { get; set; }
        public DbSet<RideRegistration> RideRegistration { get; set; }
        public DbSet<RideLeaderAssignment> RideLeaderAssignment { get; set; }
    }
}

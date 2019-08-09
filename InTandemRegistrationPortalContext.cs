﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace InTandemRegistrationPortal.Models
{
    public class InTandemRegistrationPortalContext : IdentityDbContext<InTandemUser>
    {
        public InTandemRegistrationPortalContext()
        {
        }
        public DbSet<RideEvents> RideEvents { get; set; }
        public DbSet<RideRegistration> RideRegistrations { get; set; }


        public InTandemRegistrationPortalContext(DbContextOptions<InTandemRegistrationPortalContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }
    }
}
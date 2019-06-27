using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using InTandemRegistrationPortal.Models;

namespace InTandemRegistrationPortal.Data
{
    public class ApplicationDbContext : IdentityDbContext<InTandemUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}

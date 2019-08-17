using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using InTandemRegistrationPortal.Models;
using InTandemRegistrationPortal.Data;
using Microsoft.AspNetCore.Authorization;
using InTandemRegistrationPortal.Pages.Admin;
using Microsoft.AspNetCore.Identity;
using InTandemRegistrationPortal.Authorization;

namespace InTandemRegistrationPortal.Pages.Events
{
    [AllowAnonymous]
    public class IndexModel : UserPageModel
    {

        public IndexModel(UserManager<InTandemUser> userManager, 
            ApplicationDbContext context) :
            base(userManager, context)
        {
        }

        public IList<RideEvent> RideEvents { get;set; }

        public async Task OnGetAsync()
        {
            await GetCurrentUser();
            RideEvents = await _context.RideEvent
                .AsNoTracking()
                .Include(r => r.RideLeaderAssignments)
                    .ThenInclude(r => r.InTandemUser)
                .ToListAsync();
        }
    }
}

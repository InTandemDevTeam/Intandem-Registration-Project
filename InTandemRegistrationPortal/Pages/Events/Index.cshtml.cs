using InTandemRegistrationPortal.Data;
using InTandemRegistrationPortal.Models;
using InTandemRegistrationPortal.Pages.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
        public IList<RideEvent> UpcomingEvents { get; set; }
        public IList<RideEvent> CancelledEvents { get; set; }
        public IList<RideEvent> PassedEvents { get; set; }
        public IList<RideEvent> IncompleteEvents { get; set; }

        public async Task OnGetAsync()
        {
            await GetCurrentUser();
            RideEvents = await _context.RideEvent
                .AsNoTracking()
                .Include(r => r.RideLeaderAssignments)
                    .ThenInclude(r => r.InTandemUser)
                .ToListAsync();
            UpcomingEvents = RideEvents
                .Where(m => m.Status == Status.Upcoming)
                .ToList();
            CancelledEvents = RideEvents
                .Where(m => m.Status == Status.Cancelled)
                .ToList();
            PassedEvents = RideEvents
                .Where(m => m.Status == Status.Passed)
                .ToList();
            IncompleteEvents = RideEvents
                .Where(m => m.Status == Status.Incomplete)
                .ToList();
        }
    }
}

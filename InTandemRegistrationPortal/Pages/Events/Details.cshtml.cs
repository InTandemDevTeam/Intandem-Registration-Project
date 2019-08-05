using InTandemRegistrationPortal.Data;
using InTandemRegistrationPortal.Models;
using InTandemRegistrationPortal.Pages.Admin;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace InTandemRegistrationPortal.Pages.Events
{
    public class DetailsModel : UserPageModel
    {

        public DetailsModel(UserManager<InTandemUser> userManager,
            ApplicationDbContext context) :
            base(userManager, context)
        {
        }
        [BindProperty]
        public RideEvents RideEvent { get; set; }
        public bool HasSignedUp { get; set; }
        public RideRegistration RideRegistration { get; set; }
        public bool WantToSignUp { get; set; }
        public async Task<IActionResult> OnGetAsync(int? id, bool SignUp)
        {
            WantToSignUp = SignUp;
            await GetCurrentUser();
            if (id == null)
            {
                return NotFound();
            }

            RideEvent = await _context.RideEvents.FirstOrDefaultAsync(m => m.ID == id);

            if (RideEvent == null)
            {
                return NotFound();
            }

            bool DoesUserExist = await _context.RideRegistrations
                .AsNoTracking()
                .AnyAsync(m => m.InTandemUserId.Equals(InTandemUser.Id));
            bool DoesEventExist = await _context.RideRegistrations
                .AsNoTracking()
                .AnyAsync(m => m.RideEventsID == RideEvent.ID);
            if (DoesUserExist && DoesEventExist)
            {
                HasSignedUp = true;
            }
            else
            {
                HasSignedUp = false;
            }
            return Page();
        }
        public async Task<IActionResult> OnPostYesAsync(int? id)
        {
            var RideEvent = await _context.RideEvents
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == id);
            await GetCurrentUser();
            if (InTandemUser != null && (RideEvent != null))
            {
                RideRegistration = new RideRegistration
                {
                    InTandemUserId = InTandemUser.Id,
                    RideEventsID = RideEvent.ID
                };

            }
            _context.RideRegistrations.Add(RideRegistration);
            await _context.SaveChangesAsync();
            return RedirectToPage("./Index");
        }
        public async Task<IActionResult> OnPostNoAsync()
        {
            return RedirectToPage("./Index");
        }
    }
}

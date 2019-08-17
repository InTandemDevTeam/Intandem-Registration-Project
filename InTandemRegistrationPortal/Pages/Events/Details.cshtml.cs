using InTandemRegistrationPortal.Data;
using InTandemRegistrationPortal.Models;
using InTandemRegistrationPortal.Pages.Admin;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        public RideEvent RideEvent { get; set; }
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

            RideEvent = await _context.RideEvent.FirstOrDefaultAsync(m => m.ID == id);

            if (RideEvent == null)
            {
                return NotFound();
            }

            if (InTandemUser != null)
            {
                bool DoesUserExist = await _context.RideRegistration
                    .AsNoTracking()
                    .AnyAsync(m => m.InTandemUserID.Equals(InTandemUser.Id));
                bool DoesEventExist = await _context.RideRegistration
                    .AsNoTracking()
                    .AnyAsync(m => m.RideEventID == RideEvent.ID);
                if (DoesUserExist && DoesEventExist)
                {
                    HasSignedUp = true;
                }
                else
                {
                    HasSignedUp = false;
                }
            }
            
            return Page();
        }
        public async Task<IActionResult> OnPostYesAsync(int? id)
        {
            var RideEvent = await _context.RideEvent
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == id);
            await GetCurrentUser();
            if (InTandemUser != null && (RideEvent != null))
            {
                RideRegistration = new RideRegistration
                {
                    InTandemUserID = InTandemUser.Id,
                    RideEventID = RideEvent.ID
                };

            }
            _context.RideRegistration.Add(RideRegistration);
            await _context.SaveChangesAsync();
            return RedirectToPage("./Index");
        }
        public IActionResult OnPostNo()
        {
            return RedirectToPage("./Index");
        }
    }
}

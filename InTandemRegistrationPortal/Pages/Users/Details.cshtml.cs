using InTandemRegistrationPortal.Data;
using InTandemRegistrationPortal.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace InTandemRegistrationPortal.Pages.Users
{
    public class DetailsModel : PageModel
    {
        //instances variables
        private readonly UserManager<InTandemUser> _userManager;
        private readonly ApplicationDbContext _context;
        public DetailsModel(
            UserManager<InTandemUser> userManager,
            ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }
        [BindProperty]
        public InTandemUser InTandemUser { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if ((id == null) || (id.Equals("")))
            {
                return NotFound();
            }
            InTandemUser = await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == id);

            if (InTandemUser == null)
            {
                return NotFound();
            }
            return Page();
        }
        // if yes button is clicked, the following method is executed
        // based on asp-page-handler attribute equaling "Yes"
        public async Task<IActionResult> OnPostYesAsync(string id)
        {
            // use passed in user id to find current user
            string UserId = (await _context.Users.FirstOrDefaultAsync(m => m.Id == id)).Id;
            InTandemUser user = _userManager.FindByIdAsync(UserId).Result;

            if (!ModelState.IsValid)
            {
                return Page();
            }
            // if the model state is valid and the user exists, continue
            if (ModelState.IsValid)
            {
                if (!UserExists(UserId))
                {
                    return NotFound();
                }
                else
                {
                    // approve the user if they exist
                    // update the user manager and the database
                    user.HasBeenApproved = true;
                    await _userManager.UpdateAsync(user);
                    await _context.SaveChangesAsync();
                }
            }

            return RedirectToPage("./Users");
        }

        // if yes button is clicked, the following method is executed
        // based on asp-page-handler attribute equaling "Yes"
        public async Task<IActionResult> OnPostNoAsync(string id)
        {
            // use passed in user id to find current user
            string UserId = (await _context.Users.FirstOrDefaultAsync(m => m.Id == id)).Id;
            InTandemUser user = _userManager.FindByIdAsync(UserId).Result;
            if (!ModelState.IsValid)
            {
                return Page();
            }
            // if the model state is valid and the user exists, continue
            if (ModelState.IsValid)
            {
                if (!UserExists(UserId))
                {
                    return NotFound();
                }
                else
                {
                    // deny the user if they exist
                    // update the user manager and the database
                    user.HasBeenApproved = false;
                    await _userManager.UpdateAsync(user);
                    await _context.SaveChangesAsync();
                }
            }
            return RedirectToPage("./Users");
        }

        private bool UserExists(string id)
        {
            return _context.Users.Any(m => m.Id == id);
        }
    }
}
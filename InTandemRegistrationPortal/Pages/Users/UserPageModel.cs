using InTandemRegistrationPortal.Data;
using InTandemRegistrationPortal.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace InTandemRegistrationPortal.Pages.Users
{
    public class UserPageModel : PageModel
    {
        //setup user manager
        protected readonly UserManager<InTandemUser> _userManager;
        protected readonly ApplicationDbContext _context;
        public UserPageModel(
            UserManager<InTandemUser> userManager,
            ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }
        public InTandemUser InTandemUser { get; set; }

        public async Task<IActionResult> GetCurrentUser()
        {
            InTandemUser = await _userManager.GetUserAsync(User);
            return Page();
        }
    }
}

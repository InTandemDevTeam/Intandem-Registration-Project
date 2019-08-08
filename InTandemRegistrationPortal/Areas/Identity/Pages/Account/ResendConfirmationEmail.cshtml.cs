using InTandemRegistrationPortal.Data;
using InTandemRegistrationPortal.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace InTandemRegistrationPortal.Areas.Identity.Pages.Account
{

    public class ResendConfirmationEmailModel : PageModel
    {
        private readonly UserManager<InTandemUser> _userManager;
        //private readonly SignInManager<InTandemUser> _signInManager;
        protected readonly IEmailSender _emailSender;
        //private readonly RoleManager<IdentityRole> _roleManager;
        protected readonly ApplicationDbContext _context;
        public ResendConfirmationEmailModel(
            UserManager<InTandemUser> userManager,
            ApplicationDbContext context,
            IEmailSender emailSender
            )
        {
            _userManager = userManager;
            _context = context;
            _emailSender = emailSender;
        }
        [BindProperty]
        public InTandemUser InTandemUser { get; set; }
        
        public async Task<IActionResult> OnGetAsync(string id = null)
        {
            //var test = @RouteData.Values["id"];
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
        public async Task<IActionResult> OnPostSendVerificationEmailAsync(string id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var user = await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Id.Equals(id));

            if (user == null)
            {
                //return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
                return NotFound($"Unable to load user.");
            }


            var userId = await _userManager.GetUserIdAsync(user);
            var email = await _userManager.GetEmailAsync(user);
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var callbackUrl = Url.Page(
                "/Account/ConfirmEmail",
                pageHandler: null,
                values: new { userId = userId, code = code },
                protocol: Request.Scheme);
            await _emailSender.SendEmailAsync(
                email,
                "Confirm your email",
                $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

            return RedirectToPage();
        }
    }
}
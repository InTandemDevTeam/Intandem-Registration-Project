using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using InTandemRegistrationPortal.Models;
using InTandemRegistrationPortal.Data;

namespace InTandemRegistrationPortal.Pages.Events
{
    public class RegisterModel : PageModel
    {
        private readonly UserManager<InTandemUser> _userManager;
        private readonly ApplicationDbContext _context;
        private readonly ILogger<RegisterModel> _logger;

        public RegisterModel(UserManager<InTandemUser> userManager,
            ApplicationDbContext context,
            ILogger<RegisterModel> logger)
        {
            _userManager = userManager;
            _context = context;
            _logger = logger;
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var user = await _userManager.GetUserAsync(User);

            _logger.LogInformation($"User {user.Id} registered for event {id}");
            var enrollment = new RideRegistration { RideEventID = id, InTandemUserID = user.Id };
            await _context.RideRegistration.AddAsync(enrollment);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
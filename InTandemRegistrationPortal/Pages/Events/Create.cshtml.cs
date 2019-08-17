using InTandemRegistrationPortal.Data;
using InTandemRegistrationPortal.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace InTandemRegistrationPortal.Pages.Events
{
    //[Authorize(Roles = "Captain, Admin")]
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<InTandemUser> _userManager;
        public CreateModel(UserManager<InTandemUser> userManager,
            ApplicationDbContext context)

        //public CreateModel(UserManager<InTandemUser> userManager,
        //    ApplicationDbContext context) :
        //    base(userManager, context)
        {
            _context = context;
            _userManager = userManager;
        }

        


        [BindProperty]
        public RideEvent RideEvents { get; set; }
        [BindProperty]
        public InputModel Input { get; set; }
        public RideLeaderAssignment RideLeaderAssignment { get; set; }
        public SelectList Users => new SelectList(_userManager.Users
            .ToDictionary(k => k.FullName, v => v.FullName), "Key", "Value");
        //public SelectList Users => new SelectList(_context.Users
        //    .AsNoTracking()
        //    .ToList());
        public class InputModel
        {
            public string SelectedUser { get; set; }
        }
        public IActionResult OnGet()
        {
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var selectedUser = await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.FullName == Input.SelectedUser);
            _context.RideEvent.Add(RideEvents);
            RideLeaderAssignment = new RideLeaderAssignment
            {
                InTandemUserID = selectedUser.Id,
                RideEventID = RideEvents.ID
            };
            _context.RideLeaderAssignment.Add(RideLeaderAssignment);

            await _context.SaveChangesAsync();
            

            return RedirectToPage("./Index");
        }
    }
}
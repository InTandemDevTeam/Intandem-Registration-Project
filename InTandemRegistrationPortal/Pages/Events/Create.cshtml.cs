using InTandemRegistrationPortal.Data;
using InTandemRegistrationPortal.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
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

        {
            _context = context;
            _userManager = userManager;
        }

        


        [BindProperty]
        public RideEvent RideEvents { get; set; }
        [BindProperty]
        public InputModel Input { get; set; }
        public RideLeaderAssignment RideLeaderAssignment { get; set; }
        public MultiSelectList Users => new MultiSelectList(_userManager.Users
            .ToDictionary(k => k.FullName, v => v.FullName), "Key", "Value");
        // used to fetch list of selected users
        public class InputModel
        {
            public IList<string> SelectedUser { get; set; }
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
            _context.RideEvent.Add(RideEvents);
            foreach (var user in Input.SelectedUser)
            {
                // finds single selected user in list by full name
                // will change this so it searches by id instead
                var selectedUser = await _context.Users
                    .AsNoTracking()
                    .FirstOrDefaultAsync(m => m.FullName == user);
                RideLeaderAssignment = new RideLeaderAssignment
                {
                    InTandemUserID = selectedUser.Id,
                    RideEventID = RideEvents.ID
                };
                _context.RideLeaderAssignment.Add(RideLeaderAssignment);
            }
            await _context.SaveChangesAsync();
            

            return RedirectToPage("./Index");
        }
    }
}
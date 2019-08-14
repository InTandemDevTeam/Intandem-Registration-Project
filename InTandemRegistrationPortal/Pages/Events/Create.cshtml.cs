using InTandemRegistrationPortal.Data;
using InTandemRegistrationPortal.Models;
using InTandemRegistrationPortal.Pages.Admin;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace InTandemRegistrationPortal.Pages.Events
{
    //[Authorize(Roles = "Captain, Admin")]
    public class CreateModel : UserPageModel
    {
        
        public CreateModel(UserManager<InTandemUser> userManager,
            ApplicationDbContext context) :
            base(userManager, context)
        {
        }

        


        [BindProperty]
        public RideEvents RideEvents { get; set; }
        public InputModel Input { get; set; }
        public RideLeaderAssignment RideLeaderAssignment { get; set; }
        public SelectList Users => new SelectList(_context.Users
            .AsNoTracking()
            .ToList());
        //.ToDictionary(k => k.FullName, v => v.FullName), "Key", "Value");
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
            await GetCurrentUser();
            if (!ModelState.IsValid)
            {
                return Page();
            }
            //var selectedUser = await _context.Users
            //    .AsNoTracking()
            //    .FirstOrDefaultAsync(m => m.UserName.Equals(Input.SelectedUser));
            //await _context.SaveChangesAsync();
            // commented code below generates NullReferenceException
            // var test = RideEvents.ID;
            RideLeaderAssignment = new RideLeaderAssignment
            {
                InTandemUserId = InTandemUser.Id,
                //RideEventsID = RideEvents.ID
                RideEventsID = 1
            };
            //RideEvents.RideLeaderAssignments.Add(RideLeaderAssignment);

            _context.RideEvents.Add(RideEvents);
           // _context.RideLeaderAssignments.Add(RideLeaderAssignment);

            await _context.SaveChangesAsync();
            

            return RedirectToPage("./Index");
        }
    }
}
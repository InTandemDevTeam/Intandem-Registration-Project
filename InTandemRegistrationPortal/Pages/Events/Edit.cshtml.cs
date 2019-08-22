using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using InTandemRegistrationPortal.Models;
using InTandemRegistrationPortal.Data;
using Microsoft.AspNetCore.Identity;

namespace InTandemRegistrationPortal.Pages.Events
{
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<InTandemUser> _userManager;

        public EditModel(ApplicationDbContext context,
            UserManager<InTandemUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [BindProperty]
        public RideEvent RideEvent { get; set; }
        [BindProperty]
        public InputModel Input { get; set; }
        public RideLeaderAssignment RideLeaderAssignment { get; set; }
        public MultiSelectList Users => new MultiSelectList(_userManager.Users
            .ToDictionary(k => k.FullName, v => v.FullName), "Key", "Value");
        public class InputModel
        {
            public IList<string> SelectedUser { get; set; }
        }
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            RideEvent = await _context.RideEvent
                .AsNoTracking()
                .Include(r => r.RideLeaderAssignments)
                    .ThenInclude(r => r.InTandemUser)
                .FirstOrDefaultAsync(m => m.ID == id);
            IList<string> assignedLeaders = RideEvent.RideLeaderAssignments.Select(RideLeaderAssignment => RideLeaderAssignment.InTandemUser.FullName).ToList();

            Input = new InputModel
            {
                SelectedUser = assignedLeaders
            };
            if (RideEvent == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {

            if (!ModelState.IsValid)
            {
                return Page();
            }
            List<InTandemUser> SelectedInTandemUsers = new List<InTandemUser> { };
            foreach (var user in Input.SelectedUser)
            {
                SelectedInTandemUsers.Add(_context.Users
                .FirstOrDefault(u => u.FullName == user));
            }
                

            var RideEventToUpdate = await _context.RideEvent
                .AsNoTracking()
                .Include(r => r.RideLeaderAssignments)
                    .ThenInclude(r => r.InTandemUser)
                .FirstOrDefaultAsync(m => m.ID == id);
            
            //takes list of names of ride leaders selected and adds them to a list of ride leader assignments

            foreach (var leaderToBeAssigned in SelectedInTandemUsers)
            {
                // if the leader is not already assigned to the ride, assign them to the ride
                if (!RideEventToUpdate.RideLeaderAssignments.Any(u => u.InTandemUser.FullName.Equals(leaderToBeAssigned.FullName)))
                {
                    var NewLeaderAdded = new RideLeaderAssignment
                    {
                        InTandemUser = leaderToBeAssigned,
                        InTandemUserID = leaderToBeAssigned.Id
                    };
                    RideEventToUpdate.RideLeaderAssignments.Add(NewLeaderAdded);
                }
            }
            //default entity tracking does not include navigation properties

            if (RideEventToUpdate == null)
            {
                return NotFound();
            }
            // TruUpDateModelAsync is used to prevent overposting
            if (await TryUpdateModelAsync<RideEvent>(
                RideEventToUpdate,
                "RideEvent",
                i => i.EventName, i => i.EventDate, 
                i => i.EventType, i => i.Description, 
                i => i.Location, i => i.Distance, 
                i => i.RideLeaderAssignments, i => i.EventRatio,
                i => i.MaxSignup, i => i.Status,
                i => i.IsActive
                ))
            {
               
                _context.Update(RideEventToUpdate);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }

            return RedirectToPage("./Index");
        }
    }
}

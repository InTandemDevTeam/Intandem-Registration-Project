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
        public SelectList Users => new SelectList(_userManager.Users
            .ToDictionary(k => k.FullName, v => v.FullName), "Key", "Value");
        public class InputModel
        {
            public string SelectedUser { get; set; }
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

            // FirstOrDefault is NOT MEANT TO STAY IN PRODUCTION CODE
            // for multiple ride leaders, will iterate through list and populate listbox
            Input = new InputModel
            {
                SelectedUser = RideEvent.RideLeaderAssignments.FirstOrDefault().InTandemUser.FullName

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
            var SelectedInTandemUser = await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.FullName.Equals(Input.SelectedUser));
            var RideEventToUpdate = await _context.RideEvent
                .AsNoTracking()
                .Include(r => r.RideLeaderAssignments)
                    .ThenInclude(r => r.InTandemUser)
                .FirstOrDefaultAsync(m => m.ID == id);
            //RideEventToUpdate.RideLeaderAssignments.FirstOrDefault().InTandemUser = SelectedInTandemUser;
            //RideEventToUpdate.RideLeaderAssignments.FirstOrDefault().InTandemUser = SelectedInTandemUser;
            //RideEventToUpdate.RideLeaderAssignments.FirstOrDefault().InTandemUserID = SelectedInTandemUser.Id;
            foreach(var leader in RideEventToUpdate.RideLeaderAssignments)
            {

                if (!Input.SelectedUser.Equals(leader.InTandemUser.FullName))
                {
                    leader.InTandemUser = SelectedInTandemUser;
                    leader.InTandemUserID = SelectedInTandemUser.Id;
                }
            }
            
            //RideEventToUpdate.RideLeaderAssignments.FirstOrDefault(a => a.InTandemUserID.Equals(SelectedInTandemUser.Id))
            if (RideEventToUpdate == null)
            {
                return NotFound();
            }
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
                //if (String.IsNullOrWhiteSpace(
                //    instructorToUpdate.OfficeAssignment?.Location))
                //{
                //    instructorToUpdate.OfficeAssignment = null;
                //}
                //UpdateInstructorCourses(_context, selectedCourses, instructorToUpdate);
                _context.Update(RideEventToUpdate);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }
            //default entity tracking does not include navigation properties
            //_context.Attach(RideEvents).State = EntityState.Modified;

            //try
            //{
            //    await _context.SaveChangesAsync();
            //}
            //catch (DbUpdateConcurrencyException)
            //{
            //    if (!RideEventsExists(RideEvents.ID))
            //    {
            //        return NotFound();
            //    }
            //    else
            //    {
            //        throw;
            //    }
            //}

            return RedirectToPage("./Index");
        }

        private bool RideEventsExists(int id)
        {
            return _context.RideEvent.Any(e => e.ID == id);
        }
    }
}

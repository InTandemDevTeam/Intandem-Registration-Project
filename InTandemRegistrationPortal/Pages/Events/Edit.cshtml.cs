﻿using InTandemRegistrationPortal.Data;
using InTandemRegistrationPortal.Helpers;
using InTandemRegistrationPortal.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

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

        public List<SelectListItem> EventTypes { get; set; }
        public List<SelectListItem> Statuses { get; set; }
        public List<SelectListItem> MaxSignUpTypes { get; set; }
        public class InputModel
        {
            // add validation for fields below
            [Required(ErrorMessage ="Please select one or more ride leaders")]
            [Display(Name = "Leader(s)")]
            public List<string> SelectedUser { get; set; }
            [Required(ErrorMessage = "Please select a type of event")]
            [Display(Name = "Type of event")]
            public string SelectedEventType { get; set; }
            [Required(ErrorMessage = "Please select a status")]
            [Display(Name = "Status")]
            public string SelectedStatus { get; set; }
            [Required(ErrorMessage = "Please enter the limiting factor for max sign ups")]
            [Display(Name = "Factor limiting sign ups")]
            public string SelectedMaxSignUpType { get; set; }
        }
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            EventTypes = new List<SelectListItem>();
            Statuses = new List<SelectListItem>();
            MaxSignUpTypes = new List<SelectListItem>();
            foreach (EventType eventType in Enum.GetValues(typeof(EventType)))
            {


                EventTypes.Add(new SelectListItem
                {
                    Value = eventType.GetDescription(),
                    Text = eventType.GetDescription()
                });

            }
            foreach (Status status in Enum.GetValues(typeof(Status)))
            {
                if (!status.GetDescription().Equals("Cancelled"))
                {

                    Statuses.Add(new SelectListItem
                    {
                        Value = status.GetDescription(),
                        Text = status.GetDescription()
                    });
                }

            }
            foreach (MaxSignUpType type in Enum.GetValues(typeof(MaxSignUpType)))
            {
                MaxSignUpTypes.Add(new SelectListItem
                {
                    Value = type.GetDescription(),
                    Text = type.GetDescription()
                });
            }
            // get RideEvent as well as the leader assignments
            RideEvent = await _context.RideEvent
                .AsNoTracking()
                .Include(r => r.RideLeaderAssignments)
                    .ThenInclude(r => r.InTandemUser)
                .FirstOrDefaultAsync(m => m.ID == id);
            List<string> assignedLeaders = RideEvent.RideLeaderAssignments
                .Select(RideLeaderAssignment => RideLeaderAssignment.InTandemUser.FullName)
                .ToList();
            Input = new InputModel
            {
                SelectedUser = assignedLeaders,
                SelectedStatus = RideEvent.Status.GetDescription(),
                SelectedEventType = RideEvent.EventType.GetDescription(),
                SelectedMaxSignUpType = RideEvent.MaxSignUpType.GetDescription()
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
                

            RideEvent RideEventToUpdate = await _context.RideEvent
                .Include(r => r.RideLeaderAssignments)
                    .ThenInclude(r => r.InTandemUser)
                .FirstOrDefaultAsync(m => m.ID == id);
            

            //takes list of names of ride leaders selected and adds them to a list of ride leader assignments
            var assignedLeaders = RideEventToUpdate.RideLeaderAssignments
                .Select(u => u.InTandemUser)
                .ToList();
            foreach (var leaderToBeAssigned in SelectedInTandemUsers)
            {
                // if the leader is not already assigned to the ride, assign them to the ride
                if (!assignedLeaders.Any(u => u.FullName.Equals(leaderToBeAssigned.FullName)))
                {
                    var NewLeaderAdded = new RideLeaderAssignment
                    {
                        InTandemUser = leaderToBeAssigned,
                        InTandemUserID = leaderToBeAssigned.Id
                    };
                    RideEventToUpdate.RideLeaderAssignments.Add(NewLeaderAdded);
                }

            }
            // to indicate ride leader needs to be deleted, compare both lists
            // if the length of selected users is less than the number of assigned leaders
            // find which one is not in the selected users list and delete that assignment
            foreach (var assignedLeader in assignedLeaders)
            {
                // if a user is in the assignedUsers list but not in the selectedUsers list
                // remove the ride leader assignment containing that user
                if (!SelectedInTandemUsers.Any(u => u.FullName.Equals(assignedLeader.FullName)))
                {

                    RideLeaderAssignment RideLeaderAssignmentToBeRemoved = RideEventToUpdate.RideLeaderAssignments
                        .SingleOrDefault(u => u.InTandemUser.FullName.Equals(assignedLeader.FullName));
                    
                    RideEventToUpdate.RideLeaderAssignments.Remove(RideLeaderAssignmentToBeRemoved);
                }
            }


            RideEvent.Status = EnumExtensionMethods.GetValueFromDescription<Status>(Input.SelectedStatus);
            RideEvent.EventType = EnumExtensionMethods.GetValueFromDescription<EventType>(Input.SelectedEventType);
            RideEvent.MaxSignUpType = EnumExtensionMethods.GetValueFromDescription<MaxSignUpType>(Input.SelectedMaxSignUpType);
            // default entity tracking does not include navigation properties

            if (RideEventToUpdate == null)
            {
                return NotFound();
            }
            // TruUpdateModelAsync is used to prevent overposting
            if (await TryUpdateModelAsync<RideEvent>(
                RideEventToUpdate,
                "RideEvent",
                i => i.EventName, i => i.EventDate, 
                i => i.EventType, i => i.Description, 
                i => i.Location, i => i.Distance, 
                i => i.RideLeaderAssignments,
                i => i.MaxSignup, i => i.Status
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

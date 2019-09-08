using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using InTandemRegistrationPortal.Data;
using InTandemRegistrationPortal.Models;
using InTandemRegistrationPortal.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyIntandemBooking.Helpers;

namespace InTandemRegistrationPortal.Pages.Events
{
    public class EventWizard2Model : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<InTandemUser> _userManager;
        public EventWizard2Model(UserManager<InTandemUser> userManager,
            ApplicationDbContext context)

        {
            _context = context;
            _userManager = userManager;
        }
        [BindProperty]
        public EventWizard2 EventWizard2 { get; set; }
        [BindProperty]
        public InputModel Input { get; set; }
        // used to fetch list of selected users
        public class InputModel
        {
            [Required(ErrorMessage = "Please select a ride leader")]
            [Display(Name = "Select a ride leader")]
            public IList<string> SelectedUser { get; set; }
        }
        public MultiSelectList Users => new MultiSelectList(_userManager.Users
            .ToDictionary(k => k.FullName, v => v.FullName), "Key", "Value");
        public void OnGet()
        {
            var wizardEvent = HttpContext.Session.GetJson<RideEvent>("WizardEvent");
            if (wizardEvent == null)
            {
                HttpContext.Session.SetJson("RideEvent", new RideEvent());
            }
            else
            {
                EventWizard2 = new EventWizard2
                {

                    MaxSignup = wizardEvent.MaxSignup,
                    MaxSignUpType = wizardEvent.MaxSignUpType,
                    Status = wizardEvent.Status,
                };
            }
        } // OnGet
        public IActionResult OnPostPrevious()
        {
            return RedirectToPage("./EventWizard1");
        }
        public async Task<IActionResult> OnPostNextAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var wizardEvent = HttpContext.Session.GetJson<RideEvent>("WizardEvent");
            wizardEvent.MaxSignup = EventWizard2.MaxSignup;
            wizardEvent.MaxSignUpType = EventWizard2.MaxSignUpType;
            wizardEvent.Status = EventWizard2.Status;
            HttpContext.Session.SetJson("WizardEvent", wizardEvent);
            _context.RideEvent.Add(wizardEvent);
            foreach (var user in Input.SelectedUser)
            {
                // finds single selected user in list by full name
                // will change this so it searches by id instead
                var selectedUser = await _context.Users
                    .AsNoTracking()
                    .FirstOrDefaultAsync(m => m.FullName == user);
                RideLeaderAssignment RideLeaderAssignment = new RideLeaderAssignment
                {
                    InTandemUserID = selectedUser.Id,
                    RideEventID = wizardEvent.ID
                };
                _context.RideLeaderAssignment.Add(RideLeaderAssignment);
            }
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");

        }
    }
}
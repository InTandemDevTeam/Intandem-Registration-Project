using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using InTandemRegistrationPortal.Data;
using InTandemRegistrationPortal.Helpers;
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
            [Required(ErrorMessage = "Please select a status")]
            [Display(Name = "Select a status")]
            public string SelectedStatus { get; set; }
            [Display(Name = "Factor limiting sign ups")]
            [Required(ErrorMessage = "Please enter the limiting factor for max sign ups")]
            public string SelectedMaxSignUpType { get; set; }

        }
        public List<SelectListItem> Statuses { get; set; }
        public List<SelectListItem> MaxSignUpTypes { get; set; } 
        public MultiSelectList Users => new MultiSelectList(_userManager.Users
            .ToDictionary(k => k.FullName, v => v.FullName), "Key", "Value");

        public void OnGet()
        {
            Statuses = new List<SelectListItem>();
            MaxSignUpTypes = new List<SelectListItem>();
            foreach (Status status in Enum.GetValues(typeof(Status)))
            {
                Statuses.Add(new SelectListItem
                {
                    Value = status.GetDescription(),
                    Text = status.GetDescription()
                });
            }
            foreach (MaxSignUpType type in Enum.GetValues(typeof(MaxSignUpType)))
            {
                MaxSignUpTypes.Add(new SelectListItem
                {
                    Value = type.GetDescription(),
                    Text = type.GetDescription()
                });
            }
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
            wizardEvent.MaxSignUpType = EnumExtensionMethods.GetValueFromDescription<MaxSignUpType>(Input.SelectedMaxSignUpType);
            wizardEvent.Status = EnumExtensionMethods.GetValueFromDescription<Status>(Input.SelectedStatus);
            HttpContext.Session.SetJson("WizardEvent", null);
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
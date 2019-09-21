using InTandemRegistrationPortal.Helpers;
using InTandemRegistrationPortal.Models;
using InTandemRegistrationPortal.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyIntandemBooking.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace InTandemRegistrationPortal.Pages.Events
{
    public class EventWizard1Model : PageModel
    {
        [BindProperty]
        public EventWizard1 EventWizard1 { get; set; }
        public List<SelectListItem> EventTypes { get; set; }
        [BindProperty]
        public InputModel Input { get; set; }
        public class InputModel
        {
            [Required(ErrorMessage = "Please select the type of event")]
            [Display(Name = "Type of event")]
            public string SelectedEventType { get; set; }
        }
        public void OnGet()
        {
            EventTypes = new List<SelectListItem>();
            foreach (EventType eventType in Enum.GetValues(typeof(EventType)))
            {
                EventTypes.Add(new SelectListItem
                {
                    Value = eventType.GetDescription(),
                    Text = eventType.GetDescription()
                });
            }
            
            var wizardEvent = HttpContext.Session.GetJson<RideEvent>("WizardEvent");
            if (wizardEvent == null)
            {
                HttpContext.Session.SetJson("WizardEvent", new RideEvent());
            }
            else
            {
                EventWizard1 = new EventWizard1
                {
                    EventName = wizardEvent.EventName,
                    EventDate = wizardEvent.EventDate,
                    Description = wizardEvent.Description,
                    Location = wizardEvent.Location,
                    Distance = wizardEvent.Distance,
                    EventType = wizardEvent.EventType
                };
                Input.SelectedEventType = EventWizard1.EventType.GetDescription();
            }
        } // OnGet
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var wizardEvent = HttpContext.Session.GetJson<RideEvent>("WizardEvent");
            wizardEvent.EventName = EventWizard1.EventName;
            wizardEvent.EventDate = EventWizard1.EventDate;
            wizardEvent.Description = EventWizard1.Description;
            wizardEvent.Location = EventWizard1.Location;
            wizardEvent.Distance = EventWizard1.Distance;
            wizardEvent.EventType = EnumExtensionMethods.GetValueFromDescription<EventType>(Input.SelectedEventType);
            HttpContext.Session.SetJson("WizardEvent", wizardEvent);


            return RedirectToPage("./EventWizard2");
        } //OnPost
        public IActionResult OnPostClear()
        {
            HttpContext.Session.SetJson("WizardEvent", null);
            return RedirectToPage("./Index");
        }
    }
}
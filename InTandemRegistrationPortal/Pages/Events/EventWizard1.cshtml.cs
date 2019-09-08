using InTandemRegistrationPortal.Models;
using InTandemRegistrationPortal.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyIntandemBooking.Helpers;

namespace InTandemRegistrationPortal.Pages.Events
{
    public class EventWizard1Model : PageModel
    {
        [BindProperty]
        public EventWizard1 EventWizard1 { get; set; }
        public void OnGet()
        {
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
                    Distance = wizardEvent.Distance
                };
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
            HttpContext.Session.SetJson("WizardEvent", wizardEvent);


            return RedirectToPage("./EventWizard2");
        } //OnPost
    }
}
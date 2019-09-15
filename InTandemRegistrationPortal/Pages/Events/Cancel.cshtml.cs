using InTandemRegistrationPortal.Data;
using InTandemRegistrationPortal.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace InTandemRegistrationPortal.Pages.Events
{
    public class CancelModel : PageModel
    {
        protected readonly ApplicationDbContext _context;
        
        public CancelModel(
            ApplicationDbContext context)
        {
            _context = context;
        }
        [BindProperty]
        public InputModel Input { get; set; }
        public RideEvent RideEvent { get; set; }
        public RideRegistration RideRegistration { get; set; }
        public class InputModel
        {
            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "Reason for cancelling (required)")]
            public string ReasonForCancellation { get; set; }

        }
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            RideEvent = await _context.RideEvent.FirstOrDefaultAsync(m => m.ID == id);
            if (RideEvent == null)
            {
                return NotFound();
            }
            return Page();
        }
        public async Task<IActionResult> OnPostAsync(int? id)
        {
            RideEvent = await _context.RideEvent.FirstOrDefaultAsync(m => m.ID == id);
            if (RideEvent == null)
            {
                return NotFound();
            }
            RideEvent.ReasonForCancellation = Input.ReasonForCancellation;
            RideEvent.Status = Status.Cancelled;
            _context.Attach(RideEvent).State = EntityState.Modified;

            await _context.SaveChangesAsync();
            return RedirectToPage("./Index");
        }
    }
}
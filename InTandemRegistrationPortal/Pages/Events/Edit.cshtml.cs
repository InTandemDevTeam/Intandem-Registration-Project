using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Rides2.Models;

namespace Rides2.Pages.Events
{
    public class EditModel : PageModel
    {
        private readonly Rides2.Models.Rides2Context _context;

        public EditModel(Rides2.Models.Rides2Context context)
        {
            _context = context;
        }

        [BindProperty]
        public RideEvents RideEvents { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            RideEvents = await _context.RideEvents.FirstOrDefaultAsync(m => m.ID == id);

            if (RideEvents == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(RideEvents).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RideEventsExists(RideEvents.ID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool RideEventsExists(int id)
        {
            return _context.RideEvents.Any(e => e.ID == id);
        }
    }
}

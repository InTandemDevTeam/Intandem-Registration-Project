using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Rides2.Models;

namespace Rides2.Pages.Events
{
    public class DetailsModel : PageModel
    {
        private readonly Rides2.Models.Rides2Context _context;

        public DetailsModel(Rides2.Models.Rides2Context context)
        {
            _context = context;
        }

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
    }
}

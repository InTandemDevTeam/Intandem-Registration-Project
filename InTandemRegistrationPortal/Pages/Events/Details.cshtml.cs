using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using InTandemRegistrationPortal.Models;
using InTandemRegistrationPortal.Data;

namespace InTandemRegistrationPortal.Pages.Events
{
    public class DetailsModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DetailsModel(ApplicationDbContext context)
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

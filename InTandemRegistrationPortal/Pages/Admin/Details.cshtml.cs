using System.Collections.Generic;
using System.Threading.Tasks;
using InTandemRegistrationPortal.Data;
using InTandemRegistrationPortal.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;


namespace InTandemRegistrationPortal.Pages.Admin
{
    public class DetailsModel : PageModel
    {
        //instances variables
        private readonly ApplicationDbContext _context;
        public DetailsModel(
            ApplicationDbContext context)
        {
            _context = context;
        }
        [BindProperty]
        public InTandemUser User { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            User = await _context.Users.FirstOrDefaultAsync(m => m.Id == id.ToString());

            if (User == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
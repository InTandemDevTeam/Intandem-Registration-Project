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
        public InTandemUser CurrentUser { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if ((id == null) || (id.Equals("")))
            {
                return NotFound();
            }
            CurrentUser = await _context.Users.FirstOrDefaultAsync(m => m.Id == id);

            if (CurrentUser == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
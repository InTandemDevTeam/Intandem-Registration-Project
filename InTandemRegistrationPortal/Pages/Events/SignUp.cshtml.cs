using ExpressiveAnnotations.Attributes;
using InTandemRegistrationPortal.Data;
using InTandemRegistrationPortal.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace InTandemRegistrationPortal.Pages.Events
{

    public class SignUpModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<InTandemUser> _userManager;

        public SignUpModel(
            UserManager<InTandemUser> userManager,
            ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }
        [BindProperty]

        //public InputModel Input { get; set; }

        public InTandemUser InTandemUser { get; set; }

        public string FullName { get; set; }
        public async Task<IActionResult> OnGetAsync(string id)
        {
            if ((id == null) || (id.Equals("")))
            {
                return NotFound();
            }
            InTandemUser = await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == id);
            if (InTandemUser == null)
            {
                return NotFound();
            }
            //create full name to get on Details page
            FullName = InTandemUser.FirstName + " " + InTandemUser.LastName;
            return Page();
        }
    }
}
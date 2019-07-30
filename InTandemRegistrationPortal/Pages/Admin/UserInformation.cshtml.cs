using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InTandemRegistrationPortal.Data;
using InTandemRegistrationPortal.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace InTandemRegistrationPortal.Pages.Admin
{
    public class UserInformationModel : PageModel
    {
        //instances variables
        private readonly UserManager<InTandemUser> _userManager;
        private readonly ApplicationDbContext _context;

        public UserInformationModel(
            UserManager<InTandemUser> userManager,
            ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }
        [BindProperty]

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
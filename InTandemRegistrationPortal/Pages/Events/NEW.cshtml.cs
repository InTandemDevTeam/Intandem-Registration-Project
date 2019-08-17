using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using InTandemRegistrationPortal.Models;
using InTandemRegistrationPortal.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace InTandemRegistrationPortal.Pages.Events
{
    public class NewModel : PageModel
    {
        private readonly UserManager<InTandemUser> _userManager;
                    public string lblActiveText = "false";
        public bool bActiveState = false;
        public string sTemp = "";
        private readonly ApplicationDbContext _context;

        public NewModel(
            ApplicationDbContext context,
            UserManager<InTandemUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public RideEvent RideEvents { get; set; }
        public SelectList Users => new SelectList(_context.Users
            .AsNoTracking()
            .ToList());


        public async Task<IActionResult> OnPostAsync()
        {
            sTemp = Request.Form["RideEvents_bActive"];
//            ("hello there" + sTemp + " that was sTemp.");
//if (RideEvents_bActive)

            {

            }
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.RideEvent.Add(RideEvents);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
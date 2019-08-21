using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InTandemRegistrationPortal.Data;
using InTandemRegistrationPortal.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace InTandemRegistrationPortal.Pages.Users
{
    public class DeleteModel : PageModel
    {
        //instances variables
        private readonly ApplicationDbContext _context;
        public DeleteModel(
            ApplicationDbContext context)
        {
            _context = context;
        }
        [BindProperty]
        public InTandemUser CurrentUser { get; set; }

        public string ErrorMessage { get; set; }
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
        }public async Task<IActionResult> OnPostAsync(string id)
        {
            if ((id == null) || (id.Equals("")))
            {
                return NotFound();
            }
            var User = await _context.Users.FirstOrDefaultAsync(m => m.Id == id);

            if (User == null)
            {
                return NotFound();
            }
            try
            {
                _context.Users.Remove(User);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Users");
            }
            catch (DbUpdateException /* ex */)
            {
                //Log the error (uncomment ex variable name and write a log.)
                return RedirectToAction("./Delete",
                                     new { id, saveChangesError = true });
            }
        }
    }
}
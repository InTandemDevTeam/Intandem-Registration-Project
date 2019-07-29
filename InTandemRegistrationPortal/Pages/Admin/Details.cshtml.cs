using InTandemRegistrationPortal.Data;
using InTandemRegistrationPortal.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InTandemRegistrationPortal.Pages.Admin
{
    public class DetailsModel : PageModel
    {
        //instances variables
        private readonly UserManager<InTandemUser> _userManager;
        private readonly ApplicationDbContext _context;
        public DetailsModel(
            UserManager<InTandemUser> userManager,
            ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }
        [BindProperty]

        public InputModel Input { get; set; }
        public InTandemUser InTandemUser { get; set; }

        public class InputModel {
            [Required]
            [Display(Name ="Will you approve this ueer?")]
            public bool? HasBeenApproved { get; set; }
        }

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
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string id)
        {
            InTandemUser something = _userManager.FindByIdAsync((await _context.Users.FirstOrDefaultAsync(m => m.Id == id))?.Id).Result;
            if (!ModelState.IsValid)
            {
                return Page();
            }
            if (ModelState.IsValid)
            {
                something.HasBeenApproved = Input.HasBeenApproved;
                await _userManager.UpdateAsync(something);
                await _context.SaveChangesAsync();
            }

            //_context.Attach(InTandemUser).State = EntityState.Modified;

            //try
            //{

                
            //}
            //catch (DbUpdateConcurrencyException)
            //{
            //    if (!UserExists(InTandemUser.Id))
            //    {
            //        return NotFound();
            //    }
            //    else
            //    {
            //        throw;
            //    }
            //}

            return RedirectToPage("./Users");
        }

        private bool UserExists(string id)
        {
            return _context.Users.Any(m => m.Id == id);
        }
    }
}
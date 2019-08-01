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

        public string FullName { get; set; }

        public class InputModel {
            [Required]
            [Display(Name ="Will you approve this ueer?")]
            public bool? HasBeenApproved { get; set; }
        }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if ((id == null) || (id.Equals(""))
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

        public async Task<IActionResult> OnPostAsync(string id)
        {
            var test = InTandemUser;
            string UserId = (await _context.Users.FirstOrDefaultAsync(m => m.Id == id)).Id;
            InTandemUser user = _userManager.FindByIdAsync(UserId).Result;
            //InTandemUser user = _userManager.FindByIdAsync((await _context.Users.FirstOrDefaultAsync(m => m.Id == id))?.Id).Result;

            if (!ModelState.IsValid)
            {
                return Page();
            }
            // if the model state is valid and the user exists, continue
            if (ModelState.IsValid)
            {
                if (!UserExists(UserId))
                {
                    return NotFound();
                }
                else
                {
                    user.HasBeenApproved = Input.HasBeenApproved;
                    await _userManager.UpdateAsync(user);
                    await _context.SaveChangesAsync();
                }
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
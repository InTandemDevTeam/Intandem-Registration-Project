using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InTandemRegistrationPortal.Data;
using InTandemRegistrationPortal.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace InTandemRegistrationPortal.Pages.Admin
{
    public class UsersModel : PageModel
    {
        //instances variables
        private readonly ApplicationDbContext _context;
        public UsersModel(
            ApplicationDbContext context)
        {
            _context = context;
        }
        public IList<InTandemUser> Users { get; set; }

        public async Task OnGetAsync()
        {
            Users = await _context.Users
                .Where(r => r.Role != "Admin")
                .ToListAsync();
        }
    }
}
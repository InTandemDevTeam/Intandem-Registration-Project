using InTandemRegistrationPortal.Data;
using InTandemRegistrationPortal.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace InTandemRegistrationPortal.Pages.Admin
{
    public class UserPageModel : PageModel
    {
        public InTandemUser InTandemUser { get; set; }

        public void GetCurrentUser(ApplicationDbContext context, string id)
        {
            if ((id == null) || (id.Equals(""))
            {
                InTandemUser = null;
            }
            InTandemUser = context.Users
                .AsNoTracking()
                .FirstOrDefault(m => m.Id == id);
            
        }
    }
}

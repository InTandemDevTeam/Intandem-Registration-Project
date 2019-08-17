using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using InTandemRegistrationPortal.Authorization;
using InTandemRegistrationPortal.Services;
using InTandemRegistrationPortal.ViewModels;

namespace InTandemRegistrationPortal.Pages.Users
{
    [Authorize(Roles = Constants.AdministratorsRole)]
    public class IndexModel : PageModel
    {
        private readonly UserService _service;

        public IndexModel(UserService service)
        {
            _service = service;
        }

        public IList<UserViewModel> Users { get; set; }

        public async Task OnGetAsync()
        {
            Users = await _service.GetAllUsersAsync();
        }
    }
}
using InTandemRegistrationPortal.Services;
using InTandemRegistrationPortal.ViewModels;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InTandemRegistrationPortal.Pages.Users
{
    public class IndexModel : PageModel
    {
        //instances variables
        private readonly UserService _service;
        public IndexModel(
            UserService service)
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
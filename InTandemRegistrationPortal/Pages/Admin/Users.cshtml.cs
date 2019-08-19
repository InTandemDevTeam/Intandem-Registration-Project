using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InTandemRegistrationPortal.Data;
using InTandemRegistrationPortal.Models;
using InTandemRegistrationPortal.Services;
using InTandemRegistrationPortal.ViewModels;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace InTandemRegistrationPortal.Pages.Admin
{
    public class UsersModel : PageModel
    {
        //instances variables
        private readonly UserService _service;
        public UsersModel(
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
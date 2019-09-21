using InTandemRegistrationPortal.Services;
using InTandemRegistrationPortal.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
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
        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; }
        public SelectList Members { get; set; }
        [BindProperty(SupportsGet = true)]
        public string LeaderName { get; set; }
        public async Task OnGetAsync()
        {
            if (!string.IsNullOrEmpty(SearchString))
            {
                Users = await _service.SearchAsync(SearchString);
            } 
            else
            {
                Users = await _service.GetAllUsersAsync();
            }
            

        }
    }
}
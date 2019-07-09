using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace InTandemRegistrationPortal.Pages
{
    [Authorize (Roles = "Captain, Stoker")]
    public class PrivacyModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
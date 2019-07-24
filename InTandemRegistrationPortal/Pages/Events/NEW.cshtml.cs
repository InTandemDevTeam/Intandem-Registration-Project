using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Rides2.Models;

namespace Rides2.Pages.Events
{
    public class NewModel : PageModel
    {
        
                    public string lblActiveText = "false";
        public bool bActiveState = false;
        public string sTemp = "";
        private readonly Rides2.Models.Rides2Context _context;

        public NewModel(Rides2.Models.Rides2Context context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public RideEvents RideEvents { get; set; }
        
        public async Task<IActionResult> OnPostAsync()
        {
            sTemp = Request.Form["RideEvents_bActive"];
//            ("hello there" + sTemp + " that was sTemp.");
//if (RideEvents_bActive)

            {

            }
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.RideEvents.Add(RideEvents);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
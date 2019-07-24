using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Rides2.Models;

namespace Rides2.Pages.Events
{
    public class IndexModel : PageModel
    {
        private readonly Rides2.Models.Rides2Context _context;

        public IndexModel(Rides2.Models.Rides2Context context)
        {
            _context = context;
        }

        public IList<RideEvents> RideEvents { get;set; }

        public async Task OnGetAsync()
        {
            RideEvents = await _context.RideEvents.ToListAsync();
        }
    }
}

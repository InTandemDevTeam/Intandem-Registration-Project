using InTandemRegistrationPortal.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InTandemRegistrationPortal.ViewComponents
{
    public class UserInformationViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _context;
        
        public UserInformationViewComponent(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InVokeAsync(string id)
        {
            //search for and render user
        }
    }
}

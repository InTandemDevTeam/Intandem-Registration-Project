using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InTandemRegistrationPortal.Data;
using InTandemRegistrationPortal.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace InTandemRegistrationPortal.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class SearchUserController : ControllerBase
    {
        // instance variables

        private readonly UserManager<InTandemUser> _userManager;
        public SearchUserController(UserManager<InTandemUser> userManager)
        {
           
            _userManager = userManager;
        }
        [Produces("application/json")]
        [HttpGet("Search")]
        public async Task<IActionResult> Search()
        {
            try
            {
                string term = HttpContext.Request.Query["term"].ToString();
                var users = await (from u in _userManager.Users
                                      select u).ToListAsync();
                    
                if (!String.IsNullOrEmpty(term))
                {
                    users = users.Where(u => u.FullName.Contains(term)).ToList();
                }
/*                List<string> test = new List<string>(users.Count);
                foreach (InTandemUser u in users)
                {
                    test.Add(u.FullName);
                }*/
                return Ok(users);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
    }
}
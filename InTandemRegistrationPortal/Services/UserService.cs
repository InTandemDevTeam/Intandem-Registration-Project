using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using InTandemRegistrationPortal.Data;
using InTandemRegistrationPortal.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InTandemRegistrationPortal.ViewModels;

namespace InTandemRegistrationPortal.Services
{
    public class UserService
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<InTandemUser> _userManager;

        public UserService(ApplicationDbContext context,
            UserManager<InTandemUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        
        public async Task<IList<UserViewModel>> GetAllUsersAsync()
        {
            var users = from user in await _userManager.Users.ToListAsync()
                        select new UserViewModel
                        {
                            Name = user.FullName,
                            Id = user.Id,
                            Email = user.Email,
                            Roles = string.Join(',', _userManager.GetRolesAsync(user).Result),
                            HasBeenApproved = user.HasBeenApproved,
                        };

            return users.ToList();
        }
        public async Task<IList<UserViewModel>> SearchAsync(string SearchString)
        {
            var users = from user in await _userManager.Users.ToListAsync()
                        select new UserViewModel
                        {
                            Name = user.FullName,
                            Id = user.Id,
                            Email = user.Email,
                            Roles = string.Join(',', _userManager.GetRolesAsync(user).Result),
                            HasBeenApproved = user.HasBeenApproved,
                        };
            if (!string.IsNullOrEmpty(SearchString))
            {
                users = from user in users
                        where user.Name.Contains(SearchString)
                        select user;
            }
            return users.ToList();
        }
    }
}

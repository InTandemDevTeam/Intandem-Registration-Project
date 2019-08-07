using ExpressiveAnnotations.Attributes;
using InTandemRegistrationPortal.Authorization;
using InTandemRegistrationPortal.Data;
using InTandemRegistrationPortal.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace InTandemRegistrationPortal.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<InTandemUser> _signInManager;
        private readonly UserManager<InTandemUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _context;
        public RegisterModel(
            UserManager<InTandemUser> userManager,
            SignInManager<InTandemUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender,
            RoleManager<IdentityRole> roleManager,
            ApplicationDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            _roleManager = roleManager;
            _context = context;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public InTandemUser InTandemUser { get; set; }

        public SelectList Roles => new SelectList(_roleManager.Roles
                    .Where(x => x.Name != Constants.AdministratorsRole)
                    .ToDictionary(k => k.Name, v => v.Name), "Key", "Value");

        public DateTime DateRegistered { get; set; }
        public class InputModel
        {
            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "First name (required)")]
            public string FirstName { get; set; }

            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "Last name (required)")]
            public string LastName { get; set; }

            [Required]
            [Phone]
            [Display(Name = "Phone number (required)")]
            public string PhoneNumber { get; set; }

            [Required(ErrorMessage = "Please select what type of User you are")]
            [Display(Name = "What type of user are you? (required)")]
            public string Role { get; set; }

            [RequiredIf(@"Role == 'Captain' || Role == 'Stoker'", ErrorMessage = "Please answer whether you have completed training")]
            [Display(Name = "Have you already completed training? ")]
            public bool? HasBeenTrained { get; set; }

            [RequiredIf(@"Role == 'Captain' || Role == 'Stoker'", ErrorMessage = "Please enter your height")]
            [DataType(DataType.Text)]
            [Display(Name = "Height (required)")]
            public string Height { get; set; }

            [RequiredIf(@"Role == 'Captain' || Role == 'Stoker'", ErrorMessage = "Please enter your weight")]
            [DataType(DataType.Text)]
            [Display(Name = "Weight (required)")]
            public string Weight { get; set; }

            [RequiredIf("Role == 'Captain'", ErrorMessage = "Please answer whether you have your own seat")]
            [DataType(DataType.Text)]
            [Display(Name = "Do you have your own seat? (required)")]
            public string HasSeat { get; set; }

            [RequiredIf("Role == 'Captain'", ErrorMessage = "Please answer whether you have a tandem bike")]
            [DataType(DataType.Text)]
            [Display(Name = "Do you have your own tandem bike? (required)")]
            public string HasTandem { get; set; }

            [RequiredIf("Role == 'Captain'", ErrorMessage = "Please answer whether you have a single bike")]
            [DataType(DataType.Text)]
            [Display(Name = "Do you have your own single bike? (required)")]
            public string HasSingleBike { get; set; }

            
            [DataType(DataType.Text)]
            [Display(Name = "Do you have a guide dog? (required)")]
            public string Dog { get; set; }

            
            [DataType(DataType.Text)]
            [Display(Name = "Do you have any special equipment? (required)")]
            public string SpecialEquipment { get; set; }


            [Required]
            [EmailAddress]
            [Display(Name = "Email (required)")]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }
            
        }

        public void OnGet(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {

            if (ModelState.IsValid)
            {
                var user = new InTandemUser {
                    FirstName = Input.FirstName,
                    LastName = Input.LastName,
                    UserName = Input.Email,
                    Email = Input.Email,
                    DateRegistered = DateTime.Today,
                    Height = Input.Height,
                    Weight = Input.Weight,
                    HasSeat = Input.HasSeat,
                    Role = Input.Role,
                    HasBeenTrained = Input.HasBeenTrained,
                    HasTandem = Input.HasTandem,
                    HasSingleBike = Input.HasSingleBike,
                    Dog = Input.Dog,
                    SpecialEquipment = Input.SpecialEquipment,
                    HasBeenApproved = null,
                    PhoneNumber = Input.PhoneNumber
                };
                var result = await _userManager.CreateAsync(user, Input.Password);
                var admins = await _context.Users
                    .Where(r => r.Role == "Admin")
                    .ToListAsync(); 
                if (result.Succeeded)
                {
                    if (!await _roleManager.RoleExistsAsync(user.Role))
                    {
                        var users = new IdentityRole(user.Role);
                        var res = await _roleManager.CreateAsync(users);
                        if (res.Succeeded)
                        {
                            await _userManager.AddToRoleAsync(user, user.Role);
                        }
                    }
                    _logger.LogInformation("User created a new account with password.");

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { userId = user.Id, code = code },
                        protocol: Request.Scheme);
                    
                    // proof of concept code for sending two emails

                    string msgBody = "Thank you for creating an account with InTandem. \n";
                    string userid = (await _context.Users
                        .AsNoTracking()
                        .FirstOrDefaultAsync(u => u.Email == user.Email)).Id;
                    returnUrl = returnUrl ??
                        Url.Content($"~/Identity/Account/ResendConfirmationEmail/{userid}");

                    if (user.HasBeenApproved == null)
                    {
                        foreach (InTandemUser admin in admins)
                        {
                            await _emailSender.SendEmailAsync(admin.Email, "Admin email",
                            msgBody + $"Admin, please confirm this account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                        }
                    }
                    await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                        msgBody + $"In order to access the web portal, please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");
                    //await _signInManager.SignInAsync(user, isPersistent: false);
                    return LocalRedirect(returnUrl);
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}

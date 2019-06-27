﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using InTandemRegistrationPortal.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace InTandemRegistrationPortal.Areas.Identity.Pages.Account.Manage
{
    public partial class IndexModel : PageModel
    {
        private readonly UserManager<InTandemUser> _userManager;
        private readonly SignInManager<InTandemUser> _signInManager;
        private readonly IEmailSender _emailSender;

        public IndexModel(
            UserManager<InTandemUser> userManager,
            SignInManager<InTandemUser> signInManager,
            IEmailSender emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
        }

        public string Username { get; set; }

        public bool IsEmailConfirmed { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "First name")]
            public string FirstName { get; set; }

            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "Last name")]
            public string LastName { get; set; }


            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "Height")]
            public string Height { get; set; }

            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "Weight")]
            public string Weight { get; set; }

            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "Has Seat>")]
            public string HasSeat { get; set; }

            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "Has Tandem?")]
            public string HasTandem { get; set; }

            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "Has Single Bike?")]
            public string HasSingleBike { get; set; }

            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "Dog")]
            public string Dog { get; set; }


            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "Special Equipment")]
            public string SpecialEquipment { get; set; }

            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "Rider Level")]
            public string RiderLevel { get; set; }

            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "Has Been Trained?")]
            public string HasBeenTrained { get; set; }

            [Required]
            [Display(Name = "Date of Birth")]
            [DataType(DataType.Date)]
            public DateTime DateOfBirth { get; set; }

            [Required]
            [EmailAddress]
            public string Email { get; set; }

            [Phone]
            [Display(Name = "Phone number")]
            public string PhoneNumber { get; set; }
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var userName = await _userManager.GetUserNameAsync(user);
            var email = await _userManager.GetEmailAsync(user);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);

            Username = userName;

            Input = new InputModel
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                DateOfBirth = user.DateOfBirth,
                Height = user.Height,
                Weight = user.Weight,
                HasSeat = user.HasSeat,
                HasTandem = user.HasTandem,
                HasSingleBike = user.HasSingleBike,
                Dog = user.Dog,
                SpecialEquipment = user.SpecialEquipment,
                RiderLevel = user.RiderLevel,
                HasBeenTrained = user.HasBeenTrained,
                Email = email,
                PhoneNumber = phoneNumber

            };

            IsEmailConfirmed = await _userManager.IsEmailConfirmedAsync(user);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var email = await _userManager.GetEmailAsync(user);
            if (Input.Email != email)
            {
                var setEmailResult = await _userManager.SetEmailAsync(user, Input.Email);
                if (!setEmailResult.Succeeded)
                {
                    var userId = await _userManager.GetUserIdAsync(user);
                    throw new InvalidOperationException($"Unexpected error occurred setting email for user with ID '{userId}'.");
                }
            }
            if (Input.FirstName != user.FirstName)
            {
                user.FirstName = Input.FirstName;
            }

            if (Input.LastName != user.LastName)
            {
                user.LastName = Input.LastName;
            }


            if (Input.DateOfBirth != user.DateOfBirth)
            {
                user.DateOfBirth = Input.DateOfBirth;
            }

            if (Input.Height != user.Height)
            {
                user.Height = Input.Height;
            }

            if (Input.Weight != user.Weight)
            {
                user.Weight = Input.Weight;
            }

            if (Input.HasSeat != user.HasSeat)
            {
                user.HasSeat = Input.HasSeat;
            }

            if (Input.HasTandem != user.HasTandem)
            {
                user.HasTandem = Input.HasTandem;
            }

            if (Input.HasSingleBike != user.HasSingleBike)
            {
                user.HasSingleBike = Input.HasSingleBike;
            }

            if (Input.Dog != user.Dog)
            {
                user.Dog = Input.Dog;
            }

            if (Input.SpecialEquipment != user.SpecialEquipment)
            {
                user.SpecialEquipment = Input.SpecialEquipment;
            }

            if (Input.RiderLevel != user.RiderLevel)
            {
                user.RiderLevel = Input.RiderLevel;
            }

            if (Input.HasBeenTrained != user.HasBeenTrained)
            {
                user.HasBeenTrained = Input.HasBeenTrained;
            }

            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            if (Input.PhoneNumber != phoneNumber)
            {
                var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);
                if (!setPhoneResult.Succeeded)
                {
                    var userId = await _userManager.GetUserIdAsync(user);
                    throw new InvalidOperationException($"Unexpected error occurred setting phone number for user with ID '{userId}'.");
                }
            }

            await _userManager.UpdateAsync(user);

            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Your profile has been updated";
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostSendVerificationEmailAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }


            var userId = await _userManager.GetUserIdAsync(user);
            var email = await _userManager.GetEmailAsync(user);
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var callbackUrl = Url.Page(
                "/Account/ConfirmEmail",
                pageHandler: null,
                values: new { userId = userId, code = code },
                protocol: Request.Scheme);
            await _emailSender.SendEmailAsync(
                email,
                "Confirm your email",
                $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

            StatusMessage = "Verification email sent. Please check your email.";
            return RedirectToPage();
        }
    }
}

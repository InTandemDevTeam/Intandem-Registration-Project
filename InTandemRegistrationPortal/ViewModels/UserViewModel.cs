using System;
using System.ComponentModel.DataAnnotations;

namespace InTandemRegistrationPortal.ViewModels
{
    public class UserViewModel
    {
        public string Name { get; set; }
        public string Id { get; set; }
        public string Email { get; set; }

        [Display(Name = "Role(s)")]
        public string Roles { get; set; }

        public bool? HasBeenApproved { get; set; }
    }
}

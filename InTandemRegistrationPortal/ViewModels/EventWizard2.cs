using System.ComponentModel.DataAnnotations;

namespace InTandemRegistrationPortal.ViewModels
{
    public class EventWizard2
    {
        // event ratio to be for phase 2
        [Required(ErrorMessage = "Please enter the number of maximum sign ups")]
        [Display(Name = "Maximum Sign Ups")]
        public int? MaxSignup { get; set; }
        // limited by bikes, stoker, or people
        [Display(Name = "Factor limiting sign ups")]
        [Required(ErrorMessage = "Please enter the limiting factor for max sign ups")]
        public int? MaxSignUpType { get; set; }
        // cancelled, incomplete, pending, passed
        // admin has not decided this is not ready
        // upcoming (ready), passed, incomplete, cancelled
        // confirmed
        // auto mark as passed the day after
        // for calander and registration pages, users should not be offered rides that are incomplete or cancelled
        // cannot register for incomplete or cancelled rides
        // calendar should show rides that are cancelled
        [Required]
        public int Status { get; set; }


    }
}

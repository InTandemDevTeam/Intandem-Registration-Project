using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

// model to be added onto later
namespace InTandemRegistrationPortal.Models
{
    public class InTandemUser : IdentityUser
    {
        [PersonalData]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [PersonalData]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [PersonalData]
        [Display(Name = "Role")]
        public string Role { get; set; }

        [PersonalData]
        [Display(Name = "Date Registered")]
        public string DateRegistered { get; set; }  // TODO: Why is this a string?


        // stoker/captain properties
        
        [PersonalData]
        [Display(Name = "Height")]
        public string Height { get; set; }

        [PersonalData]
        [Display(Name = "Weight")]
        public string Weight { get; set; }

        [PersonalData]
        [Display(Name = "Do you have your own seat?")]
        public bool? HasSeat { get; set; }

        [PersonalData]
        [Display(Name = "Have you been trained?")]
        public bool? HasBeenTrained { get; set; }

        [PersonalData]
        [Display(Name = "Total miles traveled: ")]

        public int TotalMilesTraveled { get; set; } 


        // Captain-specific information
        [PersonalData]
        [Display(Name = "Did you come here through New York Cares?")]
        public bool? FromNYCares { get; set; }

        [PersonalData]
        [Display(Name = "Do you have your own tandem bike?")]
        public bool? HasTandem { get; set; }

        [PersonalData]
        [Display(Name = "Do you have your own single bike?")]
        public bool? HasSingleBike { get; set; }

        // in years
        [PersonalData]
        [Display(Name = "How many years experience do you have biking?")]
        public int? RiderLevel { get; set; }

        // Stoker-specific informaiton
        [PersonalData]
        [Display(Name = "Do you have a guide dog?")]

        public bool? Dog { get; set; }
        [PersonalData]
        [Display(Name = "Do you have any special equipment?")]

        public string SpecialEquipment { get; set; }

        [Display(Name = "Approval status")]
        public bool? HasBeenApproved { get; set; }

        [Display(Name = "Full Name")]
        public string FullName
        {
            get
            {
                return FirstName + " " + LastName;
            }
        }

        public ICollection<RideRegistration> RideRegistrations { get; set; }
    }
}
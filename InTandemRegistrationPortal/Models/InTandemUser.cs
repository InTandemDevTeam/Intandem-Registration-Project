using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

// model to be added onto later
namespace InTandemRegistrationPortal.Models
{
    public class InTandemUser : IdentityUser
    {
        [PersonalData]
        public string FirstName { get; set; }
        [PersonalData]
        public string LastName { get; set; }
        [PersonalData]
        public DateTime DateOfBirth { get; set; }
        [PersonalData]
        public string Role { get; set; }
        [PersonalData]
        public DateTime DateRegistered { get; set; }
        // stoker/captain properties
        
        [PersonalData]
        public string Height { get; set; }
        [PersonalData]
        public string Weight { get; set; }
        [PersonalData]
        public bool? HasSeat { get; set; }
        [PersonalData]
        public bool? HasBeenTrained { get; set; }

        [PersonalData]
        public int TotalMilesTraveled { get; set; } 
        // Captain-specific information
        [PersonalData]
        public bool? FromNYCares { get; set; }
        [PersonalData]
        public bool? HasTandem { get; set; }
        [PersonalData]
        public bool? HasSingleBike { get; set; }
        // Stoker-specific informaiton
        [PersonalData]
        public bool? Dog { get; set; }
        [PersonalData]
        public string SpecialEquipment { get; set; }
        [PersonalData]
        public string RiderLevel { get; set; }
        public bool? HasBeenApproved { get; set; }

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
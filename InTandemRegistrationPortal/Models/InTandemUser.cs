using Microsoft.AspNetCore.Identity;
using System;

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
        // Captain-specific informaiton
        //change to bool object instead of string
        [PersonalData]
        public string HasSeat { get; set; }
        //change to bool object instead of string
        [PersonalData]
        public string HasTandem { get; set; }
        //change to bool object instead of string
        [PersonalData]
        public string HasSingleBike { get; set; }
        // Stoker-specific informaiton
        [PersonalData]
        public string Dog { get; set; }
        [PersonalData]
        public string SpecialEquipment { get; set; }
        [PersonalData]
        public string RiderLevel { get; set; }
        [PersonalData]
        //change to bool object instead of string
        public string HasBeenTrained { get; set; }

        public bool? HasBeenApproved { get; set; }
    }
}
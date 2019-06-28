using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// model to be added onto later
namespace InTandemRegistrationPortal.Models
{
    public enum TypeOfUser
    {
        Admin, Captain, Stoker, Volunteer
    }
    public class InTandemUser : IdentityUser
    {
        [PersonalData]
        public string FirstName { get; set; }
        [PersonalData]
        public string LastName { get; set; }
        [PersonalData]
        public DateTime DateOfBirth { get; set; }
        public TypeOfUser TypeOfUser { get; set; }

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
    }
}
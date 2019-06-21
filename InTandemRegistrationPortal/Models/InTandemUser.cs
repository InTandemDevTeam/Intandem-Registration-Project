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
        Volunteer, Stoker, Captain
    }
    public class InTandemUser : IdentityUser
    {
        [PersonalData]
        public string LastName { get; set; }
        [PersonalData]
        public string FirstName { get; set; }
        [PersonalData]
        public DateTime DateOfBirth { get; set; }
        [PersonalData]
        public TypeOfUser TypeOfUser { get; set; }
    }
}
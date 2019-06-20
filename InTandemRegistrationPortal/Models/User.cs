using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InTandemRegistrationPortal.Models
{
    public enum TypeOfUser
    {
        Stoker, Captain, Volunteer
    }
    public class User
    {
        public int ID { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string EMailAdress { get; set; }
        public string Password { get; set; }
        public DateTime DateOfBirth { get; set; }
        public TypeOfUser TypeOfUser { get; set; }
    }
}

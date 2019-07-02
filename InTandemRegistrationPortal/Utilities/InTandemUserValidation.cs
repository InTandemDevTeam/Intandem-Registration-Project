using InTandemRegistrationPortal.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InTandemRegistrationPortal.Utilities
{
    public class InTandemUserValidation : ValidationAttribute
    {
        //validation should return error message if field is left blank
        //based on user type

        private readonly string allowedUser;

        public InTandemUserValidation(string allowedUser)
        {
            this.allowedUser = allowedUser;
        }

        //input that is entered
        //usertype will be passed to this class
        //if usertype does not match what that field requires
        //not required, otherwise required fields determined 
        //by user type
        public override bool IsValid(object value)
        {
            //reimplement required attribute chedck
            if (value == null || value.ToString().Equals(""))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}

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

        private readonly string User;

        public InTandemUserValidation(string User)
        {
            this.User = User;
        }

        //input that is entered
        //usertype will be passed to this class
        //if usertype does not match what that field requires
        //not required, otherwise required fields determined 
        //by user type
        public override bool IsValid(object value)
        {
            return base.IsValid(value);
        }
    }
}

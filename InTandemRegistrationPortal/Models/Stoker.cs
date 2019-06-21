using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InTandemRegistrationPortal.Models
{
    public class Stoker : InTandemUser
    {
        // shared data between captain and stoker
        [PersonalData]
        public string Height { get; set; }
        [PersonalData]
        public string Weight { get; set; }
        // Stoker-specific informaiton
        [PersonalData]
        public string Dog { get; set; }
        [PersonalData]
        public string SpecialEquipment { get; set; }
        [PersonalData]
        public bool HasSeat { get; set; }
        [PersonalData]
        public string RiderLevel { get; set; }
        [PersonalData]
        public bool HasBeenTrained { get; set; }
    }
}

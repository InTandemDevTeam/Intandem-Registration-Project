using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InTandemRegistrationPortal.Models
{
    public class Captain : InTandemUser
    {
        // shared data between captain and stoker
        [PersonalData]
        public string Height { get; set; }
        [PersonalData]
        public string Weight { get; set; }
        // Captain-specific informaiton
        [PersonalData]
        public bool HasSeat { get; set; }
        [PersonalData]
        public bool HasTandem { get; set; }
        [PersonalData]
        public bool HasSingleBike { get; set; }
    }
}


using System.ComponentModel.DataAnnotations.Schema;

namespace InTandemRegistrationPortal.Models
{
    public class RideRegistration
    {
        public int ID { get; set; }

        public int RideEventID { get; set; }

        [ForeignKey("MyInTandemUser")]
        public string InTandemUserID { get; set; }

        public bool RiderShowUp { get; set; }

        public int Miles { get; set; }

        public RideEvent RideEvent { get; set; }

        public InTandemUser InTandemUser { get; set; }
    }
}

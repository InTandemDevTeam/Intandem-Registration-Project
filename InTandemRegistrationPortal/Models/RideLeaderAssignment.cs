using System.ComponentModel.DataAnnotations.Schema;

namespace InTandemRegistrationPortal.Models
{
    public class RideLeaderAssignment
    {        
        public int ID { get; set; }

        [ForeignKey("MyInTandemUser")]
        public string InTandemUserID { get; set; }

        public int RideEventID { get; set; }

        public InTandemUser InTandemUser { get; set; }

        public RideEvent RideEvent { get; set; }
    }
}

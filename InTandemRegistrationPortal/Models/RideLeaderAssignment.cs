
namespace InTandemRegistrationPortal.Models
{
    public class RideLeaderAssignment
    {
        
        public int ID { get; set; }
        public string InTandemUserId { get; set; }

        public int RideEventsID { get; set; }

        public InTandemUser InTandemUser { get; set; }

        public RideEvents RideEvents { get; set; }
    }
}

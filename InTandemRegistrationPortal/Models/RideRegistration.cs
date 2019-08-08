
namespace InTandemRegistrationPortal.Models
{
    public class RideRegistration
    {
        public int ID { get; set; }
        public int RideEventsID { get; set; }
        public string InTandemUserId { get; set; }
        public bool RiderShowUp { get; set; }
        public int Miles { get; set; }
        public string ReasonForCancellation { get; set; }
        public RideEvents RideEvents { get; set; }
        public InTandemUser InTandemUser { get; set; }

    }
}

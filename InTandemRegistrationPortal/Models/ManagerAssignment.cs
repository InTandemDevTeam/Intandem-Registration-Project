
namespace InTandemRegistrationPortal.Models
{
    public class ManagerAssignment
    {
        public int ID { get; set; }
        public string UserID { get; set; }

        public int EventID { get; set; }

        public InTandemUser User { get; set; }

        public RideEvents Event { get; set; }
    }
}

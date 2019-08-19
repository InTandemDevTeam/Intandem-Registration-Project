
using System.ComponentModel.DataAnnotations.Schema;

namespace InTandemRegistrationPortal.Models
{
    public class RideRegistration
    {
        //foreign key attribute is not needed
        //primary key
        public int ID { get; set; }
       
        //foreign key, app knows this based on <class name>ID

        public int RideEventID { get; set; }
        
        //foreign key, app knows this based on <class name>ID

        public string InTandemUserID { get; set; }

        public bool RiderShowUp { get; set; }

        public int Miles { get; set; }

        public RideEvent RideEvent { get; set; }

        public InTandemUser InTandemUser { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InTandemRegistrationPortal.Models
{
    public enum Status
    {
        [Description("Upcoming")]
        Upcoming,
        [Description("Incomplete")]
        Incomplete,
        [Description("Passed")]
        Passed,
        [Description("Cancelled")]
        Cancelled
    }
    public enum EventType
    {
        [Description("Park Ride")]
        ParkRide,
        [Description("Excursion")]
        Excursion,
        [Description("Non-ride Event")]
        NonRideEvent
    }
    public enum MaxSignUpType
    {
        [Description("Bikes")]
        Bikes,
        [Description("Stokers")]
        Stokers,
        [Description("People")]
        People
    }

    public class RideEvent
    {
        //primary key
        public int ID { get; set; }

        [Column(TypeName = "varchar(300)")]
        [Required]
        [Display(Name = "Name")]
        public string EventName { get; set; }

        [Display(Name = "Event Date")]
        [DataType(DataType.Date)]
        public DateTime EventDate { get; set; }

        [Column(TypeName = "varchar(500)")]
        [Required(AllowEmptyStrings = true)]
        [Display(Name = "Description")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Description { get; set; }

        [Column(TypeName = "varchar(200)")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Required(AllowEmptyStrings = true)]
        public string Location { get; set; }

        [Column(TypeName = "decimal(18, 1)")]
        public decimal? Distance { get; set; }

        [Display(Name = "Type of event")]
        public EventType? EventType { get; set; }
        //public int EventType { get; set; }

        //[Column(TypeName = "varchar(150)")]
        //[Required(AllowEmptyStrings = true)]
        //[Display(Name = "Ride Leader(s)")]
        //[DisplayFormat(ConvertEmptyStringToNull = false)]
        //navigation property
        public ICollection<RideLeaderAssignment> RideLeaderAssignments { get; set; }


        // event ratio to be for phase 2
        [Display(Name = "Maximum number of sign ups")]
        public int? MaxSignup { get; set; }
        // limited by bikes, stoker, or people

        public MaxSignUpType? MaxSignUpType { get; set; }
        // cancelled, incomplete, pending, passed
        // admin has not decided this is not ready
        // upcoming (ready), passed, incomplete, cancelled
        // confirmed
        // auto mark as passed the day after
        // for calander and registration pages, users should not be offered rides that are incomplete or cancelled
        // cannot register for incomplete or cancelled rides
        // calendar should show rides that are cancelled

        public Status? Status { get; set; }
        //public int Status { get; set; }
        // ride cancellation field
        public string ReasonForCancellation { get; set; }
        // navigation property
        // below is needed to create cross table
        public ICollection<RideRegistration> RideRegistrations { get; set; }
    } // class RideEvent

} // namespace Models

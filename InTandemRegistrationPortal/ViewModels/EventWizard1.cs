using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InTandemRegistrationPortal.ViewModels
{
    public class EventWizard1
    {
        //primary key
        public int ID { get; set; }

        [Column(TypeName = "varchar(300)")]
        [Required(ErrorMessage = "Please enter a name for this event")]
        [Display(Name = "Name")]
        public string EventName { get; set; }

        [Required(ErrorMessage = "Please enter a date")]
        [Display(Name = "Event Date")]
        [DataType(DataType.Date)]
        public DateTime EventDate { get; set; }

        [Column(TypeName = "varchar(500)")]
        [Required(AllowEmptyStrings = true, ErrorMessage = "Please enter a description")]
        [Display(Name = "Description")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Description { get; set; }

        [Column(TypeName = "varchar(200)")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Required(AllowEmptyStrings = true, ErrorMessage = "Please enter a location")]
        public string Location { get; set; }

        
        [Column(TypeName = "decimal(18, 1)")]
        public decimal? Distance { get; set; }

        [Required(ErrorMessage = "Please select the type of event")]
        [Display(Name = "Type of event")]
        public int EventType { get; set; }

    }
}

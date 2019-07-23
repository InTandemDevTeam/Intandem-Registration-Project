using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Rides2.Models
{
    public class RideEvents
    {
        public int ID { get; set; }
        [Column(TypeName = "varchar(300)")]
        [Required]
        public string EventName { get; set; }
        [Display(Name = "Event Date")]
        [DataType(DataType.Date)]
        public DateTime EventSDate { get; set; }

        [Column(TypeName = "varchar(500)")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Required(AllowEmptyStrings = true)]
        public string Description { get; set; }

        [Column(TypeName = "varchar(200)")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Required(AllowEmptyStrings = true)]
        public string Location { get; set; }

        [Column(TypeName = "decimal(18, 1)")]
        public decimal? Distance { get; set; }

        public int EventType { get; set; }

        [Column(TypeName = "varchar(150)")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Required(AllowEmptyStrings = true)]
        public string RideLeader { get; set; }

        [Column(TypeName = "varchar(50)")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Required(AllowEmptyStrings = true)]
        public string EventRatio { get; set; }

        public int? MaxSignup { get; set; }
        public int? MaxSignUpType { get; set; }
        public int Status { get; set; }
        [Required]
        public bool? bActive { get; set; }

    } // class RideEvent
    public class AspNetRoles
    {
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Required]
        public string Id { get; set; }

        [Column(TypeName = "varchar(256)")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Required(AllowEmptyStrings = true)]
        public string Name { get; set; }

        [Column(TypeName = "varchar(256)")]
        [DisplayFormat(ConvertEmptyStringToNull = true)]
        [Required(AllowEmptyStrings = true)]
        public string NormalizedName { get; set; }

        [Column(TypeName = "varchar(256)")]
        [DisplayFormat(ConvertEmptyStringToNull = true)]
        [Required(AllowEmptyStrings = true)]
        public string ConcurrencyStamp { get; set; }

    } // class AspNetRoles
    public class AspNetUsers
    {
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Required]
        public string Id { get; set; }

        [Column(TypeName = "varchar(256)")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Required(AllowEmptyStrings = true)]
        public string UserName { get; set; }

        [Column(TypeName = "varchar(256)")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Required(AllowEmptyStrings = true)]
        public string NormalizedUserName { get; set; }

        [Column(TypeName = "varchar(256)")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Required(AllowEmptyStrings = true)]
        public string Email { get; set; }

        [Required]
        public bool? EmailConfirmed { get; set; }

        [Column(TypeName = "varchar(256)")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Required(AllowEmptyStrings = true)]
        public string PasswordHash { get; set; }

        [Column(TypeName = "varchar(200)")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Required(AllowEmptyStrings = true)]
                public string SecurityStamp  { get; set;}

        [Column(TypeName = "varchar(256)")]
        [DisplayFormat(ConvertEmptyStringToNull = true)]
        [Required(AllowEmptyStrings = true)]
        public string ConcurrencyStamp { get; set; }

        [Column(TypeName = "varchar(256)")]
        [DisplayFormat(ConvertEmptyStringToNull = true)]
        [Required(AllowEmptyStrings = true)]
        public string PhoneNumber  { get; set; }

        public bool? PhoneNumberConfirmed  { get; set; }

        public bool?  TwoFactorEnabled { get; set; }

        public DateTimeOffset  LockoutEnd { get; set; }

        [Required]
        public bool?  LockoutEnabled { get; set; }

        [Required]
        public int AccessFailedCount { get; set; }


    } // class AspNetUsers


    
} // namespace Models

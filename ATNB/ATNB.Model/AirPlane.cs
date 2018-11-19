using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ATNB.Model
{
    public class AirPlane : Entity<string>
    {
        [Required]
        [StringLength(40)]
        public string Model { get; set; }

        [Required]
        [StringLength(3)]
        public string AirPlaneType { get; set; }
        
        [RegularExpression(@"^\s*(?=.*[1-9])\d*(?:\.\d{1,2})?\s*$",
            ErrorMessage = "CruiseSpeed not negative")]
        public double? CruiseSpeed { get; set; }

        [RegularExpression(@"^\s*(?=.*[1-9])\d*(?:\.\d{1,2})?\s*$",
            ErrorMessage = "EmptyWeight not negative")]
        public double? EmptyWeight { get; set; }

        [RegularExpression(@"^\s*(?=.*[1-9])\d*(?:\.\d{1,2})?\s*$",
            ErrorMessage = "MaxTakeoffWeight not negative")]
        public double? MaxTakeoffWeight { get; set; }

        [RegularExpression(@"^\s*(?=.*[1-9])\d*(?:\.\d{1,2})?\s*$",
            ErrorMessage = "MinNeededRunwaySize not negative")]
        public double? MinNeededRunwaySize { get; set; }

        public string FlyMethod = "fixed wing";

        [Display(Name="AirPort")]
        public string AirPortId { get; set; }

        [ForeignKey("AirPortId")]
        public virtual AirPort AirPort { get; set; }
    }
}

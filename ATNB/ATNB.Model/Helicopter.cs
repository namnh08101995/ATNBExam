using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ATNB.Model
{
    public class Helicopter : Entity<string>
    {
        [Required]
        [StringLength(40)]
        public string Model { get; set; }

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
            ErrorMessage = "Range not negative")]
        public double? Range { get; set; }

        public string FlyMethod = "rotated wing";

        [Display(Name = "AirPort")]
        public string AirPortId { get; set; }

        [ForeignKey("AirPortId")]
        public virtual AirPort AirPort { get; set; }
    }
}

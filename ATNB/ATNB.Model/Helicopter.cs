using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ATNB.Model
{
    public class Helicopter : Entity<string>
    {
        [Required]
        [StringLength(40)]
        public string Model { get; set; }

        public double? CruiseSpeed { get; set; }

        public double? EmptyWeight { get; set; }

        public double? MaxTakeoffWeight { get; set; }

        public double? Range { get; set; }

        public string FlyMethod = "rotated wing";

        [Display(Name = "AirPort")]
        public string AirPortId { get; set; }

        [ForeignKey("AirPortId")]
        public virtual AirPort AirPort { get; set; }
    }
}

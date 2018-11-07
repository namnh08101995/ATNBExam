using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ATNB.Model
{
    public class AirPort : Entity<string>
    {
        [Required]
        [StringLength(255)]
        [Display(Name = "AirPort Name")]
        public string Name { get; set; }

        [Required]
        public double? RunwaySize { get; set; }

        [Required]
        public int? MaxFWParkingPlace { get; set; }

        [Required]
        public int? MaxRWParkingPlace { get; set; }

        public virtual IEnumerable<AirPlane> AirPlanes { get; set; }

        public virtual IEnumerable<Helicopter> Helicopters { get; set; }
    }
}

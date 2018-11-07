﻿using System.Collections.Generic;
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
        [Display(Name = "Runway Size")]
        public double? RunwaySize { get; set; }

        [Required]
        [Display(Name = "Max FW Parking Place")]
        public int? MaxFWParkingPlace { get; set; }

        [Required]
        [Display(Name = "Max RW Parking Place")]
        public int? MaxRWParkingPlace { get; set; }

        public virtual IEnumerable<AirPlane> AirPlanes { get; set; }

        public virtual IEnumerable<Helicopter> Helicopters { get; set; }
    }
}

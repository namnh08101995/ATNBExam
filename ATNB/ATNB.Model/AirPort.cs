using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATNB.Model
{
    public class AirPort : Entity<string>
    {
        [Required]
        [StringLength(255)]
        [Display(Name = "AirPort Name")]
        public string Name { get; set; }

        public double? RunwaySize { get; set; }

        public int? MaxFWParkingPlace { get; set; }

        public int? MaxRWParkingPlace { get; set; }

        public virtual IEnumerable<AirPlane> AirPlanes { get; set; }

        public virtual IEnumerable<Helicopter> Helicopters { get; set; }
    }
}

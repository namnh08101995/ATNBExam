using ATNB.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ATNB.Web.Models
{
    public class AirPortIndexData
    {
        public IEnumerable<AirPort> AirPorts { get; set; }
        public IEnumerable<AirPlane> AirPlanes { get; set; }
        public IEnumerable<Helicopter> Helicopters { get; set; }
    }
}
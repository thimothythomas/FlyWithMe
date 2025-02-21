using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlyWithMe.Model
{
    public class Departure
    {
        public int DepartureId { get; set; }
        public int AirportId { get; set; }
        public int FlightId { get; set; }
        public DateTime DepDate { get; set; }
        public TimeSpan DepTime { get; set; }
    }
}

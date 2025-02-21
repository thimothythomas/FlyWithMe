using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlyWithMe.Model
{
    public class Arrival
    {
        public int ArrivalId { get; set; }
        public int AirportId { get; set; }
        public int FlightId { get; set; }
        public DateTime ArrDate { get; set; }
        public TimeSpan ArrTime { get; set; }
    }
}

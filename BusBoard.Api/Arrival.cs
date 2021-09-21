using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusBoard.Api
{
    public class Arrival
    {
        public string vehicleId { get; set; }
        public string direction { get; set; }
        public string destinationName { get; set; }
        public int timeToStation { get; set; }
        public string towards { get; set; }

        public override string ToString()
        { 
            int mins = timeToStation / 60;
            int seconds = timeToStation % 60;
            string secondString = seconds.ToString().Length > 1 ? seconds.ToString() : "0" + seconds;
            return vehicleId + " : " + direction + " : " + destinationName + " : " + mins + ":" + secondString + " : " + towards;
        }
    }
}

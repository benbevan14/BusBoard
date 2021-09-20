using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusBoard.ConsoleApp
{
    public class Location
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Postcode { get; set; }
        public int Radius { get; set; }
        
        public Location(double lat, double lon, string post, int radius)
        {
            Latitude = lat;
            Longitude = lon;
            Postcode = post;
            Radius = radius;
        }
    }
}

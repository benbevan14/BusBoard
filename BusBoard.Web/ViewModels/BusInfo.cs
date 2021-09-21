using BusBoard.Api;
using System.Collections.Generic;

namespace BusBoard.Web.ViewModels
{
    public class BusInfo
    {
        public string PostCode { get; set; }
        public string NaptanID { get; set; }
        public string StopName { get; set; }
        public double Distance { get; set; }
        public List<Arrival> Arrivals { get; set; }

        public BusInfo(string postCode, string naptanId, string stopName, double distance, List<Arrival> arrivals)
        {
            PostCode = postCode;
            NaptanID = naptanId;
            StopName = stopName;
            Distance = distance;
            Arrivals = arrivals;
        }
    }
}
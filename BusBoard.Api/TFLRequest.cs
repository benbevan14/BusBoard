using System.Collections.Generic;
using System.Linq;
using RestSharp;

namespace BusBoard.Api
{
    public class TFLRequest
    {
        public RestClient Client { get; set; }
        public string AppId { get; set; }
        public string AppKey { get; set; }

        public TFLRequest()
        {
            Client = new RestClient("https://api.tfl.gov.uk");
            AppId = "f1b7f9a2b331460dad132b97645ca624";
            AppKey = "b47323ef3b2145e78eda7cf2321e28f9";
        }

        public List<BusStop> QueryTFLBusStop(MarkEmbling.PostcodesIO.Results.PostcodeResult postcodeResult)
        {
            // Get the stop points nearby within the radius. 
            var request = new RestRequest("StopPoint/?lat=" + postcodeResult.Latitude.ToString()
                                        + "&lon=" + postcodeResult.Longitude.ToString()
                                        + "&stopTypes=NaptanPublicBusCoachTram" + "&radius=" + 500, Method.GET);
            

            // Add credentials for the TFL api 
            request.AddHeader("app_id", AppId);
            request.AddHeader("app_key", AppKey);

            // Collect a response for the request and turn it into bus stop list
            BusStopResponse response = Client.Execute<BusStopResponse>(request).Data;
            List<BusStop> stops = response.stopPoints;

            // Return the bus stop list
            return stops;
        }

        public List<Arrival> QueryTFLArrival(BusStop stop)
        {
            // Query TFL for the arrivals at the specified bus stop
            var request = new RestRequest("StopPoint/" + stop.naptanId + "/Arrivals", Method.GET);

            // Add credentials for the TFL api 
            request.AddHeader("app_id", AppId);
            request.AddHeader("app_key", AppKey);

            List<Arrival> arrivals = Client.Execute<List<Arrival>>(request).Data.OrderBy(arrival => arrival.timeToStation).ToList();

            return arrivals;
        }
    }
}

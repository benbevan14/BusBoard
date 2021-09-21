using System;
using System.Collections.Generic;
using System.Net;
using MarkEmbling.PostcodesIO;
using RestSharp;
using System.Linq;

namespace BusBoard.ConsoleApp
{
    public class Program
    {
        static void Main(string[] args)
        {
            // Security Protocol
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            // Read in postcode input
            Console.WriteLine("Enter a Postcode");
            var postcodeInput = Console.ReadLine().Replace(" ", "");

            // Query the postcode api
            var postcodeResult = QueryPostcode(postcodeInput);

            // Query the TFL api to get a list of bus stops near postcode
            List<BusStop> stops = QueryTFLBusStop(postcodeResult);

            // Loop through the nearest 5 stops
            for (int i = 0; i < 5; i++)
            {
                // Query the TFL api to get a list of arrivals at each bus stop
                Console.WriteLine(stops[i].ToString());
                List<Arrival> arrivals = QueryTFLArrival(stops[i]);
                if (arrivals.Count == 0)
                {
                    Console.WriteLine("No buses due at this stop");
                }
                foreach (Arrival arrival in arrivals)
                {
                    Console.WriteLine(arrival.ToString());
                }
                Console.WriteLine('\n');
            }

            Console.WriteLine("done");

            Console.ReadLine();
        }

        public static MarkEmbling.PostcodesIO.Results.PostcodeResult QueryPostcode(string input)
        {
            // Create a client to access postcodes API
            var postcodeClient = new PostcodesIOClient();

            return postcodeClient.Lookup(input);
        }

        public static List<BusStop> QueryTFLBusStop(MarkEmbling.PostcodesIO.Results.PostcodeResult postcodeResult)
        {
            // Create a client to access the TFL api
            var client = new RestClient("https://api.tfl.gov.uk");

            // Get the stop points nearby within the radius. 
            var request = new RestRequest("StopPoint/?lat=" + postcodeResult.Latitude.ToString()
                                        + "&lon=" + postcodeResult.Longitude.ToString()
                                        + "&stopTypes=NaptanPublicBusCoachTram" + "&radius=" + 500, Method.GET);

            // Add credentials for the TFL api 
            request.AddHeader("app_id", "f1b7f9a2b331460dad132b97645ca624");
            request.AddHeader("app_key", "b47323ef3b2145e78eda7cf2321e28f9");

            // Collect a response for the request and turn it into bus stop list
            StopPoint stopPoint = client.Execute<StopPoint>(request).Data;
            List<BusStop> stops = stopPoint.stopPoints;

            // Return the bus stop list
            return stops;
        }

        public static List<Arrival> QueryTFLArrival(BusStop stop)
        {
            // Create a client to access the TFL api
            var client = new RestClient("https://api.tfl.gov.uk");

            // Query TFL for the arrivals at the specified bus stop
            var request = new RestRequest("StopPoint/" + stop.naptanId + "/Arrivals", Method.GET);

            List<Arrival> arrivals = client.Execute<List<Arrival>>(request).Data.OrderBy(arrival => arrival.timeToStation).ToList();

            return arrivals;
        }
    }
}

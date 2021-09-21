using System;
using System.Collections.Generic;
using System.Net;

namespace BusBoard.ConsoleApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Security Protocol
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            // Create a PostcodeRequest object
            PostcodeRequest postReq = new PostcodeRequest();

            // Create TFLRequest object
            TFLRequest tflReq = new TFLRequest();

            // Read in postcode input
            Console.WriteLine("Enter a Postcode");
            var postcodeInput = Console.ReadLine().Replace(" ", "");

            // Query the postcode api
            var postcodeResult = postReq.QueryPostcode(postcodeInput);

            // Query the TFL api to get a list of bus stops near postcode
            List<BusStop> stops = tflReq.QueryTFLBusStop(postcodeResult);

            // Loop through the nearest 5 stops
            for (int i = 0; i < 5; i++)
            {
                // Query the TFL api to get a list of arrivals at each bus stop
                Console.WriteLine(stops[i].ToString());
                List<Arrival> arrivals = tflReq.QueryTFLArrival(stops[i]);
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
    }
}

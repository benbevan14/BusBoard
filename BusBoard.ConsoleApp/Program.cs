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
        //string cheltenham = "490005082S";
        // 910GCHLTNHM
        // 1600GL3959
        // E0011703

        static void Main(string[] args)
        {
            // Security Protocol
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            Console.WriteLine("Enter a Postcode");
            var postcodeInput = Console.ReadLine().Replace(" ", "");



            // Creating a client to access the api for the site TFL (Transport for London)
            var client = new RestClient("https://api.tfl.gov.uk");
            // Creating a client to access postcodes API
            var postcodeClient = new PostcodesIOClient();

            var postcodeResult = postcodeClient.Lookup(postcodeInput);

            Console.WriteLine(postcodeResult.Latitude);
            Console.WriteLine(postcodeResult.Longitude);

            // Get the stop points nearby within the radius. 
            var request = new RestRequest("StopPoint/?lat=" +postcodeResult.Latitude.ToString() 
                                        + "&lon=" + postcodeResult.Longitude.ToString()
                                        + "&stopTypes=NaptanPublicBusCoachTram" + "&radius=" + 500, Method.GET);

            // Adding credentials for the API tfl 
            request.AddHeader("app_id", "f1b7f9a2b331460dad132b97645ca624");
            request.AddHeader("app_key", "b47323ef3b2145e78eda7cf2321e28f9");

            // This collecting a response for the request and turning it into bus stop object 
            StopPoint stopPoint = client.Execute<StopPoint>(request).Data;
            List<BusStop> stops = stopPoint.stopPoints;

            // Displays the stop data in radius
            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine(stops[i].ToString());
            }
           

            request = new RestRequest("StopPoint/" + stops[0].naptanId + "/Arrivals", Method.GET);


            var response = client.Execute<List<Arrival>>(request).Data.OrderBy(arrival => arrival.timeToStation);

            

            foreach (var arrival in response)
            {
                Console.WriteLine(arrival.ToString());
            }

            

            Console.WriteLine("done");

            Console.ReadLine();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Net;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using RestSharp;

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
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            Console.WriteLine("start the ting");

            var client = new RestClient("https://api.tfl.gov.uk");

            Location hop = new Location(51.49984, -0.124663, "SW1A0AA", 500);

            var request = new RestRequest("StopPoint/?lat=" + hop.Latitude.ToString() 
                                        + "&lon=" + hop.Longitude.ToString()
                                        + "&stopTypes=NaptanPublicBusCoachTram" + "&radius=" + hop.Radius.ToString(), Method.GET);

            request.AddHeader("app_id", "f1b7f9a2b331460dad132b97645ca624");
            request.AddHeader("app_key", "b47323ef3b2145e78eda7cf2321e28f9");

            StopPoint response = client.Execute<StopPoint>(request).Data;

            foreach (BusStop b in response.stopPoints)
            {
                Console.WriteLine(b.ToString());
            }

            //foreach (BusStop b in response)
            //{
            //    Console.WriteLine(b.ToString());
            //}

            //dynamic content = JsonConvert.DeserializeObject(response.Content);

            //var stops = content["stopPoints"];

            //List<BusStop> busStops = new List<BusStop>();

            //foreach (var entry in stops)
            //{
            //    string data = entry.ToString().Replace(@"""", "");
            //    string naptanId = Regex.Match(data, @"naptanId: [^\n,]+").Value.Substring(10);
            //    string common = Regex.Match(data, @"commonName: [^\n,.]+").Value.Substring(12);
            //    string distance = Regex.Match(data, @"distance: [^\n,]+").Value.Substring(10);
            //    busStops.Add(new BusStop(naptanId, common, double.Parse(distance)));
            //}

            //foreach (var bus in busStops)
            //{
            //    Console.WriteLine(bus.ToString());
            //}

            //Console.WriteLine("\n\n\n\n\n");

            //request = new RestRequest("StopPoint/" + busStops[2].naptanId + "/Arrivals");

            //response = client.Get(request);

            //Console.WriteLine(response.Content);

            Console.WriteLine("done");

            Console.ReadLine();
        }
    }
}

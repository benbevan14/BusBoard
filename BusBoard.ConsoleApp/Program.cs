using System;
using System.Net;
using RestSharp;

namespace BusBoard.ConsoleApp
{
    class Program
    {
        string cheltenham = "490005082S";
        string cheltenham2 = "90001419";
        string corndel = "490008660N";
        static void Main(string[] args)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            Console.WriteLine("Hello world");

            var client = new RestClient("https://api.tfl.gov.uk/StopPoint");

            var request = new RestRequest("490005082S/Arrivals", DataFormat.Json);

            var response = client.Get(request);

            foreach (var s in response.Content.Split('{', '}'))
            { 
                foreach (var entry in s.Split(','))
                {
                    Console.WriteLine(entry);
                }
                
            }
            //Console.WriteLine(response.Content.Spl);

            Console.WriteLine("done");

            Console.ReadLine();
        }
    }
}

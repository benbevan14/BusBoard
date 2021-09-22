using System.Collections.Generic;
using System.Web.Mvc;
using BusBoard.Api;
using BusBoard.Web.Models;
using BusBoard.Web.ViewModels;

namespace BusBoard.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult BusInfo(PostcodeSelection selection)
        {
            // Add some properties to the BusInfo view model with the data you want to render on the page.
            // Write code here to populate the view model with info from the APIs.
            // Then modify the view (in Views/Home/BusInfo.cshtml) to render upcoming buses.

            // Get the latitude and longitude from the postcode api
            PostcodeRequest postReq = new PostcodeRequest();
            var postcodeResult = postReq.QueryPostcode(selection.Postcode);

            if (postcodeResult == null)
            {
                // Do a thing to reroute to the error page
            }

            // Get the bus stops within radius of the postcode from the tfl api
            TFLRequest tflReq = new TFLRequest();
            List<BusStop> stops = tflReq.QueryTFLBusStop(postcodeResult);

            List<BusInfo> info = new List<BusInfo>();

            for (int i = 0; i < 5; i++)
            {
                List<Arrival> arrivals = tflReq.QueryTFLArrival(stops[i]);
                info.Add(new ViewModels.BusInfo(selection.Postcode, stops[i].naptanId, stops[i].commonName, stops[i].distance, arrivals));
            }

            //var info = new BusInfo(selection.Postcode, first.naptanId, first.commonName, first.distance);
            return View(info);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Information about this site";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Contact us!";

            return View();
        }
    }
}
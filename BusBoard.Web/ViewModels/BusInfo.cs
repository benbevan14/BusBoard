namespace BusBoard.Web.ViewModels
{
    public class BusInfo
    {
        public string PostCode { get; set; }
        public string NaptanID { get; set; }
        public string StopName { get; set; }
        public double Distance { get; set; }

        public BusInfo(string postCode, string naptanId, string stopName, double distance)
        {
            PostCode = postCode;
            NaptanID = naptanId;
            StopName = stopName;
            Distance = distance;
        }
    }
}
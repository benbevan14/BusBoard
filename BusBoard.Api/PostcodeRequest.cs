using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarkEmbling.PostcodesIO;

namespace BusBoard.Api
{
    public class PostcodeRequest
    {
        public PostcodesIOClient Client { get; set; }

        public PostcodeRequest()
        {
            Client = new PostcodesIOClient();
        }

        public MarkEmbling.PostcodesIO.Results.PostcodeResult QueryPostcode(string input)
        {
            return Client.Lookup(input);
        }
    }
}

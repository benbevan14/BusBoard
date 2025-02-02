﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusBoard.Api
{
    public class BusStop
    {
        public string naptanId { get; set; }
        public string commonName { get; set; }
        public double distance { get; set; }

        public override string ToString()
        {
            return naptanId + " : " + commonName + " : " + (int) distance + "m";
        }
    }
}

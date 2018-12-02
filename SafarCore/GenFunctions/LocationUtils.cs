using System;
using System.Collections.Generic;
using System.Text;

namespace SafarCore.GenFunctions
{
    public class GeoPoint
    {
        public float Lat { get; set; }
        public float Lng { get; set; }

        public GeoPoint(float lat, float lng)
        {
            Lat = lat;
            Lng = lng;
        }

    }
}

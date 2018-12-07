using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SafarObjects.TripClasses
{
    public class Location
    {
        [BsonId]
        public ObjectId id { get; set; }
        public ObjectId UserId { get; set; }
        public ObjectId TripId { get; set; }
        public DateTime LocationTime { get; set; }
        public GeoPoint GeoPoint { get; set; }

        public Location()
        {
            
        }

    }

    public class GeoPoint
    {
        public double Lat { get; set; }
        public double Lng { get; set; }

        public GeoPoint()
        {
            
        }

        public GeoPoint(double lat, double lng)
        {
            Lat = lat;
            Lng = lng;
        }
    }
}

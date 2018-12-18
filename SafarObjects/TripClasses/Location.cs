using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SafarObjects.TripClasses
{
    public class Location
    {
        public string UserId { get; set; }
        public string TripId { get; set; }
        public DateTime LocationTime { get; set; }
        public GeoPoint GeoPoint { get; set; }

        public Location()
        {
            
        }

        public LocationInDb GetLocationInDb()
        {
            return new LocationInDb()
            {
                UserId = this.UserId,
                TripId = this.TripId,
                GeoPoint = this.GeoPoint,
                LocationTime = this.LocationTime
            };
        }

    }

    public class LocationInDb : Location
    {
        [BsonId]
        public ObjectId _id { get; set; }
        
        public LocationInDb()
        {

        }

        public Location GetLocation()
        {
            return new Location()
            {
                UserId = this.UserId,
                TripId = this.TripId,
                GeoPoint = this.GeoPoint,
                LocationTime = this.LocationTime
            };
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

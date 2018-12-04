using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using SafarCore.DbClasses;
using SafarCore.GenFunctions;

namespace SafarCore.TripClasses
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

        public Location(ObjectId userId, ObjectId tripId, GeoPoint geoPoint)
        {
            id = ObjectId.GenerateNewId();
            UserId = userId;
            TripId = tripId;
            LocationTime = DateTime.Now;
            GeoPoint = geoPoint;
        }

        public static async Task<FuncResult> AddUpdateLocation(Location location)
        {
            return await DbConnection.FastAddorUpdate(location, CollectionNames.Locations, new List<string>() {"id"});
        }

        public static Location GetLastLocation(ObjectId userId)
        {
            var dbConnection = new DbConnection();
            dbConnection.Connect();

            var filter = new List<FieldFilter>()
            {
                new FieldFilter("UserId", userId, FieldType.ObjectId, CompareType.Equal)
            };
            var sort = new SortFilter("LocationTime", SortType.Descending);
            var list = dbConnection.GetFilteredList<Location>(CollectionNames.Locations, filter, sort, 1);
            return list.Count>0 ? list[0] : null;
        }

        public static List<Location> GetLastUserLocationInTrip(ObjectId tripId, ObjectId userId, int count)
        {
            var dbConnection = new DbConnection();
            dbConnection.Connect();

            var filter = new List<FieldFilter>()
            {
                new FieldFilter("UserId", userId, FieldType.ObjectId, CompareType.Equal),
                new FieldFilter("TripId", tripId, FieldType.ObjectId, CompareType.Equal)
            };

            var sort = new SortFilter("LocationTime", SortType.Descending);
            var list = dbConnection.GetFilteredList<Location>(CollectionNames.Locations, filter, sort, count);
            return list;
        }

        public static List<Location> GetAllUsersLocationInTrip(ObjectId tripId, int count)
        {
            var dbConnection = new DbConnection();
            dbConnection.Connect();

            var filter = new List<FieldFilter>()
            {
                new FieldFilter("TripId", tripId, FieldType.ObjectId, CompareType.Equal)
            };

            var sort = new SortFilter("LocationTime", SortType.Descending);
            var list = dbConnection.GetFilteredList<Location>(CollectionNames.Locations, filter, sort, count);
            return list;
        }

    }
}

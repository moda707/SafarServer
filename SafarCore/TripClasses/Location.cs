using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using SafarCore.DbClasses;
using SafarCore.GenFunctions;
using SafarObjects.TripClasses;

namespace SafarCore.TripClasses
{
    public class LocationFunc : Location
    {
        //public static FuncResult AddUpdateLocation(Location location)
        //{
        //    return DbConnection.FastAddorUpdate(location.ToBsonDocument(), CollectionNames.Locations, new List<string>() {"id"});
        //}

        //public static async Task<Location> GetLastLocation(string userId)
        //{
        //    var dbConnection = new DbConnection();
            

        //    var filter = new List<FieldFilter>()
        //    {
        //        new FieldFilter("UserId", userId, FieldType.String, CompareType.Equal)
        //    };
        //    var sort = new SortFilter("LocationTime", SortType.Descending);
        //    var list = await dbConnection.GetFilteredListAsync<Location>(CollectionNames.Locations, filter, sort, 1);
        //    return list.Count>0 ? list[0] : null;
        //}

        //public static Task<List<Location>> GetLastUserLocationInTrip(string tripId, string userId, int count)
        //{
        //    var dbConnection = new DbConnection();
            

        //    var filter = new List<FieldFilter>()
        //    {
        //        new FieldFilter("UserId", userId, FieldType.String, CompareType.Equal),
        //        new FieldFilter("TripId", tripId, FieldType.String, CompareType.Equal)
        //    };

        //    var sort = new SortFilter("LocationTime", SortType.Descending);
        //    var list = dbConnection.GetFilteredListAsync<Location>(CollectionNames.Locations, filter, sort, count);
        //    return list;
        //}

        //public static Task<List<Location>> GetAllUsersLocationInTrip(string tripId, int count)
        //{
        //    var dbConnection = new DbConnection();
            

        //    var filter = new List<FieldFilter>()
        //    {
        //        new FieldFilter("TripId", tripId, FieldType.String, CompareType.Equal)
        //    };

        //    var sort = new SortFilter("LocationTime", SortType.Descending);
        //    var list = dbConnection.GetFilteredListAsync<Location>(CollectionNames.Locations, filter, sort, count);
        //    return list;
        //}

    }
}

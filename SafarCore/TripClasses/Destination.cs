using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using SafarCore.DbClasses;
using SafarCore.GenFunctions;
using SafarObjects.TripClasses;

namespace SafarCore.TripClasses
{
    public class DestinationFunc : Destination
    {
        
        #region Add Update Delete Functions

        public static Task<FuncResult> AddUpdateDestination(Destination destination)
        {
            return DbConnection.FastAddorUpdate(destination, CollectionNames.Destinations,
                new List<string>() {"DestinationId"});
        }

        public static Task<FuncResult> DeleteDestination(string destinationId)
        {
            var filter = new List<FieldFilter>()
            {
                new FieldFilter("DestinationId", destinationId, FieldType.String, CompareType.Equal)
            };

            var res = DbConnection.DeleteMany(filter, CollectionNames.Destinations);
            return res;
        }

        #endregion

        #region Get Destinations

        public static Task<List<Destination>> GetDestinationsByTripId(string tripId)
        {
            var dbConnection = new DbConnection();
            
            var filter = new List<FieldFilter>()
            {
                new FieldFilter("TripId", tripId, FieldType.String, CompareType.Equal)
            };
            var r = dbConnection.GetFilteredListAsync<Destination>(
                CollectionNames.Destinations, filter);

            return r;
        }

        public static Task<List<GeoPoint>> GetRouteByTrip(string tripId)
        {
            return new Task<List<GeoPoint>>(() => new List<GeoPoint>());
        }

        public static Task<List<Destination>> GetRecommendations(string tripId)
        {
            return new Task<List<Destination>>(() => new List<Destination>());
        }

        #endregion


    }
}

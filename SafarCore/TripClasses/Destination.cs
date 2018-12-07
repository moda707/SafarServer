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

        public static async Task<FuncResult> AddUpdateDestination(Destination destination)
        {
            return await DbConnection.FastAddorUpdate(destination, CollectionNames.Destinations,
                new List<string>() {"DestinationId"});
        }

        #endregion

        #region Get Destinations

        public static async Task<List<SafarObjects.TripClasses.Destination>> GetDestinationsByTripId(string tripId)
        {
            var otripId = ObjectId.Parse(tripId);

            var dbConnection = new DbConnection();
            dbConnection.Connect();
            var filter = new List<FieldFilter>()
            {
                new FieldFilter("TripId", otripId, FieldType.ObjectId, CompareType.Equal)
            };
            var r = await dbConnection.GetFilteredListAsync<SafarObjects.TripClasses.Destination>(
                CollectionNames.Destinations, filter);

            return r;
        }


        #endregion


    }
}

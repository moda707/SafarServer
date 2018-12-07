using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using SafarCore.DbClasses;
using SafarCore.GenFunctions;

namespace SafarCore.TripClasses
{
    public class Destination
    {
        [BsonId]
        public ObjectId DestinationId { get; set; }
        public string Title { get; set; }
        public GeoPoint Location { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public Destination()
        {
            
        }

        #region Add Update Delete Functions

        public static async Task<FuncResult> AddUpdateDestination(Destination destination)
        {
            return await DbConnection.FastAddorUpdate(destination, CollectionNames.Destinations,
                new List<string>() {"DestinationId"});
        }

        #endregion

        #region Get Destinations
        public static async Task<List<Destination>> GetDestinationsByTripId(string tripId)
        {
            var otripId = ObjectId.Parse(tripId);

            var dbConnection = new DbConnection();
            dbConnection.Connect();
            var filter = new List<FieldFilter>()
            {
                new FieldFilter("TripId", otripId, FieldType.ObjectId, CompareType.Equal)
            };
            var r = await dbConnection.GetFilteredListAsync<Destination>(CollectionNames.Destinations, filter);
            
            return r;
        }


        #endregion


    }
}

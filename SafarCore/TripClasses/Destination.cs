using System;
using System.Collections.Generic;
using System.Text;
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

        public static FuncResult AddUpdateDestination(Destination destination)
        {
            return DbConnection.FastAddorUpdate(destination, CollectionNames.Destinations,
                new List<string>() {"DestinationId"});
        }

        #endregion

        #region Get Destinations
        public static List<Destination> GetDestinationsByTripId(ObjectId tripId)
        {
            return new List<Destination>();
        }


        #endregion


    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using SafarCore.DbClasses;

namespace SafarCore.TripClasses
{
    public class Fellow
    {
        public ObjectId TripId { get; set; }
        public ObjectId UserId { get; set; }
        public FellowType FellowType { get; set; }
        public FellowStatus FellowStatus { get; set; }

        public Fellow()
        {
            
        }
        #region Converter

        public static Fellow ConvertFellowInDbtoFellow(FellowInDb fellow)
        {
            return new Fellow()
            {
                TripId = fellow.TripId,
                UserId = fellow.UserId,
                FellowType = (FellowType)fellow.FellowType,
                FellowStatus = (FellowStatus)fellow.FellowStatus
            };
        }

        #endregion

        #region Get Fellows

        public static List<Fellow> GetFellowsByTripId(ObjectId tripId)
        {
            return new List<Fellow>();
        }

        public static List<ObjectId> GetAllTripIdsByUser(ObjectId ouserId)
        {
            var dbConnection = new DbConnection();
            dbConnection.ConnectOpenReg();

            var filter = new List<FieldFilter>()
            {
                new FieldFilter("UserId", ouserId, FieldType.ObjectId, CompareType.Equal)
            };
            var l = dbConnection.GetFilteredList<FellowInDb>(CollectionNames.Fellows, filter);

            return l.Select(x=> x.TripId).ToList();
        }

        #endregion

    }

    public class FellowInDb
    {
        [BsonId]
        public ObjectId id { get; set; }
        public ObjectId TripId { get; set; }
        public ObjectId UserId { get; set; }
        public int FellowType { get; set; }
        public int FellowStatus { get; set; }

        public FellowInDb()
        {
            
        }
    }

    public enum FellowType
    {
        Type1 = 0,
        Type2 = 1
    }

    public enum FellowStatus
    {
        Status1 = 0,
        Status2 = 1
    }
}

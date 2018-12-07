using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using SafarCore.DbClasses;
using SafarObjects.TripClasses;

namespace SafarCore.TripClasses
{
    public class FellowFunc : Fellow
    {
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

        public static List<SafarObjects.TripClasses.Fellow> GetFellowsByTripId(ObjectId tripId)
        {
            return new List<SafarObjects.TripClasses.Fellow>();
        }

        public static List<ObjectId> GetAllTripIdsByUser(ObjectId ouserId)
        {
            var dbConnection = new DbConnection();
            dbConnection.Connect();

            var filter = new List<FieldFilter>()
            {
                new FieldFilter("UserId", ouserId, FieldType.ObjectId, CompareType.Equal)
            };
            var l = dbConnection.GetFilteredList<FellowInDb>(CollectionNames.Fellows, filter);

            return l.Select(x=> x.TripId).ToList();
        }

        #endregion

    }
}

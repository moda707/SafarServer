using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SafarCore.DbClasses;
using SafarObjects.TripClasses;

namespace SafarCore.TripClasses
{
    public class FellowFunc : Fellow
    {
        
        #region Get Fellows

        public static Task<List<Fellow>> GetFellowsByTripId(string tripId)
        {
            //var dbConnection = new DbConnection();

            //var filter = new List<FieldFilter>()
            //{
            //    new FieldFilter("TripId", tripId, FieldType.String, CompareType.Equal)
            //};
            //var l = dbConnection.GetFilteredListAsync<Fellow>(CollectionNames.Fellows, filter);

            //return l;
            return new Task<List<Fellow>>(() => new List<Fellow>());
        }

        public static List<string> GetAllTripIdsByUser(string userId)
        {
            //var dbConnection = new DbConnection();

            //var filter = new List<FieldFilter>()
            //{
            //    new FieldFilter("UserId", userId, FieldType.String, CompareType.Equal)
            //};
            //var l = dbConnection.GetFilteredList<Fellow>(CollectionNames.Fellows, filter);

            //return l.Select(x=> x.TripId).ToList();
            return new List<string>(GetAllTripIdsByUser(""));
        }

        #endregion

    }
}

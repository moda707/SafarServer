using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson;
using SafarCore.DbClasses;
using SafarCore.GenFunctions;

namespace SafarCore.TripClasses
{
    public class Trip
    {
        public ObjectId TripId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public ObjectId LeaderId { get; set; }
        public List<Fellow> Fellows { get; set; }
        public List<Destination> Destinations { get; set; }
        public int Capacity { get; set; }

        public Trip()
        {
            
        }

        #region Converter

        public static Trip ConvertTripInDbtoTrip(TripInDb tripInDb)
        {
            return new Trip()
            {
                TripId = tripInDb.TripId,
                Title = tripInDb.Title,
                Description = tripInDb.Description,
                CreateDate = tripInDb.CreateDate,
                StartDate = tripInDb.StartDate,
                EndDate = tripInDb.EndDate,
                LeaderId = tripInDb.LeaderId,
                Capacity = tripInDb.Capacity,
                Fellows = Fellow.GetFellowsByTripId(tripInDb.TripId),
                Destinations = Destination.GetDestinationsByTripId(tripInDb.TripId)
            };
        }

        public static TripInDb ConvertTripTranstoTripInDb(TripTrans tripTrans)
        {
            return new TripInDb()
            {
                TripId = string.IsNullOrEmpty(tripTrans.TripId)?
                    ObjectId.GenerateNewId():
                    ObjectId.Parse(tripTrans.TripId),
                Title = tripTrans.Title,
                Description = tripTrans.Description,
                CreateDate = tripTrans.CreateDate,
                StartDate = tripTrans.StartDate,
                EndDate = tripTrans.EndDate,
                LeaderId = ObjectId.Parse(tripTrans.LeaderId),
                Capacity = tripTrans.Capacity
            };
        }
        #endregion

        #region Add Update Delete Functions

        public static FuncResult AddUpdateTrip(TripTrans tripTrans)
        {
            var tripInDb = ConvertTripTranstoTripInDb(tripTrans);
            return DbConnection.FastAddorUpdate(tripInDb, CollectionNames.Trip, new List<string>() {"TripId"});
        }

        #endregion

        #region Get Trips

        public static Trip GetTripById(string tripId)
        {
            var otripId = ObjectId.Parse(tripId);
            var dbConnection = new DbConnection();
            dbConnection.ConnectOpenReg();

            var filter = new List<FieldFilter>()
            {
                new FieldFilter("TripId", otripId, FieldType.ObjectId, CompareType.Equal)
            };
            var l = dbConnection.GetFilteredList<TripInDb>(CollectionNames.Trip, filter);
            var trip = ConvertTripInDbtoTrip(l[0]);
            return trip;
        }

        public static List<Trip> GetTripsByUserId(string userId)
        {
            var ouserId = ObjectId.Parse(userId);

            var tripIdsList = Fellow.GetAllTripIdsByUser(ouserId);

            var filter = new List<FieldFilter>()
            {
                new FieldFilter("TripId", tripIdsList, FieldType.ListObjectId, CompareType.IN)
            };

            var dbConnection = new DbConnection();
            dbConnection.ConnectOpenReg();

            return dbConnection.GetFilteredList<TripInDb>(CollectionNames.Trip, filter)
                .Select(ConvertTripInDbtoTrip).ToList();
        }

        #endregion

    }

    public class TripInDb
    {
        public ObjectId TripId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public ObjectId LeaderId { get; set; }
        public int Capacity { get; set; }

        public TripInDb()
        {
            
        }
    }

    public class TripTrans
    {
        public string TripId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string LeaderId { get; set; }
        public List<string> FellowIds { get; set; }
        public List<KeyValuePair<string,string>> Destinations { get; set; }
        public int Capacity { get; set; }

        public TripTrans()
        {
            
        }
    }
}

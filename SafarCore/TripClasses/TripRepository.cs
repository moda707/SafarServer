using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using SafarCore.DbClasses;
using SafarObjects.TripClasses;

namespace SafarCore.TripClasses
{
    public interface ITripRepository
    {
        Task<FuncResult> AddUpdateTrip(Trip trip);
        Task<FuncResult> DeleteTrip(string tripId);
        Task<Trip> GetTripById(string tripId);
        Task<List<Trip>> GetTripsByUserId(string userId);

    }
    public class TripRepository : ITripRepository
    {

        readonly DbConnection _context;

        public TripRepository(DbConnection context)
        {
            _context = context;
        }

        #region Converter

        public async Task<Trip> ConvertTripInDbtoTrip(TripInDb tripInDb)
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
                Fellows = await FellowFunc.GetFellowsByTripId(tripInDb.TripId),
                Destinations = await DestinationFunc.GetDestinationsByTripId(tripInDb.TripId)
            };
        }

        public TripInDb ConvertTriptoTripInDb(Trip tripTrans)
        {
            return new TripInDb()
            {
                TripId = string.IsNullOrEmpty(tripTrans.TripId)?
                    Guid.NewGuid().ToString():
                    tripTrans.TripId,
                Title = tripTrans.Title,
                Description = tripTrans.Description,
                CreateDate = tripTrans.CreateDate,
                StartDate = tripTrans.StartDate,
                EndDate = tripTrans.EndDate,
                LeaderId = tripTrans.LeaderId,
                Capacity = tripTrans.Capacity
            };
        }
        #endregion

        #region Add Update Delete Functions

        public Task<FuncResult> AddUpdateTrip(Trip trip)
        {
            try
            {
                var tripInDb = ConvertTriptoTripInDb(trip);
                return _context.AddorUpdateAsync(tripInDb.ToBsonDocument(), CollectionNames.Trip, new List<string>() { "TripId" });
            }
            catch (Exception e)
            {
                return new Task<FuncResult>(() => new FuncResult(ResultEnum.Unsuccessfull, e.Message));
            }
        }

        public Task<FuncResult> DeleteTrip(string tripId)
        {
            var filter = new List<FieldFilter>()
            {
                new FieldFilter("TripId", tripId, FieldType.String, CompareType.Equal)
            };
            return _context.DeleteMany(filter, CollectionNames.Trip);
        }

        #endregion

        #region Get Trips

        public async Task<Trip> GetTripById(string tripId)
        {
            var filter = new List<FieldFilter>()
            {
                new FieldFilter("TripId", tripId, FieldType.String, CompareType.Equal)
            };
            var l = await _context.GetFilteredListAsync<TripInDb>(CollectionNames.Trip, filter);
            var trip = await ConvertTripInDbtoTrip(l[0]);
            return trip;
        }

        public async Task<List<Trip>> GetTripsByUserId(string userId)
        {
            var tripIdsList = FellowFunc.GetAllTripIdsByUser(userId);

            var filter = new List<FieldFilter>()
            {
                new FieldFilter("TripId", tripIdsList, FieldType.ListString, CompareType.IN)
            };
            
            var tripsInDbList = await _context.GetFilteredListAsync<TripInDb>(CollectionNames.Trip, filter);
            var tripsList = new List<Trip>();
            foreach (var tripInDb in tripsInDbList)
            {
                var t = await ConvertTripInDbtoTrip(tripInDb);
                tripsList.Add(t);
            }

            return tripsList;
        }

        #endregion

    }
}

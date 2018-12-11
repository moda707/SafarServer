using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Bson;
using SafarCore.DbClasses;
using SafarObjects.ChatsClasses;

namespace SafarCore.ChatClasses
{
    public class TimelineMessageFunc : TimelineMessage
    {
        public static Task<FuncResult> AddUpdateTimelineMessage(TimelineMessage timelineMessage)
        {
            var dbConnection = new DbConnection();

            var col = dbConnection.GetMongoCollection(CollectionNames.TimelineMessages);
            return dbConnection.AddorUpdateAsync(timelineMessage.ToBsonDocument(), col,
                new List<string>() {"MessageId"});
        }

        public static Task<List<TimelineMessage>> GetTimelineMessages(string timelineId, int startPoint = 0, int count = 20)
        {
            var dbConnection = new DbConnection();
            var filter = new List<FieldFilter>()
            {
                new FieldFilter("TimelineId", timelineId, FieldType.String, CompareType.Equal)
            };
            var sort = new SortFilter("MessageDate", SortType.Descending);

            return dbConnection.GetFilteredListAsync<TimelineMessage>(CollectionNames.TimelineMessages, filter, sort, count);
        }
    }

    public class TimelineFunc : Timeline
    {
        public static Task<FuncResult> AddUpdateTimeline(Timeline timeline)
        {
            var dbConnection = new DbConnection();

            var col = dbConnection.GetMongoCollection(CollectionNames.Timelines);
            return dbConnection.AddorUpdateAsync(timeline.ToBsonDocument(), col,
                new List<string>() { "TimelineId" });
        }

        public static string GetTimelineIdByTrip(string tripId)
        {
            var dbConnection = new DbConnection();

            var filter = new List<FieldFilter>()
            {
                new FieldFilter("TripId", tripId, FieldType.String, CompareType.Equal)
            };

            var l = dbConnection.GetFilteredList<Timeline>(CollectionNames.Timelines, filter);
            return l.Count>0 ? l[0].TimelineId : null;
        }
    }
}

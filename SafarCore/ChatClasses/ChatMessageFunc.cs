using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using SafarCore.DbClasses;
using SafarCore.UserClasses;
using SafarObjects.ChatsClasses;

namespace SafarCore.ChatClasses
{
    public class ChatMessageFunc : ChatMessage
    {
        #region ADD UPDATE DELETE Functions

        public static FuncResult AddUpdateMessage(ChatMessage chatMessage)
        {
            var chatMessageInDb = chatMessage.GetChatMessageInDb().ToBsonDocument();

            //add it to db

            var addres = DbConnection.FastAddorUpdate(chatMessageInDb, CollectionNames.Chats);

            return addres;
        }

        public static Task<FuncResult> DeleteMessage(string messageId)
        {
            var omessageId = ObjectId.Parse(messageId);
            var dbConnection = new DbConnection();
            
            var t = dbConnection.DeleteManyAsync(CollectionNames.Chats,
                new List<FieldFilter>()
                {
                    new FieldFilter("MessageId", omessageId, FieldType.ObjectId, CompareType.Equal)
                });
            return t;
        }

        #endregion

        #region Get Messages
        public static async Task<List<ChatMessage>> GetChatMessages(string tripId, int startIndex = 0, int count = 20)
        {
            //var otripId = ObjectId.Parse(tripId);
            var dbConnection = new DbConnection();

            var filter = new List<FieldFilter>()
            {
                new FieldFilter("TripId", tripId, FieldType.String, CompareType.Equal)
            };

            var sort = new SortFilter("MessageDate", SortType.Descending);
            var chatList = await dbConnection.GetFilteredListAsync<ChatMessageInDb>(CollectionNames.Chats, filter, sort, count);
            
            return chatList.Select(x=> x.GetChatMessage()).ToList();
        }

        public static Task<List<string>> GetImagesListByTrip(string tripId)
        {
            return new Task<List<string>>(() => new List<string>());
        }

        public static Task<List<string>> GetVideosListByTrip(string tripId)
        {
            return new Task<List<string>>(() => new List<string>());
        }
        #endregion

    }


    public class ChatMessageInDbFunc : ChatMessageInDb
    {
        
    }
    
    public class ImageInfoFunc : ImageInfo
    {

    }
    
}

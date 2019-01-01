using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using SafarCore.DbClasses;
using SafarCore.UserClasses;
using SafarObjects.ChatsClasses;

namespace SafarCore.ChatClasses
{
    public interface IChatMessageRepository
    {
        Task<FuncResult> AddUpdateMessage(ChatMessage chatMessage);
        Task<FuncResult> DeleteMessage(string messageId);
        Task<List<ChatMessage>> GetChatMessages(string tripId, int startIndex = 0, int count = 20);

    }
    public class ChatMessageRepository:IChatMessageRepository
    {
        readonly IDbConnection _context;

        public ChatMessageRepository(IDbConnection context)
        {
            _context = context;
        }

        #region ADD UPDATE DELETE Functions

        public Task<FuncResult> AddUpdateMessage(ChatMessage chatMessage)
        {
            var chatMessageInDb = chatMessage.GetChatMessageInDb().ToBsonDocument();

            //add it to db
            var addres = _context.AddorUpdateAsync(chatMessageInDb, CollectionNames.Chats);

            return addres;
        }

        public Task<FuncResult> DeleteMessage(string messageId)
        {
            var omessageId = ObjectId.Parse(messageId);
            
            var t = _context.DeleteManyAsync(CollectionNames.Chats,
                new List<FieldFilter>()
                {
                    new FieldFilter("MessageId", omessageId, FieldType.ObjectId, CompareType.Equal)
                });
            return t;
        }

        #endregion

        #region Get Messages
        public async Task<List<ChatMessage>> GetChatMessages(string tripId, int startIndex = 0, int count = 20)
        {
            var filter = new List<FieldFilter>()
            {
                new FieldFilter("TripId", tripId, FieldType.String, CompareType.Equal)
            };

            var sort = new SortFilter("MessageDate", SortType.Descending);
            var chatList = await _context.GetFilteredListAsync<ChatMessageInDb>(CollectionNames.Chats, filter, sort, count);
            
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
    
    public class ImageInfoFunc : ImageInfo
    {

    }
    
}

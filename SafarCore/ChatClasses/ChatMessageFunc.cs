using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Bson;
using SafarCore.DbClasses;
using SafarCore.UserClasses;
using SafarObjects.ChatsClasses;

namespace SafarCore.ChatClasses
{
    public class ChatMessageFunc : ChatMessage
    {
        
        #region Converters

        public static ChatMessageFunc ConvertChatMsgInDbtoChatMsg(ChatMessageInDb chatMessageInDb)
        {
            return new ChatMessageFunc()
            {
                MessageId = chatMessageInDb.MessageId,
                TripId = chatMessageInDb.TripId,
                MessageText = chatMessageInDb.MessageText,
                FromId = chatMessageInDb.FromId,
                FromName = Users.GetUserById(chatMessageInDb.FromId.ToString()).DisplayName,
                MessageDate = chatMessageInDb.MessageDate,
                UriLink = chatMessageInDb.UriLink,
                MessageType = (ChatMessageType)chatMessageInDb.MessageType,
                MessageStatus = (ChatMessageStatus)chatMessageInDb.MessageStatus,
                MessageSeenStatus = (ChatMessageSeenStatus)chatMessageInDb.MessageSeenStatus
            };
        }
        
        public static ChatMessageInDb ConvertChatMsgTranstoChatMsgInDb(ChatMessageTrans chatMessageTrans)
        {
            return new ChatMessageInDb()
            {
                MessageId = chatMessageTrans.MessageId,
                TripId = chatMessageTrans.TripId,
                MessageText = chatMessageTrans.MessageText,
                FromId = chatMessageTrans.FromId,
                MessageDate = chatMessageTrans.MessageDate,
                UriLink = chatMessageTrans.UriLink,
                MessageType = chatMessageTrans.MessageType,
                MessageStatus = chatMessageTrans.MessageStatus,
                MessageSeenStatus = chatMessageTrans.MessageSeenStatus
            };
        }
        
        #endregion

        #region ADD UPDATE DELETE Functions

        public static Task<FuncResult> AddUpdateMessage(ChatMessageTrans chatMessageTrans)
        {
            var chatMessageInDb = ConvertChatMsgTranstoChatMsgInDb(chatMessageTrans);

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
        public static Task<List<ChatMessageFunc>> GetChatMessages(string tripId, int startIndex = 0, int count = 20)
        {
            var otripId = ObjectId.Parse(tripId);
            var dbConnection = new DbConnection();

            var filter = new List<FieldFilter>()
            {
                new FieldFilter("TripId", otripId, FieldType.ObjectId, CompareType.Equal)
            };

            var sort = new SortFilter("MessageDate", SortType.Descending);
            var chatList = dbConnection.GetFilteredListAsync<ChatMessageFunc>(CollectionNames.Chats, filter, sort, count);
            
            return chatList;
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

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
                FromName = Users.getUserById(chatMessageInDb.FromId.ToString()).DisplayName,
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
                MessageId = ObjectId.Parse(chatMessageTrans.MessageId),
                TripId = ObjectId.Parse(chatMessageTrans.TripId),
                MessageText = chatMessageTrans.MessageText,
                FromId = ObjectId.Parse(chatMessageTrans.FromId),
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
            dbConnection.Connect();
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
            dbConnection.Connect();

            var filter = new List<FieldFilter>()
            {
                new FieldFilter("TripId", otripId, FieldType.ObjectId, CompareType.Equal)
            };

            var sort = new SortFilter("MessageDate", SortType.Descending);
            var chatList = dbConnection.GetFilteredListAsync<ChatMessageFunc>(CollectionNames.Chats, filter, sort, count);
            
            return chatList;
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

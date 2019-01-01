using SafarObjects.ChatsClasses;
using SafarObjects.TripClasses;
using SafarSDK.Models;

namespace SafarSDK.Core
{
    public static class ModelExtensions
    {
        public static FellowModel ToModel(this Fellow dto)
        {
            return new FellowModel
            {
                UserId = dto.UserId,
                TripId = dto.TripId,
                FellowStatus = dto.FellowStatus,
                FellowType = dto.FellowType
            };
        }

        public static ChatMessageModel ToModel(this ChatMessage dto)
        {
            return new ChatMessageModel
            {
                TripId = dto.TripId,
                MessageDate = dto.MessageDate,
                MessageText = dto.MessageText,
                MessageId = dto.MessageId,
                FromName = dto.FromName,
                MessageType = dto.MessageType,
                MessageSeenStatus = dto.MessageSeenStatus,
                UriLink = dto.UriLink,
                MessageStatus = dto.MessageStatus,
                FromId = dto.FromId
            };
        }
    }
}

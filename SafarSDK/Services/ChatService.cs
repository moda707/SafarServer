using System.Linq;
using System.Threading.Tasks;
using Inx.Networking;
using SafarSDK.Core;
using SafarSDK.Models;

namespace SafarSDK.Services
{
    public interface IChatService
    {
        Task<ChatMessageModel[]> GetMessagesAsync(string tripId);
    }

    public class ChatService : IChatService
    {
        readonly MessageClient msgClient;

        public ChatService(MessageClient msgClient)
        {
            this.msgClient = msgClient;
        }
        
        public async Task<ChatMessageModel[]> GetMessagesAsync(string tripId)
        {
            var dtos = await msgClient.GetMessagesByTrip(tripId);

            return dtos.Select(ModelExtensions.ToModel).ToArray();
        }
    }
}

using System.Threading.Tasks;
using Inx.Networking.Core;
using SafarObjects.ChatsClasses;

namespace SafarSDK
{
    public class MessageClient
    {
        readonly INetworkingClient _client;

        public MessageClient(INetworkingClient client)
        {
            _client = client;
        }

        public async Task<ChatMessage[]> GetMessagesByTrip(string tripId)
        {
            return await _client.GetAsync<ChatMessage[]>($"chat/{tripId}/messages");
        }
    }
}

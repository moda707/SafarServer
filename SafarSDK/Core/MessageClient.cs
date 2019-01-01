using System;
using Inx.Networking.Core;
using System.Threading.Tasks;
using SafarObjects.ChatsClasses;

namespace Inx.Networking
{
    public class MessageClient
    {
        readonly INetworkingClient client;

        public MessageClient(INetworkingClient client)
        {
            this.client = client;
        }

        public async Task<ChatMessage[]> GetMessagesByConversationAsync(int conversationId)
        {
            return await client.GetAsync<ChatMessage[]>($"chat/{conversationId}/messages");
        }
    }
}

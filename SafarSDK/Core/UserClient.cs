using System;
using Inx.Networking.Core;
using System.Threading.Tasks;
using SafarObjects.UserClasses;

namespace Inx.Networking
{
    public class UserClient
    {
        readonly INetworkingClient client;

        public UserClient(INetworkingClient client)
        {
            this.client = client;
        }

        public async Task<Users[]> GetMyFriendsAsync()
        {
            return await client.GetAsync<Users[]>("users/my-friends");
        }

        public async Task<Profile> GetProfileAsync()
        {
            return await client.GetAsync<Profile>("users/me");
        }
    }
}

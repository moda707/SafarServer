using System.Threading.Tasks;
using SafarObjects.UserClasses;

namespace SafarSDK.Core
{
    public class AuthClient
    {
        readonly Inx.Networking.Core.INetworkingClient client;

        public AuthClient(Inx.Networking.Core.INetworkingClient client)
        {
            this.client = client;
        }

        public async Task<AccessTokenObj> SignInAsync(SignIn dto)
        {
            return await client.PostAsync<SignIn, AccessTokenObj>("user/signin", dto); ;
        }

        public async Task<AccessTokenObj> SignUpAsync(SignUp dto)
        {
            return await client.PostAsync<SignUp, AccessTokenObj>("user/signup", dto); ;
        }
    }
}

using System.Linq;
using System.Threading.Tasks;
using Inx.Networking;
using SafarSDK.Models;

namespace SafarSDK.Services
{
    public interface IFellowService
    {
        Task<FellowModel[]> GetMyFriendsAsync();
    }

    public class FellowService : IFellowService
    {
        readonly UserClient client;

        public FellowService(UserClient client)
        {
            this.client = client;
        }

        public async Task<FellowModel[]> GetMyFriendsAsync()
        {
            var dtos = await client.GetMyFriendsAsync();

            return dtos.Select(x => new FellowModel
            {
                
            }).ToArray();
        }
    }
}

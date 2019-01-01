using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using SafarCore.ChatClasses;
using SafarCore.DbClasses;
using SafarCore.TripClasses;
using SafarCore.UserClasses;
using SafarObjects.ChatsClasses;
using SafarObjects.UserClasses;

namespace SafarCore.SignalR
{
    [Authorize]
    public class ChatHub : Hub
    {
        readonly UserManager<Users> _userManager;
        readonly IChatMessageRepository _chatMessageRepository;
        private readonly IUsersFunc _usersFunc;

        public ChatHub(
            IChatMessageRepository chatMessageRepository,
            IUsersFunc usersFunc,
            UserManager<Users> userManager)
        {
            _chatMessageRepository = chatMessageRepository;
            _userManager = userManager;
            _usersFunc = usersFunc;
        }

        public async Task Send(ChatMessage message)
        {
            //get current user
            var currentUser = await GetCurrentUserAsync();

            //get fellows
            var fellows = await FellowFunc.GetFellowsByTripId(message.TripId);

            //get their connectionId
            var recipients = fellows.Where(x => x.UserId != currentUser.UserId).Select(x => x.UserId).ToArray();
            if (recipients.Length == 0)
            {
                return;
            }

            //add message to db
            await _chatMessageRepository.AddUpdateMessage(message);

            
            Broastcast("message.sent", message, recipients);
        }
        
        async void Broastcast<T>(string message, T data, params string[] recipientIds)
        {
            var connections = await _usersFunc.GetUsersConnection(recipientIds);

            await Clients.Client(Context.ConnectionId).SendAsync(message, data);

            Parallel.ForEach(connections, async (connectionId) =>
            {
                await Clients.Client(connectionId).SendAsync(message, data);
            });
        }

        public override async Task OnConnectedAsync()
        {
            await base.OnConnectedAsync();

            var currentUser = await GetCurrentUserAsync();

            //add or update the current connectionId for the currentUser
            _usersFunc.UpdateConnectionId(currentUser.UserId.ToString(), Context.ConnectionId);
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            await base.OnDisconnectedAsync(exception);

            var currentUser = await GetCurrentUserAsync();

            //remove the connectionId in the db for currentUser
            _usersFunc.UpdateConnectionId(currentUser.UserId.ToString(), "");
        }

        protected async Task<Users> GetCurrentUserAsync()
        {
            return await _userManager.GetUserAsync(this.Context.User);
        }
    }
}

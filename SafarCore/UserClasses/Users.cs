using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Bson;
using SafarCore.DbClasses;
using SafarObjects.TripClasses;
using SafarObjects.UserClasses;

namespace SafarCore.UserClasses
{
    public interface IUsersFunc
    {
        Task<Users> GetUserByEmailPassword(string username, string password);

        Task<FuncResult> AddUser(SignUp userSignUp);

        Task<string[]> GetUsersConnection(string[] recipientIds);

        void UpdateConnectionId(string userId, string connectionId);
        //List<Users> GetAll();
    }
    public class UsersFunc:IUsersFunc
    {
        readonly IDbConnection _context;

        public UsersFunc(IDbConnection context)
        {
            _context = context;
        }

        public Task<FuncResult> AddUser(SignUp userSignUp)
        {
            //check the existence of this email
            if (CheckUserExistsByEmail(userSignUp.Email))
            {
                return new Task<FuncResult>(new Func<FuncResult>(() => new FuncResult(ResultEnum.Unsuccessfull,
                    "User with this email is already registered, please reset your password if you forgot it.")));
            }
            
            var user = new Users()
            {
                UserId = Guid.NewGuid().ToString(),
                DisplayName = userSignUp.DisplayName,
                Email = userSignUp.Email,
                Password = userSignUp.Password,
                LastActivity = DateTime.Now,
                ProfileImage = "",
                ConnectionId = "",
                token = ""
            };
            
            return _context.AddorUpdateAsync(user.ToBsonDocument(), CollectionNames.User, new List<string>() {"UserId"});
        }

        public bool CheckUserExistsByEmail(string email)
        {
            var filter = new List<FieldFilter>()
            {
                new FieldFilter("Email", email, FieldType.String, CompareType.Equal)
            };

            var users = _context.GetFilteredList<UsersInDb>(CollectionNames.User, filter);
            return users.Count > 0;
        }

        //public static Users GetUserById(string userId)
        //{
        //    var dbConnection = new DbConnection();
            
        //    var filter = new List<FieldFilter>()
        //    {
        //        new FieldFilter("UserId", userId, FieldType.String, CompareType.Equal)
        //    };

        //    var user = dbConnection.GetFilteredList<UsersInDb>(CollectionNames.User, filter);

        //    return user[0].GetUser();
        //}

        public async Task<Users> GetUserByEmailPassword(string email, string password)
        {
            var filter = new List<FieldFilter>()
            {
                new FieldFilter("Email", email, FieldType.String, CompareType.Equal),
                new FieldFilter("Password", password, FieldType.String, CompareType.Equal)
            };

            var users = await _context.GetFilteredListAsync<UsersInDb>(CollectionNames.User, filter);
            Users user = null;

            if (users.Count == 0)
            {
                return null;
            }

            user = users[0].GetUser();
            return user;
        }

        //public static bool UserExists(string email)
        //{
        //    var dbConnection = new DbConnection();

        //    var filter = new List<FieldFilter>()
        //    {
        //        new FieldFilter("Email", email, FieldType.String, CompareType.Equal)
        //    };

        //    var hasDocument = dbConnection.HasDocument(CollectionNames.User, filter);

        //    return hasDocument;
        //}

        public static Task<FuncResult> UpdateUser(Users users)
        {
            return new Task<FuncResult>(() => new FuncResult(ResultEnum.Successfull));
        }

        public static Task<FuncResult> DeleteUser(string userId)
        {
            return new Task<FuncResult>(() => new FuncResult(ResultEnum.Successfull));
        }

        public static Task<List<FriendFunc>> GetFriendsByUser(string userId)
        {
            return new Task<List<FriendFunc>>(() => new List<FriendFunc>());
        }

        public static Task<FriendFunc> GetFriendById(string friendId)
        {
            return new Task<FriendFunc>(() => new FriendFunc());
        }

        public static Task<List<Fellow>> GetFellowsByTrip(string tripId)
        {
            return new Task<List<Fellow>>(() => new List<Fellow>());
        }

        public static Task<UserProfile> GetUserProfile(string userId)
        {
            return null;
        }

        public static Task<UserStatus> GetUserStatus(string userId)
        {
            return new Task<UserStatus>(() => UserStatus.Offline);
        }

        public async Task<string[]> GetUsersConnection(string[] recipientIds)
        {
            var filter = new List<FieldFilter>()
            {
                new FieldFilter("UserId", recipientIds.ToList(), FieldType.ListString, CompareType.IN)
            };

            var user = await _context.GetFilteredListAsync<UsersInDb>(CollectionNames.User, filter);
            return user.Select(x => x.ConnectionId).ToArray();
        }

        public void UpdateConnectionId(string userId, string connectionId)
        {
            var filter = new List<FieldFilter>()
            {
                new FieldFilter("UserId", userId, FieldType.String, CompareType.Equal)
            };

            var update = new List<FieldUpdate>()
            {
                new FieldUpdate("ConnectionId", connectionId, FieldType.String)
            };

            _context.UpdateField(CollectionNames.User, filter, update);
        }
    }
}

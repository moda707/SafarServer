using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using SafarCore.DbClasses;
using SafarCore.GenFunctions;
using SafarObjects.TripClasses;
using SafarObjects.UserClasses;

namespace SafarCore.UserClasses
{
    public class UsersFunc : Users
    {
       public static FuncResult AddUser(Users userTrans)
        {
            //check the existence of this email


            var user = userTrans.GetUserInDb().ToBsonDocument();
            
            return DbConnection.FastAddorUpdate(user, CollectionNames.User, new List<string>() {"UserId"});
        }

        public static Users GetUserById(string userId)
        {
            var dbConnection = new DbConnection();
            
            var filter = new List<FieldFilter>()
            {
                new FieldFilter("UserId", userId, FieldType.String, CompareType.Equal)
            };

            var user = dbConnection.GetFilteredList<UsersInDb>(CollectionNames.User, filter);

            return user[0].GetUser();
        }

        public static async Task<List<UsersInDb>> GetUserByEmailPassword(string email, string password)
        {
            var dbConnection = new DbConnection();
            
            var filter = new List<FieldFilter>()
            {
                new FieldFilter("Email", email, FieldType.String, CompareType.Equal),
                new FieldFilter("Password", password, FieldType.String, CompareType.Equal)
            };

            var user = await dbConnection.GetFilteredListAsync<UsersInDb>(CollectionNames.User, filter);

            return user;
        }

        public static bool UserExists(string email)
        {
            var dbConnection = new DbConnection();

            var filter = new List<FieldFilter>()
            {
                new FieldFilter("Email", email, FieldType.String, CompareType.Equal)
            };

            var hasDocument = dbConnection.HasDocument(CollectionNames.User, filter);

            return hasDocument;
        }

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
    }
}

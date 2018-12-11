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
    public class Users : SafarObjects.UserClasses.Users
    {
       public static Task<FuncResult> AddUser(UsersTrans userTrans)
        {
            //check the existence of this email


            var user = userTrans.GetUsersObject();
            
            return DbConnection.FastAddorUpdate(user, CollectionNames.User, new List<string>() {"UserId"});
        }

        public static Users GetUserById(string userId)
        {
            var ouserId = ObjectId.Parse(userId);
            var dbConnection = new DbConnection();
            
            var filter = new List<FieldFilter>()
            {
                new FieldFilter("UserId", ouserId, FieldType.ObjectId, CompareType.Equal)
            };

            var user = dbConnection.GetFilteredList<Users>(CollectionNames.User, filter);

            return user[0];
        }

        public static Task<List<Users>> GetUserByEmailPassword(string email, string password)
        {
            var dbConnection = new DbConnection();
            
            var filter = new List<FieldFilter>()
            {
                new FieldFilter("Email", email, FieldType.String, CompareType.Equal),
                new FieldFilter("Password", password, FieldType.String, CompareType.Equal)
            };

            var user = dbConnection.GetFilteredListAsync<Users>(CollectionNames.User, filter);

            return user;
        }

        public static Task<FuncResult> UpdateUser(Users users)
        {
            return new Task<FuncResult>(() => new FuncResult(ResultEnum.Successfull));
        }

        public static Task<FuncResult> DeleteUser(string userId)
        {
            return new Task<FuncResult>(() => new FuncResult(ResultEnum.Successfull));
        }

        public static Task<List<Friend>> GetFriendsByUser(string userId)
        {
            return new Task<List<Friend>>(() => new List<Friend>());
        }

        public static Task<Friend> GetFriendById(string friendId)
        {
            return new Task<Friend>(() => new Friend());
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

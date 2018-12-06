using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using SafarCore.DbClasses;
using SafarCore.GenFunctions;

namespace SafarCore.UserClasses
{
    public class Users
    {
        [BsonId]
        public ObjectId _id { get; set; }
        public ObjectId UserId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string DisplayName { get; set; }
        public string ProfileImage { get; set; }
        public DateTime LastActivity { get; set; }

        public Users()
        {
            
        }

        public static async Task<FuncResult> AddUser(UsersTrans userTrans)
        {
            var user = userTrans.GetUsersObject();
            
            return await DbConnection.FastAddorUpdate(user, CollectionNames.User, new List<string>() {"UserId"});
        }

        public static Users getUserById(string userId)
        {
            var ouserId = ObjectId.Parse(userId);
            var dbConnection = new DbConnection();
            dbConnection.Connect();
            var filter = new List<FieldFilter>()
            {
                new FieldFilter("UserId", ouserId, FieldType.ObjectId, CompareType.Equal)
            };

            var user = dbConnection.GetFilteredList<Users>(CollectionNames.User, filter);

            return user[0];
        }

        public static Task<List<Users>> getUserByEmailPassword(string email, string password)
        {
            var dbConnection = new DbConnection();
            dbConnection.Connect();
            var filter = new List<FieldFilter>()
            {
                new FieldFilter("Email", email, FieldType.String, CompareType.Equal),
                new FieldFilter("Password", password, FieldType.String, CompareType.Equal)
            };

            var user = dbConnection.GetFilteredListAsync<Users>(CollectionNames.User, filter);

            return user;
        }
    }

    public class UsersTrans
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string DisplayName { get; set; }
        public string ProfileImage { get; set; }

        public UsersTrans()
        {
            
        }

        public UsersTrans(string email, string password, string displayName, string profileImage)
        {
            Email = email;
            Password = password;
            DisplayName = displayName;
            ProfileImage = profileImage;
        }

        public Users GetUsersObject()
        {
            return new Users()
            {
                UserId = ObjectId.GenerateNewId(),
                DisplayName = this.DisplayName,
                Email = this.Email,
                Password = this.Password,
                ProfileImage = this.ProfileImage,
                LastActivity = DateTime.Now
            };
        }
    }
}

using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SafarObjects.UserClasses
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

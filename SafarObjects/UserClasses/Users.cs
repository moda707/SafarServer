using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SafarObjects.UserClasses
{
    public class UsersInDb : Users
    {
        [BsonId]
        public ObjectId _id { get; set; }

        public UsersInDb() 
        {
            
        }
        
        public Users GetUser()
        {
            return new Users()
            {
                UserId = this.UserId,
                DisplayName = this.DisplayName,
                Email = this.Email,
                Password = this.Password,
                LastActivity = this.LastActivity,
                ProfileImage = this.ProfileImage
            };
        }
    }

    public class Users
    {
        public string UserId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string DisplayName { get; set; }
        public string ProfileImage { get; set; }
        public DateTime LastActivity { get; set; }

        public Users()
        {

        }

        public UsersInDb GetUserInDb()
        {
            return new UsersInDb()
            {
                UserId = this.UserId,
                DisplayName = this.DisplayName,
                Email = this.Email,
                Password = this.Password,
                LastActivity = this.LastActivity,
                ProfileImage = this.ProfileImage,
                _id = ObjectId.GenerateNewId()
            };
        }
    }

    public class UserProfile
    {

    }

    public enum UserStatus
    {
        Online = 0,
        Offline = 1
    }
}

using System.Threading.Tasks;
using MongoDB.Bson;
using SafarCore.GenFunctions;

namespace SafarCore.UserClasses
{
    public class Users
    {
        public string UserId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string DisplayName { get; set; }
        public string ProfileImage { get; set; }
        public string LastActivity { get; set; }

        public Users()
        {
            
        }

        public static async Task<FuncResult> AddUser(Users user)
        {
            var pusher = new PusherFunc();
            var t = await pusher.Push("myChannel", "myEvent", new {message = "hello"});


            return FuncResult.Successful;
        }

        public static Users getUserById(ObjectId userId)
        {

            return new Users();
        }
    }
}

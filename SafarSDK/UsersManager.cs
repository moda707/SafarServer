using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SafarCore.UserClasses;

namespace SafarSDK
{
    public class UsersManager
    {
        static HttpClient client = new HttpClient();
        public static async Task<FuncResult> AddUser(UsersTrans userT)
        {
            client.BaseAddress = new Uri(ServerConfig.getUrl());
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            var json = JsonConvert.SerializeObject(userT);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("api/User", httpContent);

            return response.IsSuccessStatusCode ?
                new FuncResult(ResultEnum.Successfull) :
                new FuncResult(ResultEnum.Unsuccessfull, response.ReasonPhrase);
        }

        public static async Task<Users> GetUserById(string userId)
        {
            client.BaseAddress = new Uri(ServerConfig.getUrl());
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await client.GetStringAsync($"api/User/{userId}");
            return JsonConvert.DeserializeObject<Users>(response);
        }

        public static async Task<Users> GetUserByEmailPass(string email, string password)
        {
            client.BaseAddress = new Uri(ServerConfig.getUrl());
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await client.GetStringAsync($"api/User/{email}/{password}");
            var userTrans = JsonConvert.DeserializeObject<UsersTrans>(response);
            return userTrans.GetUsersObject();
        }
    }
}

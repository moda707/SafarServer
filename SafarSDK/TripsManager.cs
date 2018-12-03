using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SafarCore.ChatsClasses;
using SafarCore.TripClasses;

namespace SafarSDK
{
    public class TripsManager
    {
        static HttpClient client = new HttpClient();

        public static async Task<Trip> GetTrip(string tripId)
        {
            client.BaseAddress = new Uri(ServerConfig.getUrl());
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await client.GetStringAsync($"api/Trip/{tripId}");
            return JsonConvert.DeserializeObject<Trip>(response);
        }

        public static async Task<FuncResult> AddTrip(TripTrans trip)
        {
            client.BaseAddress = new Uri(ServerConfig.getUrl());
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await client.PostAsJsonAsync("api/Trip", trip);

            return response.IsSuccessStatusCode ?
                new FuncResult(ResultEnum.Successfull) :
                new FuncResult(ResultEnum.Unsuccessfull, response.ReasonPhrase);
        }
    }
}

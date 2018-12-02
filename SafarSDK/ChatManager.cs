using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SafarCore.ChatsClasses;

namespace SafarSDK
{
    public class ChatManager
    {
        static HttpClient client = new HttpClient();

        public static async Task<FuncResult> SendMessage(ChatMessage message)
        {
            client.BaseAddress = new Uri(ServerConfig.getUrl());
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            
            var response = await client.PostAsJsonAsync("Chat", message);
            
            return response.IsSuccessStatusCode ? 
                new FuncResult(ResultEnum.Successfull): 
                new FuncResult(ResultEnum.Unsuccessfull, response.ReasonPhrase);
        }

        public static async Task<List<ChatMessage>> GetMessages(string tripId, int startIndex, int count)
        {
            client.BaseAddress = new Uri(ServerConfig.getUrl());
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await client.GetStringAsync($"Chat/{tripId}/{startIndex}/{count}");
            return JsonConvert.DeserializeObject<List<ChatMessage>>(response);
        }
    }
}

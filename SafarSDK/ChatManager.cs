using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SafarObjects.ChatsClasses;

namespace SafarSDK
{
    public class ChatManager
    {
        

        public static async Task<FuncResult> SendMessage(ChatMessage message)
        {
            try
            {
                var client = new HttpClient
                {
                    BaseAddress = new Uri(ServerConfig.getUrl())
                };
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                var json = JsonConvert.SerializeObject(message);
                var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await client.PostAsync("api/Chat", httpContent);
                client.Dispose();

                return response.IsSuccessStatusCode
                    ? new FuncResult(ResultEnum.Successfull)
                    : new FuncResult(ResultEnum.Unsuccessfull, response.ReasonPhrase);
            }
            catch (Exception e)
            {
                return new FuncResult(ResultEnum.Unsuccessfull, e.Message);
            }
        }

        public static async Task<List<ChatMessage>> GetMessages(string tripId, int startIndex, int count)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(ServerConfig.getUrl());
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await client.GetStringAsync($"api/chat/{tripId}/{startIndex}/{count}");
            client.Dispose();
            return JsonConvert.DeserializeObject<List<ChatMessage>>(response);
        }
    }
}

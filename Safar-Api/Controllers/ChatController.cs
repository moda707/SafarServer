using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SafarApi.Core;
using SafarCore.ChatClasses;
using SafarObjects.ChatsClasses;
using SafarObjects.UserClasses;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SafarApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class ChatController : SafarControllerBase
    {
        private readonly IHostingEnvironment _environment;
        private readonly IChatMessageRepository _chatMessageRepository;
        
        public ChatController(IHostingEnvironment environment, 
            IChatMessageRepository chatMessageRepository,
            UserManager<Users> userManager)
            : base(userManager)
        {
            _environment = environment ?? throw new ArgumentNullException(nameof(environment));
            _chatMessageRepository = chatMessageRepository;
        }
        
        // GET api/<controller>/5
        [HttpGet("{tripId}/{startIndex}/{count}")]
        public async Task<List<ChatMessage>> Get(string tripId, int startIndex, int count)
        {
            var k = await _chatMessageRepository.GetChatMessages(tripId, startIndex, count);
            return k;
        }

        // POST api/<controller>W
        [HttpPost]
        public void Post([FromBody]ChatMessage value)
        {
            value.MessageId = Guid.NewGuid().ToString();
            value.MessageDate = DateTime.Now;
            var t = _chatMessageRepository.AddUpdateMessage(value);
        }

        //[HttpPost]
        //public void Post([FromBody]ChatMessage value, IFormFile file)
        //{
        //    //Add message
        //    var t = ChatMessageRepository.AddUpdateMessage(value);
        //    if (t.Result == ResultEnum.Successfull)
        //    {
        //        //Add Image
        //        var uploads = Path.Combine(_environment.WebRootPath, "TripsContent/" + value.TripId + "/Images/Original");
        //        var compressedimg = Path.Combine(_environment.WebRootPath, "TripsContent/" + value.TripId + "/Images/Compressed");

        //        if (file.Length > 0)
        //        {
        //            var filename = value.MessageId + "." + file.FileName.Split('.').LastOrDefault();
        //            using (var fileStream = new FileStream(Path.Combine(uploads, filename), FileMode.Create))
        //            {
        //                file.CopyToAsync(fileStream);

        //                ImageProcessing.CompressImage(value.MessageId, uploads, compressedimg);
        //            }
        //        }
        //    }
        //}

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

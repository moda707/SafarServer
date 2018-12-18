﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SafarCore;
using SafarCore.ChatClasses;
using SafarCore.GenFunctions;
using SafarObjects.ChatsClasses;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SafarApi.Controllers
{
    [Route("api/[controller]")]
    public class ChatController : Controller
    {
        private readonly IHostingEnvironment _environment;
        public ChatController(IHostingEnvironment environment)
        {
            _environment = environment ?? throw new ArgumentNullException(nameof(environment));
        }

        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        [HttpGet("{tripId}/{startIndex}/{count}")]
        public async Task<List<ChatMessage>> Get(string tripId, int startIndex, int count)
        {
            var k = await ChatMessageFunc.GetChatMessages(tripId, startIndex, count);
            return k;
        }

        // POST api/<controller>W
        [HttpPost]
        public void Post([FromBody]ChatMessage value)
        {
            value.MessageId = Guid.NewGuid().ToString();
            value.MessageDate = DateTime.Now;
            var t = ChatMessageFunc.AddUpdateMessage(value);
        }

        //[HttpPost]
        //public void Post([FromBody]ChatMessage value, IFormFile file)
        //{
        //    //Add message
        //    var t = ChatMessageFunc.AddUpdateMessage(value);
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

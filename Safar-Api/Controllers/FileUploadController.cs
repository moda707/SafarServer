using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SafarCore.GenFunctions;
using SafarObjects.ChatsClasses;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SafarApi.Controllers
{
    [Route("api/[controller]")]
    public class FileUploadController : Controller
    {
        private readonly IHostingEnvironment _environment;
        public FileUploadController(IHostingEnvironment environment)
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
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        [HttpPost]
        public async Task Post([FromBody]ImageInfo value, IFormFile file)
        {
            var uploads = Path.Combine(_environment.WebRootPath, "TripsContent/" + value.TripId + "/Images/Original");
            var compressedimg = Path.Combine(_environment.WebRootPath, "TripsContent/" + value.TripId + "/Images/Compressed");

            if (file.Length > 0)
            {
                var filename = value.MessageId + "." + file.FileName.Split('.').LastOrDefault();
                using (var fileStream = new FileStream(Path.Combine(uploads, filename), FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);

                    ImageProcessing.CompressImage(value.MessageId, uploads, compressedimg);
                }
            }
        }

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

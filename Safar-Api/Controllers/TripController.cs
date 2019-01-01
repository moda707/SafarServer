using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using SafarApi.Core;
using SafarCore.DbClasses;
using SafarCore.TripClasses;
using SafarObjects.TripClasses;
using SafarObjects.UserClasses;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SafarApi.Controllers
{
    [Route("api/[controller]")]
    public class TripController : SafarControllerBase
    {
        private readonly ITripRepository _tripRepository;
        public TripController(ITripRepository tripRepository,
            UserManager<Users> userManager) : base(userManager)
        {
            _tripRepository = tripRepository;
        }

        // GET: api/<controller>
        [HttpGet]
        public string Get()
        {
            return "It is running";
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public async Task<Trip> Get(string tripId)
        {
            return await _tripRepository.GetTripById(tripId);
        }

        // POST api/<controller>
        [HttpPost]
        public void Post([FromBody]Trip value)
        {
            _tripRepository.AddUpdateTrip(value);
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

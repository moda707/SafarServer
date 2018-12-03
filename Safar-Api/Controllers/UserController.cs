using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SafarCore.UserClasses;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SafarApi.Controllers
{
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        // GET: api/<controller>
        [HttpGet]
        public void Get()
        {
            
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public async Task<Users> Get(string id)
        {
            return await Users.getUserById(id);
        }

        [HttpGet("{email}/{password}")]
        public async Task<Users> Get(string email, string password)
        {
            return await Users.getUserByEmailPassword(email, password);
        }

        // POST api/<controller>
        [HttpPost]
        public async void Post([FromBody]UsersTrans value)
        {
            var t = await Users.AddUser(value);
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

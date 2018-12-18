using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SafarCore.UserClasses;
using SafarObjects.UserClasses;

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
        public Users Get(string id)
        {
            return UsersFunc.GetUserById(id);
        }

        [HttpGet("{email}/{password}")]
        public async Task<Users> Get(string email, string password)
        {

            try
            {
                var uu = await UsersFunc.GetUserByEmailPassword(email, password);
                var u = (uu)[0].GetUser();
                return u;
            }
            catch (Exception e)
            {
                return new Users();
            }
        }

        // POST api/<controller>
        [HttpPost]
        public  void Post([FromBody]Users value)
        {
            var t =  UsersFunc.AddUser(value);
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

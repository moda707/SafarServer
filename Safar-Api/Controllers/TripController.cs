﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using SafarCore.DbClasses;
using SafarCore.TripClasses;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SafarApi.Controllers
{
    [Route("api/[controller]")]
    public class TripController : Controller
    {
        // GET: api/<controller>
        [HttpGet]
        public string Get()
        {
            var dbConnection = new DbConnection();
            
            var col = dbConnection.GetMongoCollection(CollectionNames.User);
            var data = col.AsQueryable().ToList();

            return "It is running";
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public async Task<Trip> Get(string tripId)
        {
            return await Trip.GetTripById(tripId);
        }

        // POST api/<controller>
        [HttpPost]
        public async void Post([FromBody]TripTrans value)
        {
            await Trip.AddUpdateTrip(value);
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

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SafarObjects.TripClasses
{
    public class Destination
    {
        [BsonId]
        public ObjectId _id { get; set; }
        public string DestinationId { get; set; }
        public string Title { get; set; }
        public GeoPoint Location { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public Destination()
        {
            
        }
        
    }
}

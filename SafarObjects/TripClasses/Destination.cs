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
        public string DestinationId { get; set; }
        public string Title { get; set; }
        public GeoPoint Location { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public Destination()
        {
            
        }

        public DestinationInDb GetDestinationInDb()
        {
            return new DestinationInDb()
            {
                DestinationId = this.DestinationId,
                Location = this.Location,
                Title = this.Title,
                StartDate = this.StartDate,
                EndDate = this.EndDate
            };
        }
    }

    public class DestinationInDb
    {
        [BsonId]
        public ObjectId _id { get; set; }
        public string DestinationId { get; set; }
        public string Title { get; set; }
        public GeoPoint Location { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public DestinationInDb()
        {

        }

        public Destination GetDestination()
        {
            return new Destination()
            {
                DestinationId = this.DestinationId,
                Location = this.Location,
                Title = this.Title,
                StartDate = this.StartDate,
                EndDate = this.EndDate
            };
        }
    }
}

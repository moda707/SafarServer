using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace SafarObjects.TripClasses
{
    public class Trip
    {
        public ObjectId TripId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public ObjectId LeaderId { get; set; }
        public List<Fellow> Fellows { get; set; }
        public List<Destination> Destinations { get; set; }
        public int Capacity { get; set; }

        public Trip()
        {
            
        }
        
    }

    public class TripInDb
    {
        public ObjectId TripId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public ObjectId LeaderId { get; set; }
        public int Capacity { get; set; }

        public TripInDb()
        {
            
        }
    }

    public class TripTrans
    {
        public string TripId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string LeaderId { get; set; }
        public List<string> FellowIds { get; set; }
        public List<KeyValuePair<string,string>> Destinations { get; set; }
        public int Capacity { get; set; }

        public TripTrans()
        {
            
        }
    }
}

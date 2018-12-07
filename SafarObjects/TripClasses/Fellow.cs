using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;


namespace SafarObjects.TripClasses
{
    public class Fellow
    {
        public ObjectId TripId { get; set; }
        public ObjectId UserId { get; set; }
        public FellowType FellowType { get; set; }
        public FellowStatus FellowStatus { get; set; }

        public Fellow()
        {
            
        }
    }

    public class FellowInDb
    {
        [BsonId]
        public ObjectId id { get; set; }
        public ObjectId TripId { get; set; }
        public ObjectId UserId { get; set; }
        public int FellowType { get; set; }
        public int FellowStatus { get; set; }

        public FellowInDb()
        {
            
        }
    }

    public enum FellowType
    {
        Type1 = 0,
        Type2 = 1
    }

    public enum FellowStatus
    {
        Status1 = 0,
        Status2 = 1
    }
}

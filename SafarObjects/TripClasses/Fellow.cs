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
        [BsonId]
        public ObjectId _id { get; set; }
        public string TripId { get; set; }
        public string UserId { get; set; }
        public FellowType FellowType { get; set; }
        public FellowStatus FellowStatus { get; set; }

        public Fellow()
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

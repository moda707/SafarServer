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
        public string TripId { get; set; }
        public string UserId { get; set; }
        public FellowType FellowType { get; set; }
        public FellowStatus FellowStatus { get; set; }

        public Fellow()
        {
            
        }

        public FellowInDb GetFellowInDb()
        {
            return new FellowInDb()
            {
                UserId = this.UserId,
                TripId = this.TripId,
                FellowStatus = this.FellowStatus,
                FellowType = this.FellowType
            };
        }
    }

    public class FellowInDb:Fellow
    {
        [BsonId]
        public ObjectId _id { get; set; }
        

        public FellowInDb()
        {

        }

        public Fellow GetFellow()
        {
            return new Fellow()
            {
                UserId = this.UserId,
                TripId = this.TripId,
                FellowStatus = this.FellowStatus,
                FellowType = this.FellowType
            };
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using SafarCore.DbClasses;

namespace SafarCore.GenFunctions
{
    public class Logger
    {
        [BsonId]
        public ObjectId Id { get; set; }
        public ObjectId UserId { get; set; }
        public string Classname { get; set; }
        public string Functionname { get; set; }
        public string Message { get; set; }
        public DateTime ErrorTime { get; set; }

        public Logger()
        {

        }

        public Logger(ObjectId userId, string classname, string functionname, string message)
        {
            UserId = userId;
            Classname = classname;
            Functionname = functionname;
            Message = message;
            ErrorTime = DateTime.Now;
        }

        public FuncResult AddLog()
        {
            var dbConnection = new DbConnection();
            

            return dbConnection.AddorUpdate(this.ToBsonDocument(),
                dbConnection.GetMongoCollection(CollectionNames.Logs));
        }

        public static List<Logger> GetLogs(int count)
        {
            var dbConnection = new DbConnection();
            

            var col = dbConnection.GetFilteredList<Logger>(CollectionNames.Logs, null)
                .OrderByDescending(x => x.ErrorTime).Take(count).ToList();
            return col;
        }
    }
}

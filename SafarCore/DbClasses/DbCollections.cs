using System;
using System.Collections.Generic;
using System.Text;

namespace SafarCore.DbClasses
{
    public class Collections
    {
        public Collections()
        {

        }

        //retruns the exact name of collection in Db
        public static string GetCollectionName(CollectionNames collectionNames)
        {
            switch (collectionNames)
            {
                case CollectionNames.User:
                    return "User";
                case CollectionNames.Trip:
                    return "Trip";
                case CollectionNames.Chats:
                    return "Chats";
                case CollectionNames.Timelines:
                    return "Timelines";
                case CollectionNames.Destinations:
                    return "Destinations";
                case CollectionNames.Locations:
                    return "Locations";
                case CollectionNames.Friends:
                    return "Friends";
                
                case CollectionNames.Logs:
                    return "Logs";

                case CollectionNames.ApiLogger:
                    return "ApiLogger";

                default:
                    return "";
            }


        }


    }

    //List of all collections we have in Db
    public enum CollectionNames
    {
        User = 0,
        Trip = 1,

        Chats = 2,
        Timelines = 3,
        Destinations = 4,
        Locations = 5,
        Fellows = 6,

        Friends = 7,
        
        Logs = 100,
        
        ApiLogger = 200
    }
}

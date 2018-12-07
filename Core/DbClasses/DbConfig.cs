using System;
using System.Collections.Generic;
using System.Text;

namespace SafarCore.DbClasses
{
    public class DbConfig
    {
        private const string UserName = "moda_admin";
        private const string Password = "177282523";
        private const string ConnectionName = "";
        private const string ConnectionString = "mongodb://127.0.0.1:27017";
        public static string GetConnectionString()
        {
            return ConnectionString;
        }
    }
}

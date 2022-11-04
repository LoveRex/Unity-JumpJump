using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts
{
    class MyDocument
    {
        public object _id { get; set; }
        public string name { get; set; }
    }
    internal class DBHelper
    {
        public void initDB()
        {
            // Replace the uri string with your MongoDB deployment's connection string.
            var client = new MongoClient(
                "mongodb://localhost:27017/"
            );
            var database = client.GetDatabase("BT");
            var collect = database.GetCollection<MyDocument>("user");

            var filter = Builders<MyDocument>.Filter.Empty;
            var result = collect.Find(filter).ToList();
        }
    }
}

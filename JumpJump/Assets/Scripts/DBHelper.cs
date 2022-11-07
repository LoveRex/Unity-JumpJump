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
        private static DBHelper instance;
        public static DBHelper Instance
        {
            get { return instance ?? (instance = new DBHelper()); }
        }

        private IMongoDatabase database
        {
            get { return client.GetDatabase("BT"); }
        }

        private static MongoClient _client;
        private static MongoClient client
        {
            get { return _client ?? (_client = new MongoClient("mongodb://localhost:27017/")); }
        }


        /// <summary>
        /// 获取collection表
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private IMongoCollection<BsonDocument> getCollection(string name)
        {
            return database.GetCollection<BsonDocument>(name);
        }

        /// <summary>
        /// 获取表中所有信息
        /// </summary>
        /// <param name="str_collection">表名称</param>
        public List<BsonDocument> getAllInfo(string str_collection)
        {
            var collection = getCollection(str_collection);
            var filter = Builders<BsonDocument>.Filter.Empty;
            var sort = Builders<BsonDocument>.Sort;
            var result = collection.Find(filter).Sort(sort.Descending("score")).ToList();
            return result;
        }

        /// <summary>
        /// 插入记录
        /// </summary>
        /// <param name="str_collection"></param>
        /// <param name="document"></param>
        public void insertDB(string str_collection, BsonDocument document)
        {
            var collection = getCollection(str_collection);
            collection.InsertOne(document);
        }

        /// <summary>
        /// 查询是否存在，存在即更新，不存在插入
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="str_collection"></param>
        /// <param name="q_name"></param>
        /// <param name="q_value"></param>
        /// <param name="u_name"></param>
        /// <param name="u_value"></param>
        /// <param name="document"></param>
        public void selectAndUpdateDB<T>(string str_collection, string q_name, T q_value, string u_name, T u_value, BsonDocument document)
        {
            var collection = getCollection(str_collection);
            var q_filter = Builders<BsonDocument>.Filter.Eq(q_name, q_value);
            var q_result = collection.Find(q_filter).ToList();
            if (q_result.Count > 0)
            {
                var u_filter = Builders<BsonDocument>.Filter;
                var update = Builders<BsonDocument>.Update;
                var project = Builders<BsonDocument>.Projection;
                collection.UpdateOne(u_filter.Eq(q_name, q_value), update.Set(u_name, u_value));
            }
            else
            {
                insertDB(str_collection, document);
            }


            
        }

    }
}

using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace ServiceQuery.Xunit
{
    public class MongoDbHelper
    {
        public static string GetConnectionString()
        {
            var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            return config.GetConnectionString("MongoDb");
        }

        public static IQueryable<MongoDbTestClass> GetTestList()
        {
            MongoClient client = new MongoClient(GetConnectionString());
            var db = client.GetDatabase("NewServiceQueryIntegrationTest");
            var collection = db.GetCollection<MongoDbTestClass>("ServiceQueryTestClasses");
            var list = collection.AsQueryable().ToList();
            if (list.Count() != 4)
            {
                list = new MongoDbTestClass().GetDefaultListM();
                foreach (var item in list)
                    collection.InsertOne(item);
            }
            return collection.AsQueryable();
        }
    }
}
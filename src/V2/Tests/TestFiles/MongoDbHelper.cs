using Microsoft.EntityFrameworkCore;
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

            var list = new MongoDbTestClass().GetDefaultList();
            var templist = collection.AsQueryable().ToList();

            // Delete previous records
            if (templist.Count > 0)
            {
                foreach (var item in templist)
                    collection.DeleteOne(x => x.MongoKeyString == item.MongoKeyString);
            }

            // Add new records
            foreach (var item in list)
            {
                var temp = (MongoDbTestClass)item;
                item.DatabaseKey = 0;
                collection.InsertOne(temp);
            }

            return collection.AsQueryable();
        }

        public static IQueryable<MongoDbTestClass> GetTestNullCopyList()
        {
            MongoClient client = new MongoClient(GetConnectionString());
            var db = client.GetDatabase("NewServiceQueryIntegrationTest");
            var collection = db.GetCollection<MongoDbTestClass>("ServiceQueryTestClasses");

            var list = new MongoDbTestClass().GetDefaultList();
            var templist = collection.AsQueryable().ToList();

            // Delete previous records
            if (templist.Count > 0)
            {
                foreach (var item in templist)
                    collection.DeleteOne(x => x.MongoKeyString == item.MongoKeyString);
            }

            // Add new records
            foreach (var item in list)
            {
                var temp = (MongoDbTestClass)item;
                item.CopyToNullVals();
                collection.InsertOne(temp);
            }

            return collection.AsQueryable();
        }
    }
}
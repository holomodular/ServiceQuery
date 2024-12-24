using Microsoft.Extensions.Configuration;
using MongoDB.Bson.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace ServiceQuery.Xunit.Integration
{
    public class MongoDbHelper
    {
        public static bool MongoDbInitialized = false;

        public static string GetConnectionString()
        {
            var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            return config.GetConnectionString("MongoDb");
        }

        public static IQueryable<MongoDbTestClass> GetTestList()
        {
            if (!MongoDbInitialized)
            {
                MongoDbInitialized = true;
                BsonSerializer.RegisterSerializer(new GuidSerializer(GuidRepresentation.Standard));
            }

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
            if (!MongoDbInitialized)
            {
                MongoDbInitialized = true;
                BsonSerializer.RegisterSerializer(new GuidSerializer(GuidRepresentation.Standard));
            }

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

        public static async Task<IQueryable<MongoDbTestClass>> GetTestListAsync()
        {
            if (!MongoDbInitialized)
            {
                MongoDbInitialized = true;
                BsonSerializer.RegisterSerializer(new GuidSerializer(GuidRepresentation.Standard));
            }

            MongoClient client = new MongoClient(GetConnectionString());
            var db = client.GetDatabase("NewServiceQueryIntegrationTest");
            var collection = db.GetCollection<MongoDbTestClass>("ServiceQueryTestClasses");

            var list = new MongoDbTestClass().GetDefaultList();
            var templist = await collection.AsQueryable().ToListAsync();

            // Delete previous records
            if (templist.Count > 0)
            {
                foreach (var item in templist)
                    await collection.DeleteOneAsync(x => x.MongoKeyString == item.MongoKeyString);
            }

            // Add new records
            foreach (var item in list)
            {
                var temp = (MongoDbTestClass)item;
                item.DatabaseKey = 0;
                await collection.InsertOneAsync(temp);
            }

            return collection.AsQueryable();
        }

        public static async Task<IQueryable<MongoDbTestClass>> GetTestNullCopyListAsync()
        {
            if (!MongoDbInitialized)
            {
                MongoDbInitialized = true;
                BsonSerializer.RegisterSerializer(new GuidSerializer(GuidRepresentation.Standard));
            }

            MongoClient client = new MongoClient(GetConnectionString());
            var db = client.GetDatabase("NewServiceQueryIntegrationTest");
            var collection = db.GetCollection<MongoDbTestClass>("ServiceQueryTestClasses");

            var list = new MongoDbTestClass().GetDefaultList();
            var templist = await collection.AsQueryable().ToListAsync();

            // Delete previous records
            if (templist.Count > 0)
            {
                foreach (var item in templist)
                    await collection.DeleteOneAsync(x => x.MongoKeyString == item.MongoKeyString);
            }

            // Add new records
            foreach (var item in list)
            {
                var temp = (MongoDbTestClass)item;
                item.CopyToNullVals();
                await collection.InsertOneAsync(temp);
            }

            return collection.AsQueryable();
        }
    }
}
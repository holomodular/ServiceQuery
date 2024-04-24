using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ServiceQuery.Xunit.Integration
{
    public class MongoDbTestClass : TestClass
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string MongoKeyString { get; set; }

        public override List<TestClass> GetDefaultList()
        {
            //added out of order
            return new List<TestClass>()
            {
                (MongoDbTestClass)GetDefault3Record(new MongoDbTestClass()),
                (MongoDbTestClass)GetDefault0Record(new MongoDbTestClass()),
                (MongoDbTestClass)GetDefault2Record(new MongoDbTestClass()),
                (MongoDbTestClass)GetDefault1Record(new MongoDbTestClass()),
            };
        }
    }
}
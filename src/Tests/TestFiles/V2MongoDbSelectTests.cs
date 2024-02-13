namespace ServiceQuery.Xunit
{
    public class MongoDbSelectTests : LinqAsyncSelectTests<MongoDbTestClass>
    {
        public override IQueryable<MongoDbTestClass> GetTestList()
        {
            return MongoDbHelper.GetTestList();
        }
    }
}
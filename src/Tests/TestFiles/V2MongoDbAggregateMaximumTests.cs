namespace ServiceQuery.Xunit
{
    public class MongoDbAggregateMaximumTests : LinqAsyncAggregateMaximumTests<MongoDbTestClass>
    {
        public override IQueryable<MongoDbTestClass> GetTestList()
        {
            return MongoDbHelper.GetTestList();
        }
    }
}
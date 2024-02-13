namespace ServiceQuery.Xunit
{
    public class MongoDbAggregateMinimumTests : LinqAsyncAggregateMinimumTests<MongoDbTestClass>
    {
        public override IQueryable<MongoDbTestClass> GetTestList()
        {
            return MongoDbHelper.GetTestList();
        }
    }
}
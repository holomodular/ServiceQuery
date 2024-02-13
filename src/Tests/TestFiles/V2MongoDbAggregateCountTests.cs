namespace ServiceQuery.Xunit
{
    public class MongoDbAggregateCountTests : LinqAsyncAggregateCountTests<MongoDbTestClass>
    {
        public override IQueryable<MongoDbTestClass> GetTestList()
        {
            return MongoDbHelper.GetTestList();
        }
    }
}
namespace ServiceQuery.Xunit
{
    public class MongoDbAggregateMinimumTests : AggregateMinimumTests<MongoDbTestClass>
    {
        public override IQueryable<MongoDbTestClass> GetTestList()
        {
            return MongoDbHelper.GetTestList();
        }
    }
}
namespace ServiceQuery.Xunit
{
    public class MongoDbAggregateMaximumTests : AggregateMaximumTests<MongoDbTestClass>
    {
        public override IQueryable<MongoDbTestClass> GetTestList()
        {
            return MongoDbHelper.GetTestList();
        }
    }
}
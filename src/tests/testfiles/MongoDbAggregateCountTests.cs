namespace ServiceQuery.Xunit
{
    public class MongoDbAggregateCountTests : AggregateCountTests<MongoDbTestClass>
    {
        public override IQueryable<MongoDbTestClass> GetTestList()
        {
            return MongoDbHelper.GetTestList();
        }
    }
}
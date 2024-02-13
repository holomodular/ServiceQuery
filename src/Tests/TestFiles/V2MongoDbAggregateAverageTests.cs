namespace ServiceQuery.Xunit
{
    public class MongoDbAggregateAverageTests : LinqAsyncAggregateAverageTests<MongoDbTestClass>
    {
        public MongoDbAggregateAverageTests()
        {
            ValidateDecimal = false;
            ValidateNullLong = false;
        }

        public override IQueryable<MongoDbTestClass> GetTestList()
        {
            return MongoDbHelper.GetTestList();
        }
    }
}
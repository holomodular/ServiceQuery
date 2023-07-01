namespace ServiceQuery.Xunit
{
    public class MongoDbAggregateAverageTests : AggregateAverageTests<MongoDbTestClass>
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
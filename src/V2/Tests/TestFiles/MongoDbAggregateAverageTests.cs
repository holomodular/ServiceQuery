namespace ServiceQuery.Xunit
{
    [Collection("MongoDb")]
    public class MongoDbAggregateAverageTests : AggregateAverageTests<MongoDbTestClass>
    {
        public MongoDbAggregateAverageTests()
        {
            ValidateDecimal = false;
            ValidateNullLong = false;
            ValidateUInt128 = false;
        }

        public override IQueryable<MongoDbTestClass> GetTestList()
        {
            return MongoDbHelper.GetTestList();
        }

        public override IQueryable<MongoDbTestClass> GetTestNullCopyList()
        {
            return MongoDbHelper.GetTestNullCopyList();
        }
    }
}
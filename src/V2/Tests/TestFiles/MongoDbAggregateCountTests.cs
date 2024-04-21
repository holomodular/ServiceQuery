namespace ServiceQuery.Xunit
{
    [Collection("MongoDb")]
    public class MongoDbAggregateCountTests : AggregateCountTests<MongoDbTestClass>
    {
        public MongoDbAggregateCountTests()
        {
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
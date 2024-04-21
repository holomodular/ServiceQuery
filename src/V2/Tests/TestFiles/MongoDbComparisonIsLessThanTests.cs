namespace ServiceQuery.Xunit
{
    [Collection("MongoDb")]
    public class MongoDbComparisonIsLessThanTests : ComparisonIsLessThanTests<MongoDbTestClass>
    {
        public MongoDbComparisonIsLessThanTests()
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
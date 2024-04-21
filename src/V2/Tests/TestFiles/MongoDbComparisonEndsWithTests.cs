namespace ServiceQuery.Xunit
{
    [Collection("MongoDb")]
    public class MongoDbComparisonEndsWithTests : ComparisonEndsWithTests<MongoDbTestClass>
    {
        public MongoDbComparisonEndsWithTests()
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
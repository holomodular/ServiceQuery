namespace ServiceQuery.Xunit
{
    [Collection("MongoDb")]
    public class MongoDbComparisonIsNotEqualTests : ComparisonIsNotEqualTests<MongoDbTestClass>
    {
        public MongoDbComparisonIsNotEqualTests()
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
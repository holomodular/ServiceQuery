namespace ServiceQuery.Xunit
{
    [Collection("MongoDb")]
    public class MongoDbDistinctTests : DistinctTests<MongoDbTestClass>
    {
        public MongoDbDistinctTests()
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
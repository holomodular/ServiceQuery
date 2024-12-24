namespace ServiceQuery.Xunit.Integration
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

        public override async Task<IQueryable<MongoDbTestClass>> GetTestListAsync()
        {
            return await MongoDbHelper.GetTestListAsync();
        }

        public override async Task<IQueryable<MongoDbTestClass>> GetTestNullCopyListAsync()
        {
            return await MongoDbHelper.GetTestNullCopyListAsync();
        }
    }

    [Collection("MongoDb")]
    public class MongoDbComparisonIsLessThanTestsAsync : ComparisonIsLessThanTestsAsyncMongoDb<MongoDbTestClass>
    {
        public MongoDbComparisonIsLessThanTestsAsync()
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

        public override async Task<IQueryable<MongoDbTestClass>> GetTestListAsync()
        {
            return await MongoDbHelper.GetTestListAsync();
        }

        public override async Task<IQueryable<MongoDbTestClass>> GetTestNullCopyListAsync()
        {
            return await MongoDbHelper.GetTestNullCopyListAsync();
        }
    }
}
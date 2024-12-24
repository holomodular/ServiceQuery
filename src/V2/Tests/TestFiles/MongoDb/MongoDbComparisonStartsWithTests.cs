namespace ServiceQuery.Xunit.Integration
{
    [Collection("MongoDb")]
    public class MongoDbComparisonStartsWithTests : ComparisonStartsWithTests<MongoDbTestClass>
    {
        public MongoDbComparisonStartsWithTests()
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
    public class MongoDbComparisonStartsWithTestsAsync : ComparisonStartsWithTestsAsyncMongoDb<MongoDbTestClass>
    {
        public MongoDbComparisonStartsWithTestsAsync()
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
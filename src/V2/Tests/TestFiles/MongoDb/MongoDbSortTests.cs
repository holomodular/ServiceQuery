namespace ServiceQuery.Xunit.Integration
{
    [Collection("MongoDb")]
    public class MongoDbSortTests : SortTests<MongoDbTestClass>
    {
        public MongoDbSortTests()
        {
            ValidateUInt128 = false;
            ValidateDateTimeOffset = false;
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
    public class MongoDbSortTestsAsync : SortTestsAsyncMongoDb<MongoDbTestClass>
    {
        public MongoDbSortTestsAsync()
        {
            ValidateUInt128 = false;
            ValidateDateTimeOffset = false;
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
namespace ServiceQuery.Xunit.Integration
{
    [Collection("MongoDb")]
    public class MongoDbAggregateSumTests : AggregateSumTests<MongoDbTestClass>
    {
        public MongoDbAggregateSumTests()
        {
            ValidateDecimal = false;
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
    public class MongoDbAggregateSumTestsAsync : AggregateSumTestsAsyncMongoDb<MongoDbTestClass>
    {
        public MongoDbAggregateSumTestsAsync()
        {
            ValidateDecimal = false;
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
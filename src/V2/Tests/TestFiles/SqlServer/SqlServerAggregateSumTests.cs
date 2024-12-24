namespace ServiceQuery.Xunit.Integration
{
    [Collection("SqlServer")]
    public class SqlServerAggregateSumTests : AggregateSumTests<TestClass>
    {
        public SqlServerAggregateSumTests()
        {
            ValidateUInt128 = false;
        }

        public override IQueryable<TestClass> GetTestList()
        {
            return SqlServerHelper.GetTestList();
        }

        public override IQueryable<TestClass> GetTestNullCopyList()
        {
            return SqlServerHelper.GetTestNullCopyList();
        }

        public override async Task<IQueryable<TestClass>> GetTestListAsync()
        {
            return await SqlServerHelper.GetTestListAsync();
        }

        public override async Task<IQueryable<TestClass>> GetTestNullCopyListAsync()
        {
            return await SqlServerHelper.GetTestNullCopyListAsync();
        }
    }

    [Collection("SqlServer")]
    public class SqlServerAggregateSumTestsAsync : AggregateSumTestsAsyncEfc<TestClass>
    {
        public SqlServerAggregateSumTestsAsync()
        {
            ValidateUInt128 = false;
        }

        public override IQueryable<TestClass> GetTestList()
        {
            return SqlServerHelper.GetTestList();
        }

        public override IQueryable<TestClass> GetTestNullCopyList()
        {
            return SqlServerHelper.GetTestNullCopyList();
        }

        public override async Task<IQueryable<TestClass>> GetTestListAsync()
        {
            return await SqlServerHelper.GetTestListAsync();
        }

        public override async Task<IQueryable<TestClass>> GetTestNullCopyListAsync()
        {
            return await SqlServerHelper.GetTestNullCopyListAsync();
        }
    }
}
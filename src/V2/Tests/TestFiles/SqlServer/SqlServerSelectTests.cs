namespace ServiceQuery.Xunit.Integration
{
    [Collection("SqlServer")]
    public class SqlServerSelectTests : SelectTests<TestClass>
    {
        public SqlServerSelectTests()
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
    public class SqlServerSelectTestsAsync : SelectTestsAsyncEfc<TestClass>
    {
        public SqlServerSelectTestsAsync()
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
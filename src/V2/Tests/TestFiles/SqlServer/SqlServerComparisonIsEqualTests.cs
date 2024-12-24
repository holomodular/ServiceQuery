namespace ServiceQuery.Xunit.Integration
{
    [Collection("SqlServer")]
    public class SqlServerComparisonIsEqualTests : ComparisonIsEqualTests<TestClass>
    {
        public SqlServerComparisonIsEqualTests()
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
    public class SqlServerComparisonIsEqualTestsAsync : ComparisonIsEqualTestsAsyncEfc<TestClass>
    {
        public SqlServerComparisonIsEqualTestsAsync()
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
namespace ServiceQuery.Xunit
{
    [Collection("InMemory")]
    public class SqlServerInMemComparisonIsNotEqualTests : ComparisonIsNotEqualTests<TestClass>
    {
        public SqlServerInMemComparisonIsNotEqualTests()
        {
        }

        public override IQueryable<TestClass> GetTestList()
        {
            return SqlServerInMemHelper.GetTestList();
        }

        public override IQueryable<TestClass> GetTestNullCopyList()
        {
            return SqlServerInMemHelper.GetTestNullCopyList();
        }

        public override async Task<IQueryable<TestClass>> GetTestListAsync()
        {
            return await SqlServerInMemHelper.GetTestListAsync();
        }

        public override async Task<IQueryable<TestClass>> GetTestNullCopyListAsync()
        {
            return await SqlServerInMemHelper.GetTestNullCopyListAsync();
        }
    }

    [Collection("InMemory")]
    public class SqlServerInMemComparisonIsNotEqualTestsAsync : ComparisonIsNotEqualTestsAsyncEfc<TestClass>
    {
        public SqlServerInMemComparisonIsNotEqualTestsAsync()
        {
        }

        public override IQueryable<TestClass> GetTestList()
        {
            return SqlServerInMemHelper.GetTestList();
        }

        public override IQueryable<TestClass> GetTestNullCopyList()
        {
            return SqlServerInMemHelper.GetTestNullCopyList();
        }

        public override async Task<IQueryable<TestClass>> GetTestListAsync()
        {
            return await SqlServerInMemHelper.GetTestListAsync();
        }

        public override async Task<IQueryable<TestClass>> GetTestNullCopyListAsync()
        {
            return await SqlServerInMemHelper.GetTestNullCopyListAsync();
        }
    }
}
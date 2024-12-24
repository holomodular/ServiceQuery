namespace ServiceQuery.Xunit
{
    [Collection("InMemory")]
    public class SqlServerInMemSortTests : SortTests<TestClass>
    {
        public SqlServerInMemSortTests()
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
    public class SqlServerInMemSortTestsAsync : SortTestsAsyncEfc<TestClass>
    {
        public SqlServerInMemSortTestsAsync()
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
namespace ServiceQuery.Xunit
{
    [Collection("InMemory")]
    public class SqlServerInMemExpressionTests : ExpressionTests<TestClass>
    {
        public SqlServerInMemExpressionTests()
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
    public class SqlServerInMemExpressionTestsAsync : ExpressionTestsAsyncEfc<TestClass>
    {
        public SqlServerInMemExpressionTestsAsync()
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
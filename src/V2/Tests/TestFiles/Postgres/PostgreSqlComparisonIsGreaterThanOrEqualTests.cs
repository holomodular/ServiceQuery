namespace ServiceQuery.Xunit.Integration
{
    [Collection("Postgres")]
    public class PostgreSqlComparisonIsGreaterThanOrEqualTests : ComparisonIsGreaterThanOrEqualTests<TestClass>
    {
        public PostgreSqlComparisonIsGreaterThanOrEqualTests()
        {
            ValidateUInt128 = false;
        }

        public override IQueryable<TestClass> GetTestList()
        {
            return PostgreSqlHelper.GetTestList();
        }

        public override IQueryable<TestClass> GetTestNullCopyList()
        {
            return PostgreSqlHelper.GetTestNullCopyList();
        }

        public override async Task<IQueryable<TestClass>> GetTestListAsync()
        {
            return await PostgreSqlHelper.GetTestListAsync();
        }

        public override async Task<IQueryable<TestClass>> GetTestNullCopyListAsync()
        {
            return await PostgreSqlHelper.GetTestNullCopyListAsync();
        }
    }

    [Collection("Postgres")]
    public class PostgreSqlComparisonIsGreaterThanOrEqualTestsAsync : ComparisonIsGreaterThanOrEqualTestsAsyncEfc<TestClass>
    {
        public PostgreSqlComparisonIsGreaterThanOrEqualTestsAsync()
        {
            ValidateUInt128 = false;
        }

        public override IQueryable<TestClass> GetTestList()
        {
            return PostgreSqlHelper.GetTestList();
        }

        public override IQueryable<TestClass> GetTestNullCopyList()
        {
            return PostgreSqlHelper.GetTestNullCopyList();
        }

        public override async Task<IQueryable<TestClass>> GetTestListAsync()
        {
            return await PostgreSqlHelper.GetTestListAsync();
        }

        public override async Task<IQueryable<TestClass>> GetTestNullCopyListAsync()
        {
            return await PostgreSqlHelper.GetTestNullCopyListAsync();
        }
    }
}
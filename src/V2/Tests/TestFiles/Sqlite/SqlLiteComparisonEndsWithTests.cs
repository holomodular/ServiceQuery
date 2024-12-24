namespace ServiceQuery.Xunit.Integration
{
    [Collection("Sqlite")]
    public class SqlLiteComparisonEndsWithTests : ComparisonEndsWithTests<TestClass>
    {
        public SqlLiteComparisonEndsWithTests()
        {
            ValidateDateTimeOffset = false;
            ValidateTimeSpan = false;
            ValidateUInt128 = false;
            ValidateUInt64 = false;
        }

        public override IQueryable<TestClass> GetTestList()
        {
            return SqlLiteHelper.GetTestList();
        }

        public override IQueryable<TestClass> GetTestNullCopyList()
        {
            return SqlLiteHelper.GetTestNullCopyList();
        }

        public override async Task<IQueryable<TestClass>> GetTestListAsync()
        {
            return await SqlLiteHelper.GetTestListAsync();
        }

        public override async Task<IQueryable<TestClass>> GetTestNullCopyListAsync()
        {
            return await SqlLiteHelper.GetTestNullCopyListAsync();
        }
    }

    [Collection("Sqlite")]
    public class SqlLiteComparisonEndsWithTestsAsync : ComparisonEndsWithTestsAsyncEfc<TestClass>
    {
        public SqlLiteComparisonEndsWithTestsAsync()
        {
            ValidateDateTimeOffset = false;
            ValidateTimeSpan = false;
            ValidateUInt128 = false;
            ValidateUInt64 = false;
        }

        public override IQueryable<TestClass> GetTestList()
        {
            return SqlLiteHelper.GetTestList();
        }

        public override IQueryable<TestClass> GetTestNullCopyList()
        {
            return SqlLiteHelper.GetTestNullCopyList();
        }

        public override async Task<IQueryable<TestClass>> GetTestListAsync()
        {
            return await SqlLiteHelper.GetTestListAsync();
        }

        public override async Task<IQueryable<TestClass>> GetTestNullCopyListAsync()
        {
            return await SqlLiteHelper.GetTestNullCopyListAsync();
        }
    }
}
namespace ServiceQuery.Xunit
{
    [Collection("Sqlite")]
    public class SqlLiteComparisonIsNotEqualTests : ComparisonIsNotEqualTests<TestClass>
    {
        public SqlLiteComparisonIsNotEqualTests()
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
    }
}
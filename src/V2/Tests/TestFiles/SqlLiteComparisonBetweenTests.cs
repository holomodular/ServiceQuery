namespace ServiceQuery.Xunit
{
    [Collection("Sqlite")]
    public class SqlLiteComparisonBetweenTests : ComparisonBetweenTests<TestClass>
    {
        public SqlLiteComparisonBetweenTests()
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
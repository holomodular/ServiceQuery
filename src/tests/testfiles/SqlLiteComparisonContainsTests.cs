namespace ServiceQuery.Xunit
{
    public class SqlLiteComparisonContainsTests : ComparisonContainsTests<TestClass>
    {
        public SqlLiteComparisonContainsTests()
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
    }
}
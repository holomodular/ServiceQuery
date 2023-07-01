namespace ServiceQuery.Xunit
{
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
    }
}
namespace ServiceQuery.Xunit
{
    public class SqlLiteComparisonIsLessThanTests : ComparisonIsLessThanTests<TestClass>
    {
        public SqlLiteComparisonIsLessThanTests()
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
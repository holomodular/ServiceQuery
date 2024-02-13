namespace ServiceQuery.Xunit
{
    public class SqlLiteComparisonIsGreaterThanTests : LinqAsyncComparisonIsGreaterThanTests<TestClass>
    {
        public SqlLiteComparisonIsGreaterThanTests()
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
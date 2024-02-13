namespace ServiceQuery.Xunit
{
    public class SqlLiteComparisonStartsWithTests : LinqAsyncComparisonStartsWithTests<TestClass>
    {
        public SqlLiteComparisonStartsWithTests()
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
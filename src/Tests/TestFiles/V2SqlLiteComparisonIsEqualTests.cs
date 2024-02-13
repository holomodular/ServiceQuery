namespace ServiceQuery.Xunit
{
    public class SqlLiteComparisonIsEqualTests : LinqAsyncComparisonIsEqualTests<TestClass>
    {
        public SqlLiteComparisonIsEqualTests()
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
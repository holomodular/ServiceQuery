namespace ServiceQuery.Xunit
{
    public class SqlLiteComparisonIsLessThanOrEqualTests : LinqAsyncComparisonIsLessThanOrEqualTests<TestClass>
    {
        public SqlLiteComparisonIsLessThanOrEqualTests()
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
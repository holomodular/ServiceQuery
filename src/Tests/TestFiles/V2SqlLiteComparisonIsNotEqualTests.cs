namespace ServiceQuery.Xunit
{
    public class SqlLiteComparisonIsNotEqualTests : LinqAsyncComparisonIsNotEqualTests<TestClass>
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
    }
}
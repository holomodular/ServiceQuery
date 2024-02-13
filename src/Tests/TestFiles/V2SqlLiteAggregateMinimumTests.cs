namespace ServiceQuery.Xunit
{
    public class SqlLiteAggregateMinimumTests : LinqAsyncAggregateMinimumTests<TestClass>
    {
        public SqlLiteAggregateMinimumTests()
        {
            ValidateDateTimeOffset = false;
            ValidateTimeSpan = false;
            ValidateUInt128 = false;
            ValidateUInt64 = false;
            ValidateDecimal = false;
        }

        public override IQueryable<TestClass> GetTestList()
        {
            return SqlLiteHelper.GetTestList();
        }
    }
}
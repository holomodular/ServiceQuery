namespace ServiceQuery.Xunit
{
    public class SqlLiteAggregateCountTests : LinqAsyncAggregateCountTests<TestClass>
    {
        public SqlLiteAggregateCountTests()
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
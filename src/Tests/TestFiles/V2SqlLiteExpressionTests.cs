namespace ServiceQuery.Xunit
{
    public class SqlLiteExpressionTests : LinqAsyncExpressionTests<TestClass>
    {
        public SqlLiteExpressionTests()
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
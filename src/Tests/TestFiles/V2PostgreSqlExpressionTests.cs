namespace ServiceQuery.Xunit
{
    public class PostgreSqlExpressionTests : LinqAsyncExpressionTests<TestClass>
    {
        public PostgreSqlExpressionTests()
        {
            ValidateUInt128 = false;
        }

        public override IQueryable<TestClass> GetTestList()
        {
            return PostgreSqlHelper.GetTestList();
        }
    }
}
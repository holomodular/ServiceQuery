namespace ServiceQuery.Xunit
{
    public class PostgreSqlExpressionTests : ExpressionTests<TestClass>
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
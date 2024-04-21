namespace ServiceQuery.Xunit
{
    [Collection("Postgres")]
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

        public override IQueryable<TestClass> GetTestNullCopyList()
        {
            return PostgreSqlHelper.GetTestNullCopyList();
        }
    }
}
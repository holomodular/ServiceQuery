namespace ServiceQuery.Xunit
{
    public class SqlServerInMemExpressionTests : ExpressionTests<TestClass>
    {
        public SqlServerInMemExpressionTests()
        {
        }

        public override IQueryable<TestClass> GetTestList()
        {
            return SqlServerInMemHelper.GetTestList();
        }
    }
}
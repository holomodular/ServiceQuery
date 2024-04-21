namespace ServiceQuery.Xunit
{
    [Collection("InMemory")]
    public class SqlServerInMemExpressionTests : ExpressionTests<TestClass>
    {
        public SqlServerInMemExpressionTests()
        {
        }

        public override IQueryable<TestClass> GetTestList()
        {
            return SqlServerInMemHelper.GetTestList();
        }

        public override IQueryable<TestClass> GetTestNullCopyList()
        {
            return SqlServerInMemHelper.GetTestNullCopyList();
        }
    }
}
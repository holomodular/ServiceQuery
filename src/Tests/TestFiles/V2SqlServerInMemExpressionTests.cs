namespace ServiceQuery.Xunit
{
    public class SqlServerInMemExpressionTests : LinqAsyncExpressionTests<TestClass>
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
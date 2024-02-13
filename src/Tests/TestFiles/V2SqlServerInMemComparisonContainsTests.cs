namespace ServiceQuery.Xunit
{
    public class SqlServerInMemComparisonContainsTests : LinqAsyncComparisonContainsTests<TestClass>
    {
        public SqlServerInMemComparisonContainsTests()
        {
        }

        public override IQueryable<TestClass> GetTestList()
        {
            return SqlServerInMemHelper.GetTestList();
        }
    }
}
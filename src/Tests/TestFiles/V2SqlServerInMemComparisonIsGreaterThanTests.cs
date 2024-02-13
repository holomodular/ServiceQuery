namespace ServiceQuery.Xunit
{
    public class SqlServerInMemComparisonIsGreaterThanTests : LinqAsyncComparisonIsGreaterThanTests<TestClass>
    {
        public SqlServerInMemComparisonIsGreaterThanTests()
        {
        }

        public override IQueryable<TestClass> GetTestList()
        {
            return SqlServerInMemHelper.GetTestList();
        }
    }
}
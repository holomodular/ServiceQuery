namespace ServiceQuery.Xunit
{
    public class SqlServerInMemComparisonIsGreaterThanOrEqualTests : LinqAsyncComparisonIsGreaterThanOrEqualTests<TestClass>
    {
        public SqlServerInMemComparisonIsGreaterThanOrEqualTests()
        {
        }

        public override IQueryable<TestClass> GetTestList()
        {
            return SqlServerInMemHelper.GetTestList();
        }
    }
}
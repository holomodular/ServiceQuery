namespace ServiceQuery.Xunit
{
    public class SqlServerInMemComparisonIsLessThanTests : LinqAsyncComparisonIsLessThanTests<TestClass>
    {
        public SqlServerInMemComparisonIsLessThanTests()
        {
        }

        public override IQueryable<TestClass> GetTestList()
        {
            return SqlServerInMemHelper.GetTestList();
        }
    }
}
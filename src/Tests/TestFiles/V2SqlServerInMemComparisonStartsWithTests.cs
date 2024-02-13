namespace ServiceQuery.Xunit
{
    public class SqlServerInMemComparisonStartsWithTests : LinqAsyncComparisonStartsWithTests<TestClass>
    {
        public SqlServerInMemComparisonStartsWithTests()
        {
        }

        public override IQueryable<TestClass> GetTestList()
        {
            return SqlServerInMemHelper.GetTestList();
        }
    }
}
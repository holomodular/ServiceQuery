namespace ServiceQuery.Xunit
{
    public class SqlServerInMemComparisonEndsWithTests : LinqAsyncComparisonEndsWithTests<TestClass>
    {
        public SqlServerInMemComparisonEndsWithTests()
        {
        }

        public override IQueryable<TestClass> GetTestList()
        {
            return SqlServerInMemHelper.GetTestList();
        }
    }
}
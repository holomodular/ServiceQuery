namespace ServiceQuery.Xunit
{
    public class SqlServerInMemComparisonIsEqualTests : LinqAsyncComparisonIsEqualTests<TestClass>
    {
        public SqlServerInMemComparisonIsEqualTests()
        {
        }

        public override IQueryable<TestClass> GetTestList()
        {
            return SqlServerInMemHelper.GetTestList();
        }
    }
}
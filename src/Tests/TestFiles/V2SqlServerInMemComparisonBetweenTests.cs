namespace ServiceQuery.Xunit
{
    public class SqlServerInMemComparisonBetweenTests : LinqAsyncComparisonBetweenTests<TestClass>
    {
        public SqlServerInMemComparisonBetweenTests()
        {
        }

        public override IQueryable<TestClass> GetTestList()
        {
            return SqlServerInMemHelper.GetTestList();
        }
    }
}
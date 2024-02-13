namespace ServiceQuery.Xunit
{
    public class SqlServerInMemAggregateAverageTests : LinqAsyncAggregateAverageTests<TestClass>
    {
        public SqlServerInMemAggregateAverageTests()
        {
        }

        public override IQueryable<TestClass> GetTestList()
        {
            return SqlServerInMemHelper.GetTestList();
        }
    }
}
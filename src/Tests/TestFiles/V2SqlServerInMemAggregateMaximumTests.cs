namespace ServiceQuery.Xunit
{
    public class SqlServerInMemAggregateMaximumTests : LinqAsyncAggregateMaximumTests<TestClass>
    {
        public SqlServerInMemAggregateMaximumTests()
        {
        }

        public override IQueryable<TestClass> GetTestList()
        {
            return SqlServerInMemHelper.GetTestList();
        }
    }
}
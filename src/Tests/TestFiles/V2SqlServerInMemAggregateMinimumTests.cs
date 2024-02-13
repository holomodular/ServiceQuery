namespace ServiceQuery.Xunit
{
    public class SqlServerInMemAggregateMinimumTests : LinqAsyncAggregateMinimumTests<TestClass>
    {
        public SqlServerInMemAggregateMinimumTests()
        {
        }

        public override IQueryable<TestClass> GetTestList()
        {
            return SqlServerInMemHelper.GetTestList();
        }
    }
}
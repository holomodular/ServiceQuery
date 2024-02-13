namespace ServiceQuery.Xunit
{
    public class SqlServerInMemAggregateCountTests : LinqAsyncAggregateCountTests<TestClass>
    {
        public SqlServerInMemAggregateCountTests()
        {
        }

        public override IQueryable<TestClass> GetTestList()
        {
            return SqlServerInMemHelper.GetTestList();
        }
    }
}
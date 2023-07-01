namespace ServiceQuery.Xunit
{
    public class SqlServerInMemAggregateMaximumTests : AggregateMaximumTests<TestClass>
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
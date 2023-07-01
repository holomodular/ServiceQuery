namespace ServiceQuery.Xunit
{
    public class SqlServerInMemAggregateMinimumTests : AggregateMinimumTests<TestClass>
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
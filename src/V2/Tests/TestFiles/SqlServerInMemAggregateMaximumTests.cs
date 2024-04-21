namespace ServiceQuery.Xunit
{
    [Collection("InMemory")]
    public class SqlServerInMemAggregateMaximumTests : AggregateMaximumTests<TestClass>
    {
        public SqlServerInMemAggregateMaximumTests()
        {
        }

        public override IQueryable<TestClass> GetTestList()
        {
            return SqlServerInMemHelper.GetTestList();
        }

        public override IQueryable<TestClass> GetTestNullCopyList()
        {
            return SqlServerInMemHelper.GetTestNullCopyList();
        }
    }
}
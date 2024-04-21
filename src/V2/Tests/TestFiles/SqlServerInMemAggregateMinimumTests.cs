namespace ServiceQuery.Xunit
{
    [Collection("InMemory")]
    public class SqlServerInMemAggregateMinimumTests : AggregateMinimumTests<TestClass>
    {
        public SqlServerInMemAggregateMinimumTests()
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
namespace ServiceQuery.Xunit
{
    [Collection("InMemory")]
    public class SqlServerInMemAggregateCountTests : AggregateCountTests<TestClass>
    {
        public SqlServerInMemAggregateCountTests()
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
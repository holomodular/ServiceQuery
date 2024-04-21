namespace ServiceQuery.Xunit
{
    [Collection("InMemory")]
    public class SqlServerInMemAggregateAverageTests : AggregateAverageTests<TestClass>
    {
        public SqlServerInMemAggregateAverageTests()
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
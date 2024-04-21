namespace ServiceQuery.Xunit
{
    [Collection("InMemory")]
    public class SqlServerInMemComparisonIsGreaterThanTests : ComparisonIsGreaterThanTests<TestClass>
    {
        public SqlServerInMemComparisonIsGreaterThanTests()
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
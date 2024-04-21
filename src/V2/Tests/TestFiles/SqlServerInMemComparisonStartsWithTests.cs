namespace ServiceQuery.Xunit
{
    [Collection("InMemory")]
    public class SqlServerInMemComparisonStartsWithTests : ComparisonStartsWithTests<TestClass>
    {
        public SqlServerInMemComparisonStartsWithTests()
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
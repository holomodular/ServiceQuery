namespace ServiceQuery.Xunit
{
    [Collection("InMemory")]
    public class SqlServerInMemComparisonBetweenTests : ComparisonBetweenTests<TestClass>
    {
        public SqlServerInMemComparisonBetweenTests()
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
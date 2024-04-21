namespace ServiceQuery.Xunit
{
    [Collection("InMemory")]
    public class SqlServerInMemComparisonIsNotEqualTests : ComparisonIsNotEqualTests<TestClass>
    {
        public SqlServerInMemComparisonIsNotEqualTests()
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
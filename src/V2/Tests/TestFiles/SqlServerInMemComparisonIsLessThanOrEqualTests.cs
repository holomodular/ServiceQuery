namespace ServiceQuery.Xunit
{
    [Collection("InMemory")]
    public class SqlServerInMemComparisonIsLessThanOrEqualTests : ComparisonIsLessThanOrEqualTests<TestClass>
    {
        public SqlServerInMemComparisonIsLessThanOrEqualTests()
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
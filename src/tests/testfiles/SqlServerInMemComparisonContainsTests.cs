namespace ServiceQuery.Xunit
{
    public class SqlServerInMemComparisonContainsTests : ComparisonContainsTests<TestClass>
    {
        public SqlServerInMemComparisonContainsTests()
        {
        }

        public override IQueryable<TestClass> GetTestList()
        {
            return SqlServerInMemHelper.GetTestList();
        }
    }
}
namespace ServiceQuery.Xunit
{
    public class SqlServerInMemComparisonEndsWithTests : ComparisonEndsWithTests<TestClass>
    {
        public SqlServerInMemComparisonEndsWithTests()
        {
        }

        public override IQueryable<TestClass> GetTestList()
        {
            return SqlServerInMemHelper.GetTestList();
        }
    }
}
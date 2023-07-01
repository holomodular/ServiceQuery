namespace ServiceQuery.Xunit
{
    public class SqlServerInMemComparisonStartsWithTests : ComparisonStartsWithTests<TestClass>
    {
        public SqlServerInMemComparisonStartsWithTests()
        {
        }

        public override IQueryable<TestClass> GetTestList()
        {
            return SqlServerInMemHelper.GetTestList();
        }
    }
}
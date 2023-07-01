namespace ServiceQuery.Xunit
{
    public class SqlServerInMemComparisonBetweenTests : ComparisonBetweenTests<TestClass>
    {
        public SqlServerInMemComparisonBetweenTests()
        {
        }

        public override IQueryable<TestClass> GetTestList()
        {
            return SqlServerInMemHelper.GetTestList();
        }
    }
}
namespace ServiceQuery.Xunit
{
    public class SqlServerInMemComparisonIsLessThanTests : ComparisonIsLessThanTests<TestClass>
    {
        public SqlServerInMemComparisonIsLessThanTests()
        {
        }

        public override IQueryable<TestClass> GetTestList()
        {
            return SqlServerInMemHelper.GetTestList();
        }
    }
}
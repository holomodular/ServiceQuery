namespace ServiceQuery.Xunit
{
    public class SqlServerInMemComparisonIsEqualTests : ComparisonIsEqualTests<TestClass>
    {
        public SqlServerInMemComparisonIsEqualTests()
        {
        }

        public override IQueryable<TestClass> GetTestList()
        {
            return SqlServerInMemHelper.GetTestList();
        }
    }
}
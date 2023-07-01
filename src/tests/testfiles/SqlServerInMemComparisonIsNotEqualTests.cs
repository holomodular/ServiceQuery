namespace ServiceQuery.Xunit
{
    public class SqlServerInMemComparisonIsNotEqualTests : ComparisonIsNotEqualTests<TestClass>
    {
        public SqlServerInMemComparisonIsNotEqualTests()
        {
        }

        public override IQueryable<TestClass> GetTestList()
        {
            return SqlServerInMemHelper.GetTestList();
        }
    }
}
namespace ServiceQuery.Xunit
{
    public class SqlServerInMemComparisonIsNotEqualTests : LinqAsyncComparisonIsNotEqualTests<TestClass>
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
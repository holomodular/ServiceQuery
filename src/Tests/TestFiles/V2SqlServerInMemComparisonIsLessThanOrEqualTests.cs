namespace ServiceQuery.Xunit
{
    public class SqlServerInMemComparisonIsLessThanOrEqualTests : LinqAsyncComparisonIsLessThanOrEqualTests<TestClass>
    {
        public SqlServerInMemComparisonIsLessThanOrEqualTests()
        {
        }

        public override IQueryable<TestClass> GetTestList()
        {
            return SqlServerInMemHelper.GetTestList();
        }
    }
}
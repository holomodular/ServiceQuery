namespace ServiceQuery.Xunit
{
    public class SqlServerInMemAggregateAverageTests : AggregateAverageTests<TestClass>
    {
        public SqlServerInMemAggregateAverageTests()
        {
        }

        public override IQueryable<TestClass> GetTestList()
        {
            return SqlServerInMemHelper.GetTestList();
        }
    }
}
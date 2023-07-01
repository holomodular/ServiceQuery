namespace ServiceQuery.Xunit
{
    public class SqlServerInMemAggregateCountTests : AggregateCountTests<TestClass>
    {
        public SqlServerInMemAggregateCountTests()
        {
        }

        public override IQueryable<TestClass> GetTestList()
        {
            return SqlServerInMemHelper.GetTestList();
        }
    }
}
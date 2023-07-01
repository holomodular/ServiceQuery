namespace ServiceQuery.Xunit
{
    public class SqlServerInMemAggregateSumTests : AggregateSumTests<TestClass>
    {
        public SqlServerInMemAggregateSumTests()
        {
        }

        public override IQueryable<TestClass> GetTestList()
        {
            return SqlServerInMemHelper.GetTestList();
        }
    }
}
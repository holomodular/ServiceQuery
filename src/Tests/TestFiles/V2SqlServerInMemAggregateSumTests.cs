namespace ServiceQuery.Xunit
{
    public class SqlServerInMemAggregateSumTests : LinqAsyncAggregateSumTests<TestClass>
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
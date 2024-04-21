namespace ServiceQuery.Xunit
{
    [Collection("InMemory")]
    public class SqlServerInMemAggregateSumTests : AggregateSumTests<TestClass>
    {
        public SqlServerInMemAggregateSumTests()
        {
        }

        public override IQueryable<TestClass> GetTestList()
        {
            return SqlServerInMemHelper.GetTestList();
        }

        public override IQueryable<TestClass> GetTestNullCopyList()
        {
            return SqlServerInMemHelper.GetTestNullCopyList();
        }
    }
}
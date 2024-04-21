namespace ServiceQuery.Xunit
{
    [Collection("InMemory")]
    public class SqlServerInMemSelectTests : SelectTests<TestClass>
    {
        public SqlServerInMemSelectTests()
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
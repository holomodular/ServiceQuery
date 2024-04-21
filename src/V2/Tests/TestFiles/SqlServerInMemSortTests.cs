namespace ServiceQuery.Xunit
{
    [Collection("InMemory")]
    public class SqlServerInMemSortTests : SortTests<TestClass>
    {
        public SqlServerInMemSortTests()
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
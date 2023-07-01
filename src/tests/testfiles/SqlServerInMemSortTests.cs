namespace ServiceQuery.Xunit
{
    public class SqlServerInMemSortTests : SortTests<TestClass>
    {
        public SqlServerInMemSortTests()
        {
        }

        public override IQueryable<TestClass> GetTestList()
        {
            return SqlServerInMemHelper.GetTestList();
        }
    }
}
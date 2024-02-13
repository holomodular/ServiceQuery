namespace ServiceQuery.Xunit
{
    public class SqlServerInMemSortTests : LinqAsyncSortTests<TestClass>
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
namespace ServiceQuery.Xunit
{
    public class SqlServerInMemSelectTests : LinqAsyncSelectTests<TestClass>
    {
        public SqlServerInMemSelectTests()
        {
        }

        public override IQueryable<TestClass> GetTestList()
        {
            return SqlServerInMemHelper.GetTestList();
        }
    }
}
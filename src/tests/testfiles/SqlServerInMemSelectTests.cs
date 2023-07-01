namespace ServiceQuery.Xunit
{
    public class SqlServerInMemSelectTests : SelectTests<TestClass>
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
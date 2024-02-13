namespace ServiceQuery.Xunit
{
    public class SqlServerInMemDistinctTests : LinqAsyncDistinctTests<TestClass>
    {
        public SqlServerInMemDistinctTests()
        {
        }

        public override IQueryable<TestClass> GetTestList()
        {
            return SqlServerInMemHelper.GetTestList();
        }
    }
}
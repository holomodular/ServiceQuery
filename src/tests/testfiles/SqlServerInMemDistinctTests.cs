namespace ServiceQuery.Xunit
{
    public class SqlServerInMemDistinctTests : DistinctTests<TestClass>
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
namespace ServiceQuery.Xunit
{
    [Collection("InMemory")]
    public class SqlServerInMemDistinctTests : DistinctTests<TestClass>
    {
        public SqlServerInMemDistinctTests()
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
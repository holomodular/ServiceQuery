namespace ServiceQuery.Xunit
{
    public class SqlServerSortTests : LinqAsyncSortTests<TestClass>
    {
        public SqlServerSortTests()
        {
            ValidateUInt128 = false;
        }

        public override IQueryable<TestClass> GetTestList()
        {
            return SqlServerHelper.GetTestList();
        }
    }
}
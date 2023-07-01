namespace ServiceQuery.Xunit
{
    public class SqlServerSortTests : SortTests<TestClass>
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
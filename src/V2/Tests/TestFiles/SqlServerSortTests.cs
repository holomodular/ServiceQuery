namespace ServiceQuery.Xunit
{
    [Collection("SqlServer")]
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

        public override IQueryable<TestClass> GetTestNullCopyList()
        {
            return SqlServerHelper.GetTestNullCopyList();
        }
    }
}
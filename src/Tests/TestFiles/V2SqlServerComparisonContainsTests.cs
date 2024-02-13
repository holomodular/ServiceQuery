namespace ServiceQuery.Xunit
{
    public class SqlServerComparisonContainsTests : LinqAsyncComparisonContainsTests<TestClass>
    {
        public SqlServerComparisonContainsTests()
        {
            ValidateUInt128 = false;
        }

        public override IQueryable<TestClass> GetTestList()
        {
            return SqlServerHelper.GetTestList();
        }
    }
}
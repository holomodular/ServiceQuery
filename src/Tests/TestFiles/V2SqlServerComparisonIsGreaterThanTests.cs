namespace ServiceQuery.Xunit
{
    public class SqlServerComparisonIsGreaterThanTests : LinqAsyncComparisonIsGreaterThanTests<TestClass>
    {
        public SqlServerComparisonIsGreaterThanTests()
        {
            ValidateUInt128 = false;
        }

        public override IQueryable<TestClass> GetTestList()
        {
            return SqlServerHelper.GetTestList();
        }
    }
}
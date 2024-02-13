namespace ServiceQuery.Xunit
{
    public class SqlServerComparisonIsLessThanTests : LinqAsyncComparisonIsLessThanTests<TestClass>
    {
        public SqlServerComparisonIsLessThanTests()
        {
            ValidateUInt128 = false;
        }

        public override IQueryable<TestClass> GetTestList()
        {
            return SqlServerHelper.GetTestList();
        }
    }
}
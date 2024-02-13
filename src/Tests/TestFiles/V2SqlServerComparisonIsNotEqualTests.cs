namespace ServiceQuery.Xunit
{
    public class SqlServerComparisonIsNotEqualTests : LinqAsyncComparisonIsNotEqualTests<TestClass>
    {
        public SqlServerComparisonIsNotEqualTests()
        {
            ValidateUInt128 = false;
        }

        public override IQueryable<TestClass> GetTestList()
        {
            return SqlServerHelper.GetTestList();
        }
    }
}
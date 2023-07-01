namespace ServiceQuery.Xunit
{
    public class SqlServerComparisonIsEqualTests : ComparisonIsEqualTests<TestClass>
    {
        public SqlServerComparisonIsEqualTests()
        {
            ValidateUInt128 = false;
        }

        public override IQueryable<TestClass> GetTestList()
        {
            return SqlServerHelper.GetTestList();
        }
    }
}
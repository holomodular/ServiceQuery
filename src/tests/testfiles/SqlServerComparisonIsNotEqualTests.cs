namespace ServiceQuery.Xunit
{
    public class SqlServerComparisonIsNotEqualTests : ComparisonIsNotEqualTests<TestClass>
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
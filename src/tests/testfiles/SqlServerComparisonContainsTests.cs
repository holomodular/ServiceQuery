namespace ServiceQuery.Xunit
{
    public class SqlServerComparisonContainsTests : ComparisonContainsTests<TestClass>
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
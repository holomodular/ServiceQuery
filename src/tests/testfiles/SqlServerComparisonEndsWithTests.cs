namespace ServiceQuery.Xunit
{
    public class SqlServerComparisonEndsWithTests : ComparisonEndsWithTests<TestClass>
    {
        public SqlServerComparisonEndsWithTests()
        {
            ValidateUInt128 = false;
        }

        public override IQueryable<TestClass> GetTestList()
        {
            return SqlServerHelper.GetTestList();
        }
    }
}
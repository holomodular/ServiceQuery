namespace ServiceQuery.Xunit
{
    public class SqlServerComparisonStartsWithTests : ComparisonStartsWithTests<TestClass>
    {
        public SqlServerComparisonStartsWithTests()
        {
            ValidateUInt128 = false;
        }

        public override IQueryable<TestClass> GetTestList()
        {
            return SqlServerHelper.GetTestList();
        }
    }
}
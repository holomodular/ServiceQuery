namespace ServiceQuery.Xunit
{
    [Collection("Postgres")]
    public class PostgreSqlComparisonIsGreaterThanTests : ComparisonIsGreaterThanTests<TestClass>
    {
        public PostgreSqlComparisonIsGreaterThanTests()
        {
            ValidateUInt128 = false;
        }

        public override IQueryable<TestClass> GetTestList()
        {
            return PostgreSqlHelper.GetTestList();
        }

        public override IQueryable<TestClass> GetTestNullCopyList()
        {
            return PostgreSqlHelper.GetTestNullCopyList();
        }
    }
}
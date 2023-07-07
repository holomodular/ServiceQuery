namespace ServiceQuery.Xunit
{
    public class PostgreSqlComparisonContainsTests : ComparisonContainsTests<TestClass>
    {
        public PostgreSqlComparisonContainsTests()
        {
            ValidateUInt128 = false;
        }

        public override IQueryable<TestClass> GetTestList()
        {
            return PostgreSqlHelper.GetTestList();
        }
    }
}
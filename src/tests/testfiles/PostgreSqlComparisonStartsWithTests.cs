namespace ServiceQuery.Xunit
{
    public class PostgreSqlComparisonStartsWithTests : ComparisonStartsWithTests<TestClass>
    {
        public PostgreSqlComparisonStartsWithTests()
        {
            ValidateUInt128 = false;
        }

        public override IQueryable<TestClass> GetTestList()
        {
            return PostgreSqlHelper.GetTestList();
        }
    }
}
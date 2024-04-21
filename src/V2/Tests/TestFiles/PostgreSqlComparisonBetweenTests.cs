namespace ServiceQuery.Xunit
{
    [Collection("Postgres")]
    public class PostgreSqlComparisonBetweenTests : ComparisonBetweenTests<TestClass>
    {
        public PostgreSqlComparisonBetweenTests()
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
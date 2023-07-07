namespace ServiceQuery.Xunit
{
    public class PostgreSqlComparisonEndsWithTests : ComparisonEndsWithTests<TestClass>
    {
        public PostgreSqlComparisonEndsWithTests()
        {
            ValidateUInt128 = false;
        }

        public override IQueryable<TestClass> GetTestList()
        {
            return PostgreSqlHelper.GetTestList();
        }
    }
}
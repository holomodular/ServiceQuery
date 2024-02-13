namespace ServiceQuery.Xunit
{
    public class PostgreSqlComparisonContainsTests : LinqAsyncComparisonContainsTests<TestClass>
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
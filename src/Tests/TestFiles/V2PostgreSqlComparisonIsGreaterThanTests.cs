namespace ServiceQuery.Xunit
{
    public class PostgreSqlComparisonIsGreaterThanTests : LinqAsyncComparisonIsGreaterThanTests<TestClass>
    {
        public PostgreSqlComparisonIsGreaterThanTests()
        {
            ValidateUInt128 = false;
        }

        public override IQueryable<TestClass> GetTestList()
        {
            return PostgreSqlHelper.GetTestList();
        }
    }
}
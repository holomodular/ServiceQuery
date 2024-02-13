namespace ServiceQuery.Xunit
{
    public class PostgreSqlComparisonIsGreaterThanOrEqualTests : LinqAsyncComparisonIsGreaterThanOrEqualTests<TestClass>
    {
        public PostgreSqlComparisonIsGreaterThanOrEqualTests()
        {
            ValidateUInt128 = false;
        }

        public override IQueryable<TestClass> GetTestList()
        {
            return PostgreSqlHelper.GetTestList();
        }
    }
}
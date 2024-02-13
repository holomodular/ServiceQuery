namespace ServiceQuery.Xunit
{
    public class PostgreSqlComparisonIsEqualTests : LinqAsyncComparisonIsEqualTests<TestClass>
    {
        public PostgreSqlComparisonIsEqualTests()
        {
            ValidateUInt128 = false;
        }

        public override IQueryable<TestClass> GetTestList()
        {
            return PostgreSqlHelper.GetTestList();
        }
    }
}
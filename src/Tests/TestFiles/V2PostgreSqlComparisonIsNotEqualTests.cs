namespace ServiceQuery.Xunit
{
    public class PostgreSqlComparisonIsNotEqualTests : LinqAsyncComparisonIsNotEqualTests<TestClass>
    {
        public PostgreSqlComparisonIsNotEqualTests()
        {
            ValidateUInt128 = false;
        }

        public override IQueryable<TestClass> GetTestList()
        {
            return PostgreSqlHelper.GetTestList();
        }
    }
}
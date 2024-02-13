namespace ServiceQuery.Xunit
{
    public class PostgreSqlComparisonIsLessThanOrEqualTests : LinqAsyncComparisonIsLessThanOrEqualTests<TestClass>
    {
        public PostgreSqlComparisonIsLessThanOrEqualTests()
        {
            ValidateUInt128 = false;
        }

        public override IQueryable<TestClass> GetTestList()
        {
            return PostgreSqlHelper.GetTestList();
        }
    }
}
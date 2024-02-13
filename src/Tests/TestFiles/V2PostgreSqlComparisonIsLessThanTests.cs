namespace ServiceQuery.Xunit
{
    public class PostgreSqlComparisonIsLessThanTests : LinqAsyncComparisonIsLessThanTests<TestClass>
    {
        public PostgreSqlComparisonIsLessThanTests()
        {
            ValidateUInt128 = false;
        }

        public override IQueryable<TestClass> GetTestList()
        {
            return PostgreSqlHelper.GetTestList();
        }
    }
}
namespace ServiceQuery.Xunit
{
    public class PostgreSqlComparisonBetweenTests : LinqAsyncComparisonBetweenTests<TestClass>
    {
        public PostgreSqlComparisonBetweenTests()
        {
            ValidateUInt128 = false;
        }

        public override IQueryable<TestClass> GetTestList()
        {
            return PostgreSqlHelper.GetTestList();
        }
    }
}
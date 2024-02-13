namespace ServiceQuery.Xunit
{
    public class PostgreSqlComparisonEndsWithTests : LinqAsyncComparisonEndsWithTests<TestClass>
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
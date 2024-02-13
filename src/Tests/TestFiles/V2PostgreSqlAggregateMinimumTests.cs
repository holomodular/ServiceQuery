namespace ServiceQuery.Xunit
{
    public class PostgreSqlAggregateMinimumTests : LinqAsyncAggregateMinimumTests<TestClass>
    {
        public PostgreSqlAggregateMinimumTests()
        {
            ValidateUInt128 = false;
        }

        public override IQueryable<TestClass> GetTestList()
        {
            return PostgreSqlHelper.GetTestList();
        }
    }
}
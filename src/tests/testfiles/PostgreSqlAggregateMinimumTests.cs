namespace ServiceQuery.Xunit
{
    public class PostgreSqlAggregateMinimumTests : AggregateMinimumTests<TestClass>
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
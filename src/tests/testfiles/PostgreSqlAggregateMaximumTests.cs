namespace ServiceQuery.Xunit
{
    public class PostgreSqlAggregateMaximumTests : AggregateMaximumTests<TestClass>
    {
        public PostgreSqlAggregateMaximumTests()
        {
            ValidateUInt128 = false;
        }

        public override IQueryable<TestClass> GetTestList()
        {
            return PostgreSqlHelper.GetTestList();
        }
    }
}
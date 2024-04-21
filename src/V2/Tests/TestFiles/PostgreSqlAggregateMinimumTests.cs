namespace ServiceQuery.Xunit
{
    [Collection("Postgres")]
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

        public override IQueryable<TestClass> GetTestNullCopyList()
        {
            return PostgreSqlHelper.GetTestNullCopyList();
        }
    }
}
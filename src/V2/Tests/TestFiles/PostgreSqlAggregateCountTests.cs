namespace ServiceQuery.Xunit
{
    [Collection("Postgres")]
    public class PostgreSqlAggregateCountTests : AggregateCountTests<TestClass>
    {
        public PostgreSqlAggregateCountTests()
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
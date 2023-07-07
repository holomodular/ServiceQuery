namespace ServiceQuery.Xunit
{
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
    }
}
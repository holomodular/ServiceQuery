namespace ServiceQuery.Xunit
{
    public class PostgreSqlAggregateAverageTests : AggregateAverageTests<TestClass>
    {
        public PostgreSqlAggregateAverageTests()
        {
            ValidateUInt128 = false;
        }

        public override IQueryable<TestClass> GetTestList()
        {
            return PostgreSqlHelper.GetTestList();
        }
    }
}
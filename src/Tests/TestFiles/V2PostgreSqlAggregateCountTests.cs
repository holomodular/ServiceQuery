namespace ServiceQuery.Xunit
{
    public class PostgreSqlAggregateCountTests : LinqAsyncAggregateCountTests<TestClass>
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
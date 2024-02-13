namespace ServiceQuery.Xunit
{
    public class PostgreSqlAggregateSumTests : LinqAsyncAggregateSumTests<TestClass>
    {
        public PostgreSqlAggregateSumTests()
        {
            ValidateUInt128 = false;
        }

        public override IQueryable<TestClass> GetTestList()
        {
            return PostgreSqlHelper.GetTestList();
        }
    }
}
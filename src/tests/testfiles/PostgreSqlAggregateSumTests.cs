namespace ServiceQuery.Xunit
{
    public class PostgreSqlAggregateSumTests : AggregateSumTests<TestClass>
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
namespace ServiceQuery.Xunit
{
    public class SqlServerAggregateSumTests : AggregateSumTests<TestClass>
    {
        public SqlServerAggregateSumTests()
        {
            ValidateUInt128 = false;
        }

        public override IQueryable<TestClass> GetTestList()
        {
            return SqlServerHelper.GetTestList();
        }
    }
}
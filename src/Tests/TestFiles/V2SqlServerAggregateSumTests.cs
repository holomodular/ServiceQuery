namespace ServiceQuery.Xunit
{
    public class SqlServerAggregateSumTests : LinqAsyncAggregateSumTests<TestClass>
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
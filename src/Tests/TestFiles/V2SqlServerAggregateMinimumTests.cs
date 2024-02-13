namespace ServiceQuery.Xunit
{
    public class SqlServerAggregateMinimumTests : LinqAsyncAggregateMinimumTests<TestClass>
    {
        public SqlServerAggregateMinimumTests()
        {
            ValidateUInt128 = false;
        }

        public override IQueryable<TestClass> GetTestList()
        {
            return SqlServerHelper.GetTestList();
        }
    }
}
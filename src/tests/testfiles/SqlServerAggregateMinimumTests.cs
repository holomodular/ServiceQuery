namespace ServiceQuery.Xunit
{
    public class SqlServerAggregateMinimumTests : AggregateMinimumTests<TestClass>
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
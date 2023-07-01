namespace ServiceQuery.Xunit
{
    public class SqlServerAggregateMaximumTests : AggregateMaximumTests<TestClass>
    {
        public SqlServerAggregateMaximumTests()
        {
            ValidateUInt128 = false;
        }

        public override IQueryable<TestClass> GetTestList()
        {
            return SqlServerHelper.GetTestList();
        }
    }
}
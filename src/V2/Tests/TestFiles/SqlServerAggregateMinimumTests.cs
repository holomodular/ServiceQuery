namespace ServiceQuery.Xunit
{
    [Collection("SqlServer")]
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

        public override IQueryable<TestClass> GetTestNullCopyList()
        {
            return SqlServerHelper.GetTestNullCopyList();
        }
    }
}
namespace ServiceQuery.Xunit
{
    [Collection("SqlServer")]
    public class SqlServerAggregateCountTests : AggregateCountTests<TestClass>
    {
        public SqlServerAggregateCountTests()
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
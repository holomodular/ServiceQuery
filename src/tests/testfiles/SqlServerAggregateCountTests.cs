namespace ServiceQuery.Xunit
{
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
    }
}
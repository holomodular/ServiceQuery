namespace ServiceQuery.Xunit
{
    public class SqlServerAggregateCountTests : LinqAsyncAggregateCountTests<TestClass>
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
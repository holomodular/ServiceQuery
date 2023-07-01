namespace ServiceQuery.Xunit
{
    public class SqlServerAggregateAverageTests : AggregateAverageTests<TestClass>
    {
        public SqlServerAggregateAverageTests()
        {
            ValidateUInt128 = false;
        }

        public override IQueryable<TestClass> GetTestList()
        {
            return SqlServerHelper.GetTestList();
        }
    }
}
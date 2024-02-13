namespace ServiceQuery.Xunit
{
    public class SqlServerComparisonStartsWithTests : LinqAsyncComparisonStartsWithTests<TestClass>
    {
        public SqlServerComparisonStartsWithTests()
        {
            ValidateUInt128 = false;
        }

        public override IQueryable<TestClass> GetTestList()
        {
            return SqlServerHelper.GetTestList();
        }
    }
}
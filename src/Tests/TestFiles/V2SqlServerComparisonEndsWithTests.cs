namespace ServiceQuery.Xunit
{
    public class SqlServerComparisonEndsWithTests : LinqAsyncComparisonEndsWithTests<TestClass>
    {
        public SqlServerComparisonEndsWithTests()
        {
            ValidateUInt128 = false;
        }

        public override IQueryable<TestClass> GetTestList()
        {
            return SqlServerHelper.GetTestList();
        }
    }
}
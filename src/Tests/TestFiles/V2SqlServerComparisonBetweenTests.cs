namespace ServiceQuery.Xunit
{
    public class SqlServerComparisonBetweenTests : LinqAsyncComparisonBetweenTests<TestClass>
    {
        public SqlServerComparisonBetweenTests()
        {
            ValidateUInt128 = false;
        }

        public override IQueryable<TestClass> GetTestList()
        {
            return SqlServerHelper.GetTestList();
        }
    }
}
namespace ServiceQuery.Xunit
{
    public class SqlServerComparisonIsLessThanOrEqualTests : LinqAsyncComparisonIsLessThanOrEqualTests<TestClass>
    {
        public SqlServerComparisonIsLessThanOrEqualTests()
        {
            ValidateUInt128 = false;
        }

        public override IQueryable<TestClass> GetTestList()
        {
            return SqlServerHelper.GetTestList();
        }
    }
}
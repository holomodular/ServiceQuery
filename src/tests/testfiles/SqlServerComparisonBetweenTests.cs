namespace ServiceQuery.Xunit
{
    public class SqlServerComparisonBetweenTests : ComparisonBetweenTests<TestClass>
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
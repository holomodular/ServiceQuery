namespace ServiceQuery.Xunit
{
    public class CosmosComparisonIsGreaterThanTests : ComparisonIsGreaterThanTests<TestClass>
    {
        public CosmosComparisonIsGreaterThanTests()
        {
            ValidateUInt128 = false;
        }

        public override IQueryable<TestClass> GetTestList()
        {
            return CosmosHelper.GetTestList();
        }
    }
}
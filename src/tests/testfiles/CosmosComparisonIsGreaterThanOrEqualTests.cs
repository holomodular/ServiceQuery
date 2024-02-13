namespace ServiceQuery.Xunit
{
    [Collection("Cosmos")]
    public class CosmosComparisonIsGreaterThanOrEqualTests : ComparisonIsGreaterThanOrEqualTests<TestClass>
    {
        public CosmosComparisonIsGreaterThanOrEqualTests()
        {
            ValidateUInt128 = false;
        }

        public override IQueryable<TestClass> GetTestList()
        {
            return CosmosHelper.GetTestList();
        }
    }
}
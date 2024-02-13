namespace ServiceQuery.Xunit
{
    [Collection("Cosmos")]
    public class CosmosComparisonContainsTests : ComparisonContainsTests<TestClass>
    {
        public CosmosComparisonContainsTests()
        {
            ValidateUInt128 = false;
        }

        public override IQueryable<TestClass> GetTestList()
        {
            return CosmosHelper.GetTestList();
        }
    }
}
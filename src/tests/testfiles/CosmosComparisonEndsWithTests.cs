namespace ServiceQuery.Xunit
{
    [Collection("Cosmos")]
    public class CosmosComparisonEndsWithTests : ComparisonEndsWithTests<TestClass>
    {
        public CosmosComparisonEndsWithTests()
        {
            ValidateUInt128 = false;
        }

        public override IQueryable<TestClass> GetTestList()
        {
            return CosmosHelper.GetTestList();
        }
    }
}
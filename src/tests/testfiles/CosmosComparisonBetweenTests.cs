namespace ServiceQuery.Xunit
{
    [Collection("Cosmos")]
    public class CosmosComparisonBetweenTests : ComparisonBetweenTests<TestClass>
    {
        public CosmosComparisonBetweenTests()
        {
            ValidateUInt128 = false;
        }

        public override IQueryable<TestClass> GetTestList()
        {
            return CosmosHelper.GetTestList();
        }
    }
}
namespace ServiceQuery.Xunit
{
    [Collection("Cosmos")]
    public class CosmosComparisonStartsWithTests : ComparisonStartsWithTests<TestClass>
    {
        public CosmosComparisonStartsWithTests()
        {
            ValidateUInt128 = false;
        }

        public override IQueryable<TestClass> GetTestList()
        {
            return CosmosHelper.GetTestList();
        }
    }
}
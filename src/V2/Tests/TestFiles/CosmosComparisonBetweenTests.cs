namespace ServiceQuery.Xunit
{
    [Collection("Cosmos")]
    public class CosmosComparisonBetweenTests : ComparisonBetweenTests<TestClass>
    {
        public CosmosComparisonBetweenTests()
        {
            ValidateUInt128 = false;
            ValidateTimeOnly = false;
        }

        public override IQueryable<TestClass> GetTestList()
        {
            return CosmosHelper.GetTestList();
        }

        public override IQueryable<TestClass> GetTestNullCopyList()
        {
            return CosmosHelper.GetTestNullCopyList();
        }
    }
}
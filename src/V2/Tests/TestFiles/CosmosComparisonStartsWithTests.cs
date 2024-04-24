namespace ServiceQuery.Xunit.Integration
{
    [Collection("Cosmos")]
    public class CosmosComparisonStartsWithTests : ComparisonStartsWithTests<TestClass>
    {
        public CosmosComparisonStartsWithTests()
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
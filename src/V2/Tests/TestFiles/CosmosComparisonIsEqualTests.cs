namespace ServiceQuery.Xunit
{
    [Collection("Cosmos")]
    public class CosmosComparisonIsEqualTests : ComparisonIsEqualTests<TestClass>
    {
        public CosmosComparisonIsEqualTests()
        {
            ValidateTimeOnly = false;
            ValidateUInt128 = false;
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
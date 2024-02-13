namespace ServiceQuery.Xunit
{
    [Collection("Cosmos")]
    public class CosmosComparisonIsGreaterThanOrEqualTests : LinqAsyncComparisonIsGreaterThanOrEqualTests<TestClass>
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
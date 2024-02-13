namespace ServiceQuery.Xunit
{
    [Collection("Cosmos")]
    public class CosmosComparisonIsGreaterThanTests : LinqAsyncComparisonIsGreaterThanTests<TestClass>
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
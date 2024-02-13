namespace ServiceQuery.Xunit
{
    [Collection("Cosmos")]
    public class CosmosComparisonIsEqualTests : LinqAsyncComparisonIsEqualTests<TestClass>
    {
        public CosmosComparisonIsEqualTests()
        {
        }

        public override IQueryable<TestClass> GetTestList()
        {
            return CosmosHelper.GetTestList();
        }
    }
}
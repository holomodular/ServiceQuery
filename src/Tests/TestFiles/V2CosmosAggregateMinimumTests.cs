namespace ServiceQuery.Xunit
{
    [Collection("Cosmos")]
    public class CosmosAggregateMinimumTests : LinqAsyncAggregateMinimumTests<TestClass>
    {
        public CosmosAggregateMinimumTests()
        {
        }

        public override IQueryable<TestClass> GetTestList()
        {
            return CosmosHelper.GetTestList();
        }
    }
}
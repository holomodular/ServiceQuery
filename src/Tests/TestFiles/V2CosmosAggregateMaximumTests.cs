namespace ServiceQuery.Xunit
{
    [Collection("Cosmos")]
    public class CosmosAggregateMaximumTests : LinqAsyncAggregateMaximumTests<TestClass>
    {
        public CosmosAggregateMaximumTests()
        {
        }

        public override IQueryable<TestClass> GetTestList()
        {
            return CosmosHelper.GetTestList();
        }
    }
}
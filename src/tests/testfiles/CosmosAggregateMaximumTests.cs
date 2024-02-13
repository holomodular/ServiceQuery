namespace ServiceQuery.Xunit
{
    [Collection("Cosmos")]
    public class CosmosAggregateMaximumTests : AggregateMaximumTests<TestClass>
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
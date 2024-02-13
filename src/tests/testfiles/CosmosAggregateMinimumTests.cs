namespace ServiceQuery.Xunit
{
    [Collection("Cosmos")]
    public class CosmosAggregateMinimumTests : AggregateMinimumTests<TestClass>
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
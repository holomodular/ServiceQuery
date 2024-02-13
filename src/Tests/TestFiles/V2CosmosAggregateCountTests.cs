namespace ServiceQuery.Xunit
{
    [Collection("Cosmos")]
    public class CosmosAggregateCountTests : LinqAsyncAggregateCountTests<TestClass>
    {
        public CosmosAggregateCountTests()
        {
        }

        public override IQueryable<TestClass> GetTestList()
        {
            return CosmosHelper.GetTestList();
        }
    }
}
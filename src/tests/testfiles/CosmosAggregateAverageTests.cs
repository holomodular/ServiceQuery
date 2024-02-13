namespace ServiceQuery.Xunit
{
    [Collection("Cosmos")]
    public class CosmosAggregateAverageTests : AggregateAverageTests<TestClass>
    {
        public CosmosAggregateAverageTests()
        {
            CosmosIntLongRounding = true;
        }

        public override IQueryable<TestClass> GetTestList()
        {
            return CosmosHelper.GetTestList();
        }
    }
}
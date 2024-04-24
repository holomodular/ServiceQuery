namespace ServiceQuery.Xunit.Integration
{
    [Collection("Cosmos")]
    public class CosmosAggregateAverageTests : AggregateAverageTests<TestClass>
    {
        public CosmosAggregateAverageTests()
        {
            CosmosIntLongRounding = true;
            ValidateTimeOnly = false;
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
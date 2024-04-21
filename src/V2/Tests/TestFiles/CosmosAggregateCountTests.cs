namespace ServiceQuery.Xunit
{
    [Collection("Cosmos")]
    public class CosmosAggregateCountTests : AggregateCountTests<TestClass>
    {
        public CosmosAggregateCountTests()
        {
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
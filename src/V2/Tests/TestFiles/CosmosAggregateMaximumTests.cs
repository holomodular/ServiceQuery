namespace ServiceQuery.Xunit.Integration
{
    [Collection("Cosmos")]
    public class CosmosAggregateMaximumTests : AggregateMaximumTests<TestClass>
    {
        public CosmosAggregateMaximumTests()
        {
            ValidateTimeOnly = false;
            ValidateUInt128 = false;
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
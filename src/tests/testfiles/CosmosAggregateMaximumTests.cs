namespace ServiceQuery.Xunit
{
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
namespace ServiceQuery.Xunit
{
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
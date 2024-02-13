namespace ServiceQuery.Xunit
{
    [Collection("Cosmos")]
    public class CosmosAggregateSumTests : LinqAsyncAggregateSumTests<TestClass>
    {
        public CosmosAggregateSumTests()
        {
            NullSumIsNull = true;
        }

        public override IQueryable<TestClass> GetTestList()
        {
            return CosmosHelper.GetTestList();
        }
    }
}
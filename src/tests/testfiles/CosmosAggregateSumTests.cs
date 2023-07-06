namespace ServiceQuery.Xunit
{
    public class CosmosAggregateSumTests : AggregateSumTests<TestClass>
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
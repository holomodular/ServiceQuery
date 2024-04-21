namespace ServiceQuery.Xunit
{
    [Collection("Cosmos")]
    public class CosmosAggregateSumTests : AggregateSumTests<TestClass>
    {
        public CosmosAggregateSumTests()
        {
            NullSumIsNull = true;
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
namespace ServiceQuery.Xunit.Integration
{
    [Collection("Cosmos")]
    public class CosmosAggregateSumTestsAsync : AggregateSumTestsAsyncEfc<TestClass>
    {
        public CosmosAggregateSumTestsAsync()
        {
            NullSumIsNull = true;
            ValidateTimeOnly = false;

#if NET8_0
            CosmosSequenceNoElementsError = false;
#endif
#if NET9_0
            CosmosSequenceNoElementsError = true;
#endif
        }

        public override IQueryable<TestClass> GetTestList()
        {
            return CosmosHelper.GetTestList();
        }

        public override IQueryable<TestClass> GetTestNullCopyList()
        {
            return CosmosHelper.GetTestNullCopyList();
        }

        public override async Task<IQueryable<TestClass>> GetTestListAsync()
        {
            return await CosmosHelper.GetTestListAsync();
        }

        public override async Task<IQueryable<TestClass>> GetTestNullCopyListAsync()
        {
            return await CosmosHelper.GetTestNullCopyListAsync();
        }
    }
}
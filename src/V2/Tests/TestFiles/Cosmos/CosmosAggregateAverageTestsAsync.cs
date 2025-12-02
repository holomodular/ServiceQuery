namespace ServiceQuery.Xunit.Integration
{
    [Collection("Cosmos")]
    public class CosmosAggregateAverageTestsAsync : AggregateAverageTestsAsyncEfc<TestClass>
    {
        public CosmosAggregateAverageTestsAsync()
        {
            CosmosIntLongRounding = false;
            ValidateTimeOnly = false;
            //#if NET8_0
            //                        CosmosSequenceNoElementsError = false;
            //#endif
            //#if NET9_0
            //            CosmosSequenceNoElementsError = true;
            //#endif
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
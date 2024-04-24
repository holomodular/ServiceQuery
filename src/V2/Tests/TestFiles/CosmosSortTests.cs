namespace ServiceQuery.Xunit.Integration
{
    [Collection("Cosmos")]
    public class CosmosSortTests : SortTests<TestClass>
    {
        public CosmosSortTests()
        {
            ValidateUInt128 = false;
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
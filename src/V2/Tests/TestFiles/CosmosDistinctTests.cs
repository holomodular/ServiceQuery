namespace ServiceQuery.Xunit
{
    [Collection("Cosmos")]
    public class CosmosDistinctTests : DistinctTests<TestClass>
    {
        public CosmosDistinctTests()
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
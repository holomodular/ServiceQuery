namespace ServiceQuery.Xunit
{
    [Collection("Cosmos")]
    public class CosmosSortTests : SortTests<TestClass>
    {
        public CosmosSortTests()
        {
            ValidateUInt128 = false;
        }

        public override IQueryable<TestClass> GetTestList()
        {
            return CosmosHelper.GetTestList();
        }
    }
}
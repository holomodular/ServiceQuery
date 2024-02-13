namespace ServiceQuery.Xunit
{
    [Collection("Cosmos")]
    public class CosmosSortTests : LinqAsyncSortTests<TestClass>
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
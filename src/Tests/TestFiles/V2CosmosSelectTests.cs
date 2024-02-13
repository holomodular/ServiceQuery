namespace ServiceQuery.Xunit
{
    [Collection("Cosmos")]
    public class CosmosSelectTests : LinqAsyncSelectTests<TestClass>
    {
        public CosmosSelectTests()
        {
            ValidateUInt128 = false;
        }

        public override IQueryable<TestClass> GetTestList()
        {
            return CosmosHelper.GetTestList();
        }
    }
}
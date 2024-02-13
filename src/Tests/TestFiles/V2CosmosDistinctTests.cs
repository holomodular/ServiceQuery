namespace ServiceQuery.Xunit
{
    [Collection("Cosmos")]
    public class CosmosDistinctTests : LinqAsyncDistinctTests<TestClass>
    {
        public CosmosDistinctTests()
        {
            ValidateUInt128 = false;
        }

        public override IQueryable<TestClass> GetTestList()
        {
            return CosmosHelper.GetTestList();
        }
    }
}
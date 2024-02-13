namespace ServiceQuery.Xunit
{
    [Collection("Cosmos")]
    public class CosmosComparisonIsLessThanTests : LinqAsyncComparisonIsLessThanTests<TestClass>
    {
        public CosmosComparisonIsLessThanTests()
        {
            ValidateUInt128 = false;
        }

        public override IQueryable<TestClass> GetTestList()
        {
            return CosmosHelper.GetTestList();
        }
    }
}
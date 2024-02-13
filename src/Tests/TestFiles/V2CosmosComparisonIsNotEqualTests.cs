namespace ServiceQuery.Xunit
{
    [Collection("Cosmos")]
    public class CosmosComparisonIsNotEqualTests : LinqAsyncComparisonIsNotEqualTests<TestClass>
    {
        public CosmosComparisonIsNotEqualTests()
        {
            ValidateUInt128 = false;
        }

        public override IQueryable<TestClass> GetTestList()
        {
            return CosmosHelper.GetTestList();
        }
    }
}
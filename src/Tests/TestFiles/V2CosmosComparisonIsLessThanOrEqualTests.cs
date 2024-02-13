namespace ServiceQuery.Xunit
{
    [Collection("Cosmos")]
    public class CosmosComparisonIsLessThanOrEqualTests : LinqAsyncComparisonIsLessThanOrEqualTests<TestClass>
    {
        public CosmosComparisonIsLessThanOrEqualTests()
        {
            ValidateUInt128 = false;
        }

        public override IQueryable<TestClass> GetTestList()
        {
            return CosmosHelper.GetTestList();
        }
    }
}
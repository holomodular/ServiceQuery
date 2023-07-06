namespace ServiceQuery.Xunit
{
    public class CosmosComparisonIsEqualTests : ComparisonIsEqualTests<TestClass>
    {
        public CosmosComparisonIsEqualTests()
        {
        }

        public override IQueryable<TestClass> GetTestList()
        {
            return CosmosHelper.GetTestList();
        }
    }
}
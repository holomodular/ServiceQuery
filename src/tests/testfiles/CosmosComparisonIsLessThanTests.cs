namespace ServiceQuery.Xunit
{
    public class CosmosComparisonIsLessThanTests : ComparisonIsLessThanTests<TestClass>
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
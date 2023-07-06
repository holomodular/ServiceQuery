namespace ServiceQuery.Xunit
{
    public class CosmosComparisonIsNotEqualTests : ComparisonIsNotEqualTests<TestClass>
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
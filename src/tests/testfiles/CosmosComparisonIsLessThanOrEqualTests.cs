namespace ServiceQuery.Xunit
{
    public class CosmosComparisonIsLessThanOrEqualTests : ComparisonIsLessThanOrEqualTests<TestClass>
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
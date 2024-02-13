namespace ServiceQuery.Xunit
{
    [Collection("Cosmos")]
    public class CosmosSelectTests : SelectTests<TestClass>
    {
        public CosmosSelectTests()
        {
            ValidateUInt128 = false;
            OverrideDatetimeForCosmos = true;
        }

        public override IQueryable<TestClass> GetTestList()
        {
            return CosmosHelper.GetTestList();
        }
    }
}
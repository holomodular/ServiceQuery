namespace ServiceQuery.Xunit
{
    public class CosmosSelectTests : SelectTests<TestClass>
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
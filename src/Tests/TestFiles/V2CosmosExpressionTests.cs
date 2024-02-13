namespace ServiceQuery.Xunit
{
    [Collection("Cosmos")]
    public class CosmosExpressionTests : LinqAsyncExpressionTests<TestClass>
    {
        public CosmosExpressionTests()
        {
            ValidateUInt128 = false;
        }

        public override IQueryable<TestClass> GetTestList()
        {
            return CosmosHelper.GetTestList();
        }
    }
}
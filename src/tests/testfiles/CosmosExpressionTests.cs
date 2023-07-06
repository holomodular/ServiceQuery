namespace ServiceQuery.Xunit
{
    public class CosmosExpressionTests : ExpressionTests<TestClass>
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
namespace ServiceQuery.Xunit
{
    public class SqlServerExpressionTests : ExpressionTests<TestClass>
    {
        public SqlServerExpressionTests()
        {
            ValidateUInt128 = false;
        }

        public override IQueryable<TestClass> GetTestList()
        {
            return SqlServerHelper.GetTestList();
        }
    }
}
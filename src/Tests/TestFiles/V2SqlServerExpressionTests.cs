namespace ServiceQuery.Xunit
{
    public class SqlServerExpressionTests : LinqAsyncExpressionTests<TestClass>
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
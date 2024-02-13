namespace ServiceQuery.Xunit
{
    public class PostgreSqlSelectTests : LinqAsyncSelectTests<TestClass>
    {
        public PostgreSqlSelectTests()
        {
            ValidateUInt128 = false;
        }

        public override IQueryable<TestClass> GetTestList()
        {
            return PostgreSqlHelper.GetTestList();
        }
    }
}
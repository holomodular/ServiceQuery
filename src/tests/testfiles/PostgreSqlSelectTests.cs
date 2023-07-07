namespace ServiceQuery.Xunit
{
    public class PostgreSqlSelectTests : SelectTests<TestClass>
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
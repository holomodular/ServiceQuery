namespace ServiceQuery.Xunit
{
    public class SqlServerSelectTests : SelectTests<TestClass>
    {
        public SqlServerSelectTests()
        {
            ValidateUInt128 = false;
        }

        public override IQueryable<TestClass> GetTestList()
        {
            return SqlServerHelper.GetTestList();
        }
    }
}
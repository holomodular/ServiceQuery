namespace ServiceQuery.Xunit
{
    public class SqlServerDistinctTests : DistinctTests<TestClass>
    {
        public SqlServerDistinctTests()
        {
            ValidateUInt128 = false;
        }

        public override IQueryable<TestClass> GetTestList()
        {
            return SqlServerHelper.GetTestList();
        }
    }
}
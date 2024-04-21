namespace ServiceQuery.Xunit
{
    public abstract class BaseTest<T> where T : class
    {
        public abstract IQueryable<T> GetTestList();

        public bool ValidateUInt128 = true;
        public bool ValidateUInt64 = true;
        public bool ValidateDateTimeOffset = true;
        public bool ValidateTimeSpan = true;
        public bool ValidateDecimal = true;
    }

    public class BaseTest : BaseTest<TestClass>
    {
        public BaseTest()
        { }

        public override IQueryable<TestClass> GetTestList()
        {
            return new TestClass().GetDefaultList().AsQueryable();
        }
    }
}
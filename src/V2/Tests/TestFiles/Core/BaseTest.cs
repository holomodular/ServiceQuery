namespace ServiceQuery.Xunit
{
    public abstract class BaseTest<T> where T : class
    {
        public abstract IQueryable<T> GetTestList();

        public abstract Task<IQueryable<T>> GetTestListAsync();

        public abstract IQueryable<T> GetTestNullCopyList();

        public abstract Task<IQueryable<T>> GetTestNullCopyListAsync();

        public bool ValidateUInt128 = true;
        public bool ValidateUInt64 = true;
        public bool ValidateDateTimeOffset = true;
        public bool ValidateTimeOnly = true;
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

        public override IQueryable<TestClass> GetTestNullCopyList()
        {
            var list = new TestClass().GetDefaultList();
            foreach (var item in list)
                item.CopyToNullVals();
            return list.AsQueryable();
        }

        public override Task<IQueryable<TestClass>> GetTestListAsync()
        {
            return Task.FromResult(new TestClass().GetDefaultList().AsQueryable());
        }

        public override Task<IQueryable<TestClass>> GetTestNullCopyListAsync()
        {
            var list = new TestClass().GetDefaultList();
            foreach (var item in list)
                item.CopyToNullVals();
            return Task.FromResult(list.AsQueryable());
        }
    }
}
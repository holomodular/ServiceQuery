using Microsoft.EntityFrameworkCore;

namespace ServiceQuery.Xunit
{
    public class SortTestsAsyncEfc : SortTestsAsyncEfc<TestClass>
    {
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
            return Task.FromResult(new TestClass().GetDefaultList().AsAsyncInMemoryQueryable());
        }

        public override Task<IQueryable<TestClass>> GetTestNullCopyListAsync()
        {
            var list = new TestClass().GetDefaultList();
            foreach (var item in list)
                item.CopyToNullVals();
            return Task.FromResult(list.AsAsyncInMemoryQueryable());
        }
    }

    public abstract class SortTestsAsyncEfc<T> : BaseTest<T> where T : class, ITestClass, new()
    {
        protected void ValidateSort(List<T> sortedList, bool asc)
        {
            Assert.NotNull(sortedList);
            Assert.True(sortedList.Count == 4);
            if (asc)
            {
                Assert.True(sortedList[0].IntVal == 0);
                Assert.True(sortedList[1].IntVal == 1);
                Assert.True(sortedList[2].IntVal == 2);
                Assert.True(sortedList[3].IntVal == 3);
            }
            else
            {
                Assert.True(sortedList[0].IntVal == 3);
                Assert.True(sortedList[1].IntVal == 2);
                Assert.True(sortedList[2].IntVal == 1);
                Assert.True(sortedList[3].IntVal == 0);
            }
        }

        [Fact]
        public async Task SortAscTest()
        {
            var sourceQueryable = await GetTestListAsync();
            IQueryable<T> testQueryable;
            IServiceQuery serviceQuery;
            List<T> result;

            // Boolean
            serviceQuery = ServiceQueryBuilder.New().Sort(nameof(TestClass.BoolVal), true).Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.True(result[0].IntVal == 0 || result[0].IntVal == 2);
            Assert.True(result[1].IntVal == 0 || result[1].IntVal == 2);
            Assert.True(result[2].IntVal == 1 || result[2].IntVal == 3);
            Assert.True(result[3].IntVal == 1 || result[3].IntVal == 3);

            result = await testQueryable.ToListAsync();
            Assert.True(result[0].IntVal == 0 || result[0].IntVal == 2);
            Assert.True(result[1].IntVal == 0 || result[1].IntVal == 2);
            Assert.True(result[2].IntVal == 1 || result[2].IntVal == 3);
            Assert.True(result[3].IntVal == 1 || result[3].IntVal == 3);

            serviceQuery = ServiceQueryBuilder.New().SortAsc(nameof(TestClass.BoolVal)).Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.True(result[0].IntVal == 0 || result[0].IntVal == 2);
            Assert.True(result[1].IntVal == 0 || result[1].IntVal == 2);
            Assert.True(result[2].IntVal == 1 || result[2].IntVal == 3);
            Assert.True(result[3].IntVal == 1 || result[3].IntVal == 3);

            // ByteArray
            //var tempbyteArray = new byte[1] { 1 };
            //serviceQuery = ServiceQueryBuilder.New().IsEqual(nameof(TestClass.ByteArrayVal), System.Text.Encoding.UTF8.GetString(tempbyteArray)).Build();
            //testQueryable = serviceQuery.GetQueryable(sourceQueryable);
            //result = await testQueryable.ToListAsync();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 1);
            //result = await testQueryable.ToListAsync();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 1);

            // Byte
            serviceQuery = ServiceQueryBuilder.New().Sort(nameof(TestClass.ByteVal), true).Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            ValidateSort(result, true);

            // Char
            serviceQuery = ServiceQueryBuilder.New().Sort(nameof(TestClass.CharVal), true).Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            ValidateSort(result, true);

            if (ValidateDateTimeOffset)
            {
                // DateTimeOffset
                var tempDateTimeOffset = new T().GetDefault1Record(new T()).DateTimeOffsetVal;
                serviceQuery = ServiceQueryBuilder.New().Sort(nameof(TestClass.DateTimeOffsetVal), true).Build();
                testQueryable = serviceQuery.Apply(sourceQueryable);
                result = await testQueryable.ToListAsync();
                ValidateSort(result, true);
            }

#if NET6_0_OR_GREATER
            // DateOnly
            var tempDateOnly = new T().GetDefault1Record(new T()).DateOnlyVal;
            serviceQuery = ServiceQueryBuilder.New().Sort(nameof(TestClass.DateOnlyVal), true).Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            ValidateSort(result, true);
#endif

            // DateTime
            var tempDateTime = new T().GetDefault1Record(new T()).DateTimeVal;
            serviceQuery = ServiceQueryBuilder.New().Sort(nameof(TestClass.DateTimeVal), true).Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            ValidateSort(result, true);

            // Decimal
            if (ValidateDecimal)
            {
                serviceQuery = ServiceQueryBuilder.New().Sort(nameof(TestClass.DecimalVal), true).Build();
                testQueryable = serviceQuery.Apply(sourceQueryable);
                result = await testQueryable.ToListAsync();
                ValidateSort(result, true);
            }

            // Double
            serviceQuery = ServiceQueryBuilder.New().Sort(nameof(TestClass.DoubleVal), true).Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            ValidateSort(result, true);

            // Float
            serviceQuery = ServiceQueryBuilder.New().Sort(nameof(TestClass.FloatVal), true).Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            ValidateSort(result, true);

            // Guid
            serviceQuery = ServiceQueryBuilder.New().Sort(nameof(TestClass.GuidVal), true).Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            ValidateSort(result, true);

            // Int
            serviceQuery = ServiceQueryBuilder.New().Sort(nameof(TestClass.IntVal), true).Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            ValidateSort(result, true);

            // Long
            serviceQuery = ServiceQueryBuilder.New().Sort(nameof(TestClass.LongVal), true).Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            ValidateSort(result, true);

            // SByte
            serviceQuery = ServiceQueryBuilder.New().Sort(nameof(TestClass.SByteVal), true).Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            ValidateSort(result, true);

            // Short
            serviceQuery = ServiceQueryBuilder.New().Sort(nameof(TestClass.ShortVal), true).Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            ValidateSort(result, true);

            // Single
            serviceQuery = ServiceQueryBuilder.New().Sort(nameof(TestClass.SingleVal), true).Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            ValidateSort(result, true);

            // String
            serviceQuery = ServiceQueryBuilder.New().Sort(nameof(TestClass.StringVal), true).Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            ValidateSort(result, true);

#if NET6_0_OR_GREATER
            if (ValidateTimeOnly)
            {
                // TimeOnly
                var tempTimeOnly = new T().GetDefault1Record(new T()).TimeOnlyVal;
                serviceQuery = ServiceQueryBuilder.New().Sort(nameof(TestClass.TimeOnlyVal), true).Build();
                testQueryable = serviceQuery.Apply(sourceQueryable);
                result = await testQueryable.ToListAsync();
                ValidateSort(result, true);
            }
#endif

            if (ValidateTimeSpan)
            {
                // TimeSpan
                serviceQuery = ServiceQueryBuilder.New().Sort(nameof(TestClass.TimeSpanVal), true).Build();
                testQueryable = serviceQuery.Apply(sourceQueryable);
                result = await testQueryable.ToListAsync();
                ValidateSort(result, true);
            }
#if NET7_0_OR_GREATER
            if (ValidateUInt128)
            {
                // UInt128
                serviceQuery = ServiceQueryBuilder.New().Sort(nameof(TestClass.UInt128Val), true).Build();
                testQueryable = serviceQuery.Apply(sourceQueryable);
                result = await testQueryable.ToListAsync();
                ValidateSort(result, true);
            }
#endif
            if (ValidateUInt64)
            {
                // UInt64
                serviceQuery = ServiceQueryBuilder.New().Sort(nameof(TestClass.UInt64Val), true).Build();
                testQueryable = serviceQuery.Apply(sourceQueryable);
                result = await testQueryable.ToListAsync();
                ValidateSort(result, true);
            }

            // UInt32
            serviceQuery = ServiceQueryBuilder.New().Sort(nameof(TestClass.UInt32Val), true).Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            ValidateSort(result, true);

            // UInt16
            serviceQuery = ServiceQueryBuilder.New().Sort(nameof(TestClass.UInt16Val), true).Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            ValidateSort(result, true);
        }

        [Fact]
        public async Task SortDescTest()
        {
            var sourceQueryable = await GetTestListAsync();
            IQueryable<T> testQueryable;
            IServiceQuery serviceQuery;
            List<T> result;

            // Boolean
            serviceQuery = ServiceQueryBuilder.New().Sort(nameof(TestClass.BoolVal), false).Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.True(result[0].IntVal == 1 || result[0].IntVal == 3);
            Assert.True(result[1].IntVal == 1 || result[1].IntVal == 3);
            Assert.True(result[2].IntVal == 0 || result[2].IntVal == 2);
            Assert.True(result[3].IntVal == 0 || result[3].IntVal == 2);
            result = await testQueryable.ToListAsync();
            Assert.True(result[0].IntVal == 1 || result[0].IntVal == 3);
            Assert.True(result[1].IntVal == 1 || result[1].IntVal == 3);
            Assert.True(result[2].IntVal == 0 || result[2].IntVal == 2);
            Assert.True(result[3].IntVal == 0 || result[3].IntVal == 2);

            // ByteArray
            //var tempbyteArray = new byte[1] { 1 };
            //serviceQuery = ServiceQueryBuilder.New().IsEqual(nameof(TestClass.ByteArrayVal), System.Text.Encoding.UTF8.GetString(tempbyteArray)).Build();
            //testQueryable = serviceQuery.GetQueryable(sourceQueryable);
            //result = await testQueryable.ToListAsync();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 1);
            //result = await testQueryable.ToListAsync();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 1);

            // Byte
            serviceQuery = ServiceQueryBuilder.New().Sort(nameof(TestClass.ByteVal), false).Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            ValidateSort(result, false);

            // Char
            serviceQuery = ServiceQueryBuilder.New().Sort(nameof(TestClass.CharVal), false).Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            ValidateSort(result, false);

            if (ValidateDateTimeOffset)
            {
                // DateTimeOffset
                var tempDateTimeOffset = new T().GetDefault1Record(new T()).DateTimeOffsetVal;
                serviceQuery = ServiceQueryBuilder.New().Sort(nameof(TestClass.DateTimeOffsetVal), false).Build();
                testQueryable = serviceQuery.Apply(sourceQueryable);
                result = await testQueryable.ToListAsync();
                ValidateSort(result, false);
            }

#if NET6_0_OR_GREATER
            // DateOnly
            var tempDateOnly = new T().GetDefault1Record(new T()).DateOnlyVal;
            serviceQuery = ServiceQueryBuilder.New().Sort(nameof(TestClass.DateOnlyVal), false).Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            ValidateSort(result, false);
#endif

            // DateTime
            var tempDateTime = new T().GetDefault1Record(new T()).DateTimeVal;
            serviceQuery = ServiceQueryBuilder.New().Sort(nameof(TestClass.DateTimeVal), false).Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            ValidateSort(result, false);

            // Decimal
            if (ValidateDecimal)
            {
                serviceQuery = ServiceQueryBuilder.New().Sort(nameof(TestClass.DecimalVal), false).Build();
                testQueryable = serviceQuery.Apply(sourceQueryable);
                result = await testQueryable.ToListAsync();
                ValidateSort(result, false);
            }

            // Double
            serviceQuery = ServiceQueryBuilder.New().Sort(nameof(TestClass.DoubleVal), false).Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            ValidateSort(result, false);

            // Float
            serviceQuery = ServiceQueryBuilder.New().Sort(nameof(TestClass.FloatVal), false).Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            ValidateSort(result, false);

            // Guid
            serviceQuery = ServiceQueryBuilder.New().Sort(nameof(TestClass.GuidVal), false).Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            ValidateSort(result, false);

            // Int
            serviceQuery = ServiceQueryBuilder.New().Sort(nameof(TestClass.IntVal), false).Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            ValidateSort(result, false);

            // Long
            serviceQuery = ServiceQueryBuilder.New().Sort(nameof(TestClass.LongVal), false).Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            ValidateSort(result, false);

            // SByte
            serviceQuery = ServiceQueryBuilder.New().Sort(nameof(TestClass.SByteVal), false).Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            ValidateSort(result, false);

            // Short
            serviceQuery = ServiceQueryBuilder.New().Sort(nameof(TestClass.ShortVal), false).Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            ValidateSort(result, false);

            // Single
            serviceQuery = ServiceQueryBuilder.New().Sort(nameof(TestClass.SingleVal), false).Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            ValidateSort(result, false);

            // String
            serviceQuery = ServiceQueryBuilder.New().Sort(nameof(TestClass.StringVal), false).Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            ValidateSort(result, false);

#if NET6_0_OR_GREATER
            if (ValidateTimeOnly)
            {
                // TimeOnly
                var tempTimeOnly = new T().GetDefault1Record(new T()).TimeOnlyVal;
                serviceQuery = ServiceQueryBuilder.New().Sort(nameof(TestClass.TimeOnlyVal), false).Build();
                testQueryable = serviceQuery.Apply(sourceQueryable);
                result = await testQueryable.ToListAsync();
                ValidateSort(result, false);
            }
#endif

            if (ValidateTimeSpan)
            {
                // TimeSpan
                serviceQuery = ServiceQueryBuilder.New().Sort(nameof(TestClass.TimeSpanVal), false).Build();
                testQueryable = serviceQuery.Apply(sourceQueryable);
                result = await testQueryable.ToListAsync();
                ValidateSort(result, false);
            }
#if NET7_0_OR_GREATER
            if (ValidateUInt128)
            {
                // UInt128
                serviceQuery = ServiceQueryBuilder.New().Sort(nameof(TestClass.UInt128Val), false).Build();
                testQueryable = serviceQuery.Apply(sourceQueryable);
                result = await testQueryable.ToListAsync();
                ValidateSort(result, false);
            }
#endif
            if (ValidateUInt64)
            {
                // UInt64
                serviceQuery = ServiceQueryBuilder.New().Sort(nameof(TestClass.UInt64Val), false).Build();
                testQueryable = serviceQuery.Apply(sourceQueryable);
                result = await testQueryable.ToListAsync();
                ValidateSort(result, false);
            }

            // UInt32
            serviceQuery = ServiceQueryBuilder.New().Sort(nameof(TestClass.UInt32Val), false).Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            ValidateSort(result, false);

            // UInt16
            serviceQuery = ServiceQueryBuilder.New().Sort(nameof(TestClass.UInt16Val), false).Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            ValidateSort(result, false);
        }

        [Fact]
        public async Task SortAscRequestTest()
        {
            var sourceQueryable = await GetTestListAsync();
            IQueryable<T> testQueryable;
            IServiceQueryRequest serviceQuery;
            List<T> result;

            // Boolean
            serviceQuery = ServiceQueryRequestBuilder.New().Sort(nameof(TestClass.BoolVal), true).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.True(result[0].IntVal == 0 || result[0].IntVal == 2);
            Assert.True(result[1].IntVal == 0 || result[1].IntVal == 2);
            Assert.True(result[2].IntVal == 1 || result[2].IntVal == 3);
            Assert.True(result[3].IntVal == 1 || result[3].IntVal == 3);
            result = await testQueryable.ToListAsync();
            Assert.True(result[0].IntVal == 0 || result[0].IntVal == 2);
            Assert.True(result[1].IntVal == 0 || result[1].IntVal == 2);
            Assert.True(result[2].IntVal == 1 || result[2].IntVal == 3);
            Assert.True(result[3].IntVal == 1 || result[3].IntVal == 3);

            // ByteArray
            //var tempbyteArray = new byte[1] { 1 };
            //serviceQuery = ServiceQueryRequestBuilder.New().IsEqual(nameof(TestClass.ByteArrayVal), System.Text.Encoding.UTF8.GetString(tempbyteArray)).Build();
            //testQueryable = serviceQuery.GetQueryable(sourceQueryable);
            //result = await testQueryable.ToListAsync();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 1);
            //result = await testQueryable.ToListAsync();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 1);

            // Byte
            serviceQuery = ServiceQueryRequestBuilder.New().Sort(nameof(TestClass.ByteVal), true).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            ValidateSort(result, true);

            // Char
            serviceQuery = ServiceQueryRequestBuilder.New().Sort(nameof(TestClass.CharVal), true).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            ValidateSort(result, true);

            if (ValidateDateTimeOffset)
            {
                // DateTimeOffset
                var tempDateTimeOffset = new T().GetDefault1Record(new T()).DateTimeOffsetVal;
                serviceQuery = ServiceQueryRequestBuilder.New().Sort(nameof(TestClass.DateTimeOffsetVal), true).Build();
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
                result = await testQueryable.ToListAsync();
                ValidateSort(result, true);
            }

#if NET6_0_OR_GREATER
            // DateOnly
            var tempDateOnly = new T().GetDefault1Record(new T()).DateOnlyVal;
            serviceQuery = ServiceQueryRequestBuilder.New().Sort(nameof(TestClass.DateOnlyVal), true).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            ValidateSort(result, true);
#endif

            // DateTime
            var tempDateTime = new T().GetDefault1Record(new T()).DateTimeVal;
            serviceQuery = ServiceQueryRequestBuilder.New().Sort(nameof(TestClass.DateTimeVal), true).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            ValidateSort(result, true);

            // Decimal
            if (ValidateDecimal)
            {
                serviceQuery = ServiceQueryRequestBuilder.New().Sort(nameof(TestClass.DecimalVal), true).Build();
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
                result = await testQueryable.ToListAsync();
                ValidateSort(result, true);
            }

            // Double
            serviceQuery = ServiceQueryRequestBuilder.New().Sort(nameof(TestClass.DoubleVal), true).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            ValidateSort(result, true);

            // Float
            serviceQuery = ServiceQueryRequestBuilder.New().Sort(nameof(TestClass.FloatVal), true).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            ValidateSort(result, true);

            // Guid
            serviceQuery = ServiceQueryRequestBuilder.New().Sort(nameof(TestClass.GuidVal), true).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            ValidateSort(result, true);

            // Int
            serviceQuery = ServiceQueryRequestBuilder.New().Sort(nameof(TestClass.IntVal), true).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            ValidateSort(result, true);

            // Long
            serviceQuery = ServiceQueryRequestBuilder.New().Sort(nameof(TestClass.LongVal), true).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            ValidateSort(result, true);

            // SByte
            serviceQuery = ServiceQueryRequestBuilder.New().Sort(nameof(TestClass.SByteVal), true).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            ValidateSort(result, true);

            // Short
            serviceQuery = ServiceQueryRequestBuilder.New().Sort(nameof(TestClass.ShortVal), true).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            ValidateSort(result, true);

            // Single
            serviceQuery = ServiceQueryRequestBuilder.New().Sort(nameof(TestClass.SingleVal), true).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            ValidateSort(result, true);

            // String
            serviceQuery = ServiceQueryRequestBuilder.New().Sort(nameof(TestClass.StringVal), true).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            ValidateSort(result, true);

#if NET6_0_OR_GREATER
            if (ValidateTimeOnly)
            {
                // TimeOnly
                var tempTimeOnly = new T().GetDefault1Record(new T()).TimeOnlyVal;
                serviceQuery = ServiceQueryRequestBuilder.New().Sort(nameof(TestClass.TimeOnlyVal), true).Build();
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
                result = await testQueryable.ToListAsync();
                ValidateSort(result, true);
            }
#endif

            if (ValidateTimeSpan)
            {
                // TimeSpan
                serviceQuery = ServiceQueryRequestBuilder.New().Sort(nameof(TestClass.TimeSpanVal), true).Build();
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
                result = await testQueryable.ToListAsync();
                ValidateSort(result, true);
            }
#if NET7_0_OR_GREATER
            if (ValidateUInt128)
            {
                // UInt128
                serviceQuery = ServiceQueryRequestBuilder.New().Sort(nameof(TestClass.UInt128Val), true).Build();
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
                result = await testQueryable.ToListAsync();
                ValidateSort(result, true);
            }
#endif
            if (ValidateUInt64)
            {
                // UInt64
                serviceQuery = ServiceQueryRequestBuilder.New().Sort(nameof(TestClass.UInt64Val), true).Build();
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
                result = await testQueryable.ToListAsync();
                ValidateSort(result, true);
            }

            // UInt32
            serviceQuery = ServiceQueryRequestBuilder.New().Sort(nameof(TestClass.UInt32Val), true).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            ValidateSort(result, true);

            // UInt16
            serviceQuery = ServiceQueryRequestBuilder.New().Sort(nameof(TestClass.UInt16Val), true).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            ValidateSort(result, true);
        }

        [Fact]
        public async Task SortDescRequestTest()
        {
            var sourceQueryable = await GetTestListAsync();
            IQueryable<T> testQueryable;
            IServiceQueryRequest serviceQuery;
            List<T> result;

            // Boolean
            serviceQuery = ServiceQueryRequestBuilder.New().Sort(nameof(TestClass.BoolVal), false).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.True(result[0].IntVal == 1 || result[0].IntVal == 3);
            Assert.True(result[1].IntVal == 1 || result[1].IntVal == 3);
            Assert.True(result[2].IntVal == 0 || result[2].IntVal == 2);
            Assert.True(result[3].IntVal == 0 || result[3].IntVal == 2);
            result = await testQueryable.ToListAsync();
            Assert.True(result[0].IntVal == 1 || result[0].IntVal == 3);
            Assert.True(result[1].IntVal == 1 || result[1].IntVal == 3);
            Assert.True(result[2].IntVal == 0 || result[2].IntVal == 2);
            Assert.True(result[3].IntVal == 0 || result[3].IntVal == 2);

            // ByteArray
            //var tempbyteArray = new byte[1] { 1 };
            //serviceQuery = ServiceQueryRequestBuilder.New().IsEqual(nameof(TestClass.ByteArrayVal), System.Text.Encoding.UTF8.GetString(tempbyteArray)).Build();
            //testQueryable = serviceQuery.GetQueryable(sourceQueryable);
            //result = await testQueryable.ToListAsync();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 1);
            //result = await testQueryable.ToListAsync();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 1);

            // Byte
            serviceQuery = ServiceQueryRequestBuilder.New().Sort(nameof(TestClass.ByteVal), false).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            ValidateSort(result, false);

            // Char
            serviceQuery = ServiceQueryRequestBuilder.New().Sort(nameof(TestClass.CharVal), false).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            ValidateSort(result, false);

            if (ValidateDateTimeOffset)
            {
                // DateTimeOffset
                var tempDateTimeOffset = new T().GetDefault1Record(new T()).DateTimeOffsetVal;
                serviceQuery = ServiceQueryRequestBuilder.New().Sort(nameof(TestClass.DateTimeOffsetVal), false).Build();
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
                result = await testQueryable.ToListAsync();
                ValidateSort(result, false);
            }

#if NET6_0_OR_GREATER
            // DateOnly
            var tempDateOnly = new T().GetDefault1Record(new T()).DateOnlyVal;
            serviceQuery = ServiceQueryRequestBuilder.New().Sort(nameof(TestClass.DateOnlyVal), false).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            ValidateSort(result, false);
#endif

            // DateTime
            var tempDateTime = new T().GetDefault1Record(new T()).DateTimeVal;
            serviceQuery = ServiceQueryRequestBuilder.New().Sort(nameof(TestClass.DateTimeVal), false).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            ValidateSort(result, false);

            // Decimal
            if (ValidateDecimal)
            {
                serviceQuery = ServiceQueryRequestBuilder.New().Sort(nameof(TestClass.DecimalVal), false).Build();
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
                result = await testQueryable.ToListAsync();
                ValidateSort(result, false);
            }

            // Double
            serviceQuery = ServiceQueryRequestBuilder.New().Sort(nameof(TestClass.DoubleVal), false).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            ValidateSort(result, false);

            // Float
            serviceQuery = ServiceQueryRequestBuilder.New().Sort(nameof(TestClass.FloatVal), false).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            ValidateSort(result, false);

            // Guid
            serviceQuery = ServiceQueryRequestBuilder.New().Sort(nameof(TestClass.GuidVal), false).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            ValidateSort(result, false);

            // Int
            serviceQuery = ServiceQueryRequestBuilder.New().Sort(nameof(TestClass.IntVal), false).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            ValidateSort(result, false);

            // Long
            serviceQuery = ServiceQueryRequestBuilder.New().Sort(nameof(TestClass.LongVal), false).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            ValidateSort(result, false);

            // SByte
            serviceQuery = ServiceQueryRequestBuilder.New().Sort(nameof(TestClass.SByteVal), false).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            ValidateSort(result, false);

            // Short
            serviceQuery = ServiceQueryRequestBuilder.New().Sort(nameof(TestClass.ShortVal), false).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            ValidateSort(result, false);

            // Single
            serviceQuery = ServiceQueryRequestBuilder.New().Sort(nameof(TestClass.SingleVal), false).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            ValidateSort(result, false);

            // String
            serviceQuery = ServiceQueryRequestBuilder.New().Sort(nameof(TestClass.StringVal), false).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            ValidateSort(result, false);

#if NET6_0_OR_GREATER
            if (ValidateTimeOnly)
            {
                // TimeOnly
                var tempTimeOnly = new T().GetDefault1Record(new T()).TimeOnlyVal;
                serviceQuery = ServiceQueryRequestBuilder.New().Sort(nameof(TestClass.TimeOnlyVal), false).Build();
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
                result = await testQueryable.ToListAsync();
                ValidateSort(result, false);
            }
#endif

            if (ValidateTimeSpan)
            {
                // TimeSpan
                serviceQuery = ServiceQueryRequestBuilder.New().Sort(nameof(TestClass.TimeSpanVal), false).Build();
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
                result = await testQueryable.ToListAsync();
                ValidateSort(result, false);
            }
#if NET7_0_OR_GREATER
            if (ValidateUInt128)
            {
                // UInt128
                serviceQuery = ServiceQueryRequestBuilder.New().Sort(nameof(TestClass.UInt128Val), false).Build();
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
                result = await testQueryable.ToListAsync();
                ValidateSort(result, false);
            }
#endif
            if (ValidateUInt64)
            {
                // UInt64
                serviceQuery = ServiceQueryRequestBuilder.New().Sort(nameof(TestClass.UInt64Val), false).Build();
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
                result = await testQueryable.ToListAsync();
                ValidateSort(result, false);
            }

            // UInt32
            serviceQuery = ServiceQueryRequestBuilder.New().Sort(nameof(TestClass.UInt32Val), false).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            ValidateSort(result, false);

            // UInt16
            serviceQuery = ServiceQueryRequestBuilder.New().Sort(nameof(TestClass.UInt16Val), false).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            ValidateSort(result, false);
        }

        [Fact]
        public async Task NullCopySortAscTest()
        {
            var sourceQueryable = await GetTestNullCopyListAsync();
            IQueryable<T> testQueryable;
            IServiceQuery serviceQuery;
            List<T> result;

            // Boolean
            serviceQuery = ServiceQueryBuilder.New().Sort(nameof(TestClass.NullBoolVal), true).Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.True(result.Count == 4);
            result = await testQueryable.ToListAsync();
            Assert.True(result.Count == 4);
            //Assert.True(result[0].IntVal == 0 || result[0].IntVal == 2);
            //Assert.True(result[1].IntVal == 0 || result[1].IntVal == 2);
            //Assert.True(result[2].IntVal == 1 || result[2].IntVal == 3);
            //Assert.True(result[3].IntVal == 1 || result[3].IntVal == 3);

            serviceQuery = ServiceQueryBuilder.New().SortAsc(nameof(TestClass.NullBoolVal)).Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.True(result.Count == 4);
            //Assert.True(result[0].IntVal == 0 || result[0].IntVal == 2);
            //Assert.True(result[1].IntVal == 0 || result[1].IntVal == 2);
            //Assert.True(result[2].IntVal == 1 || result[2].IntVal == 3);
            //Assert.True(result[3].IntVal == 1 || result[3].IntVal == 3);

            // ByteArray
            //var tempbyteArray = new byte[1] { 1 };
            //serviceQuery = ServiceQueryBuilder.New().IsEqual(nameof(TestClass.NullByteArrayVal), System.Text.Encoding.UTF8.GetString(tempbyteArray)).Build();
            //testQueryable = serviceQuery.GetQueryable(sourceQueryable);
            //result = await testQueryable.ToListAsync();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 1);
            //result = await testQueryable.ToListAsync();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 1);

            // Byte
            serviceQuery = ServiceQueryBuilder.New().Sort(nameof(TestClass.NullByteVal), true).Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.True(result.Count == 4);
            ValidateSort(result, true);

            // Char
            serviceQuery = ServiceQueryBuilder.New().Sort(nameof(TestClass.NullCharVal), true).Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.True(result.Count == 4);
            ValidateSort(result, true);

            if (ValidateDateTimeOffset)
            {
                // DateTimeOffset
                var tempDateTimeOffset = new T().GetDefault1Record(new T()).DateTimeOffsetVal;
                serviceQuery = ServiceQueryBuilder.New().Sort(nameof(TestClass.NullDateTimeOffsetVal), true).Build();
                testQueryable = serviceQuery.Apply(sourceQueryable);
                result = await testQueryable.ToListAsync();
                Assert.True(result.Count == 4);
                ValidateSort(result, true);
            }

#if NET6_0_OR_GREATER

            // DateOnly
            var tempDateOnly = new T().GetDefault1Record(new T()).DateOnlyVal;
            serviceQuery = ServiceQueryBuilder.New().Sort(nameof(TestClass.NullDateOnlyVal), true).Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.True(result.Count == 4);
            ValidateSort(result, true);
#endif

            // DateTime
            var tempDateTime = new T().GetDefault1Record(new T()).DateTimeVal;
            serviceQuery = ServiceQueryBuilder.New().Sort(nameof(TestClass.NullDateTimeVal), true).Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.True(result.Count == 4);
            ValidateSort(result, true);

            // Decimal
            if (ValidateDecimal)
            {
                serviceQuery = ServiceQueryBuilder.New().Sort(nameof(TestClass.NullDecimalVal), true).Build();
                testQueryable = serviceQuery.Apply(sourceQueryable);
                result = await testQueryable.ToListAsync();
                Assert.True(result.Count == 4);
                ValidateSort(result, true);
            }

            // Double
            serviceQuery = ServiceQueryBuilder.New().Sort(nameof(TestClass.NullDoubleVal), true).Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.True(result.Count == 4);
            ValidateSort(result, true);

            // Float
            serviceQuery = ServiceQueryBuilder.New().Sort(nameof(TestClass.NullFloatVal), true).Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.True(result.Count == 4);
            ValidateSort(result, true);

            // Guid
            serviceQuery = ServiceQueryBuilder.New().Sort(nameof(TestClass.NullGuidVal), true).Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.True(result.Count == 4);
            ValidateSort(result, true);

            // Int
            serviceQuery = ServiceQueryBuilder.New().Sort(nameof(TestClass.NullIntVal), true).Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.True(result.Count == 4);
            ValidateSort(result, true);

            // Long
            serviceQuery = ServiceQueryBuilder.New().Sort(nameof(TestClass.NullLongVal), true).Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.True(result.Count == 4);
            ValidateSort(result, true);

            // SByte
            serviceQuery = ServiceQueryBuilder.New().Sort(nameof(TestClass.NullSByteVal), true).Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.True(result.Count == 4);
            ValidateSort(result, true);

            // Short
            serviceQuery = ServiceQueryBuilder.New().Sort(nameof(TestClass.NullShortVal), true).Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.True(result.Count == 4);
            ValidateSort(result, true);

            // Single
            serviceQuery = ServiceQueryBuilder.New().Sort(nameof(TestClass.NullSingleVal), true).Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.True(result.Count == 4);
            ValidateSort(result, true);

            // String
            serviceQuery = ServiceQueryBuilder.New().Sort(nameof(TestClass.NullStringVal), true).Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.True(result.Count == 4);
            ValidateSort(result, true);

#if NET6_0_OR_GREATER
            if (ValidateTimeOnly)
            {
                // TimeOnly
                var tempTimeOnly = new T().GetDefault1Record(new T()).TimeOnlyVal;
                serviceQuery = ServiceQueryBuilder.New().Sort(nameof(TestClass.NullTimeOnlyVal), true).Build();
                testQueryable = serviceQuery.Apply(sourceQueryable);
                result = await testQueryable.ToListAsync();
                Assert.True(result.Count == 4);
                ValidateSort(result, true);
            }
#endif

            if (ValidateTimeSpan)
            {
                // TimeSpan
                serviceQuery = ServiceQueryBuilder.New().Sort(nameof(TestClass.NullTimeSpanVal), true).Build();
                testQueryable = serviceQuery.Apply(sourceQueryable);
                result = await testQueryable.ToListAsync();
                Assert.True(result.Count == 4);
                ValidateSort(result, true);
            }
#if NET7_0_OR_GREATER
            if (ValidateUInt128)
            {
                // UInt128
                serviceQuery = ServiceQueryBuilder.New().Sort(nameof(TestClass.NullUInt128Val), true).Build();
                testQueryable = serviceQuery.Apply(sourceQueryable);
                result = await testQueryable.ToListAsync();
                Assert.True(result.Count == 4);
                ValidateSort(result, true);
            }
#endif
            if (ValidateUInt64)
            {
                // UInt64
                serviceQuery = ServiceQueryBuilder.New().Sort(nameof(TestClass.NullUInt64Val), true).Build();
                testQueryable = serviceQuery.Apply(sourceQueryable);
                result = await testQueryable.ToListAsync();
                Assert.True(result.Count == 4);
                ValidateSort(result, true);
            }

            // UInt32
            serviceQuery = ServiceQueryBuilder.New().Sort(nameof(TestClass.NullUInt32Val), true).Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.True(result.Count == 4);
            ValidateSort(result, true);

            // UInt16
            serviceQuery = ServiceQueryBuilder.New().Sort(nameof(TestClass.NullUInt16Val), true).Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.True(result.Count == 4);
            ValidateSort(result, true);
        }

        [Fact]
        public async Task NullCopySortDescTest()
        {
            var sourceQueryable = await GetTestNullCopyListAsync();
            IQueryable<T> testQueryable;
            IServiceQuery serviceQuery;
            List<T> result;

            // Boolean
            serviceQuery = ServiceQueryBuilder.New().SortDesc(nameof(TestClass.NullBoolVal)).Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.True(result.Count == 4);
            result = await testQueryable.ToListAsync();
            Assert.True(result.Count == 4);

            // ByteArray
            //var tempbyteArray = new byte[1] { 1 };
            //serviceQuery = ServiceQueryBuilder.New().IsEqual(nameof(TestClass.NullByteArrayVal), System.Text.Encoding.UTF8.GetString(tempbyteArray)).Build();
            //testQueryable = serviceQuery.GetQueryable(sourceQueryable);
            //result = await testQueryable.ToListAsync();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 1);
            //result = await testQueryable.ToListAsync();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 1);

            // Byte
            serviceQuery = ServiceQueryBuilder.New().SortDesc(nameof(TestClass.NullByteVal)).Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.True(result.Count == 4);
            ValidateSort(result, false);

            // Char
            serviceQuery = ServiceQueryBuilder.New().SortDesc(nameof(TestClass.NullCharVal)).Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.True(result.Count == 4);
            ValidateSort(result, false);

            if (ValidateDateTimeOffset)
            {
                // DateTimeOffset
                var tempDateTimeOffset = new T().GetDefault1Record(new T()).DateTimeOffsetVal;
                serviceQuery = ServiceQueryBuilder.New().SortDesc(nameof(TestClass.NullDateTimeOffsetVal)).Build();
                testQueryable = serviceQuery.Apply(sourceQueryable);
                result = await testQueryable.ToListAsync();
                Assert.True(result.Count == 4);
                ValidateSort(result, false);
            }

#if NET6_0_OR_GREATER
            // DateOnly
            var tempDateOnly = new T().GetDefault1Record(new T()).DateOnlyVal;
            serviceQuery = ServiceQueryBuilder.New().SortDesc(nameof(TestClass.NullDateOnlyVal)).Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.True(result.Count == 4);
            ValidateSort(result, false);
#endif

            // DateTime
            var tempDateTime = new T().GetDefault1Record(new T()).DateTimeVal;
            serviceQuery = ServiceQueryBuilder.New().SortDesc(nameof(TestClass.NullDateTimeVal)).Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.True(result.Count == 4);
            ValidateSort(result, false);

            // Decimal
            if (ValidateDecimal)
            {
                serviceQuery = ServiceQueryBuilder.New().SortDesc(nameof(TestClass.NullDecimalVal)).Build();
                testQueryable = serviceQuery.Apply(sourceQueryable);
                result = await testQueryable.ToListAsync();
                Assert.True(result.Count == 4);
                ValidateSort(result, false);
            }

            // Double
            serviceQuery = ServiceQueryBuilder.New().SortDesc(nameof(TestClass.NullDoubleVal)).Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.True(result.Count == 4);
            ValidateSort(result, false);

            // Float
            serviceQuery = ServiceQueryBuilder.New().SortDesc(nameof(TestClass.NullFloatVal)).Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.True(result.Count == 4);
            ValidateSort(result, false);

            // Guid
            serviceQuery = ServiceQueryBuilder.New().SortDesc(nameof(TestClass.NullGuidVal)).Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.True(result.Count == 4);
            ValidateSort(result, false);

            // Int
            serviceQuery = ServiceQueryBuilder.New().SortDesc(nameof(TestClass.NullIntVal)).Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.True(result.Count == 4);
            ValidateSort(result, false);

            // Long
            serviceQuery = ServiceQueryBuilder.New().SortDesc(nameof(TestClass.NullLongVal)).Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.True(result.Count == 4);
            ValidateSort(result, false);

            // SByte
            serviceQuery = ServiceQueryBuilder.New().SortDesc(nameof(TestClass.NullSByteVal)).Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.True(result.Count == 4);
            ValidateSort(result, false);

            // Short
            serviceQuery = ServiceQueryBuilder.New().SortDesc(nameof(TestClass.NullShortVal)).Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.True(result.Count == 4);
            ValidateSort(result, false);

            // Single
            serviceQuery = ServiceQueryBuilder.New().SortDesc(nameof(TestClass.NullSingleVal)).Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.True(result.Count == 4);
            ValidateSort(result, false);

            // String
            serviceQuery = ServiceQueryBuilder.New().SortDesc(nameof(TestClass.NullStringVal)).Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.True(result.Count == 4);
            ValidateSort(result, false);

#if NET6_0_OR_GREATER
            if (ValidateTimeOnly)
            {
                // TimeOnly
                var tempTimeOnly = new T().GetDefault1Record(new T()).TimeOnlyVal;
                serviceQuery = ServiceQueryBuilder.New().SortDesc(nameof(TestClass.NullTimeOnlyVal)).Build();
                testQueryable = serviceQuery.Apply(sourceQueryable);
                result = await testQueryable.ToListAsync();
                Assert.True(result.Count == 4);
                ValidateSort(result, false);
            }

#endif

            if (ValidateTimeSpan)
            {
                // TimeSpan
                serviceQuery = ServiceQueryBuilder.New().SortDesc(nameof(TestClass.NullTimeSpanVal)).Build();
                testQueryable = serviceQuery.Apply(sourceQueryable);
                result = await testQueryable.ToListAsync();
                Assert.True(result.Count == 4);
                ValidateSort(result, false);
            }
#if NET7_0_OR_GREATER
            if (ValidateUInt128)
            {
                // UInt128
                serviceQuery = ServiceQueryBuilder.New().SortDesc(nameof(TestClass.NullUInt128Val)).Build();
                testQueryable = serviceQuery.Apply(sourceQueryable);
                result = await testQueryable.ToListAsync();
                Assert.True(result.Count == 4);
                ValidateSort(result, false);
            }
#endif
            if (ValidateUInt64)
            {
                // UInt64
                serviceQuery = ServiceQueryBuilder.New().SortDesc(nameof(TestClass.NullUInt64Val)).Build();
                testQueryable = serviceQuery.Apply(sourceQueryable);
                result = await testQueryable.ToListAsync();
                Assert.True(result.Count == 4);
                ValidateSort(result, false);
            }

            // UInt32
            serviceQuery = ServiceQueryBuilder.New().SortDesc(nameof(TestClass.NullUInt32Val)).Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.True(result.Count == 4);
            ValidateSort(result, false);

            // UInt16
            serviceQuery = ServiceQueryBuilder.New().SortDesc(nameof(TestClass.NullUInt16Val)).Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.True(result.Count == 4);
            ValidateSort(result, false);
        }

        [Fact]
        public async Task NullCopySortDescBoolTest()
        {
            var sourceQueryable = await GetTestNullCopyListAsync();
            IQueryable<T> testQueryable;
            IServiceQuery serviceQuery;
            List<T> result;

            // Boolean
            serviceQuery = ServiceQueryBuilder.New().Sort(nameof(TestClass.NullBoolVal), false).Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.True(result.Count == 4);
            result = await testQueryable.ToListAsync();
            Assert.True(result.Count == 4);

            // ByteArray
            //var tempbyteArray = new byte[1] { 1 };
            //serviceQuery = ServiceQueryBuilder.New().IsEqual(nameof(TestClass.NullByteArrayVal), System.Text.Encoding.UTF8.GetString(tempbyteArray)).Build();
            //testQueryable = serviceQuery.GetQueryable(sourceQueryable);
            //result = await testQueryable.ToListAsync();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 1);
            //result = await testQueryable.ToListAsync();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 1);

            // Byte
            serviceQuery = ServiceQueryBuilder.New().Sort(nameof(TestClass.NullByteVal), false).Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.True(result.Count == 4);
            ValidateSort(result, false);

            // Char
            serviceQuery = ServiceQueryBuilder.New().Sort(nameof(TestClass.NullCharVal), false).Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.True(result.Count == 4);
            ValidateSort(result, false);

            if (ValidateDateTimeOffset)
            {
                // DateTimeOffset
                var tempDateTimeOffset = new T().GetDefault1Record(new T()).DateTimeOffsetVal;
                serviceQuery = ServiceQueryBuilder.New().Sort(nameof(TestClass.NullDateTimeOffsetVal), false).Build();
                testQueryable = serviceQuery.Apply(sourceQueryable);
                result = await testQueryable.ToListAsync();
                Assert.True(result.Count == 4);
                ValidateSort(result, false);
            }

#if NET6_0_OR_GREATER
            // DateOnly
            var tempDateOnly = new T().GetDefault1Record(new T()).DateOnlyVal;
            serviceQuery = ServiceQueryBuilder.New().Sort(nameof(TestClass.NullDateOnlyVal), false).Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.True(result.Count == 4);
            ValidateSort(result, false);
#endif

            // DateTime
            var tempDateTime = new T().GetDefault1Record(new T()).DateTimeVal;
            serviceQuery = ServiceQueryBuilder.New().Sort(nameof(TestClass.NullDateTimeVal), false).Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.True(result.Count == 4);
            ValidateSort(result, false);

            // Decimal
            if (ValidateDecimal)
            {
                serviceQuery = ServiceQueryBuilder.New().Sort(nameof(TestClass.NullDecimalVal), false).Build();
                testQueryable = serviceQuery.Apply(sourceQueryable);
                result = await testQueryable.ToListAsync();
                Assert.True(result.Count == 4);
                ValidateSort(result, false);
            }

            // Double
            serviceQuery = ServiceQueryBuilder.New().Sort(nameof(TestClass.NullDoubleVal), false).Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.True(result.Count == 4);
            ValidateSort(result, false);

            // Float
            serviceQuery = ServiceQueryBuilder.New().Sort(nameof(TestClass.NullFloatVal), false).Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.True(result.Count == 4);
            ValidateSort(result, false);

            // Guid
            serviceQuery = ServiceQueryBuilder.New().Sort(nameof(TestClass.NullGuidVal), false).Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.True(result.Count == 4);
            ValidateSort(result, false);

            // Int
            serviceQuery = ServiceQueryBuilder.New().Sort(nameof(TestClass.NullIntVal), false).Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.True(result.Count == 4);
            ValidateSort(result, false);

            // Long
            serviceQuery = ServiceQueryBuilder.New().Sort(nameof(TestClass.NullLongVal), false).Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.True(result.Count == 4);
            ValidateSort(result, false);

            // SByte
            serviceQuery = ServiceQueryBuilder.New().Sort(nameof(TestClass.NullSByteVal), false).Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.True(result.Count == 4);
            ValidateSort(result, false);

            // Short
            serviceQuery = ServiceQueryBuilder.New().Sort(nameof(TestClass.NullShortVal), false).Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.True(result.Count == 4);
            ValidateSort(result, false);

            // Single
            serviceQuery = ServiceQueryBuilder.New().Sort(nameof(TestClass.NullSingleVal), false).Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.True(result.Count == 4);
            ValidateSort(result, false);

            // String
            serviceQuery = ServiceQueryBuilder.New().Sort(nameof(TestClass.NullStringVal), false).Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.True(result.Count == 4);
            ValidateSort(result, false);

#if NET6_0_OR_GREATER
            if (ValidateTimeOnly)
            {
                // TimeOnly
                var tempTimeOnly = new T().GetDefault1Record(new T()).TimeOnlyVal;
                serviceQuery = ServiceQueryBuilder.New().Sort(nameof(TestClass.NullTimeOnlyVal), false).Build();
                testQueryable = serviceQuery.Apply(sourceQueryable);
                result = await testQueryable.ToListAsync();
                Assert.True(result.Count == 4);
                ValidateSort(result, false);
            }
#endif

            if (ValidateTimeSpan)
            {
                // TimeSpan
                serviceQuery = ServiceQueryBuilder.New().Sort(nameof(TestClass.NullTimeSpanVal), false).Build();
                testQueryable = serviceQuery.Apply(sourceQueryable);
                result = await testQueryable.ToListAsync();
                Assert.True(result.Count == 4);
                ValidateSort(result, false);
            }
#if NET7_0_OR_GREATER
            if (ValidateUInt128)
            {
                // UInt128
                serviceQuery = ServiceQueryBuilder.New().Sort(nameof(TestClass.NullUInt128Val), false).Build();
                testQueryable = serviceQuery.Apply(sourceQueryable);
                result = await testQueryable.ToListAsync();
                Assert.True(result.Count == 4);
                ValidateSort(result, false);
            }
#endif
            if (ValidateUInt64)
            {
                // UInt64
                serviceQuery = ServiceQueryBuilder.New().Sort(nameof(TestClass.NullUInt64Val), false).Build();
                testQueryable = serviceQuery.Apply(sourceQueryable);
                result = await testQueryable.ToListAsync();
                Assert.True(result.Count == 4);
                ValidateSort(result, false);
            }

            // UInt32
            serviceQuery = ServiceQueryBuilder.New().Sort(nameof(TestClass.NullUInt32Val), false).Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.True(result.Count == 4);
            ValidateSort(result, false);

            // UInt16
            serviceQuery = ServiceQueryBuilder.New().Sort(nameof(TestClass.NullUInt16Val), false).Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.True(result.Count == 4);
            ValidateSort(result, false);
        }

        [Fact]
        public async Task NullCopySortAscRequestTest()
        {
            var sourceQueryable = await GetTestNullCopyListAsync();
            IQueryable<T> testQueryable;
            IServiceQueryRequest serviceQuery;
            List<T> result;

            // Boolean
            serviceQuery = ServiceQueryRequestBuilder.New().Sort(nameof(TestClass.NullBoolVal), true).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.True(result.Count == 4);
            result = await testQueryable.ToListAsync();
            Assert.True(result.Count == 4);

            // ByteArray
            //var tempbyteArray = new byte[1] { 1 };
            //serviceQuery = ServiceQueryRequestBuilder.New().IsEqual(nameof(TestClass.NullByteArrayVal), System.Text.Encoding.UTF8.GetString(tempbyteArray)).Build();
            //testQueryable = serviceQuery.GetQueryable(sourceQueryable);
            //result = await testQueryable.ToListAsync();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 1);
            //result = await testQueryable.ToListAsync();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 1);

            // Byte
            serviceQuery = ServiceQueryRequestBuilder.New().Sort(nameof(TestClass.NullByteVal), true).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.True(result.Count == 4);
            ValidateSort(result, true);

            // Char
            serviceQuery = ServiceQueryRequestBuilder.New().Sort(nameof(TestClass.NullCharVal), true).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.True(result.Count == 4);
            ValidateSort(result, true);

            if (ValidateDateTimeOffset)
            {
                // DateTimeOffset
                var tempDateTimeOffset = new T().GetDefault1Record(new T()).DateTimeOffsetVal;
                serviceQuery = ServiceQueryRequestBuilder.New().Sort(nameof(TestClass.NullDateTimeOffsetVal), true).Build();
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
                result = await testQueryable.ToListAsync();
                Assert.True(result.Count == 4);
                ValidateSort(result, true);
            }

#if NET6_0_OR_GREATER
            // DateOnly
            var tempDateOnly = new T().GetDefault1Record(new T()).DateOnlyVal;
            serviceQuery = ServiceQueryRequestBuilder.New().Sort(nameof(TestClass.NullDateOnlyVal), true).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.True(result.Count == 4);
            ValidateSort(result, true);
#endif

            // DateTime
            var tempDateTime = new T().GetDefault1Record(new T()).DateTimeVal;
            serviceQuery = ServiceQueryRequestBuilder.New().Sort(nameof(TestClass.NullDateTimeVal), true).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.True(result.Count == 4);
            ValidateSort(result, true);

            // Decimal
            if (ValidateDecimal)
            {
                serviceQuery = ServiceQueryRequestBuilder.New().Sort(nameof(TestClass.NullDecimalVal), true).Build();
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
                result = await testQueryable.ToListAsync();
                Assert.True(result.Count == 4);
                ValidateSort(result, true);
            }

            // Double
            serviceQuery = ServiceQueryRequestBuilder.New().Sort(nameof(TestClass.NullDoubleVal), true).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.True(result.Count == 4);
            ValidateSort(result, true);

            // Float
            serviceQuery = ServiceQueryRequestBuilder.New().Sort(nameof(TestClass.NullFloatVal), true).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.True(result.Count == 4);
            ValidateSort(result, true);

            // Guid
            serviceQuery = ServiceQueryRequestBuilder.New().Sort(nameof(TestClass.NullGuidVal), true).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.True(result.Count == 4);
            ValidateSort(result, true);

            // Int
            serviceQuery = ServiceQueryRequestBuilder.New().Sort(nameof(TestClass.NullIntVal), true).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.True(result.Count == 4);
            ValidateSort(result, true);

            // Long
            serviceQuery = ServiceQueryRequestBuilder.New().Sort(nameof(TestClass.NullLongVal), true).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.True(result.Count == 4);
            ValidateSort(result, true);

            // SByte
            serviceQuery = ServiceQueryRequestBuilder.New().Sort(nameof(TestClass.NullSByteVal), true).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.True(result.Count == 4);
            ValidateSort(result, true);

            // Short
            serviceQuery = ServiceQueryRequestBuilder.New().Sort(nameof(TestClass.NullShortVal), true).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.True(result.Count == 4);
            ValidateSort(result, true);

            // Single
            serviceQuery = ServiceQueryRequestBuilder.New().Sort(nameof(TestClass.NullSingleVal), true).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.True(result.Count == 4);
            ValidateSort(result, true);

            // String
            serviceQuery = ServiceQueryRequestBuilder.New().Sort(nameof(TestClass.NullStringVal), true).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.True(result.Count == 4);
            ValidateSort(result, true);

#if NET6_0_OR_GREATER
            if (ValidateTimeOnly)
            {
                // TimeOnly
                var tempTimeOnly = new T().GetDefault1Record(new T()).TimeOnlyVal;
                serviceQuery = ServiceQueryRequestBuilder.New().Sort(nameof(TestClass.NullTimeOnlyVal), true).Build();
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
                result = await testQueryable.ToListAsync();
                Assert.True(result.Count == 4);
                ValidateSort(result, true);
            }
#endif

            if (ValidateTimeSpan)
            {
                // TimeSpan
                serviceQuery = ServiceQueryRequestBuilder.New().Sort(nameof(TestClass.NullTimeSpanVal), true).Build();
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
                result = await testQueryable.ToListAsync();
                Assert.True(result.Count == 4);
                ValidateSort(result, true);
            }
#if NET7_0_OR_GREATER
            if (ValidateUInt128)
            {
                // UInt128
                serviceQuery = ServiceQueryRequestBuilder.New().Sort(nameof(TestClass.NullUInt128Val), true).Build();
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
                result = await testQueryable.ToListAsync();
                Assert.True(result.Count == 4);
                ValidateSort(result, true);
            }
#endif
            if (ValidateUInt64)
            {
                // UInt64
                serviceQuery = ServiceQueryRequestBuilder.New().Sort(nameof(TestClass.NullUInt64Val), true).Build();
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
                result = await testQueryable.ToListAsync();
                Assert.True(result.Count == 4);
                ValidateSort(result, true);
            }

            // UInt32
            serviceQuery = ServiceQueryRequestBuilder.New().Sort(nameof(TestClass.NullUInt32Val), true).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.True(result.Count == 4);
            ValidateSort(result, true);

            // UInt16
            serviceQuery = ServiceQueryRequestBuilder.New().Sort(nameof(TestClass.NullUInt16Val), true).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.True(result.Count == 4);
            ValidateSort(result, true);
        }

        [Fact]
        public async Task NullCopySortDescRequestTest()
        {
            var sourceQueryable = await GetTestNullCopyListAsync();
            IQueryable<T> testQueryable;
            IServiceQueryRequest serviceQuery;
            List<T> result;

            // Boolean
            serviceQuery = ServiceQueryRequestBuilder.New().Sort(nameof(TestClass.NullBoolVal), false).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.True(result.Count == 4);
            result = await testQueryable.ToListAsync();
            Assert.True(result.Count == 4);

            // ByteArray
            //var tempbyteArray = new byte[1] { 1 };
            //serviceQuery = ServiceQueryRequestBuilder.New().IsEqual(nameof(TestClass.NullByteArrayVal), System.Text.Encoding.UTF8.GetString(tempbyteArray)).Build();
            //testQueryable = serviceQuery.GetQueryable(sourceQueryable);
            //result = await testQueryable.ToListAsync();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 1);
            //result = await testQueryable.ToListAsync();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 1);

            // Byte
            serviceQuery = ServiceQueryRequestBuilder.New().SortDesc(nameof(TestClass.NullByteVal)).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.True(result.Count == 4);
            ValidateSort(result, false);

            // Char
            serviceQuery = ServiceQueryRequestBuilder.New().SortDesc(nameof(TestClass.NullCharVal)).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.True(result.Count == 4);
            ValidateSort(result, false);

            if (ValidateDateTimeOffset)
            {
                // DateTimeOffset
                var tempDateTimeOffset = new T().GetDefault1Record(new T()).DateTimeOffsetVal;
                serviceQuery = ServiceQueryRequestBuilder.New().SortDesc(nameof(TestClass.NullDateTimeOffsetVal)).Build();
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
                result = await testQueryable.ToListAsync();
                Assert.True(result.Count == 4);
                ValidateSort(result, false);
            }

#if NET6_0_OR_GREATER
            // DateOnly
            var tempDateOnly = new T().GetDefault1Record(new T()).DateOnlyVal;
            serviceQuery = ServiceQueryRequestBuilder.New().SortDesc(nameof(TestClass.NullDateOnlyVal)).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.True(result.Count == 4);
            ValidateSort(result, false);
#endif

            // DateTime
            var tempDateTime = new T().GetDefault1Record(new T()).DateTimeVal;
            serviceQuery = ServiceQueryRequestBuilder.New().SortDesc(nameof(TestClass.NullDateTimeVal)).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.True(result.Count == 4);
            ValidateSort(result, false);

            // Decimal
            if (ValidateDecimal)
            {
                serviceQuery = ServiceQueryRequestBuilder.New().SortDesc(nameof(TestClass.NullDecimalVal)).Build();
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
                result = await testQueryable.ToListAsync();
                Assert.True(result.Count == 4);
                ValidateSort(result, false);
            }

            // Double
            serviceQuery = ServiceQueryRequestBuilder.New().Sort(nameof(TestClass.NullDoubleVal), false).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.True(result.Count == 4);
            ValidateSort(result, false);

            // Float
            serviceQuery = ServiceQueryRequestBuilder.New().SortDesc(nameof(TestClass.NullFloatVal)).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.True(result.Count == 4);
            ValidateSort(result, false);

            // Guid
            serviceQuery = ServiceQueryRequestBuilder.New().SortDesc(nameof(TestClass.NullGuidVal)).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.True(result.Count == 4);
            ValidateSort(result, false);

            // Int
            serviceQuery = ServiceQueryRequestBuilder.New().SortDesc(nameof(TestClass.NullIntVal)).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.True(result.Count == 4);
            ValidateSort(result, false);

            // Long
            serviceQuery = ServiceQueryRequestBuilder.New().SortDesc(nameof(TestClass.NullLongVal)).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.True(result.Count == 4);
            ValidateSort(result, false);

            // SByte
            serviceQuery = ServiceQueryRequestBuilder.New().SortDesc(nameof(TestClass.NullSByteVal)).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.True(result.Count == 4);
            ValidateSort(result, false);

            // Short
            serviceQuery = ServiceQueryRequestBuilder.New().SortDesc(nameof(TestClass.NullShortVal)).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.True(result.Count == 4);
            ValidateSort(result, false);

            // Single
            serviceQuery = ServiceQueryRequestBuilder.New().SortDesc(nameof(TestClass.NullSingleVal)).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.True(result.Count == 4);
            ValidateSort(result, false);

            // String
            serviceQuery = ServiceQueryRequestBuilder.New().SortDesc(nameof(TestClass.NullStringVal)).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.True(result.Count == 4);
            ValidateSort(result, false);

#if NET6_0_OR_GREATER
            if (ValidateTimeOnly)
            {
                // TimeOnly
                var tempTimeOnly = new T().GetDefault1Record(new T()).TimeOnlyVal;
                serviceQuery = ServiceQueryRequestBuilder.New().SortDesc(nameof(TestClass.NullTimeOnlyVal)).Build();
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
                result = await testQueryable.ToListAsync();
                Assert.True(result.Count == 4);
                ValidateSort(result, false);
            }
#endif

            if (ValidateTimeSpan)
            {
                // TimeSpan
                serviceQuery = ServiceQueryRequestBuilder.New().Sort(nameof(TestClass.NullTimeSpanVal), false).Build();
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
                result = await testQueryable.ToListAsync();
                Assert.True(result.Count == 4);
                ValidateSort(result, false);
            }
#if NET7_0_OR_GREATER
            if (ValidateUInt128)
            {
                // UInt128
                serviceQuery = ServiceQueryRequestBuilder.New().Sort(nameof(TestClass.NullUInt128Val), false).Build();
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
                result = await testQueryable.ToListAsync();
                Assert.True(result.Count == 4);
                ValidateSort(result, false);
            }
#endif
            if (ValidateUInt64)
            {
                // UInt64
                serviceQuery = ServiceQueryRequestBuilder.New().Sort(nameof(TestClass.NullUInt64Val), false).Build();
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
                result = await testQueryable.ToListAsync();
                Assert.True(result.Count == 4);
                ValidateSort(result, false);
            }

            // UInt32
            serviceQuery = ServiceQueryRequestBuilder.New().Sort(nameof(TestClass.NullUInt32Val), false).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.True(result.Count == 4);
            ValidateSort(result, false);

            // UInt16
            serviceQuery = ServiceQueryRequestBuilder.New().Sort(nameof(TestClass.NullUInt16Val), false).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.True(result.Count == 4);
            ValidateSort(result, false);
        }
    }
}
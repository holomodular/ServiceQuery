#if NET8_0_OR_GREATER

#else
using System.Data.Entity;
#endif

namespace ServiceQuery.Xunit
{
    public class LinqAsyncComparisonIsEqualTests : ComparisonIsEqualTests<TestClass>
    {
        public override IQueryable<TestClass> GetTestList()
        {
            return new TestClass().GetDefaultList().AsQueryable();
        }
    }

    public abstract class LinqAsyncComparisonIsEqualTests<T> : BaseTest<T> where T : class, ITestClass, new()
    {
        [Fact]
        public async Task IsEqualStandardTest()
        {
            var sourceQueryable = GetTestList();
            IQueryable<T> testQueryable;
            IServiceQuery serviceQuery;
            List<T> result;

            // Boolean
            serviceQuery = ServiceQueryBuilder.New().IsEqual(nameof(TestClass.BoolVal), "true").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);
#if NET8_0_OR_GREATER
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
#else
            result = await testQueryable.ToListAsync();
#endif
            Assert.NotNull(result);
            Assert.True(result.Count == 2);

            // ByteArray
            //var tempbyteArray = new byte[1] { 1 };
            //serviceQuery = ServiceQueryBuilder.New().IsEqual(nameof(TestClass.ByteArrayVal), System.Text.Encoding.UTF8.GetString(tempbyteArray)).Build();
            //testQueryable = serviceQuery.GetQueryable(sourceQueryable);
            //result = testQueryable.ToList();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 1);
            //result = await testQueryable.ToListAsync();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 1);

            // Byte
            serviceQuery = ServiceQueryBuilder.New().IsEqual(nameof(TestClass.ByteVal), "1").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
#if NET8_0_OR_GREATER
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
#else
            result = await testQueryable.ToListAsync();
#endif
            Assert.NotNull(result);
            Assert.True(result.Count == 1);

            // Char
            serviceQuery = ServiceQueryBuilder.New().IsEqual(nameof(TestClass.CharVal), "b").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
#if NET8_0_OR_GREATER
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
#else
            result = await testQueryable.ToListAsync();
#endif
            Assert.NotNull(result);
            Assert.True(result.Count == 1);

            if (ValidateDateTimeOffset)
            {
                // DateTimeOffset
                var tempDateTimeOffset = new T().GetDefault1Record().DateTimeOffsetVal;
                serviceQuery = ServiceQueryBuilder.New().IsEqual(nameof(TestClass.DateTimeOffsetVal), tempDateTimeOffset.ToString("o")).Build();
                testQueryable = serviceQuery.Apply(sourceQueryable);
                result = testQueryable.ToList();
                Assert.NotNull(result);
                Assert.True(result.Count == 1);
#if NET8_0_OR_GREATER
                result = await testQueryable.ToAsyncEnumerable().ToListAsync();
#else
            result = await testQueryable.ToListAsync();
#endif
                Assert.NotNull(result);
                Assert.True(result.Count == 1);
            }

            // DateTime
            var tempDateTime = new T().GetDefault1Record().DateTimeVal;
            serviceQuery = ServiceQueryBuilder.New().IsEqual(nameof(TestClass.DateTimeVal), tempDateTime.ToString("o")).Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
#if NET8_0_OR_GREATER
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
#else
            result = await testQueryable.ToListAsync();
#endif
            Assert.NotNull(result);
            Assert.True(result.Count == 1);

            // Decimal
            serviceQuery = ServiceQueryBuilder.New().IsEqual(nameof(TestClass.DecimalVal), "1").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
#if NET8_0_OR_GREATER
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
#else
            result = await testQueryable.ToListAsync();
#endif
            Assert.NotNull(result);
            Assert.True(result.Count == 1);

            // Double
            serviceQuery = ServiceQueryBuilder.New().IsEqual(nameof(TestClass.DoubleVal), "1").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
#if NET8_0_OR_GREATER
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
#else
            result = await testQueryable.ToListAsync();
#endif
            Assert.NotNull(result);
            Assert.True(result.Count == 1);

            // Float
            serviceQuery = ServiceQueryBuilder.New().IsEqual(nameof(TestClass.FloatVal), "1").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
#if NET8_0_OR_GREATER
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
#else
            result = await testQueryable.ToListAsync();
#endif
            Assert.NotNull(result);
            Assert.True(result.Count == 1);

            // Guid
            serviceQuery = ServiceQueryBuilder.New().IsEqual(nameof(TestClass.GuidVal), Guid.Empty.ToString()).Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
#if NET8_0_OR_GREATER
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
#else
            result = await testQueryable.ToListAsync();
#endif
            Assert.NotNull(result);
            Assert.True(result.Count == 1);

            // Int
            serviceQuery = ServiceQueryBuilder.New().IsEqual(nameof(TestClass.IntVal), "1").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
#if NET8_0_OR_GREATER
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
#else
            result = await testQueryable.ToListAsync();
#endif
            Assert.NotNull(result);
            Assert.True(result.Count == 1);

            // Long
            serviceQuery = ServiceQueryBuilder.New().IsEqual(nameof(TestClass.LongVal), "1").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
#if NET8_0_OR_GREATER
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
#else
            result = await testQueryable.ToListAsync();
#endif
            Assert.NotNull(result);
            Assert.True(result.Count == 1);

            // SByte
            serviceQuery = ServiceQueryBuilder.New().IsEqual(nameof(TestClass.SByteVal), "1").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
#if NET8_0_OR_GREATER
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
#else
            result = await testQueryable.ToListAsync();
#endif
            Assert.NotNull(result);
            Assert.True(result.Count == 1);

            // Short
            serviceQuery = ServiceQueryBuilder.New().IsEqual(nameof(TestClass.ShortVal), "1").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
#if NET8_0_OR_GREATER
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
#else
            result = await testQueryable.ToListAsync();
#endif
            Assert.NotNull(result);
            Assert.True(result.Count == 1);

            // Single
            serviceQuery = ServiceQueryBuilder.New().IsEqual(nameof(TestClass.SingleVal), "1").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
#if NET8_0_OR_GREATER
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
#else
            result = await testQueryable.ToListAsync();
#endif
            Assert.NotNull(result);
            Assert.True(result.Count == 1);

            // String
            serviceQuery = ServiceQueryBuilder.New().IsEqual(nameof(TestClass.StringVal), "a").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
#if NET8_0_OR_GREATER
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
#else
            result = await testQueryable.ToListAsync();
#endif
            Assert.NotNull(result);
            Assert.True(result.Count == 1);

            if (ValidateTimeSpan)
            {
                // TimeSpan
                serviceQuery = ServiceQueryBuilder.New().IsEqual(nameof(TestClass.TimeSpanVal), TimeSpan.FromMilliseconds(1).ToString()).Build();
                testQueryable = serviceQuery.Apply(sourceQueryable);
                result = testQueryable.ToList();
                Assert.NotNull(result);
                Assert.True(result.Count == 1);
#if NET8_0_OR_GREATER
                result = await testQueryable.ToAsyncEnumerable().ToListAsync();
#else
            result = await testQueryable.ToListAsync();
#endif
                Assert.NotNull(result);
                Assert.True(result.Count == 1);
            }
            //#if NET7_0
            //            if (ValidateUInt128)
            //            {
            //                // UInt128
            //                serviceQuery = ServiceQueryBuilder.New().IsEqual(nameof(TestClass.UInt128Val), "1").Build();
            //                testQueryable = serviceQuery.Apply(sourceQueryable);
            //                result = testQueryable.ToList();
            //                Assert.NotNull(result);
            //                Assert.True(result.Count == 1);
            //                result = await testQueryable.ToListAsync();
            //                Assert.NotNull(result);
            //                Assert.True(result.Count == 1);
            //            }
            //#endif
            if (ValidateUInt64)
            {
                // UInt64
                serviceQuery = ServiceQueryBuilder.New().IsEqual(nameof(TestClass.UInt64Val), "1").Build();
                testQueryable = serviceQuery.Apply(sourceQueryable);
                result = testQueryable.ToList();
                Assert.NotNull(result);
                Assert.True(result.Count == 1);
#if NET8_0_OR_GREATER
                result = await testQueryable.ToAsyncEnumerable().ToListAsync();
#else
            result = await testQueryable.ToListAsync();
#endif
                Assert.NotNull(result);
                Assert.True(result.Count == 1);
            }

            // UInt32
            serviceQuery = ServiceQueryBuilder.New().IsEqual(nameof(TestClass.UInt32Val), "1").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
#if NET8_0_OR_GREATER
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
#else
            result = await testQueryable.ToListAsync();
#endif
            Assert.NotNull(result);
            Assert.True(result.Count == 1);

            // UInt16
            serviceQuery = ServiceQueryBuilder.New().IsEqual(nameof(TestClass.UInt16Val), "1").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
#if NET8_0_OR_GREATER
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
#else
            result = await testQueryable.ToListAsync();
#endif
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
        }

        [Fact]
        public async Task IsEqualRequestTest()
        {
            var sourceQueryable = GetTestList();
            IQueryable<T> testQueryable;
            IServiceQueryRequest serviceQuery;
            List<T> result;

            // Boolean
            serviceQuery = ServiceQueryRequestBuilder.New().IsEqual(nameof(TestClass.BoolVal), "true").Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);
#if NET8_0_OR_GREATER
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
#else
            result = await testQueryable.ToListAsync();
#endif
            Assert.NotNull(result);
            Assert.True(result.Count == 2);

            // ByteArray
            //var tempbyteArray = new byte[1] { 1 };
            //serviceQuery = ServiceQueryRequestBuilder.New().IsEqual(nameof(TestClass.ByteArrayVal), System.Text.Encoding.UTF8.GetString(tempbyteArray)).Build();
            //testQueryable = serviceQuery.GetQueryable(sourceQueryable);
            //result = testQueryable.ToList();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 1);
            //result = await testQueryable.ToListAsync();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 1);

            // Byte
            serviceQuery = ServiceQueryRequestBuilder.New().IsEqual(nameof(TestClass.ByteVal), "1").Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
#if NET8_0_OR_GREATER
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
#else
            result = await testQueryable.ToListAsync();
#endif
            Assert.NotNull(result);
            Assert.True(result.Count == 1);

            // Char
            serviceQuery = ServiceQueryRequestBuilder.New().IsEqual(nameof(TestClass.CharVal), "b").Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
#if NET8_0_OR_GREATER
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
#else
            result = await testQueryable.ToListAsync();
#endif
            Assert.NotNull(result);
            Assert.True(result.Count == 1);

            if (ValidateDateTimeOffset)
            {
                // DateTimeOffset
                var tempDateTimeOffset = new T().GetDefault1Record().DateTimeOffsetVal;
                serviceQuery = ServiceQueryRequestBuilder.New().IsEqual(nameof(TestClass.DateTimeOffsetVal), tempDateTimeOffset.ToString("o")).Build();
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
                result = testQueryable.ToList();
                Assert.NotNull(result);
                Assert.True(result.Count == 1);
#if NET8_0_OR_GREATER
                result = await testQueryable.ToAsyncEnumerable().ToListAsync();
#else
            result = await testQueryable.ToListAsync();
#endif
                Assert.NotNull(result);
                Assert.True(result.Count == 1);
            }

            // DateTime
            var tempDateTime = new T().GetDefault1Record().DateTimeVal;
            serviceQuery = ServiceQueryRequestBuilder.New().IsEqual(nameof(TestClass.DateTimeVal), tempDateTime.ToString("o")).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
#if NET8_0_OR_GREATER
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
#else
            result = await testQueryable.ToListAsync();
#endif
            Assert.NotNull(result);
            Assert.True(result.Count == 1);

            // Decimal
            serviceQuery = ServiceQueryRequestBuilder.New().IsEqual(nameof(TestClass.DecimalVal), "1").Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
#if NET8_0_OR_GREATER
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
#else
            result = await testQueryable.ToListAsync();
#endif
            Assert.NotNull(result);
            Assert.True(result.Count == 1);

            // Double
            serviceQuery = ServiceQueryRequestBuilder.New().IsEqual(nameof(TestClass.DoubleVal), "1").Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
#if NET8_0_OR_GREATER
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
#else
            result = await testQueryable.ToListAsync();
#endif
            Assert.NotNull(result);
            Assert.True(result.Count == 1);

            // Float
            serviceQuery = ServiceQueryRequestBuilder.New().IsEqual(nameof(TestClass.FloatVal), "1").Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
#if NET8_0_OR_GREATER
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
#else
            result = await testQueryable.ToListAsync();
#endif
            Assert.NotNull(result);
            Assert.True(result.Count == 1);

            // Guid
            serviceQuery = ServiceQueryRequestBuilder.New().IsEqual(nameof(TestClass.GuidVal), Guid.Empty.ToString()).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
#if NET8_0_OR_GREATER
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
#else
            result = await testQueryable.ToListAsync();
#endif
            Assert.NotNull(result);
            Assert.True(result.Count == 1);

            // Int
            serviceQuery = ServiceQueryRequestBuilder.New().IsEqual(nameof(TestClass.IntVal), "1").Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
#if NET8_0_OR_GREATER
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
#else
            result = await testQueryable.ToListAsync();
#endif
            Assert.NotNull(result);
            Assert.True(result.Count == 1);

            // Long
            serviceQuery = ServiceQueryRequestBuilder.New().IsEqual(nameof(TestClass.LongVal), "1").Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
#if NET8_0_OR_GREATER
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
#else
            result = await testQueryable.ToListAsync();
#endif
            Assert.NotNull(result);
            Assert.True(result.Count == 1);

            // SByte
            serviceQuery = ServiceQueryRequestBuilder.New().IsEqual(nameof(TestClass.SByteVal), "1").Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
#if NET8_0_OR_GREATER
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
#else
            result = await testQueryable.ToListAsync();
#endif
            Assert.NotNull(result);
            Assert.True(result.Count == 1);

            // Short
            serviceQuery = ServiceQueryRequestBuilder.New().IsEqual(nameof(TestClass.ShortVal), "1").Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
#if NET8_0_OR_GREATER
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
#else
            result = await testQueryable.ToListAsync();
#endif
            Assert.NotNull(result);
            Assert.True(result.Count == 1);

            // Single
            serviceQuery = ServiceQueryRequestBuilder.New().IsEqual(nameof(TestClass.SingleVal), "1").Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
#if NET8_0_OR_GREATER
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
#else
            result = await testQueryable.ToListAsync();
#endif
            Assert.NotNull(result);
            Assert.True(result.Count == 1);

            // String
            serviceQuery = ServiceQueryRequestBuilder.New().IsEqual(nameof(TestClass.StringVal), "a").Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
#if NET8_0_OR_GREATER
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
#else
            result = await testQueryable.ToListAsync();
#endif
            Assert.NotNull(result);
            Assert.True(result.Count == 1);

            if (ValidateTimeSpan)
            {
                // TimeSpan
                serviceQuery = ServiceQueryRequestBuilder.New().IsEqual(nameof(TestClass.TimeSpanVal), TimeSpan.FromMilliseconds(1).ToString()).Build();
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
                result = testQueryable.ToList();
                Assert.NotNull(result);
                Assert.True(result.Count == 1);
#if NET8_0_OR_GREATER
                result = await testQueryable.ToAsyncEnumerable().ToListAsync();
#else
            result = await testQueryable.ToListAsync();
#endif
                Assert.NotNull(result);
                Assert.True(result.Count == 1);
            }
            //#if NET7_0
            //            if (ValidateUInt128)
            //            {
            //                // UInt128
            //                serviceQuery = ServiceQueryRequestBuilder.New().IsEqual(nameof(TestClass.UInt128Val), "1").Build();
            //                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            //                result = testQueryable.ToList();
            //                Assert.NotNull(result);
            //                Assert.True(result.Count == 1);
            //                result = await testQueryable.ToListAsync();
            //                Assert.NotNull(result);
            //                Assert.True(result.Count == 1);
            //            }
            //#endif
            if (ValidateUInt64)
            {
                // UInt64
                serviceQuery = ServiceQueryRequestBuilder.New().IsEqual(nameof(TestClass.UInt64Val), "1").Build();
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
                result = testQueryable.ToList();
                Assert.NotNull(result);
                Assert.True(result.Count == 1);
#if NET8_0_OR_GREATER
                result = await testQueryable.ToAsyncEnumerable().ToListAsync();
#else
            result = await testQueryable.ToListAsync();
#endif
                Assert.NotNull(result);
                Assert.True(result.Count == 1);
            }

            // UInt32
            serviceQuery = ServiceQueryRequestBuilder.New().IsEqual(nameof(TestClass.UInt32Val), "1").Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
#if NET8_0_OR_GREATER
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
#else
            result = await testQueryable.ToListAsync();
#endif
            Assert.NotNull(result);
            Assert.True(result.Count == 1);

            // UInt16
            serviceQuery = ServiceQueryRequestBuilder.New().IsEqual(nameof(TestClass.UInt16Val), "1").Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
#if NET8_0_OR_GREATER
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
#else
            result = await testQueryable.ToListAsync();
#endif
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
        }

        [Fact]
        public async Task NullIsEqualStandardTest()
        {
            var sourceQueryable = GetTestList();
            IQueryable<T> testQueryable;
            IServiceQuery serviceQuery;
            List<T> result;

            // Boolean
            serviceQuery = ServiceQueryBuilder.New().IsEqual(nameof(TestClass.NullBoolVal), "true").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
#if NET8_0_OR_GREATER
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
#else
            result = await testQueryable.ToListAsync();
#endif
            Assert.NotNull(result);
            Assert.True(result.Count == 0);

            // ByteArray
            //var tempbyteArray = new byte[1] { 1 };
            //serviceQuery = ServiceQueryBuilder.New().IsEqual(nameof(TestClass.NullByteArrayVal), System.Text.Encoding.UTF8.GetString(tempbyteArray)).Build();
            //testQueryable = serviceQuery.GetQueryable(sourceQueryable);
            //result = testQueryable.ToList();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 0);
            //result = await testQueryable.ToListAsync();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 0);

            // Byte
            serviceQuery = ServiceQueryBuilder.New().IsEqual(nameof(TestClass.NullByteVal), "1").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
#if NET8_0_OR_GREATER
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
#else
            result = await testQueryable.ToListAsync();
#endif
            Assert.NotNull(result);
            Assert.True(result.Count == 0);

            // Char
            serviceQuery = ServiceQueryBuilder.New().IsEqual(nameof(TestClass.NullCharVal), "b").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
#if NET8_0_OR_GREATER
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
#else
            result = await testQueryable.ToListAsync();
#endif
            Assert.NotNull(result);
            Assert.True(result.Count == 0);

            if (ValidateDateTimeOffset)
            {
                // DateTimeOffset
                var tempDateTimeOffset = new T().GetDefault1Record().DateTimeOffsetVal;
                serviceQuery = ServiceQueryBuilder.New().IsEqual(nameof(TestClass.NullDateTimeOffsetVal), tempDateTimeOffset.ToString("o")).Build();
                testQueryable = serviceQuery.Apply(sourceQueryable);
                result = testQueryable.ToList();
                Assert.NotNull(result);
                Assert.True(result.Count == 0);
#if NET8_0_OR_GREATER
                result = await testQueryable.ToAsyncEnumerable().ToListAsync();
#else
            result = await testQueryable.ToListAsync();
#endif
                Assert.NotNull(result);
                Assert.True(result.Count == 0);
            }

            // DateTime
            var tempDateTime = new T().GetDefault1Record().DateTimeVal;
            serviceQuery = ServiceQueryBuilder.New().IsEqual(nameof(TestClass.NullDateTimeVal), tempDateTime.ToString("o")).Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
#if NET8_0_OR_GREATER
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
#else
            result = await testQueryable.ToListAsync();
#endif
            Assert.NotNull(result);
            Assert.True(result.Count == 0);

            // Decimal
            serviceQuery = ServiceQueryBuilder.New().IsEqual(nameof(TestClass.NullDecimalVal), "1").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
#if NET8_0_OR_GREATER
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
#else
            result = await testQueryable.ToListAsync();
#endif
            Assert.NotNull(result);
            Assert.True(result.Count == 0);

            // Double
            serviceQuery = ServiceQueryBuilder.New().IsEqual(nameof(TestClass.NullDoubleVal), "1").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
#if NET8_0_OR_GREATER
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
#else
            result = await testQueryable.ToListAsync();
#endif
            Assert.NotNull(result);
            Assert.True(result.Count == 0);

            // Float
            serviceQuery = ServiceQueryBuilder.New().IsEqual(nameof(TestClass.NullFloatVal), "1").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
#if NET8_0_OR_GREATER
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
#else
            result = await testQueryable.ToListAsync();
#endif
            Assert.NotNull(result);
            Assert.True(result.Count == 0);

            // Guid
            serviceQuery = ServiceQueryBuilder.New().IsEqual(nameof(TestClass.NullGuidVal), Guid.Empty.ToString()).Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
#if NET8_0_OR_GREATER
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
#else
            result = await testQueryable.ToListAsync();
#endif
            Assert.NotNull(result);
            Assert.True(result.Count == 0);

            // Int
            serviceQuery = ServiceQueryBuilder.New().IsEqual(nameof(TestClass.NullIntVal), "1").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
#if NET8_0_OR_GREATER
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
#else
            result = await testQueryable.ToListAsync();
#endif
            Assert.NotNull(result);
            Assert.True(result.Count == 0);

            // Long
            serviceQuery = ServiceQueryBuilder.New().IsEqual(nameof(TestClass.NullLongVal), "1").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
#if NET8_0_OR_GREATER
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
#else
            result = await testQueryable.ToListAsync();
#endif
            Assert.NotNull(result);
            Assert.True(result.Count == 0);

            // SByte
            serviceQuery = ServiceQueryBuilder.New().IsEqual(nameof(TestClass.NullSByteVal), "1").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
#if NET8_0_OR_GREATER
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
#else
            result = await testQueryable.ToListAsync();
#endif
            Assert.NotNull(result);
            Assert.True(result.Count == 0);

            // Short
            serviceQuery = ServiceQueryBuilder.New().IsEqual(nameof(TestClass.NullShortVal), "1").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
#if NET8_0_OR_GREATER
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
#else
            result = await testQueryable.ToListAsync();
#endif
            Assert.NotNull(result);
            Assert.True(result.Count == 0);

            // Single
            serviceQuery = ServiceQueryBuilder.New().IsEqual(nameof(TestClass.NullSingleVal), "1").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
#if NET8_0_OR_GREATER
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
#else
            result = await testQueryable.ToListAsync();
#endif
            Assert.NotNull(result);
            Assert.True(result.Count == 0);

            // String
            serviceQuery = ServiceQueryBuilder.New().IsEqual(nameof(TestClass.NullStringVal), "a").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
#if NET8_0_OR_GREATER
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
#else
            result = await testQueryable.ToListAsync();
#endif
            Assert.NotNull(result);
            Assert.True(result.Count == 0);

            if (ValidateTimeSpan)
            {
                // TimeSpan
                serviceQuery = ServiceQueryBuilder.New().IsEqual(nameof(TestClass.NullTimeSpanVal), TimeSpan.FromMilliseconds(1).ToString()).Build();
                testQueryable = serviceQuery.Apply(sourceQueryable);
                result = testQueryable.ToList();
                Assert.NotNull(result);
                Assert.True(result.Count == 0);
#if NET8_0_OR_GREATER
                result = await testQueryable.ToAsyncEnumerable().ToListAsync();
#else
            result = await testQueryable.ToListAsync();
#endif
                Assert.NotNull(result);
                Assert.True(result.Count == 0);
            }
            //#if NET7_0
            //            if (ValidateUInt128)
            //            {
            //                // UInt128
            //                serviceQuery = ServiceQueryBuilder.New().IsEqual(nameof(TestClass.NullUInt128Val), "1").Build();
            //                testQueryable = serviceQuery.Apply(sourceQueryable);
            //                result = testQueryable.ToList();
            //                Assert.NotNull(result);
            //                Assert.True(result.Count == 0);
            //                result = await testQueryable.ToListAsync();
            //                Assert.NotNull(result);
            //                Assert.True(result.Count == 0);
            //            }
            //#endif
            if (ValidateUInt64)
            {
                // UInt64
                serviceQuery = ServiceQueryBuilder.New().IsEqual(nameof(TestClass.NullUInt64Val), "1").Build();
                testQueryable = serviceQuery.Apply(sourceQueryable);
                result = testQueryable.ToList();
                Assert.NotNull(result);
                Assert.True(result.Count == 0);
#if NET8_0_OR_GREATER
                result = await testQueryable.ToAsyncEnumerable().ToListAsync();
#else
            result = await testQueryable.ToListAsync();
#endif
                Assert.NotNull(result);
                Assert.True(result.Count == 0);
            }

            // UInt32
            serviceQuery = ServiceQueryBuilder.New().IsEqual(nameof(TestClass.NullUInt32Val), "1").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
#if NET8_0_OR_GREATER
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
#else
            result = await testQueryable.ToListAsync();
#endif
            Assert.NotNull(result);
            Assert.True(result.Count == 0);

            // UInt16
            serviceQuery = ServiceQueryBuilder.New().IsEqual(nameof(TestClass.NullUInt16Val), "1").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
#if NET8_0_OR_GREATER
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
#else
            result = await testQueryable.ToListAsync();
#endif
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
        }

        [Fact]
        public async Task NullIsEqualRequestTest()
        {
            var sourceQueryable = GetTestList();
            IQueryable<T> testQueryable;
            IServiceQueryRequest serviceQuery;
            List<T> result;

            // Boolean
            serviceQuery = ServiceQueryRequestBuilder.New().IsEqual(nameof(TestClass.NullBoolVal), "true").Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
#if NET8_0_OR_GREATER
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
#else
            result = await testQueryable.ToListAsync();
#endif
            Assert.NotNull(result);
            Assert.True(result.Count == 0);

            // ByteArray
            //var tempbyteArray = new byte[1] { 1 };
            //serviceQuery = ServiceQueryRequestBuilder.New().IsEqual(nameof(TestClass.NullByteArrayVal), System.Text.Encoding.UTF8.GetString(tempbyteArray)).Build();
            //testQueryable = serviceQuery.GetQueryable(sourceQueryable);
            //result = testQueryable.ToList();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 0);
            //result = await testQueryable.ToListAsync();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 0);

            // Byte
            serviceQuery = ServiceQueryRequestBuilder.New().IsEqual(nameof(TestClass.NullByteVal), "1").Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
#if NET8_0_OR_GREATER
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
#else
            result = await testQueryable.ToListAsync();
#endif
            Assert.NotNull(result);
            Assert.True(result.Count == 0);

            // Char
            serviceQuery = ServiceQueryRequestBuilder.New().IsEqual(nameof(TestClass.NullCharVal), "b").Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
#if NET8_0_OR_GREATER
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
#else
            result = await testQueryable.ToListAsync();
#endif
            Assert.NotNull(result);
            Assert.True(result.Count == 0);

            if (ValidateDateTimeOffset)
            {
                // DateTimeOffset
                var tempDateTimeOffset = new T().GetDefault1Record().DateTimeOffsetVal;
                serviceQuery = ServiceQueryRequestBuilder.New().IsEqual(nameof(TestClass.NullDateTimeOffsetVal), tempDateTimeOffset.ToString("o")).Build();
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
                result = testQueryable.ToList();
                Assert.NotNull(result);
                Assert.True(result.Count == 0);
#if NET8_0_OR_GREATER
                result = await testQueryable.ToAsyncEnumerable().ToListAsync();
#else
            result = await testQueryable.ToListAsync();
#endif
                Assert.NotNull(result);
                Assert.True(result.Count == 0);
            }

            // DateTime
            var tempDateTime = new T().GetDefault1Record().DateTimeVal;
            serviceQuery = ServiceQueryRequestBuilder.New().IsEqual(nameof(TestClass.NullDateTimeVal), tempDateTime.ToString("o")).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
#if NET8_0_OR_GREATER
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
#else
            result = await testQueryable.ToListAsync();
#endif
            Assert.NotNull(result);
            Assert.True(result.Count == 0);

            // Decimal
            serviceQuery = ServiceQueryRequestBuilder.New().IsEqual(nameof(TestClass.NullDecimalVal), "1").Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
#if NET8_0_OR_GREATER
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
#else
            result = await testQueryable.ToListAsync();
#endif
            Assert.NotNull(result);
            Assert.True(result.Count == 0);

            // Double
            serviceQuery = ServiceQueryRequestBuilder.New().IsEqual(nameof(TestClass.NullDoubleVal), "1").Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
#if NET8_0_OR_GREATER
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
#else
            result = await testQueryable.ToListAsync();
#endif
            Assert.NotNull(result);
            Assert.True(result.Count == 0);

            // Float
            serviceQuery = ServiceQueryRequestBuilder.New().IsEqual(nameof(TestClass.NullFloatVal), "1").Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
#if NET8_0_OR_GREATER
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
#else
            result = await testQueryable.ToListAsync();
#endif
            Assert.NotNull(result);
            Assert.True(result.Count == 0);

            // Guid
            serviceQuery = ServiceQueryRequestBuilder.New().IsEqual(nameof(TestClass.NullGuidVal), Guid.Empty.ToString()).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
#if NET8_0_OR_GREATER
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
#else
            result = await testQueryable.ToListAsync();
#endif
            Assert.NotNull(result);
            Assert.True(result.Count == 0);

            // Int
            serviceQuery = ServiceQueryRequestBuilder.New().IsEqual(nameof(TestClass.NullIntVal), "1").Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
#if NET8_0_OR_GREATER
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
#else
            result = await testQueryable.ToListAsync();
#endif
            Assert.NotNull(result);
            Assert.True(result.Count == 0);

            // Long
            serviceQuery = ServiceQueryRequestBuilder.New().IsEqual(nameof(TestClass.NullLongVal), "1").Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
#if NET8_0_OR_GREATER
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
#else
            result = await testQueryable.ToListAsync();
#endif
            Assert.NotNull(result);
            Assert.True(result.Count == 0);

            // SByte
            serviceQuery = ServiceQueryRequestBuilder.New().IsEqual(nameof(TestClass.NullSByteVal), "1").Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
#if NET8_0_OR_GREATER
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
#else
            result = await testQueryable.ToListAsync();
#endif
            Assert.NotNull(result);
            Assert.True(result.Count == 0);

            // Short
            serviceQuery = ServiceQueryRequestBuilder.New().IsEqual(nameof(TestClass.NullShortVal), "1").Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
#if NET8_0_OR_GREATER
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
#else
            result = await testQueryable.ToListAsync();
#endif
            Assert.NotNull(result);
            Assert.True(result.Count == 0);

            // Single
            serviceQuery = ServiceQueryRequestBuilder.New().IsEqual(nameof(TestClass.NullSingleVal), "1").Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
#if NET8_0_OR_GREATER
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
#else
            result = await testQueryable.ToListAsync();
#endif
            Assert.NotNull(result);
            Assert.True(result.Count == 0);

            // String
            serviceQuery = ServiceQueryRequestBuilder.New().IsEqual(nameof(TestClass.NullStringVal), "a").Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
#if NET8_0_OR_GREATER
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
#else
            result = await testQueryable.ToListAsync();
#endif
            Assert.NotNull(result);
            Assert.True(result.Count == 0);

            if (ValidateTimeSpan)
            {
                // TimeSpan
                serviceQuery = ServiceQueryRequestBuilder.New().IsEqual(nameof(TestClass.NullTimeSpanVal), TimeSpan.FromMilliseconds(1).ToString()).Build();
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
                result = testQueryable.ToList();
                Assert.NotNull(result);
                Assert.True(result.Count == 0);
#if NET8_0_OR_GREATER
                result = await testQueryable.ToAsyncEnumerable().ToListAsync();
#else
            result = await testQueryable.ToListAsync();
#endif
                Assert.NotNull(result);
                Assert.True(result.Count == 0);
            }
            //#if NET7_0
            //            if (ValidateUInt128)
            //            {
            //                // UInt128
            //                serviceQuery = ServiceQueryRequestBuilder.New().IsEqual(nameof(TestClass.NullUInt128Val), "1").Build();
            //                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            //                result = testQueryable.ToList();
            //                Assert.NotNull(result);
            //                Assert.True(result.Count == 0);
            //                result = await testQueryable.ToListAsync();
            //                Assert.NotNull(result);
            //                Assert.True(result.Count == 0);
            //            }
            //#endif
            if (ValidateUInt64)
            {
                // UInt64
                serviceQuery = ServiceQueryRequestBuilder.New().IsEqual(nameof(TestClass.NullUInt64Val), "1").Build();
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
                result = testQueryable.ToList();
                Assert.NotNull(result);
                Assert.True(result.Count == 0);
#if NET8_0_OR_GREATER
                result = await testQueryable.ToAsyncEnumerable().ToListAsync();
#else
            result = await testQueryable.ToListAsync();
#endif
                Assert.NotNull(result);
                Assert.True(result.Count == 0);
            }

            // UInt32
            serviceQuery = ServiceQueryRequestBuilder.New().IsEqual(nameof(TestClass.NullUInt32Val), "1").Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
#if NET8_0_OR_GREATER
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
#else
            result = await testQueryable.ToListAsync();
#endif
            Assert.NotNull(result);
            Assert.True(result.Count == 0);

            // UInt16
            serviceQuery = ServiceQueryRequestBuilder.New().IsEqual(nameof(TestClass.NullUInt16Val), "1").Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
#if NET8_0_OR_GREATER
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
#else
            result = await testQueryable.ToListAsync();
#endif
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
        }

        [Fact]
        public async Task FoundNullIsEqualRequestTest()
        {
            var sourceQueryable = GetTestList();
            IQueryable<T> testQueryable;
            IServiceQueryRequest serviceQuery;
            List<T> result;

            // Boolean
            serviceQuery = ServiceQueryRequestBuilder.New().IsEqual(nameof(TestClass.NullBoolVal), null).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
#if NET8_0_OR_GREATER
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
#else
            result = await testQueryable.ToListAsync();
#endif
            Assert.NotNull(result);
            Assert.True(result.Count == 4);

            // ByteArray
            //var tempbyteArray = new byte[1] { 1 };
            //serviceQuery = ServiceQueryRequestBuilder.New().IsEqual(nameof(TestClass.NullByteArrayVal), System.Text.Encoding.UTF8.GetString(tempbyteArray)).Build();
            //testQueryable = serviceQuery.GetQueryable(sourceQueryable);
            //result = testQueryable.ToList();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 4);
            //result = await testQueryable.ToListAsync();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 4);

            // Byte
            serviceQuery = ServiceQueryRequestBuilder.New().IsEqual(nameof(TestClass.NullByteVal), null).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
#if NET8_0_OR_GREATER
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
#else
            result = await testQueryable.ToListAsync();
#endif
            Assert.NotNull(result);
            Assert.True(result.Count == 4);

            // Char
            serviceQuery = ServiceQueryRequestBuilder.New().IsEqual(nameof(TestClass.NullCharVal), null).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
#if NET8_0_OR_GREATER
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
#else
            result = await testQueryable.ToListAsync();
#endif
            Assert.NotNull(result);
            Assert.True(result.Count == 4);

            if (ValidateDateTimeOffset)
            {
                // DateTimeOffset
                var tempDateTimeOffset = new T().GetDefault1Record().DateTimeOffsetVal;
                serviceQuery = ServiceQueryRequestBuilder.New().IsEqual(nameof(TestClass.NullDateTimeOffsetVal), null).Build();
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
                result = testQueryable.ToList();
                Assert.NotNull(result);
                Assert.True(result.Count == 4);
#if NET8_0_OR_GREATER
                result = await testQueryable.ToAsyncEnumerable().ToListAsync();
#else
            result = await testQueryable.ToListAsync();
#endif
                Assert.NotNull(result);
                Assert.True(result.Count == 4);
            }

            // DateTime
            var tempDateTime = new T().GetDefault1Record().DateTimeVal;
            serviceQuery = ServiceQueryRequestBuilder.New().IsEqual(nameof(TestClass.NullDateTimeVal), null).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
#if NET8_0_OR_GREATER
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
#else
            result = await testQueryable.ToListAsync();
#endif
            Assert.NotNull(result);
            Assert.True(result.Count == 4);

            // Decimal
            serviceQuery = ServiceQueryRequestBuilder.New().IsEqual(nameof(TestClass.NullDecimalVal), null).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
#if NET8_0_OR_GREATER
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
#else
            result = await testQueryable.ToListAsync();
#endif
            Assert.NotNull(result);
            Assert.True(result.Count == 4);

            // Double
            serviceQuery = ServiceQueryRequestBuilder.New().IsEqual(nameof(TestClass.NullDoubleVal), null).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
#if NET8_0_OR_GREATER
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
#else
            result = await testQueryable.ToListAsync();
#endif
            Assert.NotNull(result);
            Assert.True(result.Count == 4);

            // Float
            serviceQuery = ServiceQueryRequestBuilder.New().IsEqual(nameof(TestClass.NullFloatVal), null).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
#if NET8_0_OR_GREATER
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
#else
            result = await testQueryable.ToListAsync();
#endif
            Assert.NotNull(result);
            Assert.True(result.Count == 4);

            // Guid
            serviceQuery = ServiceQueryRequestBuilder.New().IsEqual(nameof(TestClass.NullGuidVal), null).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
#if NET8_0_OR_GREATER
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
#else
            result = await testQueryable.ToListAsync();
#endif
            Assert.NotNull(result);
            Assert.True(result.Count == 4);

            // Int
            serviceQuery = ServiceQueryRequestBuilder.New().IsEqual(nameof(TestClass.NullIntVal), null).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
#if NET8_0_OR_GREATER
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
#else
            result = await testQueryable.ToListAsync();
#endif
            Assert.NotNull(result);
            Assert.True(result.Count == 4);

            // Long
            serviceQuery = ServiceQueryRequestBuilder.New().IsEqual(nameof(TestClass.NullLongVal), null).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
#if NET8_0_OR_GREATER
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
#else
            result = await testQueryable.ToListAsync();
#endif
            Assert.NotNull(result);
            Assert.True(result.Count == 4);

            // SByte
            serviceQuery = ServiceQueryRequestBuilder.New().IsEqual(nameof(TestClass.NullSByteVal), null).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
#if NET8_0_OR_GREATER
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
#else
            result = await testQueryable.ToListAsync();
#endif
            Assert.NotNull(result);
            Assert.True(result.Count == 4);

            // Short
            serviceQuery = ServiceQueryRequestBuilder.New().IsEqual(nameof(TestClass.NullShortVal), null).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
#if NET8_0_OR_GREATER
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
#else
            result = await testQueryable.ToListAsync();
#endif
            Assert.NotNull(result);
            Assert.True(result.Count == 4);

            // Single
            serviceQuery = ServiceQueryRequestBuilder.New().IsEqual(nameof(TestClass.NullSingleVal), null).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
#if NET8_0_OR_GREATER
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
#else
            result = await testQueryable.ToListAsync();
#endif
            Assert.NotNull(result);
            Assert.True(result.Count == 4);

            // String
            serviceQuery = ServiceQueryRequestBuilder.New().IsEqual(nameof(TestClass.NullStringVal), null).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
#if NET8_0_OR_GREATER
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
#else
            result = await testQueryable.ToListAsync();
#endif
            Assert.NotNull(result);
            Assert.True(result.Count == 4);

            if (ValidateTimeSpan)
            {
                // TimeSpan
                serviceQuery = ServiceQueryRequestBuilder.New().IsEqual(nameof(TestClass.NullTimeSpanVal), null).Build();
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
                result = testQueryable.ToList();
                Assert.NotNull(result);
                Assert.True(result.Count == 4);
#if NET8_0_OR_GREATER
                result = await testQueryable.ToAsyncEnumerable().ToListAsync();
#else
            result = await testQueryable.ToListAsync();
#endif
                Assert.NotNull(result);
                Assert.True(result.Count == 4);
            }
            //#if NET7_0
            //            if (ValidateUInt128)
            //            {
            //                // UInt128
            //                serviceQuery = ServiceQueryRequestBuilder.New().IsEqual(nameof(TestClass.NullUInt128Val), "1").Build();
            //                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            //                result = testQueryable.ToList();
            //                Assert.NotNull(result);
            //                Assert.True(result.Count == 4);
            //                result = await testQueryable.ToListAsync();
            //                Assert.NotNull(result);
            //                Assert.True(result.Count == 4);
            //            }
            //#endif
            if (ValidateUInt64)
            {
                // UInt64
                serviceQuery = ServiceQueryRequestBuilder.New().IsEqual(nameof(TestClass.NullUInt64Val), null).Build();
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
                result = testQueryable.ToList();
                Assert.NotNull(result);
                Assert.True(result.Count == 4);
#if NET8_0_OR_GREATER
                result = await testQueryable.ToAsyncEnumerable().ToListAsync();
#else
            result = await testQueryable.ToListAsync();
#endif
                Assert.NotNull(result);
                Assert.True(result.Count == 4);
            }

            // UInt32
            serviceQuery = ServiceQueryRequestBuilder.New().IsEqual(nameof(TestClass.NullUInt32Val), null).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
#if NET8_0_OR_GREATER
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
#else
            result = await testQueryable.ToListAsync();
#endif
            Assert.NotNull(result);
            Assert.True(result.Count == 4);

            // UInt16
            serviceQuery = ServiceQueryRequestBuilder.New().IsEqual(nameof(TestClass.NullUInt16Val), null).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
#if NET8_0_OR_GREATER
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
#else
            result = await testQueryable.ToListAsync();
#endif
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
        }
    }
}
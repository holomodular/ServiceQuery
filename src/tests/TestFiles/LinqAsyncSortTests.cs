#if NET8_0_OR_GREATER

#else
using System.Data.Entity;
#endif

namespace ServiceQuery.Xunit
{
    public class LinqAsyncSortTests : SortTests<TestClass>
    {
        public override IQueryable<TestClass> GetTestList()
        {
            return new TestClass().GetDefaultList().AsQueryable();
        }
    }

    public abstract class LinqAsyncSortTests<T> : BaseTest<T> where T : class, ITestClass, new()
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
            var sourceQueryable = GetTestList();
            IQueryable<T> testQueryable;
            IServiceQuery serviceQuery;
            List<T> result;

            // Boolean
            serviceQuery = ServiceQueryBuilder.New().Sort(nameof(TestClass.BoolVal), true).Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.True(result[0].IntVal == 0 || result[0].IntVal == 2);
            Assert.True(result[1].IntVal == 0 || result[1].IntVal == 2);
            Assert.True(result[2].IntVal == 1 || result[2].IntVal == 3);
            Assert.True(result[3].IntVal == 1 || result[3].IntVal == 3);

#if NET8_0_OR_GREATER
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
#else
            result = await testQueryable.ToListAsync();
#endif
            Assert.True(result[0].IntVal == 0 || result[0].IntVal == 2);
            Assert.True(result[1].IntVal == 0 || result[1].IntVal == 2);
            Assert.True(result[2].IntVal == 1 || result[2].IntVal == 3);
            Assert.True(result[3].IntVal == 1 || result[3].IntVal == 3);

            serviceQuery = ServiceQueryBuilder.New().SortAsc(nameof(TestClass.BoolVal)).Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.True(result[0].IntVal == 0 || result[0].IntVal == 2);
            Assert.True(result[1].IntVal == 0 || result[1].IntVal == 2);
            Assert.True(result[2].IntVal == 1 || result[2].IntVal == 3);
            Assert.True(result[3].IntVal == 1 || result[3].IntVal == 3);

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
            serviceQuery = ServiceQueryBuilder.New().Sort(nameof(TestClass.ByteVal), true).Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            ValidateSort(result, true);

            // Char
            serviceQuery = ServiceQueryBuilder.New().Sort(nameof(TestClass.CharVal), true).Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            ValidateSort(result, true);

            if (ValidateDateTimeOffset)
            {
                // DateTimeOffset
                var tempDateTimeOffset = new T().GetDefault1Record().DateTimeOffsetVal;
                serviceQuery = ServiceQueryBuilder.New().Sort(nameof(TestClass.DateTimeOffsetVal), true).Build();
                testQueryable = serviceQuery.Apply(sourceQueryable);
                result = testQueryable.ToList();
                ValidateSort(result, true);
            }

            // DateTime
            var tempDateTime = new T().GetDefault1Record().DateTimeVal;
            serviceQuery = ServiceQueryBuilder.New().Sort(nameof(TestClass.DateTimeVal), true).Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            ValidateSort(result, true);

            // Decimal
            if (ValidateDecimal)
            {
                serviceQuery = ServiceQueryBuilder.New().Sort(nameof(TestClass.DecimalVal), true).Build();
                testQueryable = serviceQuery.Apply(sourceQueryable);
                result = testQueryable.ToList();
                ValidateSort(result, true);
            }

            // Double
            serviceQuery = ServiceQueryBuilder.New().Sort(nameof(TestClass.DoubleVal), true).Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            ValidateSort(result, true);

            // Float
            serviceQuery = ServiceQueryBuilder.New().Sort(nameof(TestClass.FloatVal), true).Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            ValidateSort(result, true);

            // Guid
            serviceQuery = ServiceQueryBuilder.New().Sort(nameof(TestClass.GuidVal), true).Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            ValidateSort(result, true);

            // Int
            serviceQuery = ServiceQueryBuilder.New().Sort(nameof(TestClass.IntVal), true).Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            ValidateSort(result, true);

            // Long
            serviceQuery = ServiceQueryBuilder.New().Sort(nameof(TestClass.LongVal), true).Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            ValidateSort(result, true);

            // SByte
            serviceQuery = ServiceQueryBuilder.New().Sort(nameof(TestClass.SByteVal), true).Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            ValidateSort(result, true);

            // Short
            serviceQuery = ServiceQueryBuilder.New().Sort(nameof(TestClass.ShortVal), true).Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            ValidateSort(result, true);

            // Single
            serviceQuery = ServiceQueryBuilder.New().Sort(nameof(TestClass.SingleVal), true).Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            ValidateSort(result, true);

            // String
            serviceQuery = ServiceQueryBuilder.New().Sort(nameof(TestClass.StringVal), true).Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            ValidateSort(result, true);

            if (ValidateTimeSpan)
            {
                // TimeSpan
                serviceQuery = ServiceQueryBuilder.New().Sort(nameof(TestClass.TimeSpanVal), true).Build();
                testQueryable = serviceQuery.Apply(sourceQueryable);
                result = testQueryable.ToList();
                ValidateSort(result, true);
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
            //                  result = await testQueryable.ToListAsync();
            //                Assert.NotNull(result);
            //                Assert.True(result.Count == 1);
            //            }
            //#endif
            if (ValidateUInt64)
            {
                // UInt64
                serviceQuery = ServiceQueryBuilder.New().Sort(nameof(TestClass.UInt64Val), true).Build();
                testQueryable = serviceQuery.Apply(sourceQueryable);
                result = testQueryable.ToList();
                ValidateSort(result, true);
            }

            // UInt32
            serviceQuery = ServiceQueryBuilder.New().Sort(nameof(TestClass.UInt32Val), true).Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            ValidateSort(result, true);

            // UInt16
            serviceQuery = ServiceQueryBuilder.New().Sort(nameof(TestClass.UInt16Val), true).Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            ValidateSort(result, true);
        }

        [Fact]
        public async Task SortDescTest()
        {
            var sourceQueryable = GetTestList();
            IQueryable<T> testQueryable;
            IServiceQuery serviceQuery;
            List<T> result;

            // Boolean
            serviceQuery = ServiceQueryBuilder.New().Sort(nameof(TestClass.BoolVal), false).Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.True(result[0].IntVal == 1 || result[0].IntVal == 3);
            Assert.True(result[1].IntVal == 1 || result[1].IntVal == 3);
            Assert.True(result[2].IntVal == 0 || result[2].IntVal == 2);
            Assert.True(result[3].IntVal == 0 || result[3].IntVal == 2);
#if NET8_0_OR_GREATER
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
#else
            result = await testQueryable.ToListAsync();
#endif
            Assert.True(result[0].IntVal == 1 || result[0].IntVal == 3);
            Assert.True(result[1].IntVal == 1 || result[1].IntVal == 3);
            Assert.True(result[2].IntVal == 0 || result[2].IntVal == 2);
            Assert.True(result[3].IntVal == 0 || result[3].IntVal == 2);

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
            serviceQuery = ServiceQueryBuilder.New().Sort(nameof(TestClass.ByteVal), false).Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            ValidateSort(result, false);

            // Char
            serviceQuery = ServiceQueryBuilder.New().Sort(nameof(TestClass.CharVal), false).Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            ValidateSort(result, false);

            if (ValidateDateTimeOffset)
            {
                // DateTimeOffset
                var tempDateTimeOffset = new T().GetDefault1Record().DateTimeOffsetVal;
                serviceQuery = ServiceQueryBuilder.New().Sort(nameof(TestClass.DateTimeOffsetVal), false).Build();
                testQueryable = serviceQuery.Apply(sourceQueryable);
                result = testQueryable.ToList();
                ValidateSort(result, false);
            }

            // DateTime
            var tempDateTime = new T().GetDefault1Record().DateTimeVal;
            serviceQuery = ServiceQueryBuilder.New().Sort(nameof(TestClass.DateTimeVal), false).Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            ValidateSort(result, false);

            // Decimal
            if (ValidateDecimal)
            {
                serviceQuery = ServiceQueryBuilder.New().Sort(nameof(TestClass.DecimalVal), false).Build();
                testQueryable = serviceQuery.Apply(sourceQueryable);
                result = testQueryable.ToList();
                ValidateSort(result, false);
            }

            // Double
            serviceQuery = ServiceQueryBuilder.New().Sort(nameof(TestClass.DoubleVal), false).Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            ValidateSort(result, false);

            // Float
            serviceQuery = ServiceQueryBuilder.New().Sort(nameof(TestClass.FloatVal), false).Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            ValidateSort(result, false);

            // Guid
            serviceQuery = ServiceQueryBuilder.New().Sort(nameof(TestClass.GuidVal), false).Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            ValidateSort(result, false);

            // Int
            serviceQuery = ServiceQueryBuilder.New().Sort(nameof(TestClass.IntVal), false).Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            ValidateSort(result, false);

            // Long
            serviceQuery = ServiceQueryBuilder.New().Sort(nameof(TestClass.LongVal), false).Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            ValidateSort(result, false);

            // SByte
            serviceQuery = ServiceQueryBuilder.New().Sort(nameof(TestClass.SByteVal), false).Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            ValidateSort(result, false);

            // Short
            serviceQuery = ServiceQueryBuilder.New().Sort(nameof(TestClass.ShortVal), false).Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            ValidateSort(result, false);

            // Single
            serviceQuery = ServiceQueryBuilder.New().Sort(nameof(TestClass.SingleVal), false).Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            ValidateSort(result, false);

            // String
            serviceQuery = ServiceQueryBuilder.New().Sort(nameof(TestClass.StringVal), false).Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            ValidateSort(result, false);

            if (ValidateTimeSpan)
            {
                // TimeSpan
                serviceQuery = ServiceQueryBuilder.New().Sort(nameof(TestClass.TimeSpanVal), false).Build();
                testQueryable = serviceQuery.Apply(sourceQueryable);
                result = testQueryable.ToList();
                ValidateSort(result, false);
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
            //                  result = await testQueryable.ToListAsync();
            //                Assert.NotNull(result);
            //                Assert.True(result.Count == 1);
            //            }
            //#endif
            if (ValidateUInt64)
            {
                // UInt64
                serviceQuery = ServiceQueryBuilder.New().Sort(nameof(TestClass.UInt64Val), false).Build();
                testQueryable = serviceQuery.Apply(sourceQueryable);
                result = testQueryable.ToList();
                ValidateSort(result, false);
            }

            // UInt32
            serviceQuery = ServiceQueryBuilder.New().Sort(nameof(TestClass.UInt32Val), false).Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            ValidateSort(result, false);

            // UInt16
            serviceQuery = ServiceQueryBuilder.New().Sort(nameof(TestClass.UInt16Val), false).Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            ValidateSort(result, false);
        }

        [Fact]
        public async Task SortAscRequestTest()
        {
            var sourceQueryable = GetTestList();
            IQueryable<T> testQueryable;
            IServiceQueryRequest serviceQuery;
            List<T> result;

            // Boolean
            serviceQuery = ServiceQueryRequestBuilder.New().Sort(nameof(TestClass.BoolVal), true).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.True(result[0].IntVal == 0 || result[0].IntVal == 2);
            Assert.True(result[1].IntVal == 0 || result[1].IntVal == 2);
            Assert.True(result[2].IntVal == 1 || result[2].IntVal == 3);
            Assert.True(result[3].IntVal == 1 || result[3].IntVal == 3);
#if NET8_0_OR_GREATER
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
#else
            result = await testQueryable.ToListAsync();
#endif
            Assert.True(result[0].IntVal == 0 || result[0].IntVal == 2);
            Assert.True(result[1].IntVal == 0 || result[1].IntVal == 2);
            Assert.True(result[2].IntVal == 1 || result[2].IntVal == 3);
            Assert.True(result[3].IntVal == 1 || result[3].IntVal == 3);

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
            serviceQuery = ServiceQueryRequestBuilder.New().Sort(nameof(TestClass.ByteVal), true).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            ValidateSort(result, true);

            // Char
            serviceQuery = ServiceQueryRequestBuilder.New().Sort(nameof(TestClass.CharVal), true).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            ValidateSort(result, true);

            if (ValidateDateTimeOffset)
            {
                // DateTimeOffset
                var tempDateTimeOffset = new T().GetDefault1Record().DateTimeOffsetVal;
                serviceQuery = ServiceQueryRequestBuilder.New().Sort(nameof(TestClass.DateTimeOffsetVal), true).Build();
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
                result = testQueryable.ToList();
                ValidateSort(result, true);
            }

            // DateTime
            var tempDateTime = new T().GetDefault1Record().DateTimeVal;
            serviceQuery = ServiceQueryRequestBuilder.New().Sort(nameof(TestClass.DateTimeVal), true).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            ValidateSort(result, true);

            // Decimal
            if (ValidateDecimal)
            {
                serviceQuery = ServiceQueryRequestBuilder.New().Sort(nameof(TestClass.DecimalVal), true).Build();
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
                result = testQueryable.ToList();
                ValidateSort(result, true);
            }

            // Double
            serviceQuery = ServiceQueryRequestBuilder.New().Sort(nameof(TestClass.DoubleVal), true).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            ValidateSort(result, true);

            // Float
            serviceQuery = ServiceQueryRequestBuilder.New().Sort(nameof(TestClass.FloatVal), true).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            ValidateSort(result, true);

            // Guid
            serviceQuery = ServiceQueryRequestBuilder.New().Sort(nameof(TestClass.GuidVal), true).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            ValidateSort(result, true);

            // Int
            serviceQuery = ServiceQueryRequestBuilder.New().Sort(nameof(TestClass.IntVal), true).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            ValidateSort(result, true);

            // Long
            serviceQuery = ServiceQueryRequestBuilder.New().Sort(nameof(TestClass.LongVal), true).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            ValidateSort(result, true);

            // SByte
            serviceQuery = ServiceQueryRequestBuilder.New().Sort(nameof(TestClass.SByteVal), true).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            ValidateSort(result, true);

            // Short
            serviceQuery = ServiceQueryRequestBuilder.New().Sort(nameof(TestClass.ShortVal), true).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            ValidateSort(result, true);

            // Single
            serviceQuery = ServiceQueryRequestBuilder.New().Sort(nameof(TestClass.SingleVal), true).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            ValidateSort(result, true);

            // String
            serviceQuery = ServiceQueryRequestBuilder.New().Sort(nameof(TestClass.StringVal), true).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            ValidateSort(result, true);

            if (ValidateTimeSpan)
            {
                // TimeSpan
                serviceQuery = ServiceQueryRequestBuilder.New().Sort(nameof(TestClass.TimeSpanVal), true).Build();
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
                result = testQueryable.ToList();
                ValidateSort(result, true);
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
            //                  result = await testQueryable.ToListAsync();
            //                Assert.NotNull(result);
            //                Assert.True(result.Count == 1);
            //            }
            //#endif
            if (ValidateUInt64)
            {
                // UInt64
                serviceQuery = ServiceQueryRequestBuilder.New().Sort(nameof(TestClass.UInt64Val), true).Build();
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
                result = testQueryable.ToList();
                ValidateSort(result, true);
            }

            // UInt32
            serviceQuery = ServiceQueryRequestBuilder.New().Sort(nameof(TestClass.UInt32Val), true).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            ValidateSort(result, true);

            // UInt16
            serviceQuery = ServiceQueryRequestBuilder.New().Sort(nameof(TestClass.UInt16Val), true).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            ValidateSort(result, true);
        }

        [Fact]
        public async Task SortDescRequestTest()
        {
            var sourceQueryable = GetTestList();
            IQueryable<T> testQueryable;
            IServiceQueryRequest serviceQuery;
            List<T> result;

            // Boolean
            serviceQuery = ServiceQueryRequestBuilder.New().Sort(nameof(TestClass.BoolVal), false).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.True(result[0].IntVal == 1 || result[0].IntVal == 3);
            Assert.True(result[1].IntVal == 1 || result[1].IntVal == 3);
            Assert.True(result[2].IntVal == 0 || result[2].IntVal == 2);
            Assert.True(result[3].IntVal == 0 || result[3].IntVal == 2);
#if NET8_0_OR_GREATER
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
#else
            result = await testQueryable.ToListAsync();
#endif
            Assert.True(result[0].IntVal == 1 || result[0].IntVal == 3);
            Assert.True(result[1].IntVal == 1 || result[1].IntVal == 3);
            Assert.True(result[2].IntVal == 0 || result[2].IntVal == 2);
            Assert.True(result[3].IntVal == 0 || result[3].IntVal == 2);

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
            serviceQuery = ServiceQueryRequestBuilder.New().Sort(nameof(TestClass.ByteVal), false).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            ValidateSort(result, false);

            // Char
            serviceQuery = ServiceQueryRequestBuilder.New().Sort(nameof(TestClass.CharVal), false).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            ValidateSort(result, false);

            if (ValidateDateTimeOffset)
            {
                // DateTimeOffset
                var tempDateTimeOffset = new T().GetDefault1Record().DateTimeOffsetVal;
                serviceQuery = ServiceQueryRequestBuilder.New().Sort(nameof(TestClass.DateTimeOffsetVal), false).Build();
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
                result = testQueryable.ToList();
                ValidateSort(result, false);
            }

            // DateTime
            var tempDateTime = new T().GetDefault1Record().DateTimeVal;
            serviceQuery = ServiceQueryRequestBuilder.New().Sort(nameof(TestClass.DateTimeVal), false).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            ValidateSort(result, false);

            // Decimal
            if (ValidateDecimal)
            {
                serviceQuery = ServiceQueryRequestBuilder.New().Sort(nameof(TestClass.DecimalVal), false).Build();
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
                result = testQueryable.ToList();
                ValidateSort(result, false);
            }

            // Double
            serviceQuery = ServiceQueryRequestBuilder.New().Sort(nameof(TestClass.DoubleVal), false).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            ValidateSort(result, false);

            // Float
            serviceQuery = ServiceQueryRequestBuilder.New().Sort(nameof(TestClass.FloatVal), false).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            ValidateSort(result, false);

            // Guid
            serviceQuery = ServiceQueryRequestBuilder.New().Sort(nameof(TestClass.GuidVal), false).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            ValidateSort(result, false);

            // Int
            serviceQuery = ServiceQueryRequestBuilder.New().Sort(nameof(TestClass.IntVal), false).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            ValidateSort(result, false);

            // Long
            serviceQuery = ServiceQueryRequestBuilder.New().Sort(nameof(TestClass.LongVal), false).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            ValidateSort(result, false);

            // SByte
            serviceQuery = ServiceQueryRequestBuilder.New().Sort(nameof(TestClass.SByteVal), false).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            ValidateSort(result, false);

            // Short
            serviceQuery = ServiceQueryRequestBuilder.New().Sort(nameof(TestClass.ShortVal), false).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            ValidateSort(result, false);

            // Single
            serviceQuery = ServiceQueryRequestBuilder.New().Sort(nameof(TestClass.SingleVal), false).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            ValidateSort(result, false);

            // String
            serviceQuery = ServiceQueryRequestBuilder.New().Sort(nameof(TestClass.StringVal), false).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            ValidateSort(result, false);

            if (ValidateTimeSpan)
            {
                // TimeSpan
                serviceQuery = ServiceQueryRequestBuilder.New().Sort(nameof(TestClass.TimeSpanVal), false).Build();
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
                result = testQueryable.ToList();
                ValidateSort(result, false);
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
            //                  result = await testQueryable.ToListAsync();
            //                Assert.NotNull(result);
            //                Assert.True(result.Count == 1);
            //            }
            //#endif
            if (ValidateUInt64)
            {
                // UInt64
                serviceQuery = ServiceQueryRequestBuilder.New().Sort(nameof(TestClass.UInt64Val), false).Build();
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
                result = testQueryable.ToList();
                ValidateSort(result, false);
            }

            // UInt32
            serviceQuery = ServiceQueryRequestBuilder.New().Sort(nameof(TestClass.UInt32Val), false).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            ValidateSort(result, false);

            // UInt16
            serviceQuery = ServiceQueryRequestBuilder.New().Sort(nameof(TestClass.UInt16Val), false).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            ValidateSort(result, false);
        }

        [Fact]
        public async Task NullSortAscTest()
        {
            var sourceQueryable = GetTestList();
            IQueryable<T> testQueryable;
            IServiceQuery serviceQuery;
            List<T> result;

            // Boolean
            serviceQuery = ServiceQueryBuilder.New().Sort(nameof(TestClass.NullBoolVal), true).Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.True(result.Count == 4);
#if NET8_0_OR_GREATER
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
#else
            result = await testQueryable.ToListAsync();
#endif
            Assert.True(result.Count == 4);
            //Assert.True(result[0].IntVal == 0 || result[0].IntVal == 2);
            //Assert.True(result[1].IntVal == 0 || result[1].IntVal == 2);
            //Assert.True(result[2].IntVal == 1 || result[2].IntVal == 3);
            //Assert.True(result[3].IntVal == 1 || result[3].IntVal == 3);

            serviceQuery = ServiceQueryBuilder.New().SortAsc(nameof(TestClass.NullBoolVal)).Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.True(result.Count == 4);
            //Assert.True(result[0].IntVal == 0 || result[0].IntVal == 2);
            //Assert.True(result[1].IntVal == 0 || result[1].IntVal == 2);
            //Assert.True(result[2].IntVal == 1 || result[2].IntVal == 3);
            //Assert.True(result[3].IntVal == 1 || result[3].IntVal == 3);

            // ByteArray
            //var tempbyteArray = new byte[1] { 1 };
            //serviceQuery = ServiceQueryBuilder.New().IsEqual(nameof(TestClass.NullByteArrayVal), System.Text.Encoding.UTF8.GetString(tempbyteArray)).Build();
            //testQueryable = serviceQuery.GetQueryable(sourceQueryable);
            //result = testQueryable.ToList();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 1);
            //result = await testQueryable.ToListAsync();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 1);

            // Byte
            serviceQuery = ServiceQueryBuilder.New().Sort(nameof(TestClass.NullByteVal), true).Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.True(result.Count == 4);

            // Char
            serviceQuery = ServiceQueryBuilder.New().Sort(nameof(TestClass.NullCharVal), true).Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.True(result.Count == 4);

            if (ValidateDateTimeOffset)
            {
                // DateTimeOffset
                var tempDateTimeOffset = new T().GetDefault1Record().DateTimeOffsetVal;
                serviceQuery = ServiceQueryBuilder.New().Sort(nameof(TestClass.NullDateTimeOffsetVal), true).Build();
                testQueryable = serviceQuery.Apply(sourceQueryable);
                result = testQueryable.ToList();
                Assert.True(result.Count == 4);
            }

            // DateTime
            var tempDateTime = new T().GetDefault1Record().DateTimeVal;
            serviceQuery = ServiceQueryBuilder.New().Sort(nameof(TestClass.NullDateTimeVal), true).Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.True(result.Count == 4);

            // Decimal
            if (ValidateDecimal)
            {
                serviceQuery = ServiceQueryBuilder.New().Sort(nameof(TestClass.NullDecimalVal), true).Build();
                testQueryable = serviceQuery.Apply(sourceQueryable);
                result = testQueryable.ToList();
                Assert.True(result.Count == 4);
            }

            // Double
            serviceQuery = ServiceQueryBuilder.New().Sort(nameof(TestClass.NullDoubleVal), true).Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.True(result.Count == 4);

            // Float
            serviceQuery = ServiceQueryBuilder.New().Sort(nameof(TestClass.NullFloatVal), true).Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.True(result.Count == 4);

            // Guid
            serviceQuery = ServiceQueryBuilder.New().Sort(nameof(TestClass.NullGuidVal), true).Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.True(result.Count == 4);

            // Int
            serviceQuery = ServiceQueryBuilder.New().Sort(nameof(TestClass.NullIntVal), true).Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.True(result.Count == 4);

            // Long
            serviceQuery = ServiceQueryBuilder.New().Sort(nameof(TestClass.NullLongVal), true).Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.True(result.Count == 4);

            // SByte
            serviceQuery = ServiceQueryBuilder.New().Sort(nameof(TestClass.NullSByteVal), true).Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.True(result.Count == 4);

            // Short
            serviceQuery = ServiceQueryBuilder.New().Sort(nameof(TestClass.NullShortVal), true).Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.True(result.Count == 4);

            // Single
            serviceQuery = ServiceQueryBuilder.New().Sort(nameof(TestClass.NullSingleVal), true).Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.True(result.Count == 4);

            // String
            serviceQuery = ServiceQueryBuilder.New().Sort(nameof(TestClass.NullStringVal), true).Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.True(result.Count == 4);

            if (ValidateTimeSpan)
            {
                // TimeSpan
                serviceQuery = ServiceQueryBuilder.New().Sort(nameof(TestClass.NullTimeSpanVal), true).Build();
                testQueryable = serviceQuery.Apply(sourceQueryable);
                result = testQueryable.ToList();
                Assert.True(result.Count == 4);
            }
            //#if NET7_0
            //            if (ValidateUInt128)
            //            {
            //                // UInt128
            //                serviceQuery = ServiceQueryBuilder.New().IsEqual(nameof(TestClass.NullUInt128Val), "1").Build();
            //                testQueryable = serviceQuery.Apply(sourceQueryable);
            //                result = testQueryable.ToList();
            //                Assert.NotNull(result);
            //                Assert.True(result.Count == 1);
            //                  result = await testQueryable.ToListAsync();
            //                Assert.NotNull(result);
            //                Assert.True(result.Count == 1);
            //            }
            //#endif
            if (ValidateUInt64)
            {
                // UInt64
                serviceQuery = ServiceQueryBuilder.New().Sort(nameof(TestClass.NullUInt64Val), true).Build();
                testQueryable = serviceQuery.Apply(sourceQueryable);
                result = testQueryable.ToList();
                Assert.True(result.Count == 4);
            }

            // UInt32
            serviceQuery = ServiceQueryBuilder.New().Sort(nameof(TestClass.NullUInt32Val), true).Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.True(result.Count == 4);

            // UInt16
            serviceQuery = ServiceQueryBuilder.New().Sort(nameof(TestClass.NullUInt16Val), true).Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.True(result.Count == 4);
        }

        [Fact]
        public async Task NullSortDescTest()
        {
            var sourceQueryable = GetTestList();
            IQueryable<T> testQueryable;
            IServiceQuery serviceQuery;
            List<T> result;

            // Boolean
            serviceQuery = ServiceQueryBuilder.New().Sort(nameof(TestClass.NullBoolVal), false).Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.True(result.Count == 4);
#if NET8_0_OR_GREATER
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
#else
            result = await testQueryable.ToListAsync();
#endif
            Assert.True(result.Count == 4);

            // ByteArray
            //var tempbyteArray = new byte[1] { 1 };
            //serviceQuery = ServiceQueryBuilder.New().IsEqual(nameof(TestClass.NullByteArrayVal), System.Text.Encoding.UTF8.GetString(tempbyteArray)).Build();
            //testQueryable = serviceQuery.GetQueryable(sourceQueryable);
            //result = testQueryable.ToList();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 1);
            //result = await testQueryable.ToListAsync();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 1);

            // Byte
            serviceQuery = ServiceQueryBuilder.New().Sort(nameof(TestClass.NullByteVal), false).Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.True(result.Count == 4);

            // Char
            serviceQuery = ServiceQueryBuilder.New().Sort(nameof(TestClass.NullCharVal), false).Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.True(result.Count == 4);

            if (ValidateDateTimeOffset)
            {
                // DateTimeOffset
                var tempDateTimeOffset = new T().GetDefault1Record().DateTimeOffsetVal;
                serviceQuery = ServiceQueryBuilder.New().Sort(nameof(TestClass.NullDateTimeOffsetVal), false).Build();
                testQueryable = serviceQuery.Apply(sourceQueryable);
                result = testQueryable.ToList();
                Assert.True(result.Count == 4);
            }

            // DateTime
            var tempDateTime = new T().GetDefault1Record().DateTimeVal;
            serviceQuery = ServiceQueryBuilder.New().Sort(nameof(TestClass.NullDateTimeVal), false).Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.True(result.Count == 4);

            // Decimal
            if (ValidateDecimal)
            {
                serviceQuery = ServiceQueryBuilder.New().Sort(nameof(TestClass.NullDecimalVal), false).Build();
                testQueryable = serviceQuery.Apply(sourceQueryable);
                result = testQueryable.ToList();
                Assert.True(result.Count == 4);
            }

            // Double
            serviceQuery = ServiceQueryBuilder.New().Sort(nameof(TestClass.NullDoubleVal), false).Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.True(result.Count == 4);

            // Float
            serviceQuery = ServiceQueryBuilder.New().Sort(nameof(TestClass.NullFloatVal), false).Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.True(result.Count == 4);

            // Guid
            serviceQuery = ServiceQueryBuilder.New().Sort(nameof(TestClass.NullGuidVal), false).Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.True(result.Count == 4);

            // Int
            serviceQuery = ServiceQueryBuilder.New().Sort(nameof(TestClass.NullIntVal), false).Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.True(result.Count == 4);

            // Long
            serviceQuery = ServiceQueryBuilder.New().Sort(nameof(TestClass.NullLongVal), false).Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.True(result.Count == 4);

            // SByte
            serviceQuery = ServiceQueryBuilder.New().Sort(nameof(TestClass.NullSByteVal), false).Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.True(result.Count == 4);

            // Short
            serviceQuery = ServiceQueryBuilder.New().Sort(nameof(TestClass.NullShortVal), false).Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.True(result.Count == 4);

            // Single
            serviceQuery = ServiceQueryBuilder.New().Sort(nameof(TestClass.NullSingleVal), false).Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.True(result.Count == 4);

            // String
            serviceQuery = ServiceQueryBuilder.New().Sort(nameof(TestClass.NullStringVal), false).Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.True(result.Count == 4);

            if (ValidateTimeSpan)
            {
                // TimeSpan
                serviceQuery = ServiceQueryBuilder.New().Sort(nameof(TestClass.NullTimeSpanVal), false).Build();
                testQueryable = serviceQuery.Apply(sourceQueryable);
                result = testQueryable.ToList();
                Assert.True(result.Count == 4);
            }
            //#if NET7_0
            //            if (ValidateUInt128)
            //            {
            //                // UInt128
            //                serviceQuery = ServiceQueryBuilder.New().IsEqual(nameof(TestClass.NullUInt128Val), "1").Build();
            //                testQueryable = serviceQuery.Apply(sourceQueryable);
            //                result = testQueryable.ToList();
            //                Assert.NotNull(result);
            //                Assert.True(result.Count == 1);
            //                  result = await testQueryable.ToListAsync();
            //                Assert.NotNull(result);
            //                Assert.True(result.Count == 1);
            //            }
            //#endif
            if (ValidateUInt64)
            {
                // UInt64
                serviceQuery = ServiceQueryBuilder.New().Sort(nameof(TestClass.NullUInt64Val), false).Build();
                testQueryable = serviceQuery.Apply(sourceQueryable);
                result = testQueryable.ToList();
                Assert.True(result.Count == 4);
            }

            // UInt32
            serviceQuery = ServiceQueryBuilder.New().Sort(nameof(TestClass.NullUInt32Val), false).Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.True(result.Count == 4);

            // UInt16
            serviceQuery = ServiceQueryBuilder.New().Sort(nameof(TestClass.NullUInt16Val), false).Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.True(result.Count == 4);
        }

        [Fact]
        public async Task NullSortAscRequestTest()
        {
            var sourceQueryable = GetTestList();
            IQueryable<T> testQueryable;
            IServiceQueryRequest serviceQuery;
            List<T> result;

            // Boolean
            serviceQuery = ServiceQueryRequestBuilder.New().Sort(nameof(TestClass.NullBoolVal), true).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.True(result.Count == 4);
#if NET8_0_OR_GREATER
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
#else
            result = await testQueryable.ToListAsync();
#endif
            Assert.True(result.Count == 4);

            // ByteArray
            //var tempbyteArray = new byte[1] { 1 };
            //serviceQuery = ServiceQueryRequestBuilder.New().IsEqual(nameof(TestClass.NullByteArrayVal), System.Text.Encoding.UTF8.GetString(tempbyteArray)).Build();
            //testQueryable = serviceQuery.GetQueryable(sourceQueryable);
            //result = testQueryable.ToList();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 1);
            //result = await testQueryable.ToListAsync();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 1);

            // Byte
            serviceQuery = ServiceQueryRequestBuilder.New().Sort(nameof(TestClass.NullByteVal), true).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.True(result.Count == 4);

            // Char
            serviceQuery = ServiceQueryRequestBuilder.New().Sort(nameof(TestClass.NullCharVal), true).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.True(result.Count == 4);

            if (ValidateDateTimeOffset)
            {
                // DateTimeOffset
                var tempDateTimeOffset = new T().GetDefault1Record().DateTimeOffsetVal;
                serviceQuery = ServiceQueryRequestBuilder.New().Sort(nameof(TestClass.NullDateTimeOffsetVal), true).Build();
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
                result = testQueryable.ToList();
                Assert.True(result.Count == 4);
            }

            // DateTime
            var tempDateTime = new T().GetDefault1Record().DateTimeVal;
            serviceQuery = ServiceQueryRequestBuilder.New().Sort(nameof(TestClass.NullDateTimeVal), true).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.True(result.Count == 4);

            // Decimal
            if (ValidateDecimal)
            {
                serviceQuery = ServiceQueryRequestBuilder.New().Sort(nameof(TestClass.NullDecimalVal), true).Build();
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
                result = testQueryable.ToList();
                Assert.True(result.Count == 4);
            }

            // Double
            serviceQuery = ServiceQueryRequestBuilder.New().Sort(nameof(TestClass.NullDoubleVal), true).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.True(result.Count == 4);

            // Float
            serviceQuery = ServiceQueryRequestBuilder.New().Sort(nameof(TestClass.NullFloatVal), true).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.True(result.Count == 4);

            // Guid
            serviceQuery = ServiceQueryRequestBuilder.New().Sort(nameof(TestClass.NullGuidVal), true).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.True(result.Count == 4);

            // Int
            serviceQuery = ServiceQueryRequestBuilder.New().Sort(nameof(TestClass.NullIntVal), true).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.True(result.Count == 4);

            // Long
            serviceQuery = ServiceQueryRequestBuilder.New().Sort(nameof(TestClass.NullLongVal), true).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.True(result.Count == 4);

            // SByte
            serviceQuery = ServiceQueryRequestBuilder.New().Sort(nameof(TestClass.NullSByteVal), true).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.True(result.Count == 4);

            // Short
            serviceQuery = ServiceQueryRequestBuilder.New().Sort(nameof(TestClass.NullShortVal), true).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.True(result.Count == 4);

            // Single
            serviceQuery = ServiceQueryRequestBuilder.New().Sort(nameof(TestClass.NullSingleVal), true).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.True(result.Count == 4);

            // String
            serviceQuery = ServiceQueryRequestBuilder.New().Sort(nameof(TestClass.NullStringVal), true).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.True(result.Count == 4);

            if (ValidateTimeSpan)
            {
                // TimeSpan
                serviceQuery = ServiceQueryRequestBuilder.New().Sort(nameof(TestClass.NullTimeSpanVal), true).Build();
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
                result = testQueryable.ToList();
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
            //                Assert.True(result.Count == 1);
            //                  result = await testQueryable.ToListAsync();
            //                Assert.NotNull(result);
            //                Assert.True(result.Count == 1);
            //            }
            //#endif
            if (ValidateUInt64)
            {
                // UInt64
                serviceQuery = ServiceQueryRequestBuilder.New().Sort(nameof(TestClass.NullUInt64Val), true).Build();
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
                result = testQueryable.ToList();
                Assert.True(result.Count == 4);
            }

            // UInt32
            serviceQuery = ServiceQueryRequestBuilder.New().Sort(nameof(TestClass.NullUInt32Val), true).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.True(result.Count == 4);

            // UInt16
            serviceQuery = ServiceQueryRequestBuilder.New().Sort(nameof(TestClass.NullUInt16Val), true).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.True(result.Count == 4);
        }

        [Fact]
        public async Task NullSortDescRequestTest()
        {
            var sourceQueryable = GetTestList();
            IQueryable<T> testQueryable;
            IServiceQueryRequest serviceQuery;
            List<T> result;

            // Boolean
            serviceQuery = ServiceQueryRequestBuilder.New().Sort(nameof(TestClass.NullBoolVal), false).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.True(result.Count == 4);
#if NET8_0_OR_GREATER
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
#else
            result = await testQueryable.ToListAsync();
#endif
            Assert.True(result.Count == 4);

            // ByteArray
            //var tempbyteArray = new byte[1] { 1 };
            //serviceQuery = ServiceQueryRequestBuilder.New().IsEqual(nameof(TestClass.NullByteArrayVal), System.Text.Encoding.UTF8.GetString(tempbyteArray)).Build();
            //testQueryable = serviceQuery.GetQueryable(sourceQueryable);
            //result = testQueryable.ToList();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 1);
            //result = await testQueryable.ToListAsync();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 1);

            // Byte
            serviceQuery = ServiceQueryRequestBuilder.New().SortDesc(nameof(TestClass.NullByteVal)).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.True(result.Count == 4);

            // Char
            serviceQuery = ServiceQueryRequestBuilder.New().SortDesc(nameof(TestClass.NullCharVal)).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.True(result.Count == 4);

            if (ValidateDateTimeOffset)
            {
                // DateTimeOffset
                var tempDateTimeOffset = new T().GetDefault1Record().DateTimeOffsetVal;
                serviceQuery = ServiceQueryRequestBuilder.New().SortDesc(nameof(TestClass.NullDateTimeOffsetVal)).Build();
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
                result = testQueryable.ToList();
                Assert.True(result.Count == 4);
            }

            // DateTime
            var tempDateTime = new T().GetDefault1Record().DateTimeVal;
            serviceQuery = ServiceQueryRequestBuilder.New().SortDesc(nameof(TestClass.NullDateTimeVal)).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.True(result.Count == 4);

            // Decimal
            if (ValidateDecimal)
            {
                serviceQuery = ServiceQueryRequestBuilder.New().SortDesc(nameof(TestClass.NullDecimalVal)).Build();
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
                result = testQueryable.ToList();
                Assert.True(result.Count == 4);
            }

            // Double
            serviceQuery = ServiceQueryRequestBuilder.New().Sort(nameof(TestClass.NullDoubleVal), false).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.True(result.Count == 4);

            // Float
            serviceQuery = ServiceQueryRequestBuilder.New().SortDesc(nameof(TestClass.NullFloatVal)).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.True(result.Count == 4);

            // Guid
            serviceQuery = ServiceQueryRequestBuilder.New().SortDesc(nameof(TestClass.NullGuidVal)).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.True(result.Count == 4);

            // Int
            serviceQuery = ServiceQueryRequestBuilder.New().SortDesc(nameof(TestClass.NullIntVal)).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.True(result.Count == 4);

            // Long
            serviceQuery = ServiceQueryRequestBuilder.New().SortDesc(nameof(TestClass.NullLongVal)).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.True(result.Count == 4);

            // SByte
            serviceQuery = ServiceQueryRequestBuilder.New().SortDesc(nameof(TestClass.NullSByteVal)).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.True(result.Count == 4);

            // Short
            serviceQuery = ServiceQueryRequestBuilder.New().SortDesc(nameof(TestClass.NullShortVal)).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.True(result.Count == 4);

            // Single
            serviceQuery = ServiceQueryRequestBuilder.New().SortDesc(nameof(TestClass.NullSingleVal)).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.True(result.Count == 4);

            // String
            serviceQuery = ServiceQueryRequestBuilder.New().SortDesc(nameof(TestClass.NullStringVal)).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.True(result.Count == 4);

            if (ValidateTimeSpan)
            {
                // TimeSpan
                serviceQuery = ServiceQueryRequestBuilder.New().Sort(nameof(TestClass.NullTimeSpanVal), false).Build();
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
                result = testQueryable.ToList();
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
            //                Assert.True(result.Count == 1);
            //                  result = await testQueryable.ToListAsync();
            //                Assert.NotNull(result);
            //                Assert.True(result.Count == 1);
            //            }
            //#endif
            if (ValidateUInt64)
            {
                // UInt64
                serviceQuery = ServiceQueryRequestBuilder.New().Sort(nameof(TestClass.NullUInt64Val), false).Build();
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
                result = testQueryable.ToList();
                Assert.True(result.Count == 4);
            }

            // UInt32
            serviceQuery = ServiceQueryRequestBuilder.New().Sort(nameof(TestClass.NullUInt32Val), false).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.True(result.Count == 4);

            // UInt16
            serviceQuery = ServiceQueryRequestBuilder.New().Sort(nameof(TestClass.NullUInt16Val), false).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.True(result.Count == 4);
        }
    }
}
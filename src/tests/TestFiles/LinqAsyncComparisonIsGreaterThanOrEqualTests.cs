namespace ServiceQuery.Xunit
{
    public class LinqAsyncComparisonIsGreaterThanOrEqualTests : LinqAsyncComparisonIsGreaterThanOrEqualTests<TestClass>
    {
        public override IQueryable<TestClass> GetTestList()
        {
            return new TestClass().GetDefaultList().AsQueryable();
        }
    }

    public abstract class LinqAsyncComparisonIsGreaterThanOrEqualTests<T> : BaseTest<T> where T : class, ITestClass, new()
    {
        [Fact]
        public async Task IsGreaterThanOrEqualOrEqualStandardTest()
        {
            var sourceQueryable = GetTestList();
            IQueryable<T> testQueryable;
            IServiceQuery serviceQuery;
            List<T> result;

            // Boolean
            serviceQuery = ServiceQueryBuilder.New().IsGreaterThanOrEqual(nameof(TestClass.BoolVal), "true").Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.Apply(sourceQueryable);
            });

            // ByteArray
            byte[] tempbyte = new byte[] { 1 };
            serviceQuery = ServiceQueryBuilder.New().IsGreaterThanOrEqual(nameof(TestClass.ByteArrayVal), System.Text.Encoding.UTF8.GetString(tempbyte)).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.Apply(sourceQueryable);
            });

            // Byte
            serviceQuery = ServiceQueryBuilder.New().IsGreaterThanOrEqual(nameof(TestClass.ByteVal), "1").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 3);
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 3);

            // Char
            serviceQuery = ServiceQueryBuilder.New().IsGreaterThanOrEqual(nameof(TestClass.CharVal), "b").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);

            if (ValidateDateTimeOffset)
            {
                // DateTimeOffset
                var tempDateTimeOffset = new T().GetDefault1Record().DateTimeOffsetVal;
                serviceQuery = ServiceQueryBuilder.New().IsGreaterThanOrEqual(nameof(TestClass.DateTimeOffsetVal), tempDateTimeOffset.ToString("o")).Build();
                testQueryable = serviceQuery.Apply(sourceQueryable);
                result = testQueryable.ToList();
                Assert.NotNull(result);
                Assert.True(result.Count == 3);
                result = await testQueryable.ToAsyncEnumerable().ToListAsync();
                Assert.NotNull(result);
                Assert.True(result.Count == 3);
            }

            // DateTime
            var tempDateTime = new T().GetDefault1Record().DateTimeVal;
            serviceQuery = ServiceQueryBuilder.New().IsGreaterThanOrEqual(nameof(TestClass.DateTimeVal), tempDateTime.ToString("o")).Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 3);
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 3);

            // Decimal
            serviceQuery = ServiceQueryBuilder.New().IsGreaterThanOrEqual(nameof(TestClass.DecimalVal), "1").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 3);
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 3);

            // Double
            serviceQuery = ServiceQueryBuilder.New().IsGreaterThanOrEqual(nameof(TestClass.DoubleVal), "1").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 3);
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 3);

            // Float
            serviceQuery = ServiceQueryBuilder.New().IsGreaterThanOrEqual(nameof(TestClass.FloatVal), "1").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 3);
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 3);

#if NET6_0
            // Guid
            serviceQuery = ServiceQueryBuilder.New().IsGreaterThanOrEqual(nameof(TestClass.GuidVal), Guid.Empty.ToString()).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.Apply(sourceQueryable);
            });
#endif
#if NET7_0_OR_GREATER
            // Guid
            serviceQuery = ServiceQueryBuilder.New().IsGreaterThanOrEqual(nameof(TestClass.GuidVal), Guid.Empty.ToString()).Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
#endif
            // Int
            serviceQuery = ServiceQueryBuilder.New().IsGreaterThanOrEqual(nameof(TestClass.IntVal), "1").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 3);
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 3);

            // Long
            serviceQuery = ServiceQueryBuilder.New().IsGreaterThanOrEqual(nameof(TestClass.LongVal), "1").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 3);
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 3);

            // SByte
            serviceQuery = ServiceQueryBuilder.New().IsGreaterThanOrEqual(nameof(TestClass.SByteVal), "1").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 3);
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 3);

            // Short
            serviceQuery = ServiceQueryBuilder.New().IsGreaterThanOrEqual(nameof(TestClass.ShortVal), "1").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 3);
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 3);

            // Single
            serviceQuery = ServiceQueryBuilder.New().IsGreaterThanOrEqual(nameof(TestClass.SingleVal), "1").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 3);
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 3);

            // String
            serviceQuery = ServiceQueryBuilder.New().IsGreaterThanOrEqual(nameof(TestClass.StringVal), "a").Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.Apply(sourceQueryable);
            });

            if (ValidateTimeSpan)
            {
                // TimeSpan
                serviceQuery = ServiceQueryBuilder.New().IsGreaterThanOrEqual(nameof(TestClass.TimeSpanVal), TimeSpan.FromMilliseconds(1).ToString()).Build();
                testQueryable = serviceQuery.Apply(sourceQueryable);
                result = testQueryable.ToList();
                Assert.NotNull(result);
                Assert.True(result.Count == 3);
                result = await testQueryable.ToAsyncEnumerable().ToListAsync();
                Assert.NotNull(result);
                Assert.True(result.Count == 3);
            }
            //#if NET7_0
            //            if (ValidateUInt128)
            //            {
            //                // UInt128
            //                serviceQuery = ServiceQueryBuilder.New().IsGreaterThanOrEqual(nameof(TestClass.UInt128Val), "1").Build();
            //                testQueryable = serviceQuery.Apply(sourceQueryable);
            //                result = testQueryable.ToList();
            //                Assert.NotNull(result);
            //                Assert.True(result.Count == 3);
            //                result = await testQueryable.ToListAsync();
            //                Assert.NotNull(result);
            //                Assert.True(result.Count == 3);
            //            }
            //#endif
            if (ValidateUInt64)
            {
                // UInt64
                serviceQuery = ServiceQueryBuilder.New().IsGreaterThanOrEqual(nameof(TestClass.UInt64Val), "1").Build();
                testQueryable = serviceQuery.Apply(sourceQueryable);
                result = testQueryable.ToList();
                Assert.NotNull(result);
                Assert.True(result.Count == 3);
                result = await testQueryable.ToAsyncEnumerable().ToListAsync();
                Assert.NotNull(result);
                Assert.True(result.Count == 3);
            }

            // UInt32
            serviceQuery = ServiceQueryBuilder.New().IsGreaterThanOrEqual(nameof(TestClass.UInt32Val), "1").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 3);
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 3);

            // UInt16
            serviceQuery = ServiceQueryBuilder.New().IsGreaterThanOrEqual(nameof(TestClass.UInt16Val), "1").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 3);
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 3);
        }

        [Fact]
        public async Task IsGreaterThanOrEqualOrEqualRequestTest()
        {
            var sourceQueryable = GetTestList();
            IQueryable<T> testQueryable;
            IServiceQueryRequest serviceQuery;
            List<T> result;

            // Boolean
            serviceQuery = ServiceQueryRequestBuilder.New().IsGreaterThanOrEqual(nameof(TestClass.BoolVal), "true").Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            });

            // ByteArray
            byte[] tempbyte = new byte[] { 1 };
            serviceQuery = ServiceQueryRequestBuilder.New().IsGreaterThanOrEqual(nameof(TestClass.ByteArrayVal), System.Text.Encoding.UTF8.GetString(tempbyte)).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            });

            // Byte
            serviceQuery = ServiceQueryRequestBuilder.New().IsGreaterThanOrEqual(nameof(TestClass.ByteVal), "1").Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 3);
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 3);

            // Char
            serviceQuery = ServiceQueryRequestBuilder.New().IsGreaterThanOrEqual(nameof(TestClass.CharVal), "b").Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);

            if (ValidateDateTimeOffset)
            {
                // DateTimeOffset
                var tempDateTimeOffset = new T().GetDefault1Record().DateTimeOffsetVal;
                serviceQuery = ServiceQueryRequestBuilder.New().IsGreaterThanOrEqual(nameof(TestClass.DateTimeOffsetVal), tempDateTimeOffset.ToString("o")).Build();
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
                result = testQueryable.ToList();
                Assert.NotNull(result);
                Assert.True(result.Count == 3);
                result = await testQueryable.ToAsyncEnumerable().ToListAsync();
                Assert.NotNull(result);
                Assert.True(result.Count == 3);
            }

            // DateTime
            var tempDateTime = new T().GetDefault1Record().DateTimeVal;
            serviceQuery = ServiceQueryRequestBuilder.New().IsGreaterThanOrEqual(nameof(TestClass.DateTimeVal), tempDateTime.ToString("o")).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 3);
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 3);

            // Decimal
            serviceQuery = ServiceQueryRequestBuilder.New().IsGreaterThanOrEqual(nameof(TestClass.DecimalVal), "1").Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 3);
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 3);

            // Double
            serviceQuery = ServiceQueryRequestBuilder.New().IsGreaterThanOrEqual(nameof(TestClass.DoubleVal), "1").Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 3);
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 3);

            // Float
            serviceQuery = ServiceQueryRequestBuilder.New().IsGreaterThanOrEqual(nameof(TestClass.FloatVal), "1").Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 3);
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 3);

#if NET6_0
            // Guid
            serviceQuery = ServiceQueryRequestBuilder.New().IsGreaterThanOrEqual(nameof(TestClass.GuidVal), Guid.Empty.ToString()).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            });
#endif
#if NET7_0_OR_GREATER
            // Guid
            serviceQuery = ServiceQueryRequestBuilder.New().IsGreaterThanOrEqual(nameof(TestClass.GuidVal), Guid.Empty.ToString()).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
#endif
            // Int
            serviceQuery = ServiceQueryRequestBuilder.New().IsGreaterThanOrEqual(nameof(TestClass.IntVal), "1").Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 3);
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 3);

            // Long
            serviceQuery = ServiceQueryRequestBuilder.New().IsGreaterThanOrEqual(nameof(TestClass.LongVal), "1").Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 3);
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 3);

            // SByte
            serviceQuery = ServiceQueryRequestBuilder.New().IsGreaterThanOrEqual(nameof(TestClass.SByteVal), "1").Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 3);
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 3);

            // Short
            serviceQuery = ServiceQueryRequestBuilder.New().IsGreaterThanOrEqual(nameof(TestClass.ShortVal), "1").Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 3);
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 3);

            // Single
            serviceQuery = ServiceQueryRequestBuilder.New().IsGreaterThanOrEqual(nameof(TestClass.SingleVal), "1").Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 3);
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 3);

            // String
            serviceQuery = ServiceQueryRequestBuilder.New().IsGreaterThanOrEqual(nameof(TestClass.StringVal), "a").Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            });

            if (ValidateTimeSpan)
            {
                // TimeSpan
                serviceQuery = ServiceQueryRequestBuilder.New().IsGreaterThanOrEqual(nameof(TestClass.TimeSpanVal), TimeSpan.FromMilliseconds(1).ToString()).Build();
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
                result = testQueryable.ToList();
                Assert.NotNull(result);
                Assert.True(result.Count == 3);
                result = await testQueryable.ToAsyncEnumerable().ToListAsync();
                Assert.NotNull(result);
                Assert.True(result.Count == 3);
            }
            //#if NET7_0
            //            if (ValidateUInt128)
            //            {
            //                // UInt128
            //                serviceQuery = ServiceQueryRequestBuilder.New().IsGreaterThanOrEqual(nameof(TestClass.UInt128Val), "1").Build();
            //                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            //                result = testQueryable.ToList();
            //                Assert.NotNull(result);
            //                Assert.True(result.Count == 3);
            //                result = await testQueryable.ToListAsync();
            //                Assert.NotNull(result);
            //                Assert.True(result.Count == 3);
            //            }
            //#endif
            if (ValidateUInt64)
            {
                // UInt64
                serviceQuery = ServiceQueryRequestBuilder.New().IsGreaterThanOrEqual(nameof(TestClass.UInt64Val), "1").Build();
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
                result = testQueryable.ToList();
                Assert.NotNull(result);
                Assert.True(result.Count == 3);
                result = await testQueryable.ToAsyncEnumerable().ToListAsync();
                Assert.NotNull(result);
                Assert.True(result.Count == 3);
            }

            // UInt32
            serviceQuery = ServiceQueryRequestBuilder.New().IsGreaterThanOrEqual(nameof(TestClass.UInt32Val), "1").Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 3);
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 3);

            // UInt16
            serviceQuery = ServiceQueryRequestBuilder.New().IsGreaterThanOrEqual(nameof(TestClass.UInt16Val), "1").Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 3);
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 3);
        }

        [Fact]
        public async Task NullIsGreaterThanOrEqualOrEqualStandardTest()
        {
            var sourceQueryable = GetTestList();
            IQueryable<T> testQueryable;
            IServiceQuery serviceQuery;
            List<T> result;

            // Boolean
            serviceQuery = ServiceQueryBuilder.New().IsGreaterThanOrEqual(nameof(TestClass.NullBoolVal), "true").Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.Apply(sourceQueryable);
            });

            // ByteArray
            byte[] tempbyte = new byte[] { 1 };
            serviceQuery = ServiceQueryBuilder.New().IsGreaterThanOrEqual(nameof(TestClass.NullByteArrayVal), System.Text.Encoding.UTF8.GetString(tempbyte)).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.Apply(sourceQueryable);
            });

            // Byte
            serviceQuery = ServiceQueryBuilder.New().IsGreaterThanOrEqual(nameof(TestClass.NullByteVal), "1").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);

            // Char
            serviceQuery = ServiceQueryBuilder.New().IsGreaterThanOrEqual(nameof(TestClass.NullCharVal), "b").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);

            if (ValidateDateTimeOffset)
            {
                // DateTimeOffset
                var tempDateTimeOffset = new T().GetDefault1Record().DateTimeOffsetVal;
                serviceQuery = ServiceQueryBuilder.New().IsGreaterThanOrEqual(nameof(TestClass.NullDateTimeOffsetVal), tempDateTimeOffset.ToString("o")).Build();
                testQueryable = serviceQuery.Apply(sourceQueryable);
                result = testQueryable.ToList();
                Assert.NotNull(result);
                Assert.True(result.Count == 0);
                result = await testQueryable.ToAsyncEnumerable().ToListAsync();
                Assert.NotNull(result);
                Assert.True(result.Count == 0);
            }

            // DateTime
            var tempDateTime = new T().GetDefault1Record().DateTimeVal;
            serviceQuery = ServiceQueryBuilder.New().IsGreaterThanOrEqual(nameof(TestClass.NullDateTimeVal), tempDateTime.ToString("o")).Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);

            // Decimal
            serviceQuery = ServiceQueryBuilder.New().IsGreaterThanOrEqual(nameof(TestClass.NullDecimalVal), "1").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);

            // Double
            serviceQuery = ServiceQueryBuilder.New().IsGreaterThanOrEqual(nameof(TestClass.NullDoubleVal), "1").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);

            // Float
            serviceQuery = ServiceQueryBuilder.New().IsGreaterThanOrEqual(nameof(TestClass.NullFloatVal), "1").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);

#if NET6_0
            // Guid
            serviceQuery = ServiceQueryBuilder.New().IsGreaterThanOrEqual(nameof(TestClass.NullGuidVal), Guid.Empty.ToString()).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.Apply(sourceQueryable);
            });
#endif
#if NET7_0_OR_GREATER
            // Guid
            serviceQuery = ServiceQueryBuilder.New().IsGreaterThanOrEqual(nameof(TestClass.NullGuidVal), Guid.Empty.ToString()).Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
#endif
            // Int
            serviceQuery = ServiceQueryBuilder.New().IsGreaterThanOrEqual(nameof(TestClass.NullIntVal), "1").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);

            // Long
            serviceQuery = ServiceQueryBuilder.New().IsGreaterThanOrEqual(nameof(TestClass.NullLongVal), "1").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);

            // SByte
            serviceQuery = ServiceQueryBuilder.New().IsGreaterThanOrEqual(nameof(TestClass.NullSByteVal), "1").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);

            // Short
            serviceQuery = ServiceQueryBuilder.New().IsGreaterThanOrEqual(nameof(TestClass.NullShortVal), "1").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);

            // Single
            serviceQuery = ServiceQueryBuilder.New().IsGreaterThanOrEqual(nameof(TestClass.NullSingleVal), "1").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);

            // String
            serviceQuery = ServiceQueryBuilder.New().IsGreaterThanOrEqual(nameof(TestClass.NullStringVal), "a").Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.Apply(sourceQueryable);
            });

            if (ValidateTimeSpan)
            {
                // TimeSpan
                serviceQuery = ServiceQueryBuilder.New().IsGreaterThanOrEqual(nameof(TestClass.NullTimeSpanVal), TimeSpan.FromMilliseconds(1).ToString()).Build();
                testQueryable = serviceQuery.Apply(sourceQueryable);
                result = testQueryable.ToList();
                Assert.NotNull(result);
                Assert.True(result.Count == 0);
                result = await testQueryable.ToAsyncEnumerable().ToListAsync();
                Assert.NotNull(result);
                Assert.True(result.Count == 0);
            }
            //#if NET7_0
            //            if (ValidateUInt128)
            //            {
            //                // UInt128
            //                serviceQuery = ServiceQueryBuilder.New().IsGreaterThanOrEqual(nameof(TestClass.NullUInt128Val), "1").Build();
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
                serviceQuery = ServiceQueryBuilder.New().IsGreaterThanOrEqual(nameof(TestClass.NullUInt64Val), "1").Build();
                testQueryable = serviceQuery.Apply(sourceQueryable);
                result = testQueryable.ToList();
                Assert.NotNull(result);
                Assert.True(result.Count == 0);
                result = await testQueryable.ToAsyncEnumerable().ToListAsync();
                Assert.NotNull(result);
                Assert.True(result.Count == 0);
            }

            // UInt32
            serviceQuery = ServiceQueryBuilder.New().IsGreaterThanOrEqual(nameof(TestClass.NullUInt32Val), "1").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);

            // UInt16
            serviceQuery = ServiceQueryBuilder.New().IsGreaterThanOrEqual(nameof(TestClass.NullUInt16Val), "1").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
        }

        [Fact]
        public async Task NullIsGreaterThanOrEqualOrEqualRequestTest()
        {
            var sourceQueryable = GetTestList();
            IQueryable<T> testQueryable;
            IServiceQueryRequest serviceQuery;
            List<T> result;

            // Boolean
            serviceQuery = ServiceQueryRequestBuilder.New().IsGreaterThanOrEqual(nameof(TestClass.NullBoolVal), "true").Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            });

            // ByteArray
            byte[] tempbyte = new byte[] { 1 };
            serviceQuery = ServiceQueryRequestBuilder.New().IsGreaterThanOrEqual(nameof(TestClass.NullByteArrayVal), System.Text.Encoding.UTF8.GetString(tempbyte)).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            });

            // Byte
            serviceQuery = ServiceQueryRequestBuilder.New().IsGreaterThanOrEqual(nameof(TestClass.NullByteVal), "1").Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);

            // Char
            serviceQuery = ServiceQueryRequestBuilder.New().IsGreaterThanOrEqual(nameof(TestClass.NullCharVal), "b").Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);

            if (ValidateDateTimeOffset)
            {
                // DateTimeOffset
                var tempDateTimeOffset = new T().GetDefault1Record().DateTimeOffsetVal;
                serviceQuery = ServiceQueryRequestBuilder.New().IsGreaterThanOrEqual(nameof(TestClass.NullDateTimeOffsetVal), tempDateTimeOffset.ToString("o")).Build();
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
                result = testQueryable.ToList();
                Assert.NotNull(result);
                Assert.True(result.Count == 0);
                result = await testQueryable.ToAsyncEnumerable().ToListAsync();
                Assert.NotNull(result);
                Assert.True(result.Count == 0);
            }

            // DateTime
            var tempDateTime = new T().GetDefault1Record().DateTimeVal;
            serviceQuery = ServiceQueryRequestBuilder.New().IsGreaterThanOrEqual(nameof(TestClass.NullDateTimeVal), tempDateTime.ToString("o")).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);

            // Decimal
            serviceQuery = ServiceQueryRequestBuilder.New().IsGreaterThanOrEqual(nameof(TestClass.NullDecimalVal), "1").Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);

            // Double
            serviceQuery = ServiceQueryRequestBuilder.New().IsGreaterThanOrEqual(nameof(TestClass.NullDoubleVal), "1").Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);

            // Float
            serviceQuery = ServiceQueryRequestBuilder.New().IsGreaterThanOrEqual(nameof(TestClass.NullFloatVal), "1").Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);

#if NET6_0
            // Guid
            serviceQuery = ServiceQueryRequestBuilder.New().IsGreaterThanOrEqual(nameof(TestClass.NullGuidVal), Guid.Empty.ToString()).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            });
#endif
#if NET7_0_OR_GREATER
            // Guid
            serviceQuery = ServiceQueryRequestBuilder.New().IsGreaterThanOrEqual(nameof(TestClass.NullGuidVal), Guid.Empty.ToString()).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
#endif
            // Int
            serviceQuery = ServiceQueryRequestBuilder.New().IsGreaterThanOrEqual(nameof(TestClass.NullIntVal), "1").Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);

            // Long
            serviceQuery = ServiceQueryRequestBuilder.New().IsGreaterThanOrEqual(nameof(TestClass.NullLongVal), "1").Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);

            // SByte
            serviceQuery = ServiceQueryRequestBuilder.New().IsGreaterThanOrEqual(nameof(TestClass.NullSByteVal), "1").Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);

            // Short
            serviceQuery = ServiceQueryRequestBuilder.New().IsGreaterThanOrEqual(nameof(TestClass.NullShortVal), "1").Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);

            // Single
            serviceQuery = ServiceQueryRequestBuilder.New().IsGreaterThanOrEqual(nameof(TestClass.NullSingleVal), "1").Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);

            // String
            serviceQuery = ServiceQueryRequestBuilder.New().IsGreaterThanOrEqual(nameof(TestClass.NullStringVal), "a").Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            });

            if (ValidateTimeSpan)
            {
                // TimeSpan
                serviceQuery = ServiceQueryRequestBuilder.New().IsGreaterThanOrEqual(nameof(TestClass.NullTimeSpanVal), TimeSpan.FromMilliseconds(1).ToString()).Build();
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
                result = testQueryable.ToList();
                Assert.NotNull(result);
                Assert.True(result.Count == 0);
                result = await testQueryable.ToAsyncEnumerable().ToListAsync();
                Assert.NotNull(result);
                Assert.True(result.Count == 0);
            }
            //#if NET7_0
            //            if (ValidateUInt128)
            //            {
            //                // UInt128
            //                serviceQuery = ServiceQueryRequestBuilder.New().IsGreaterThanOrEqual(nameof(TestClass.NullUInt128Val), "1").Build();
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
                serviceQuery = ServiceQueryRequestBuilder.New().IsGreaterThanOrEqual(nameof(TestClass.NullUInt64Val), "1").Build();
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
                result = testQueryable.ToList();
                Assert.NotNull(result);
                Assert.True(result.Count == 0);
                result = await testQueryable.ToAsyncEnumerable().ToListAsync();
                Assert.NotNull(result);
                Assert.True(result.Count == 0);
            }

            // UInt32
            serviceQuery = ServiceQueryRequestBuilder.New().IsGreaterThanOrEqual(nameof(TestClass.NullUInt32Val), "1").Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);

            // UInt16
            serviceQuery = ServiceQueryRequestBuilder.New().IsGreaterThanOrEqual(nameof(TestClass.NullUInt16Val), "1").Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
        }
    }
}
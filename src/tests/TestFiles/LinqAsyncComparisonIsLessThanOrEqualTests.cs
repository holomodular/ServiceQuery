namespace ServiceQuery.Xunit
{
    public class LinqAsyncComparisonIsLessThanOrEqualTests : LinqAsyncComparisonIsLessThanOrEqualTests<TestClass>
    {
        public override IQueryable<TestClass> GetTestList()
        {
            return new TestClass().GetDefaultList().AsQueryable();
        }
    }

    public abstract class LinqAsyncComparisonIsLessThanOrEqualTests<T> : BaseTest<T> where T : class, ITestClass, new()
    {
        [Fact]
        public async Task IsLessThanOrEqualStandardTest()
        {
            var sourceQueryable = GetTestList();
            IQueryable<T> testQueryable;
            IServiceQuery serviceQuery;
            List<T> result;

            // Boolean
            serviceQuery = ServiceQueryBuilder.New().IsLessThanOrEqual(nameof(TestClass.BoolVal), "true").Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.Apply(sourceQueryable);
            });

            // ByteArray
            byte[] tempbyte = new byte[] { 1 };
            serviceQuery = ServiceQueryBuilder.New().IsLessThanOrEqual(nameof(TestClass.ByteArrayVal), System.Text.Encoding.UTF8.GetString(tempbyte)).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.Apply(sourceQueryable);
            });

            // Byte
            serviceQuery = ServiceQueryBuilder.New().IsLessThanOrEqual(nameof(TestClass.ByteVal), "1").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);

            // Char
            serviceQuery = ServiceQueryBuilder.New().IsLessThanOrEqual(nameof(TestClass.CharVal), "b").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 3);
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 3);

            if (ValidateDateTimeOffset)
            {
                // DateTimeOffset
                var tempDateTimeOffset = new T().GetDefault1Record().DateTimeOffsetVal;
                serviceQuery = ServiceQueryBuilder.New().IsLessThanOrEqual(nameof(TestClass.DateTimeOffsetVal), tempDateTimeOffset.ToString("o")).Build();
                testQueryable = serviceQuery.Apply(sourceQueryable);
                result = testQueryable.ToList();
                Assert.NotNull(result);
                Assert.True(result.Count == 2);
                result = await testQueryable.ToAsyncEnumerable().ToListAsync();
                Assert.NotNull(result);
                Assert.True(result.Count == 2);
            }

            // DateTime
            var tempDateTime = new T().GetDefault1Record().DateTimeVal;
            serviceQuery = ServiceQueryBuilder.New().IsLessThanOrEqual(nameof(TestClass.DateTimeVal), tempDateTime.ToString("o")).Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);

            // Decimal
            serviceQuery = ServiceQueryBuilder.New().IsLessThanOrEqual(nameof(TestClass.DecimalVal), "1").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);

            // Double
            serviceQuery = ServiceQueryBuilder.New().IsLessThanOrEqual(nameof(TestClass.DoubleVal), "1").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);

            // Float
            serviceQuery = ServiceQueryBuilder.New().IsLessThanOrEqual(nameof(TestClass.FloatVal), "1").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);

#if NET7_0_OR_GREATER
            // Guid
            serviceQuery = ServiceQueryBuilder.New().IsLessThanOrEqual(nameof(TestClass.GuidVal), "11111111-1111-1111-1111-111111111111").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);
#endif
            // Int
            serviceQuery = ServiceQueryBuilder.New().IsLessThanOrEqual(nameof(TestClass.IntVal), "1").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);

            // Long
            serviceQuery = ServiceQueryBuilder.New().IsLessThanOrEqual(nameof(TestClass.LongVal), "1").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);

            // SByte
            serviceQuery = ServiceQueryBuilder.New().IsLessThanOrEqual(nameof(TestClass.SByteVal), "1").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);

            // Short
            serviceQuery = ServiceQueryBuilder.New().IsLessThanOrEqual(nameof(TestClass.ShortVal), "1").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);

            // Single
            serviceQuery = ServiceQueryBuilder.New().IsLessThanOrEqual(nameof(TestClass.SingleVal), "1").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);

            // String
            serviceQuery = ServiceQueryBuilder.New().IsLessThanOrEqual(nameof(TestClass.StringVal), "a").Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.Apply(sourceQueryable);
            });

            if (ValidateTimeSpan)
            {
                // TimeSpan
                serviceQuery = ServiceQueryBuilder.New().IsLessThanOrEqual(nameof(TestClass.TimeSpanVal), TimeSpan.FromMilliseconds(1).ToString()).Build();
                testQueryable = serviceQuery.Apply(sourceQueryable);
                result = testQueryable.ToList();
                Assert.NotNull(result);
                Assert.True(result.Count == 2);
                result = await testQueryable.ToAsyncEnumerable().ToListAsync();
                Assert.NotNull(result);
                Assert.True(result.Count == 2);
            }
            //#if NET7_0
            //            if (ValidateUInt128)
            //            {
            //                // UInt128
            //                serviceQuery = ServiceQueryBuilder.New().IsLessThanOrEqual(nameof(TestClass.UInt128Val), "1").Build();
            //                testQueryable = serviceQuery.Apply(sourceQueryable);
            //                result = testQueryable.ToList();
            //                Assert.NotNull(result);
            //                Assert.True(result.Count == 2);
            //                  result = await testQueryable.ToListAsync();
            //                Assert.NotNull(result);
            //                Assert.True(result.Count == 2);
            //            }
            //#endif
            if (ValidateUInt64)
            {
                // UInt64
                serviceQuery = ServiceQueryBuilder.New().IsLessThanOrEqual(nameof(TestClass.UInt64Val), "1").Build();
                testQueryable = serviceQuery.Apply(sourceQueryable);
                result = testQueryable.ToList();
                Assert.NotNull(result);
                Assert.True(result.Count == 2);
                result = await testQueryable.ToAsyncEnumerable().ToListAsync();
                Assert.NotNull(result);
                Assert.True(result.Count == 2);
            }

            // UInt32
            serviceQuery = ServiceQueryBuilder.New().IsLessThanOrEqual(nameof(TestClass.UInt32Val), "1").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);

            // UInt16
            serviceQuery = ServiceQueryBuilder.New().IsLessThanOrEqual(nameof(TestClass.UInt16Val), "1").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);
        }

        [Fact]
        public async Task IsLessThanOrEqualRequestTest()
        {
            var sourceQueryable = GetTestList();
            IQueryable<T> testQueryable;
            IServiceQueryRequest serviceQuery;
            List<T> result;

            // Boolean
            serviceQuery = ServiceQueryRequestBuilder.New().IsLessThanOrEqual(nameof(TestClass.BoolVal), "true").Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            });

            // ByteArray
            byte[] tempbyte = new byte[] { 1 };
            serviceQuery = ServiceQueryRequestBuilder.New().IsLessThanOrEqual(nameof(TestClass.ByteArrayVal), System.Text.Encoding.UTF8.GetString(tempbyte)).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            });

            // Byte
            serviceQuery = ServiceQueryRequestBuilder.New().IsLessThanOrEqual(nameof(TestClass.ByteVal), "1").Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);

            // Char
            serviceQuery = ServiceQueryRequestBuilder.New().IsLessThanOrEqual(nameof(TestClass.CharVal), "b").Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 3);
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 3);

            if (ValidateDateTimeOffset)
            {
                // DateTimeOffset
                var tempDateTimeOffset = new T().GetDefault1Record().DateTimeOffsetVal;
                serviceQuery = ServiceQueryRequestBuilder.New().IsLessThanOrEqual(nameof(TestClass.DateTimeOffsetVal), tempDateTimeOffset.ToString("o")).Build();
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
                result = testQueryable.ToList();
                Assert.NotNull(result);
                Assert.True(result.Count == 2);
                result = await testQueryable.ToAsyncEnumerable().ToListAsync();
                Assert.NotNull(result);
                Assert.True(result.Count == 2);
            }

            // DateTime
            var tempDateTime = new T().GetDefault1Record().DateTimeVal;
            serviceQuery = ServiceQueryRequestBuilder.New().IsLessThanOrEqual(nameof(TestClass.DateTimeVal), tempDateTime.ToString("o")).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);

            // Decimal
            serviceQuery = ServiceQueryRequestBuilder.New().IsLessThanOrEqual(nameof(TestClass.DecimalVal), "1").Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);

            // Double
            serviceQuery = ServiceQueryRequestBuilder.New().IsLessThanOrEqual(nameof(TestClass.DoubleVal), "1").Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);

            // Float
            serviceQuery = ServiceQueryRequestBuilder.New().IsLessThanOrEqual(nameof(TestClass.FloatVal), "1").Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);

#if NET7_0_OR_GREATER
            // Guid
            serviceQuery = ServiceQueryRequestBuilder.New().IsLessThanOrEqual(nameof(TestClass.GuidVal), "11111111-1111-1111-1111-111111111111").Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);
#endif
            // Int
            serviceQuery = ServiceQueryRequestBuilder.New().IsLessThanOrEqual(nameof(TestClass.IntVal), "1").Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);

            // Long
            serviceQuery = ServiceQueryRequestBuilder.New().IsLessThanOrEqual(nameof(TestClass.LongVal), "1").Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);

            // SByte
            serviceQuery = ServiceQueryRequestBuilder.New().IsLessThanOrEqual(nameof(TestClass.SByteVal), "1").Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);

            // Short
            serviceQuery = ServiceQueryRequestBuilder.New().IsLessThanOrEqual(nameof(TestClass.ShortVal), "1").Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);

            // Single
            serviceQuery = ServiceQueryRequestBuilder.New().IsLessThanOrEqual(nameof(TestClass.SingleVal), "1").Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);

            // String
            serviceQuery = ServiceQueryRequestBuilder.New().IsLessThanOrEqual(nameof(TestClass.StringVal), "a").Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            });

            if (ValidateTimeSpan)
            {
                // TimeSpan
                serviceQuery = ServiceQueryRequestBuilder.New().IsLessThanOrEqual(nameof(TestClass.TimeSpanVal), TimeSpan.FromMilliseconds(1).ToString()).Build();
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
                result = testQueryable.ToList();
                Assert.NotNull(result);
                Assert.True(result.Count == 2);
                result = await testQueryable.ToAsyncEnumerable().ToListAsync();
                Assert.NotNull(result);
                Assert.True(result.Count == 2);
            }
            //#if NET7_0
            //            if (ValidateUInt128)
            //            {
            //                // UInt128
            //                serviceQuery = ServiceQueryRequestBuilder.New().IsLessThanOrEqual(nameof(TestClass.UInt128Val), "1").Build();
            //                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            //                result = testQueryable.ToList();
            //                Assert.NotNull(result);
            //                Assert.True(result.Count == 2);
            //                  result = await testQueryable.ToListAsync();
            //                Assert.NotNull(result);
            //                Assert.True(result.Count == 2);
            //            }
            //#endif
            if (ValidateUInt64)
            {
                // UInt64
                serviceQuery = ServiceQueryRequestBuilder.New().IsLessThanOrEqual(nameof(TestClass.UInt64Val), "1").Build();
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
                result = testQueryable.ToList();
                Assert.NotNull(result);
                Assert.True(result.Count == 2);
                result = await testQueryable.ToAsyncEnumerable().ToListAsync();
                Assert.NotNull(result);
                Assert.True(result.Count == 2);
            }

            // UInt32
            serviceQuery = ServiceQueryRequestBuilder.New().IsLessThanOrEqual(nameof(TestClass.UInt32Val), "1").Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);

            // UInt16
            serviceQuery = ServiceQueryRequestBuilder.New().IsLessThanOrEqual(nameof(TestClass.UInt16Val), "1").Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);
        }

        [Fact]
        public async Task NullIsLessThanOrEqualStandardTest()
        {
            var sourceQueryable = GetTestList();
            IQueryable<T> testQueryable;
            IServiceQuery serviceQuery;
            List<T> result;

            // Boolean
            serviceQuery = ServiceQueryBuilder.New().IsLessThanOrEqual(nameof(TestClass.NullBoolVal), "true").Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.Apply(sourceQueryable);
            });

            // ByteArray
            byte[] tempbyte = new byte[] { 1 };
            serviceQuery = ServiceQueryBuilder.New().IsLessThanOrEqual(nameof(TestClass.NullByteArrayVal), System.Text.Encoding.UTF8.GetString(tempbyte)).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.Apply(sourceQueryable);
            });

            // Byte
            serviceQuery = ServiceQueryBuilder.New().IsLessThanOrEqual(nameof(TestClass.NullByteVal), "1").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);

            // Char
            serviceQuery = ServiceQueryBuilder.New().IsLessThanOrEqual(nameof(TestClass.NullCharVal), "b").Build();
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
                serviceQuery = ServiceQueryBuilder.New().IsLessThanOrEqual(nameof(TestClass.NullDateTimeOffsetVal), tempDateTimeOffset.ToString("o")).Build();
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
            serviceQuery = ServiceQueryBuilder.New().IsLessThanOrEqual(nameof(TestClass.NullDateTimeVal), tempDateTime.ToString("o")).Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);

            // Decimal
            serviceQuery = ServiceQueryBuilder.New().IsLessThanOrEqual(nameof(TestClass.NullDecimalVal), "1").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);

            // Double
            serviceQuery = ServiceQueryBuilder.New().IsLessThanOrEqual(nameof(TestClass.NullDoubleVal), "1").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);

            // Float
            serviceQuery = ServiceQueryBuilder.New().IsLessThanOrEqual(nameof(TestClass.NullFloatVal), "1").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);

#if NET7_0_OR_GREATER
            // Guid
            serviceQuery = ServiceQueryBuilder.New().IsLessThanOrEqual(nameof(TestClass.NullGuidVal), "11111111-1111-1111-1111-111111111111").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
#endif
            // Int
            serviceQuery = ServiceQueryBuilder.New().IsLessThanOrEqual(nameof(TestClass.NullIntVal), "1").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);

            // Long
            serviceQuery = ServiceQueryBuilder.New().IsLessThanOrEqual(nameof(TestClass.NullLongVal), "1").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);

            // SByte
            serviceQuery = ServiceQueryBuilder.New().IsLessThanOrEqual(nameof(TestClass.NullSByteVal), "1").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);

            // Short
            serviceQuery = ServiceQueryBuilder.New().IsLessThanOrEqual(nameof(TestClass.NullShortVal), "1").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);

            // Single
            serviceQuery = ServiceQueryBuilder.New().IsLessThanOrEqual(nameof(TestClass.NullSingleVal), "1").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);

            // String
            serviceQuery = ServiceQueryBuilder.New().IsLessThanOrEqual(nameof(TestClass.NullStringVal), "a").Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.Apply(sourceQueryable);
            });

            if (ValidateTimeSpan)
            {
                // TimeSpan
                serviceQuery = ServiceQueryBuilder.New().IsLessThanOrEqual(nameof(TestClass.NullTimeSpanVal), TimeSpan.FromMilliseconds(1).ToString()).Build();
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
            //                serviceQuery = ServiceQueryBuilder.New().IsLessThanOrEqual(nameof(TestClass.NullUInt128Val), "1").Build();
            //                testQueryable = serviceQuery.Apply(sourceQueryable);
            //                result = testQueryable.ToList();
            //                Assert.NotNull(result);
            //                Assert.True(result.Count == 0);
            //                  result = await testQueryable.ToListAsync();
            //                Assert.NotNull(result);
            //                Assert.True(result.Count == 0);
            //            }
            //#endif
            if (ValidateUInt64)
            {
                // UInt64
                serviceQuery = ServiceQueryBuilder.New().IsLessThanOrEqual(nameof(TestClass.NullUInt64Val), "1").Build();
                testQueryable = serviceQuery.Apply(sourceQueryable);
                result = testQueryable.ToList();
                Assert.NotNull(result);
                Assert.True(result.Count == 0);
                result = await testQueryable.ToAsyncEnumerable().ToListAsync();
                Assert.NotNull(result);
                Assert.True(result.Count == 0);
            }

            // UInt32
            serviceQuery = ServiceQueryBuilder.New().IsLessThanOrEqual(nameof(TestClass.NullUInt32Val), "1").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);

            // UInt16
            serviceQuery = ServiceQueryBuilder.New().IsLessThanOrEqual(nameof(TestClass.NullUInt16Val), "1").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
        }

        [Fact]
        public async Task NullIsLessThanOrEqualRequestTest()
        {
            var sourceQueryable = GetTestList();
            IQueryable<T> testQueryable;
            IServiceQueryRequest serviceQuery;
            List<T> result;

            // Boolean
            serviceQuery = ServiceQueryRequestBuilder.New().IsLessThanOrEqual(nameof(TestClass.NullBoolVal), "true").Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            });

            // ByteArray
            byte[] tempbyte = new byte[] { 1 };
            serviceQuery = ServiceQueryRequestBuilder.New().IsLessThanOrEqual(nameof(TestClass.NullByteArrayVal), System.Text.Encoding.UTF8.GetString(tempbyte)).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            });

            // Byte
            serviceQuery = ServiceQueryRequestBuilder.New().IsLessThanOrEqual(nameof(TestClass.NullByteVal), "1").Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);

            // Char
            serviceQuery = ServiceQueryRequestBuilder.New().IsLessThanOrEqual(nameof(TestClass.NullCharVal), "b").Build();
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
                serviceQuery = ServiceQueryRequestBuilder.New().IsLessThanOrEqual(nameof(TestClass.NullDateTimeOffsetVal), tempDateTimeOffset.ToString("o")).Build();
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
            serviceQuery = ServiceQueryRequestBuilder.New().IsLessThanOrEqual(nameof(TestClass.NullDateTimeVal), tempDateTime.ToString("o")).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);

            // Decimal
            serviceQuery = ServiceQueryRequestBuilder.New().IsLessThanOrEqual(nameof(TestClass.NullDecimalVal), "1").Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);

            // Double
            serviceQuery = ServiceQueryRequestBuilder.New().IsLessThanOrEqual(nameof(TestClass.NullDoubleVal), "1").Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);

            // Float
            serviceQuery = ServiceQueryRequestBuilder.New().IsLessThanOrEqual(nameof(TestClass.NullFloatVal), "1").Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);

#if NET7_0_OR_GREATER
            // Guid
            serviceQuery = ServiceQueryRequestBuilder.New().IsLessThanOrEqual(nameof(TestClass.NullGuidVal), "11111111-1111-1111-1111-111111111111").Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
#endif
            // Int
            serviceQuery = ServiceQueryRequestBuilder.New().IsLessThanOrEqual(nameof(TestClass.NullIntVal), "1").Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);

            // Long
            serviceQuery = ServiceQueryRequestBuilder.New().IsLessThanOrEqual(nameof(TestClass.NullLongVal), "1").Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);

            // SByte
            serviceQuery = ServiceQueryRequestBuilder.New().IsLessThanOrEqual(nameof(TestClass.NullSByteVal), "1").Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);

            // Short
            serviceQuery = ServiceQueryRequestBuilder.New().IsLessThanOrEqual(nameof(TestClass.NullShortVal), "1").Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);

            // Single
            serviceQuery = ServiceQueryRequestBuilder.New().IsLessThanOrEqual(nameof(TestClass.NullSingleVal), "1").Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);

            // String
            serviceQuery = ServiceQueryRequestBuilder.New().IsLessThanOrEqual(nameof(TestClass.NullStringVal), "a").Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            });

            if (ValidateTimeSpan)
            {
                // TimeSpan
                serviceQuery = ServiceQueryRequestBuilder.New().IsLessThanOrEqual(nameof(TestClass.NullTimeSpanVal), TimeSpan.FromMilliseconds(1).ToString()).Build();
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
            //                serviceQuery = ServiceQueryRequestBuilder.New().IsLessThanOrEqual(nameof(TestClass.NullUInt128Val), "1").Build();
            //                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            //                result = testQueryable.ToList();
            //                Assert.NotNull(result);
            //                Assert.True(result.Count == 0);
            //                  result = await testQueryable.ToListAsync();
            //                Assert.NotNull(result);
            //                Assert.True(result.Count == 0);
            //            }
            //#endif
            if (ValidateUInt64)
            {
                // UInt64
                serviceQuery = ServiceQueryRequestBuilder.New().IsLessThanOrEqual(nameof(TestClass.NullUInt64Val), "1").Build();
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
                result = testQueryable.ToList();
                Assert.NotNull(result);
                Assert.True(result.Count == 0);
                result = await testQueryable.ToAsyncEnumerable().ToListAsync();
                Assert.NotNull(result);
                Assert.True(result.Count == 0);
            }

            // UInt32
            serviceQuery = ServiceQueryRequestBuilder.New().IsLessThanOrEqual(nameof(TestClass.NullUInt32Val), "1").Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
            result = await testQueryable.ToAsyncEnumerable().ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);

            // UInt16
            serviceQuery = ServiceQueryRequestBuilder.New().IsLessThanOrEqual(nameof(TestClass.NullUInt16Val), "1").Build();
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
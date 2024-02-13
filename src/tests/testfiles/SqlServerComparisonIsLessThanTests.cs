using System.Data.Entity;

namespace ServiceQuery.Xunit
{
    public class SqlServerComparisonIsLessThanTests : SqlServerComparisonIsLessThanTests<TestClass>
    {
        public SqlServerComparisonIsLessThanTests()
        {
            ValidateUInt128 = false;
        }

        public override IQueryable<TestClass> GetTestList()
        {
            return SqlServerHelper.GetTestList();
        }
    }

    public abstract class SqlServerComparisonIsLessThanTests<T> : BaseTest<T> where T : class, ITestClass, new()
    {
        [Fact]
        public async Task IsLessThanStandardTest()
        {
            var sourceQueryable = GetTestList();
            IQueryable<T> testQueryable;
            IServiceQuery serviceQuery;
            List<T> result;

            // Boolean
            serviceQuery = ServiceQueryBuilder.New().IsLessThan(nameof(TestClass.BoolVal), "true").Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.Apply(sourceQueryable);
            });

            // ByteArray
            byte[] tempbyte = new byte[] { 1 };
            serviceQuery = ServiceQueryBuilder.New().IsLessThan(nameof(TestClass.ByteArrayVal), System.Text.Encoding.UTF8.GetString(tempbyte)).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.Apply(sourceQueryable);
            });

            // Byte
            serviceQuery = ServiceQueryBuilder.New().IsLessThan(nameof(TestClass.ByteVal), "1").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);

            // Char
            serviceQuery = ServiceQueryBuilder.New().IsLessThan(nameof(TestClass.CharVal), "b").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);

            if (ValidateDateTimeOffset)
            {
                // DateTimeOffset
                var tempDateTimeOffset = new T().GetDefault1Record().DateTimeOffsetVal;
                serviceQuery = ServiceQueryBuilder.New().IsLessThan(nameof(TestClass.DateTimeOffsetVal), tempDateTimeOffset.ToString("o")).Build();
                testQueryable = serviceQuery.Apply(sourceQueryable);
                result = testQueryable.ToList();
                Assert.NotNull(result);
                Assert.True(result.Count == 1);
                result = await testQueryable.ToListAsync();
                Assert.NotNull(result);
                Assert.True(result.Count == 1);
            }

            // DateTime
            var tempDateTime = new T().GetDefault1Record().DateTimeVal;
            serviceQuery = ServiceQueryBuilder.New().IsLessThan(nameof(TestClass.DateTimeVal), tempDateTime.ToString("o")).Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);

            // Decimal
            serviceQuery = ServiceQueryBuilder.New().IsLessThan(nameof(TestClass.DecimalVal), "1").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);

            // Double
            serviceQuery = ServiceQueryBuilder.New().IsLessThan(nameof(TestClass.DoubleVal), "1").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);

            // Float
            serviceQuery = ServiceQueryBuilder.New().IsLessThan(nameof(TestClass.FloatVal), "1").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
#if NET7_0_OR_GREATER

            // Guid
            serviceQuery = ServiceQueryBuilder.New().IsLessThan(nameof(TestClass.GuidVal), "11111111-1111-1111-1111-111111111111").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
#endif
            // Int
            serviceQuery = ServiceQueryBuilder.New().IsLessThan(nameof(TestClass.IntVal), "1").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);

            // Long
            serviceQuery = ServiceQueryBuilder.New().IsLessThan(nameof(TestClass.LongVal), "1").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);

            // SByte
            serviceQuery = ServiceQueryBuilder.New().IsLessThan(nameof(TestClass.SByteVal), "1").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);

            // Short
            serviceQuery = ServiceQueryBuilder.New().IsLessThan(nameof(TestClass.ShortVal), "1").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);

            // Single
            serviceQuery = ServiceQueryBuilder.New().IsLessThan(nameof(TestClass.SingleVal), "1").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);

            // String
            serviceQuery = ServiceQueryBuilder.New().IsLessThan(nameof(TestClass.StringVal), "a").Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.Apply(sourceQueryable);
            });

            if (ValidateTimeSpan)
            {
                // TimeSpan
                serviceQuery = ServiceQueryBuilder.New().IsLessThan(nameof(TestClass.TimeSpanVal), TimeSpan.FromMilliseconds(1).ToString()).Build();
                testQueryable = serviceQuery.Apply(sourceQueryable);
                result = testQueryable.ToList();
                Assert.NotNull(result);
                Assert.True(result.Count == 1);
                result = await testQueryable.ToListAsync();
                Assert.NotNull(result);
                Assert.True(result.Count == 1);
            }
            //#if NET7_0
            //            if (ValidateUInt128)
            //            {
            //                // UInt128
            //                serviceQuery = ServiceQueryBuilder.New().IsLessThan(nameof(TestClass.UInt128Val), "1").Build();
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
                serviceQuery = ServiceQueryBuilder.New().IsLessThan(nameof(TestClass.UInt64Val), "1").Build();
                testQueryable = serviceQuery.Apply(sourceQueryable);
                result = testQueryable.ToList();
                Assert.NotNull(result);
                Assert.True(result.Count == 1);
                result = await testQueryable.ToListAsync();
                Assert.NotNull(result);
                Assert.True(result.Count == 1);
            }

            // UInt32
            serviceQuery = ServiceQueryBuilder.New().IsLessThan(nameof(TestClass.UInt32Val), "1").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);

            // UInt16
            serviceQuery = ServiceQueryBuilder.New().IsLessThan(nameof(TestClass.UInt16Val), "1").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
        }

        [Fact]
        public async Task IsLessThanRequestTest()
        {
            var sourceQueryable = GetTestList();
            IQueryable<T> testQueryable;
            IServiceQueryRequest serviceQuery;
            List<T> result;

            // Boolean
            serviceQuery = ServiceQueryRequestBuilder.New().IsLessThan(nameof(TestClass.BoolVal), "true").Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            });

            // ByteArray
            byte[] tempbyte = new byte[] { 1 };
            serviceQuery = ServiceQueryRequestBuilder.New().IsLessThan(nameof(TestClass.ByteArrayVal), System.Text.Encoding.UTF8.GetString(tempbyte)).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            });

            // Byte
            serviceQuery = ServiceQueryRequestBuilder.New().IsLessThan(nameof(TestClass.ByteVal), "1").Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);

            // Char
            serviceQuery = ServiceQueryRequestBuilder.New().IsLessThan(nameof(TestClass.CharVal), "b").Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 2);

            if (ValidateDateTimeOffset)
            {
                // DateTimeOffset
                var tempDateTimeOffset = new T().GetDefault1Record().DateTimeOffsetVal;
                serviceQuery = ServiceQueryRequestBuilder.New().IsLessThan(nameof(TestClass.DateTimeOffsetVal), tempDateTimeOffset.ToString("o")).Build();
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
                result = testQueryable.ToList();
                Assert.NotNull(result);
                Assert.True(result.Count == 1);
                result = await testQueryable.ToListAsync();
                Assert.NotNull(result);
                Assert.True(result.Count == 1);
            }

            // DateTime
            var tempDateTime = new T().GetDefault1Record().DateTimeVal;
            serviceQuery = ServiceQueryRequestBuilder.New().IsLessThan(nameof(TestClass.DateTimeVal), tempDateTime.ToString("o")).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);

            // Decimal
            serviceQuery = ServiceQueryRequestBuilder.New().IsLessThan(nameof(TestClass.DecimalVal), "1").Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);

            // Double
            serviceQuery = ServiceQueryRequestBuilder.New().IsLessThan(nameof(TestClass.DoubleVal), "1").Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);

            // Float
            serviceQuery = ServiceQueryRequestBuilder.New().IsLessThan(nameof(TestClass.FloatVal), "1").Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
#if NET7_0_OR_GREATER

            // Guid
            serviceQuery = ServiceQueryRequestBuilder.New().IsLessThan(nameof(TestClass.GuidVal), "11111111-1111-1111-1111-111111111111").Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
#endif
            // Int
            serviceQuery = ServiceQueryRequestBuilder.New().IsLessThan(nameof(TestClass.IntVal), "1").Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);

            // Long
            serviceQuery = ServiceQueryRequestBuilder.New().IsLessThan(nameof(TestClass.LongVal), "1").Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);

            // SByte
            serviceQuery = ServiceQueryRequestBuilder.New().IsLessThan(nameof(TestClass.SByteVal), "1").Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);

            // Short
            serviceQuery = ServiceQueryRequestBuilder.New().IsLessThan(nameof(TestClass.ShortVal), "1").Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);

            // Single
            serviceQuery = ServiceQueryRequestBuilder.New().IsLessThan(nameof(TestClass.SingleVal), "1").Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);

            // String
            serviceQuery = ServiceQueryRequestBuilder.New().IsLessThan(nameof(TestClass.StringVal), "a").Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            });

            if (ValidateTimeSpan)
            {
                // TimeSpan
                serviceQuery = ServiceQueryRequestBuilder.New().IsLessThan(nameof(TestClass.TimeSpanVal), TimeSpan.FromMilliseconds(1).ToString()).Build();
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
                result = testQueryable.ToList();
                Assert.NotNull(result);
                Assert.True(result.Count == 1);
                result = await testQueryable.ToListAsync();
                Assert.NotNull(result);
                Assert.True(result.Count == 1);
            }
            //#if NET7_0
            //            if (ValidateUInt128)
            //            {
            //                // UInt128
            //                serviceQuery = ServiceQueryRequestBuilder.New().IsLessThan(nameof(TestClass.UInt128Val), "1").Build();
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
                serviceQuery = ServiceQueryRequestBuilder.New().IsLessThan(nameof(TestClass.UInt64Val), "1").Build();
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
                result = testQueryable.ToList();
                Assert.NotNull(result);
                Assert.True(result.Count == 1);
                result = await testQueryable.ToListAsync();
                Assert.NotNull(result);
                Assert.True(result.Count == 1);
            }

            // UInt32
            serviceQuery = ServiceQueryRequestBuilder.New().IsLessThan(nameof(TestClass.UInt32Val), "1").Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);

            // UInt16
            serviceQuery = ServiceQueryRequestBuilder.New().IsLessThan(nameof(TestClass.UInt16Val), "1").Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
        }

        [Fact]
        public async Task NullIsLessThanStandardTest()
        {
            var sourceQueryable = GetTestList();
            IQueryable<T> testQueryable;
            IServiceQuery serviceQuery;
            List<T> result;

            // Boolean
            serviceQuery = ServiceQueryBuilder.New().IsLessThan(nameof(TestClass.NullBoolVal), "true").Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.Apply(sourceQueryable);
            });

            // ByteArray
            byte[] tempbyte = new byte[] { 1 };
            serviceQuery = ServiceQueryBuilder.New().IsLessThan(nameof(TestClass.NullByteArrayVal), System.Text.Encoding.UTF8.GetString(tempbyte)).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.Apply(sourceQueryable);
            });

            // Byte
            serviceQuery = ServiceQueryBuilder.New().IsLessThan(nameof(TestClass.NullByteVal), "1").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);

            // Char
            serviceQuery = ServiceQueryBuilder.New().IsLessThan(nameof(TestClass.NullCharVal), "b").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);

            if (ValidateDateTimeOffset)
            {
                // DateTimeOffset
                var tempDateTimeOffset = new T().GetDefault1Record().DateTimeOffsetVal;
                serviceQuery = ServiceQueryBuilder.New().IsLessThan(nameof(TestClass.NullDateTimeOffsetVal), tempDateTimeOffset.ToString("o")).Build();
                testQueryable = serviceQuery.Apply(sourceQueryable);
                result = testQueryable.ToList();
                Assert.NotNull(result);
                Assert.True(result.Count == 0);
                result = await testQueryable.ToListAsync();
                Assert.NotNull(result);
                Assert.True(result.Count == 0);
            }

            // DateTime
            var tempDateTime = new T().GetDefault1Record().DateTimeVal;
            serviceQuery = ServiceQueryBuilder.New().IsLessThan(nameof(TestClass.NullDateTimeVal), tempDateTime.ToString("o")).Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);

            // Decimal
            serviceQuery = ServiceQueryBuilder.New().IsLessThan(nameof(TestClass.NullDecimalVal), "1").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);

            // Double
            serviceQuery = ServiceQueryBuilder.New().IsLessThan(nameof(TestClass.NullDoubleVal), "1").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);

            // Float
            serviceQuery = ServiceQueryBuilder.New().IsLessThan(nameof(TestClass.NullFloatVal), "1").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
#if NET7_0_OR_GREATER

            // Guid
            serviceQuery = ServiceQueryBuilder.New().IsLessThan(nameof(TestClass.NullGuidVal), "11111111-1111-1111-1111-111111111111").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
#endif
            // Int
            serviceQuery = ServiceQueryBuilder.New().IsLessThan(nameof(TestClass.NullIntVal), "1").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);

            // Long
            serviceQuery = ServiceQueryBuilder.New().IsLessThan(nameof(TestClass.NullLongVal), "1").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);

            // SByte
            serviceQuery = ServiceQueryBuilder.New().IsLessThan(nameof(TestClass.NullSByteVal), "1").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);

            // Short
            serviceQuery = ServiceQueryBuilder.New().IsLessThan(nameof(TestClass.NullShortVal), "1").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);

            // Single
            serviceQuery = ServiceQueryBuilder.New().IsLessThan(nameof(TestClass.NullSingleVal), "1").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);

            // String
            serviceQuery = ServiceQueryBuilder.New().IsLessThan(nameof(TestClass.NullStringVal), "a").Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.Apply(sourceQueryable);
            });

            if (ValidateTimeSpan)
            {
                // TimeSpan
                serviceQuery = ServiceQueryBuilder.New().IsLessThan(nameof(TestClass.NullTimeSpanVal), TimeSpan.FromMilliseconds(1).ToString()).Build();
                testQueryable = serviceQuery.Apply(sourceQueryable);
                result = testQueryable.ToList();
                Assert.NotNull(result);
                Assert.True(result.Count == 0);
                result = await testQueryable.ToListAsync();
                Assert.NotNull(result);
                Assert.True(result.Count == 0);
            }
            //#if NET7_0
            //            if (ValidateUInt128)
            //            {
            //                // UInt128
            //                serviceQuery = ServiceQueryBuilder.New().IsLessThan(nameof(TestClass.NullUInt128Val), "1").Build();
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
                serviceQuery = ServiceQueryBuilder.New().IsLessThan(nameof(TestClass.NullUInt64Val), "1").Build();
                testQueryable = serviceQuery.Apply(sourceQueryable);
                result = testQueryable.ToList();
                Assert.NotNull(result);
                Assert.True(result.Count == 0);
                result = await testQueryable.ToListAsync();
                Assert.NotNull(result);
                Assert.True(result.Count == 0);
            }

            // UInt32
            serviceQuery = ServiceQueryBuilder.New().IsLessThan(nameof(TestClass.NullUInt32Val), "1").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);

            // UInt16
            serviceQuery = ServiceQueryBuilder.New().IsLessThan(nameof(TestClass.NullUInt16Val), "1").Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
        }

        [Fact]
        public async Task NullIsLessThanRequestTest()
        {
            var sourceQueryable = GetTestList();
            IQueryable<T> testQueryable;
            IServiceQueryRequest serviceQuery;
            List<T> result;

            // Boolean
            serviceQuery = ServiceQueryRequestBuilder.New().IsLessThan(nameof(TestClass.NullBoolVal), "true").Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            });

            // ByteArray
            byte[] tempbyte = new byte[] { 1 };
            serviceQuery = ServiceQueryRequestBuilder.New().IsLessThan(nameof(TestClass.NullByteArrayVal), System.Text.Encoding.UTF8.GetString(tempbyte)).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            });

            // Byte
            serviceQuery = ServiceQueryRequestBuilder.New().IsLessThan(nameof(TestClass.NullByteVal), "1").Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);

            // Char
            serviceQuery = ServiceQueryRequestBuilder.New().IsLessThan(nameof(TestClass.NullCharVal), "b").Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);

            if (ValidateDateTimeOffset)
            {
                // DateTimeOffset
                var tempDateTimeOffset = new T().GetDefault1Record().DateTimeOffsetVal;
                serviceQuery = ServiceQueryRequestBuilder.New().IsLessThan(nameof(TestClass.NullDateTimeOffsetVal), tempDateTimeOffset.ToString("o")).Build();
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
                result = testQueryable.ToList();
                Assert.NotNull(result);
                Assert.True(result.Count == 0);
                result = await testQueryable.ToListAsync();
                Assert.NotNull(result);
                Assert.True(result.Count == 0);
            }

            // DateTime
            var tempDateTime = new T().GetDefault1Record().DateTimeVal;
            serviceQuery = ServiceQueryRequestBuilder.New().IsLessThan(nameof(TestClass.NullDateTimeVal), tempDateTime.ToString("o")).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);

            // Decimal
            serviceQuery = ServiceQueryRequestBuilder.New().IsLessThan(nameof(TestClass.NullDecimalVal), "1").Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);

            // Double
            serviceQuery = ServiceQueryRequestBuilder.New().IsLessThan(nameof(TestClass.NullDoubleVal), "1").Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);

            // Float
            serviceQuery = ServiceQueryRequestBuilder.New().IsLessThan(nameof(TestClass.NullFloatVal), "1").Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
#if NET7_0_OR_GREATER

            // Guid
            serviceQuery = ServiceQueryRequestBuilder.New().IsLessThan(nameof(TestClass.NullGuidVal), "11111111-1111-1111-1111-111111111111").Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
#endif
            // Int
            serviceQuery = ServiceQueryRequestBuilder.New().IsLessThan(nameof(TestClass.NullIntVal), "1").Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);

            // Long
            serviceQuery = ServiceQueryRequestBuilder.New().IsLessThan(nameof(TestClass.NullLongVal), "1").Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);

            // SByte
            serviceQuery = ServiceQueryRequestBuilder.New().IsLessThan(nameof(TestClass.NullSByteVal), "1").Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);

            // Short
            serviceQuery = ServiceQueryRequestBuilder.New().IsLessThan(nameof(TestClass.NullShortVal), "1").Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);

            // Single
            serviceQuery = ServiceQueryRequestBuilder.New().IsLessThan(nameof(TestClass.NullSingleVal), "1").Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);

            // String
            serviceQuery = ServiceQueryRequestBuilder.New().IsLessThan(nameof(TestClass.NullStringVal), "a").Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            });

            if (ValidateTimeSpan)
            {
                // TimeSpan
                serviceQuery = ServiceQueryRequestBuilder.New().IsLessThan(nameof(TestClass.NullTimeSpanVal), TimeSpan.FromMilliseconds(1).ToString()).Build();
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
                result = testQueryable.ToList();
                Assert.NotNull(result);
                Assert.True(result.Count == 0);
                result = await testQueryable.ToListAsync();
                Assert.NotNull(result);
                Assert.True(result.Count == 0);
            }
            //#if NET7_0
            //            if (ValidateUInt128)
            //            {
            //                // UInt128
            //                serviceQuery = ServiceQueryRequestBuilder.New().IsLessThan(nameof(TestClass.NullUInt128Val), "1").Build();
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
                serviceQuery = ServiceQueryRequestBuilder.New().IsLessThan(nameof(TestClass.NullUInt64Val), "1").Build();
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
                result = testQueryable.ToList();
                Assert.NotNull(result);
                Assert.True(result.Count == 0);
                result = await testQueryable.ToListAsync();
                Assert.NotNull(result);
                Assert.True(result.Count == 0);
            }

            // UInt32
            serviceQuery = ServiceQueryRequestBuilder.New().IsLessThan(nameof(TestClass.NullUInt32Val), "1").Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);

            // UInt16
            serviceQuery = ServiceQueryRequestBuilder.New().IsLessThan(nameof(TestClass.NullUInt16Val), "1").Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
        }
    }
}
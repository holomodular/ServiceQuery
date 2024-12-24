using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace ServiceQuery.Xunit
{
    public class ComparisonIsNullTestsAsyncMongoDb : ComparisonIsNullTestsAsyncMongoDb<TestClass>
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
            return Task.FromResult<IQueryable<TestClass>>(null);
        }

        public override Task<IQueryable<TestClass>> GetTestNullCopyListAsync()
        {
            return Task.FromResult<IQueryable<TestClass>>(null);
        }
    }

    public abstract class ComparisonIsNullTestsAsyncMongoDb<T> : BaseTest<T> where T : class, ITestClass, new()
    {
        [Fact]
        public async Task SyncIsNullStandardTest()
        {
            var sourceQueryable = await GetTestListAsync();
            if (sourceQueryable == null)
                return;
            IQueryable<T> testQueryable;
            IServiceQuery serviceQuery;
            List<T> result;

            // Boolean
            serviceQuery = ServiceQueryBuilder.New().IsNull(nameof(TestClass.BoolVal)).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.Apply(sourceQueryable);
            });

            // ByteArray
            //var tempbyteArray = new byte[1] { 1 };
            //serviceQuery = ServiceQueryBuilder.New().IsNull(nameof(TestClass.ByteArrayVal), System.Text.Encoding.UTF8.GetString(tempbyteArray)).Build();
            //testQueryable = serviceQuery.GetQueryable(sourceQueryable);
            //result = await testQueryable.ToListAsync();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 1);
            //result = await testQueryable.ToListAsync();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 1);

            // Byte
            serviceQuery = ServiceQueryBuilder.New().IsNull(nameof(TestClass.ByteVal)).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.Apply(sourceQueryable);
            });

            // Char
            serviceQuery = ServiceQueryBuilder.New().IsNull(nameof(TestClass.CharVal)).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.Apply(sourceQueryable);
            });

            if (ValidateDateTimeOffset)
            {
                // DateTimeOffset
                var tempDateTimeOffset = new T().GetDefault1Record(new T()).DateTimeOffsetVal;
                serviceQuery = ServiceQueryBuilder.New().IsNull(nameof(TestClass.DateTimeOffsetVal)).Build();
                Assert.Throws<ServiceQueryException>(() =>
                {
                    testQueryable = serviceQuery.Apply(sourceQueryable);
                });
            }

#if NET6_0_OR_GREATER
            // DateOnly
            var tempDateOnly = new T().GetDefault1Record(new T()).DateOnlyVal;
            serviceQuery = ServiceQueryBuilder.New().IsNull(nameof(TestClass.DateOnlyVal)).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.Apply(sourceQueryable);
            });
#endif
            // DateTime
            var tempDateTime = new T().GetDefault1Record(new T()).DateTimeVal;
            serviceQuery = ServiceQueryBuilder.New().IsNull(nameof(TestClass.DateTimeVal)).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.Apply(sourceQueryable);
            });

            // Decimal
            serviceQuery = ServiceQueryBuilder.New().IsNull(nameof(TestClass.DecimalVal)).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.Apply(sourceQueryable);
            });

            // Double
            serviceQuery = ServiceQueryBuilder.New().IsNull(nameof(TestClass.DoubleVal)).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.Apply(sourceQueryable);
            });

            // Float
            serviceQuery = ServiceQueryBuilder.New().IsNull(nameof(TestClass.FloatVal)).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.Apply(sourceQueryable);
            });

            // Guid
            serviceQuery = ServiceQueryBuilder.New().IsNull(nameof(TestClass.GuidVal)).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.Apply(sourceQueryable);
            });

            // Int
            serviceQuery = ServiceQueryBuilder.New().IsNull(nameof(TestClass.IntVal)).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.Apply(sourceQueryable);
            });

            // Long
            serviceQuery = ServiceQueryBuilder.New().IsNull(nameof(TestClass.LongVal)).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.Apply(sourceQueryable);
            });

            // SByte
            serviceQuery = ServiceQueryBuilder.New().IsNull(nameof(TestClass.SByteVal)).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.Apply(sourceQueryable);
            });

            // Short
            serviceQuery = ServiceQueryBuilder.New().IsNull(nameof(TestClass.ShortVal)).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.Apply(sourceQueryable);
            });

            // Single
            serviceQuery = ServiceQueryBuilder.New().IsNull(nameof(TestClass.SingleVal)).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.Apply(sourceQueryable);
            });

            // String
            serviceQuery = ServiceQueryBuilder.New().IsNull(nameof(TestClass.StringVal)).Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);

#if NET6_0_OR_GREATER
            if (ValidateTimeOnly)
            {
                // TimeOnly
                var tempTimeOnly = new T().GetDefault1Record(new T()).TimeOnlyVal;
                serviceQuery = ServiceQueryBuilder.New().IsNull(nameof(TestClass.TimeOnlyVal)).Build();
                Assert.Throws<ServiceQueryException>(() =>
                {
                    testQueryable = serviceQuery.Apply(sourceQueryable);
                });
            }
#endif

            if (ValidateTimeSpan)
            {
                // TimeSpan
                serviceQuery = ServiceQueryBuilder.New().IsNull(nameof(TestClass.TimeSpanVal)).Build();
                Assert.Throws<ServiceQueryException>(() =>
                {
                    testQueryable = serviceQuery.Apply(sourceQueryable);
                });
            }
#if NET7_0_OR_GREATER
            if (ValidateUInt128)
            {
                // UInt128
                serviceQuery = ServiceQueryBuilder.New().IsNull(nameof(TestClass.UInt128Val)).Build();
                Assert.Throws<ServiceQueryException>(() =>
                {
                    testQueryable = serviceQuery.Apply(sourceQueryable);
                });
            }
#endif

            if (ValidateUInt64)
            {
                // UInt64
                serviceQuery = ServiceQueryBuilder.New().IsNull(nameof(TestClass.UInt64Val)).Build();
                Assert.Throws<ServiceQueryException>(() =>
                {
                    testQueryable = serviceQuery.Apply(sourceQueryable);
                });
            }

            // UInt32
            serviceQuery = ServiceQueryBuilder.New().IsNull(nameof(TestClass.UInt32Val)).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.Apply(sourceQueryable);
            });

            // UInt16
            serviceQuery = ServiceQueryBuilder.New().IsNull(nameof(TestClass.UInt16Val)).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.Apply(sourceQueryable);
            });
        }

        [Fact]
        public async Task SyncIsNullRequestTest()
        {
            var sourceQueryable = await GetTestListAsync();
            if (sourceQueryable == null)
                return;
            IQueryable<T> testQueryable;
            IServiceQueryRequest serviceQuery;
            List<T> result;

            // Boolean
            serviceQuery = ServiceQueryRequestBuilder.New().IsNull(nameof(TestClass.BoolVal)).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            });

            // ByteArray
            //var tempbyteArray = new byte[1] { 1 };
            //serviceQuery = ServiceQueryRequestBuilder.New().IsNull(nameof(TestClass.ByteArrayVal), System.Text.Encoding.UTF8.GetString(tempbyteArray)).Build();
            //testQueryable = serviceQuery.GetQueryable(sourceQueryable);
            //result = await testQueryable.ToListAsync();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 1);
            //result = await testQueryable.ToListAsync();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 1);

            // Byte
            serviceQuery = ServiceQueryRequestBuilder.New().IsNull(nameof(TestClass.ByteVal)).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            });

            // Char
            serviceQuery = ServiceQueryRequestBuilder.New().IsNull(nameof(TestClass.CharVal)).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            });

            if (ValidateDateTimeOffset)
            {
                // DateTimeOffset
                var tempDateTimeOffset = new T().GetDefault1Record(new T()).DateTimeOffsetVal;
                serviceQuery = ServiceQueryRequestBuilder.New().IsNull(nameof(TestClass.DateTimeOffsetVal)).Build();
                Assert.Throws<ServiceQueryException>(() =>
                {
                    testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
                });
            }

#if NET6_0_OR_GREATER
            // DateOnly
            var tempDateOnly = new T().GetDefault1Record(new T()).DateOnlyVal;
            serviceQuery = ServiceQueryRequestBuilder.New().IsNull(nameof(TestClass.DateOnlyVal)).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            });
#endif

            // DateTime
            var tempDateTime = new T().GetDefault1Record(new T()).DateTimeVal;
            serviceQuery = ServiceQueryRequestBuilder.New().IsNull(nameof(TestClass.DateTimeVal)).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            });

            // Decimal
            serviceQuery = ServiceQueryRequestBuilder.New().IsNull(nameof(TestClass.DecimalVal)).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            });

            // Double
            serviceQuery = ServiceQueryRequestBuilder.New().IsNull(nameof(TestClass.DoubleVal)).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            });

            // Float
            serviceQuery = ServiceQueryRequestBuilder.New().IsNull(nameof(TestClass.FloatVal)).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            });

            // Guid
            serviceQuery = ServiceQueryRequestBuilder.New().IsNull(nameof(TestClass.GuidVal)).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            });

            // Int
            serviceQuery = ServiceQueryRequestBuilder.New().IsNull(nameof(TestClass.IntVal)).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            });

            // Long
            serviceQuery = ServiceQueryRequestBuilder.New().IsNull(nameof(TestClass.LongVal)).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            });

            // SByte
            serviceQuery = ServiceQueryRequestBuilder.New().IsNull(nameof(TestClass.SByteVal)).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            });

            // Short
            serviceQuery = ServiceQueryRequestBuilder.New().IsNull(nameof(TestClass.ShortVal)).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            });

            // Single
            serviceQuery = ServiceQueryRequestBuilder.New().IsNull(nameof(TestClass.SingleVal)).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            });

            // String
            serviceQuery = ServiceQueryRequestBuilder.New().IsNull(nameof(TestClass.StringVal)).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);

#if NET6_0_OR_GREATER
            if (ValidateTimeOnly)
            {
                // TimeOnly
                var tempTimeOnly = new T().GetDefault1Record(new T()).TimeOnlyVal;
                serviceQuery = ServiceQueryRequestBuilder.New().IsNull(nameof(TestClass.TimeOnlyVal)).Build();
                Assert.Throws<ServiceQueryException>(() =>
                {
                    testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
                });
            }
#endif

            if (ValidateTimeSpan)
            {
                // TimeSpan
                serviceQuery = ServiceQueryRequestBuilder.New().IsNull(nameof(TestClass.TimeSpanVal)).Build();
                Assert.Throws<ServiceQueryException>(() =>
                {
                    testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
                });
            }
#if NET7_0_OR_GREATER
            if (ValidateUInt128)
            {
                // UInt128
                serviceQuery = ServiceQueryRequestBuilder.New().IsNull(nameof(TestClass.UInt128Val)).Build();
                Assert.Throws<ServiceQueryException>(() =>
                {
                    testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
                });
            }
#endif
            if (ValidateUInt64)
            {
                // UInt64
                serviceQuery = ServiceQueryRequestBuilder.New().IsNull(nameof(TestClass.UInt64Val)).Build();
                Assert.Throws<ServiceQueryException>(() =>
                {
                    testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
                });
            }

            // UInt32
            serviceQuery = ServiceQueryRequestBuilder.New().IsNull(nameof(TestClass.UInt32Val)).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            });

            // UInt16
            serviceQuery = ServiceQueryRequestBuilder.New().IsNull(nameof(TestClass.UInt16Val)).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            });
        }

        [Fact]
        public async Task SyncNullIsNullStandardTest()
        {
            var sourceQueryable = await GetTestListAsync();
            if (sourceQueryable == null)
                return;
            IQueryable<T> testQueryable;
            IServiceQuery serviceQuery;
            List<T> result;

            // Boolean
            serviceQuery = ServiceQueryBuilder.New().IsNull(nameof(TestClass.NullBoolVal)).Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);

            // ByteArray
            //var tempbyteArray = new byte[1] { 1 };
            //serviceQuery = ServiceQueryBuilder.New().IsNull(nameof(TestClass.NullByteArrayVal), System.Text.Encoding.UTF8.GetString(tempbyteArray)).Build();
            //testQueryable = serviceQuery.GetQueryable(sourceQueryable);
            //result = await testQueryable.ToListAsync();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 4);
            //result = await testQueryable.ToListAsync();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 4);

            // Byte
            serviceQuery = ServiceQueryBuilder.New().IsNull(nameof(TestClass.NullByteVal)).Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);

            // Char
            serviceQuery = ServiceQueryBuilder.New().IsNull(nameof(TestClass.NullCharVal)).Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);

            if (ValidateDateTimeOffset)
            {
                // DateTimeOffset
                var tempDateTimeOffset = new T().GetDefault1Record(new T()).DateTimeOffsetVal;
                serviceQuery = ServiceQueryBuilder.New().IsNull(nameof(TestClass.NullDateTimeOffsetVal)).Build();
                testQueryable = serviceQuery.Apply(sourceQueryable);
                result = await testQueryable.ToListAsync();
                Assert.NotNull(result);
                Assert.True(result.Count == 4);
                result = await testQueryable.ToListAsync();
                Assert.NotNull(result);
                Assert.True(result.Count == 4);
            }

#if NET6_0_OR_GREATER
            // DateOnly
            var tempDateOnly = new T().GetDefault1Record(new T()).DateOnlyVal;
            serviceQuery = ServiceQueryBuilder.New().IsNull(nameof(TestClass.NullDateOnlyVal)).Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
#endif

            // DateTime
            var tempDateTime = new T().GetDefault1Record(new T()).DateTimeVal;
            serviceQuery = ServiceQueryBuilder.New().IsNull(nameof(TestClass.NullDateTimeVal)).Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);

            // Decimal
            serviceQuery = ServiceQueryBuilder.New().IsNull(nameof(TestClass.NullDecimalVal)).Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);

            // Double
            serviceQuery = ServiceQueryBuilder.New().IsNull(nameof(TestClass.NullDoubleVal)).Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);

            // Float
            serviceQuery = ServiceQueryBuilder.New().IsNull(nameof(TestClass.NullFloatVal)).Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);

            // Guid
            serviceQuery = ServiceQueryBuilder.New().IsNull(nameof(TestClass.NullGuidVal)).Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);

            // Int
            serviceQuery = ServiceQueryBuilder.New().IsNull(nameof(TestClass.NullIntVal)).Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);

            // Long
            serviceQuery = ServiceQueryBuilder.New().IsNull(nameof(TestClass.NullLongVal)).Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);

            // SByte
            serviceQuery = ServiceQueryBuilder.New().IsNull(nameof(TestClass.NullSByteVal)).Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);

            // Short
            serviceQuery = ServiceQueryBuilder.New().IsNull(nameof(TestClass.NullShortVal)).Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);

            // Single
            serviceQuery = ServiceQueryBuilder.New().IsNull(nameof(TestClass.NullSingleVal)).Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);

            // String
            serviceQuery = ServiceQueryBuilder.New().IsNull(nameof(TestClass.NullStringVal)).Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);

#if NET6_0_OR_GREATER
            if (ValidateTimeOnly)
            {
                // TimeOnly
                var tempTimeOnly = new T().GetDefault1Record(new T()).TimeOnlyVal;
                serviceQuery = ServiceQueryBuilder.New().IsNull(nameof(TestClass.NullTimeOnlyVal)).Build();
                testQueryable = serviceQuery.Apply(sourceQueryable);
                result = await testQueryable.ToListAsync();
                Assert.NotNull(result);
                Assert.True(result.Count == 4);
                result = await testQueryable.ToListAsync();
                Assert.NotNull(result);
                Assert.True(result.Count == 4);
            }
#endif

            if (ValidateTimeSpan)
            {
                // TimeSpan
                serviceQuery = ServiceQueryBuilder.New().IsNull(nameof(TestClass.NullTimeSpanVal)).Build();
                testQueryable = serviceQuery.Apply(sourceQueryable);
                result = await testQueryable.ToListAsync();
                Assert.NotNull(result);
                Assert.True(result.Count == 4);
                result = await testQueryable.ToListAsync();
                Assert.NotNull(result);
                Assert.True(result.Count == 4);
            }
#if NET7_0_OR_GREATER
            if (ValidateUInt128)
            {
                // UInt128
                serviceQuery = ServiceQueryBuilder.New().IsNull(nameof(TestClass.NullUInt128Val)).Build();
                testQueryable = serviceQuery.Apply(sourceQueryable);
                result = await testQueryable.ToListAsync();
                Assert.NotNull(result);
                Assert.True(result.Count == 4);
                result = await testQueryable.ToListAsync();
                Assert.NotNull(result);
                Assert.True(result.Count == 4);
            }
#endif
            if (ValidateUInt64)
            {
                // UInt64
                serviceQuery = ServiceQueryBuilder.New().IsNull(nameof(TestClass.NullUInt64Val)).Build();
                testQueryable = serviceQuery.Apply(sourceQueryable);
                result = await testQueryable.ToListAsync();
                Assert.NotNull(result);
                Assert.True(result.Count == 4);
                result = await testQueryable.ToListAsync();
                Assert.NotNull(result);
                Assert.True(result.Count == 4);
            }

            // UInt32
            serviceQuery = ServiceQueryBuilder.New().IsNull(nameof(TestClass.NullUInt32Val)).Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);

            // UInt16
            serviceQuery = ServiceQueryBuilder.New().IsNull(nameof(TestClass.NullUInt16Val)).Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
        }

        [Fact]
        public async Task SyncNullIsNullRequestTest()
        {
            var sourceQueryable = await GetTestListAsync();
            if (sourceQueryable == null)
                return;
            IQueryable<T> testQueryable;
            IServiceQueryRequest serviceQuery;
            List<T> result;

            // Boolean
            serviceQuery = ServiceQueryRequestBuilder.New().IsNull(nameof(TestClass.NullBoolVal)).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);

            // ByteArray
            //var tempbyteArray = new byte[1] { 1 };
            //serviceQuery = ServiceQueryRequestBuilder.New().IsNull(nameof(TestClass.NullByteArrayVal), System.Text.Encoding.UTF8.GetString(tempbyteArray)).Build();
            //testQueryable = serviceQuery.GetQueryable(sourceQueryable);
            //result = await testQueryable.ToListAsync();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 4);
            //result = await testQueryable.ToListAsync();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 4);

            // Byte
            serviceQuery = ServiceQueryRequestBuilder.New().IsNull(nameof(TestClass.NullByteVal)).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);

            // Char
            serviceQuery = ServiceQueryRequestBuilder.New().IsNull(nameof(TestClass.NullCharVal)).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);

            if (ValidateDateTimeOffset)
            {
                // DateTimeOffset
                var tempDateTimeOffset = new T().GetDefault1Record(new T()).DateTimeOffsetVal;
                serviceQuery = ServiceQueryRequestBuilder.New().IsNull(nameof(TestClass.NullDateTimeOffsetVal)).Build();
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
                result = await testQueryable.ToListAsync();
                Assert.NotNull(result);
                Assert.True(result.Count == 4);
                result = await testQueryable.ToListAsync();
                Assert.NotNull(result);
                Assert.True(result.Count == 4);
            }

#if NET6_0_OR_GREATER
            // DateOnly
            var tempDateOnly = new T().GetDefault1Record(new T()).DateOnlyVal;
            serviceQuery = ServiceQueryRequestBuilder.New().IsNull(nameof(TestClass.NullDateOnlyVal)).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
#endif

            // DateTime
            var tempDateTime = new T().GetDefault1Record(new T()).DateTimeVal;
            serviceQuery = ServiceQueryRequestBuilder.New().IsNull(nameof(TestClass.NullDateTimeVal)).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);

            // Decimal
            serviceQuery = ServiceQueryRequestBuilder.New().IsNull(nameof(TestClass.NullDecimalVal)).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);

            // Double
            serviceQuery = ServiceQueryRequestBuilder.New().IsNull(nameof(TestClass.NullDoubleVal)).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);

            // Float
            serviceQuery = ServiceQueryRequestBuilder.New().IsNull(nameof(TestClass.NullFloatVal)).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);

            // Guid
            serviceQuery = ServiceQueryRequestBuilder.New().IsNull(nameof(TestClass.NullGuidVal)).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);

            // Int
            serviceQuery = ServiceQueryRequestBuilder.New().IsNull(nameof(TestClass.NullIntVal)).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);

            // Long
            serviceQuery = ServiceQueryRequestBuilder.New().IsNull(nameof(TestClass.NullLongVal)).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);

            // SByte
            serviceQuery = ServiceQueryRequestBuilder.New().IsNull(nameof(TestClass.NullSByteVal)).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);

            // Short
            serviceQuery = ServiceQueryRequestBuilder.New().IsNull(nameof(TestClass.NullShortVal)).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);

            // Single
            serviceQuery = ServiceQueryRequestBuilder.New().IsNull(nameof(TestClass.NullSingleVal)).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);

            // String
            serviceQuery = ServiceQueryRequestBuilder.New().IsNull(nameof(TestClass.NullStringVal)).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);

#if NET6_0_OR_GREATER
            if (ValidateTimeOnly)
            {
                // TimeOnly
                var tempTimeOnly = new T().GetDefault1Record(new T()).TimeOnlyVal;
                serviceQuery = ServiceQueryRequestBuilder.New().IsNull(nameof(TestClass.NullTimeOnlyVal)).Build();
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
                result = await testQueryable.ToListAsync();
                Assert.NotNull(result);
                Assert.True(result.Count == 4);
                result = await testQueryable.ToListAsync();
                Assert.NotNull(result);
                Assert.True(result.Count == 4);
            }
#endif

            if (ValidateTimeSpan)
            {
                // TimeSpan
                serviceQuery = ServiceQueryRequestBuilder.New().IsNull(nameof(TestClass.NullTimeSpanVal)).Build();
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
                result = await testQueryable.ToListAsync();
                Assert.NotNull(result);
                Assert.True(result.Count == 4);
                result = await testQueryable.ToListAsync();
                Assert.NotNull(result);
                Assert.True(result.Count == 4);
            }
#if NET7_0_OR_GREATER
            if (ValidateUInt128)
            {
                // UInt128
                serviceQuery = ServiceQueryRequestBuilder.New().IsNull(nameof(TestClass.NullUInt128Val)).Build();
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
                result = await testQueryable.ToListAsync();
                Assert.NotNull(result);
                Assert.True(result.Count == 4);
                result = await testQueryable.ToListAsync();
                Assert.NotNull(result);
                Assert.True(result.Count == 4);
            }
#endif
            if (ValidateUInt64)
            {
                // UInt64
                serviceQuery = ServiceQueryRequestBuilder.New().IsNull(nameof(TestClass.NullUInt64Val)).Build();
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
                result = await testQueryable.ToListAsync();
                Assert.NotNull(result);
                Assert.True(result.Count == 4);
                result = await testQueryable.ToListAsync();
                Assert.NotNull(result);
                Assert.True(result.Count == 4);
            }

            // UInt32
            serviceQuery = ServiceQueryRequestBuilder.New().IsNull(nameof(TestClass.NullUInt32Val)).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);

            // UInt16
            serviceQuery = ServiceQueryRequestBuilder.New().IsNull(nameof(TestClass.NullUInt16Val)).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
        }

        [Fact]
        public async Task SyncFoundNullIsEqualRequestTest()
        {
            var sourceQueryable = await GetTestListAsync();
            if (sourceQueryable == null)
                return;
            IQueryable<T> testQueryable;
            IServiceQueryRequest serviceQuery;
            List<T> result;

            // Boolean
            serviceQuery = ServiceQueryRequestBuilder.New().IsNull(nameof(TestClass.NullBoolVal)).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);

            // ByteArray
            //var tempbyteArray = new byte[1] { 1 };
            //serviceQuery = ServiceQueryRequestBuilder.New().IsNull(nameof(TestClass.NullByteArrayVal), System.Text.Encoding.UTF8.GetString(tempbyteArray)).Build();
            //testQueryable = serviceQuery.GetQueryable(sourceQueryable);
            //result = await testQueryable.ToListAsync();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 4);
            //result = await testQueryable.ToListAsync();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 4);

            // Byte
            serviceQuery = ServiceQueryRequestBuilder.New().IsNull(nameof(TestClass.NullByteVal)).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);

            // Char
            serviceQuery = ServiceQueryRequestBuilder.New().IsNull(nameof(TestClass.NullCharVal)).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);

            if (ValidateDateTimeOffset)
            {
                // DateTimeOffset
                var tempDateTimeOffset = new T().GetDefault1Record(new T()).DateTimeOffsetVal;
                serviceQuery = ServiceQueryRequestBuilder.New().IsNull(nameof(TestClass.NullDateTimeOffsetVal)).Build();
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
                result = await testQueryable.ToListAsync();
                Assert.NotNull(result);
                Assert.True(result.Count == 4);
                result = await testQueryable.ToListAsync();
                Assert.NotNull(result);
                Assert.True(result.Count == 4);
            }

#if NET6_0_OR_GREATER
            // DateOnly
            var tempDateOnly = new T().GetDefault1Record(new T()).DateOnlyVal;
            serviceQuery = ServiceQueryRequestBuilder.New().IsNull(nameof(TestClass.NullDateOnlyVal)).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
#endif

            // DateTime
            var tempDateTime = new T().GetDefault1Record(new T()).DateTimeVal;
            serviceQuery = ServiceQueryRequestBuilder.New().IsNull(nameof(TestClass.NullDateTimeVal)).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);

            // Decimal
            serviceQuery = ServiceQueryRequestBuilder.New().IsNull(nameof(TestClass.NullDecimalVal)).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);

            // Double
            serviceQuery = ServiceQueryRequestBuilder.New().IsNull(nameof(TestClass.NullDoubleVal)).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);

            // Float
            serviceQuery = ServiceQueryRequestBuilder.New().IsNull(nameof(TestClass.NullFloatVal)).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);

            // Guid
            serviceQuery = ServiceQueryRequestBuilder.New().IsNull(nameof(TestClass.NullGuidVal)).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);

            // Int
            serviceQuery = ServiceQueryRequestBuilder.New().IsNull(nameof(TestClass.NullIntVal)).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);

            // Long
            serviceQuery = ServiceQueryRequestBuilder.New().IsNull(nameof(TestClass.NullLongVal)).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);

            // SByte
            serviceQuery = ServiceQueryRequestBuilder.New().IsNull(nameof(TestClass.NullSByteVal)).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);

            // Short
            serviceQuery = ServiceQueryRequestBuilder.New().IsNull(nameof(TestClass.NullShortVal)).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);

            // Single
            serviceQuery = ServiceQueryRequestBuilder.New().IsNull(nameof(TestClass.NullSingleVal)).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);

            // String
            serviceQuery = ServiceQueryRequestBuilder.New().IsNull(nameof(TestClass.NullStringVal)).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);

#if NET6_0_OR_GREATER
            if (ValidateTimeOnly)
            {
                // TimeOnly
                var tempTimeOnly = new T().GetDefault1Record(new T()).TimeOnlyVal;
                serviceQuery = ServiceQueryRequestBuilder.New().IsNull(nameof(TestClass.NullTimeOnlyVal)).Build();
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
                result = await testQueryable.ToListAsync();
                Assert.NotNull(result);
                Assert.True(result.Count == 4);
                result = await testQueryable.ToListAsync();
                Assert.NotNull(result);
                Assert.True(result.Count == 4);
            }
#endif

            if (ValidateTimeSpan)
            {
                // TimeSpan
                serviceQuery = ServiceQueryRequestBuilder.New().IsNull(nameof(TestClass.NullTimeSpanVal)).Build();
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
                result = await testQueryable.ToListAsync();
                Assert.NotNull(result);
                Assert.True(result.Count == 4);
                result = await testQueryable.ToListAsync();
                Assert.NotNull(result);
                Assert.True(result.Count == 4);
            }
#if NET7_0_OR_GREATER
            if (ValidateUInt128)
            {
                // UInt128
                serviceQuery = ServiceQueryRequestBuilder.New().IsNull(nameof(TestClass.NullUInt128Val)).Build();
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
                result = await testQueryable.ToListAsync();
                Assert.NotNull(result);
                Assert.True(result.Count == 4);
                result = await testQueryable.ToListAsync();
                Assert.NotNull(result);
                Assert.True(result.Count == 4);
            }
#endif
            if (ValidateUInt64)
            {
                // UInt64
                serviceQuery = ServiceQueryRequestBuilder.New().IsNull(nameof(TestClass.NullUInt64Val)).Build();
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
                result = await testQueryable.ToListAsync();
                Assert.NotNull(result);
                Assert.True(result.Count == 4);
                result = await testQueryable.ToListAsync();
                Assert.NotNull(result);
                Assert.True(result.Count == 4);
            }

            // UInt32
            serviceQuery = ServiceQueryRequestBuilder.New().IsNull(nameof(TestClass.NullUInt32Val)).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);

            // UInt16
            serviceQuery = ServiceQueryRequestBuilder.New().IsNull(nameof(TestClass.NullUInt16Val)).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
        }

        [Fact]
        public async Task SyncNullCopyIsNullStandardTest()
        {
            var sourceQueryable = await GetTestNullCopyListAsync();
            if (sourceQueryable == null)
                return;
            IQueryable<T> testQueryable;
            IServiceQuery serviceQuery;
            List<T> result;

            // Boolean
            serviceQuery = ServiceQueryBuilder.New().IsNull(nameof(TestClass.NullBoolVal)).Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);

            // ByteArray
            //var tempbyteArray = new byte[1] { 1 };
            //serviceQuery = ServiceQueryBuilder.New().IsNull(nameof(TestClass.NullByteArrayVal), System.Text.Encoding.UTF8.GetString(tempbyteArray)).Build();
            //testQueryable = serviceQuery.GetQueryable(sourceQueryable);
            //result = await testQueryable.ToListAsync();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 0);
            //result = await testQueryable.ToListAsync();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 0);

            // Byte
            serviceQuery = ServiceQueryBuilder.New().IsNull(nameof(TestClass.NullByteVal)).Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);

            // Char
            serviceQuery = ServiceQueryBuilder.New().IsNull(nameof(TestClass.NullCharVal)).Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);

            if (ValidateDateTimeOffset)
            {
                // DateTimeOffset
                var tempDateTimeOffset = new T().GetDefault1Record(new T()).DateTimeOffsetVal;
                serviceQuery = ServiceQueryBuilder.New().IsNull(nameof(TestClass.NullDateTimeOffsetVal)).Build();
                testQueryable = serviceQuery.Apply(sourceQueryable);
                result = await testQueryable.ToListAsync();
                Assert.NotNull(result);
                Assert.True(result.Count == 0);
                result = await testQueryable.ToListAsync();
                Assert.NotNull(result);
                Assert.True(result.Count == 0);
            }

#if NET6_0_OR_GREATER
            // DateOnly
            var tempDateOnly = new T().GetDefault1Record(new T()).DateOnlyVal;
            serviceQuery = ServiceQueryBuilder.New().IsNull(nameof(TestClass.NullDateOnlyVal)).Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
#endif

            // DateTime
            var tempDateTime = new T().GetDefault1Record(new T()).DateTimeVal;
            serviceQuery = ServiceQueryBuilder.New().IsNull(nameof(TestClass.NullDateTimeVal)).Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);

            // Decimal
            serviceQuery = ServiceQueryBuilder.New().IsNull(nameof(TestClass.NullDecimalVal)).Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);

            // Double
            serviceQuery = ServiceQueryBuilder.New().IsNull(nameof(TestClass.NullDoubleVal)).Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);

            // Float
            serviceQuery = ServiceQueryBuilder.New().IsNull(nameof(TestClass.NullFloatVal)).Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);

            // Guid
            serviceQuery = ServiceQueryBuilder.New().IsNull(nameof(TestClass.NullGuidVal)).Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);

            // Int
            serviceQuery = ServiceQueryBuilder.New().IsNull(nameof(TestClass.NullIntVal)).Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);

            // Long
            serviceQuery = ServiceQueryBuilder.New().IsNull(nameof(TestClass.NullLongVal)).Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);

            // SByte
            serviceQuery = ServiceQueryBuilder.New().IsNull(nameof(TestClass.NullSByteVal)).Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);

            // Short
            serviceQuery = ServiceQueryBuilder.New().IsNull(nameof(TestClass.NullShortVal)).Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);

            // Single
            serviceQuery = ServiceQueryBuilder.New().IsNull(nameof(TestClass.NullSingleVal)).Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);

            // String
            serviceQuery = ServiceQueryBuilder.New().IsNull(nameof(TestClass.NullStringVal)).Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);

#if NET6_0_OR_GREATER
            if (ValidateTimeOnly)
            {
                // TimeOnly
                var tempTimeOnly = new T().GetDefault1Record(new T()).TimeOnlyVal;
                serviceQuery = ServiceQueryBuilder.New().IsNull(nameof(TestClass.NullTimeOnlyVal)).Build();
                testQueryable = serviceQuery.Apply(sourceQueryable);
                result = await testQueryable.ToListAsync();
                Assert.NotNull(result);
                Assert.True(result.Count == 0);
                result = await testQueryable.ToListAsync();
                Assert.NotNull(result);
                Assert.True(result.Count == 0);
            }
#endif

            if (ValidateTimeSpan)
            {
                // TimeSpan
                serviceQuery = ServiceQueryBuilder.New().IsNull(nameof(TestClass.NullTimeSpanVal)).Build();
                testQueryable = serviceQuery.Apply(sourceQueryable);
                result = await testQueryable.ToListAsync();
                Assert.NotNull(result);
                Assert.True(result.Count == 0);
                result = await testQueryable.ToListAsync();
                Assert.NotNull(result);
                Assert.True(result.Count == 0);
            }
#if NET7_0_OR_GREATER
            if (ValidateUInt128)
            {
                // UInt128
                serviceQuery = ServiceQueryBuilder.New().IsNull(nameof(TestClass.NullUInt128Val)).Build();
                testQueryable = serviceQuery.Apply(sourceQueryable);
                result = await testQueryable.ToListAsync();
                Assert.NotNull(result);
                Assert.True(result.Count == 0);
                result = await testQueryable.ToListAsync();
                Assert.NotNull(result);
                Assert.True(result.Count == 0);
            }
#endif
            if (ValidateUInt64)
            {
                // UInt64
                serviceQuery = ServiceQueryBuilder.New().IsNull(nameof(TestClass.NullUInt64Val)).Build();
                testQueryable = serviceQuery.Apply(sourceQueryable);
                result = await testQueryable.ToListAsync();
                Assert.NotNull(result);
                Assert.True(result.Count == 0);
                result = await testQueryable.ToListAsync();
                Assert.NotNull(result);
                Assert.True(result.Count == 0);
            }

            // UInt32
            serviceQuery = ServiceQueryBuilder.New().IsNull(nameof(TestClass.NullUInt32Val)).Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);

            // UInt16
            serviceQuery = ServiceQueryBuilder.New().IsNull(nameof(TestClass.NullUInt16Val)).Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
        }

        [Fact]
        public async Task SyncNullCopyIsNullRequestTest()
        {
            var sourceQueryable = await GetTestNullCopyListAsync();
            if (sourceQueryable == null)
                return;
            IQueryable<T> testQueryable;
            IServiceQueryRequest serviceQuery;
            List<T> result;

            // Boolean
            serviceQuery = ServiceQueryRequestBuilder.New().IsNull(nameof(TestClass.NullBoolVal)).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);

            // ByteArray
            //var tempbyteArray = new byte[1] { 1 };
            //serviceQuery = ServiceQueryRequestBuilder.New().IsNull(nameof(TestClass.NullByteArrayVal), System.Text.Encoding.UTF8.GetString(tempbyteArray)).Build();
            //testQueryable = serviceQuery.GetQueryable(sourceQueryable);
            //result = await testQueryable.ToListAsync();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 0);
            //result = await testQueryable.ToListAsync();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 0);

            // Byte
            serviceQuery = ServiceQueryRequestBuilder.New().IsNull(nameof(TestClass.NullByteVal)).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);

            // Char
            serviceQuery = ServiceQueryRequestBuilder.New().IsNull(nameof(TestClass.NullCharVal)).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);

            if (ValidateDateTimeOffset)
            {
                // DateTimeOffset
                var tempDateTimeOffset = new T().GetDefault1Record(new T()).DateTimeOffsetVal;
                serviceQuery = ServiceQueryRequestBuilder.New().IsNull(nameof(TestClass.NullDateTimeOffsetVal)).Build();
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
                result = await testQueryable.ToListAsync();
                Assert.NotNull(result);
                Assert.True(result.Count == 0);
                result = await testQueryable.ToListAsync();
                Assert.NotNull(result);
                Assert.True(result.Count == 0);
            }

#if NET6_0_OR_GREATER
            // DateOnly
            var tempDateOnly = new T().GetDefault1Record(new T()).DateOnlyVal;
            serviceQuery = ServiceQueryRequestBuilder.New().IsNull(nameof(TestClass.NullDateOnlyVal)).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
#endif

            // DateTime
            var tempDateTime = new T().GetDefault1Record(new T()).DateTimeVal;
            serviceQuery = ServiceQueryRequestBuilder.New().IsNull(nameof(TestClass.NullDateTimeVal)).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);

            // Decimal
            serviceQuery = ServiceQueryRequestBuilder.New().IsNull(nameof(TestClass.NullDecimalVal)).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);

            // Double
            serviceQuery = ServiceQueryRequestBuilder.New().IsNull(nameof(TestClass.NullDoubleVal)).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);

            // Float
            serviceQuery = ServiceQueryRequestBuilder.New().IsNull(nameof(TestClass.NullFloatVal)).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);

            // Guid
            serviceQuery = ServiceQueryRequestBuilder.New().IsNull(nameof(TestClass.NullGuidVal)).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);

            // Int
            serviceQuery = ServiceQueryRequestBuilder.New().IsNull(nameof(TestClass.NullIntVal)).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);

            // Long
            serviceQuery = ServiceQueryRequestBuilder.New().IsNull(nameof(TestClass.NullLongVal)).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);

            // SByte
            serviceQuery = ServiceQueryRequestBuilder.New().IsNull(nameof(TestClass.NullSByteVal)).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);

            // Short
            serviceQuery = ServiceQueryRequestBuilder.New().IsNull(nameof(TestClass.NullShortVal)).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);

            // Single
            serviceQuery = ServiceQueryRequestBuilder.New().IsNull(nameof(TestClass.NullSingleVal)).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);

            // String
            serviceQuery = ServiceQueryRequestBuilder.New().IsNull(nameof(TestClass.NullStringVal)).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);

#if NET6_0_OR_GREATER
            if (ValidateTimeOnly)
            {
                // TimeOnly
                var tempTimeOnly = new T().GetDefault1Record(new T()).TimeOnlyVal;
                serviceQuery = ServiceQueryRequestBuilder.New().IsNull(nameof(TestClass.NullTimeOnlyVal)).Build();
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
                result = await testQueryable.ToListAsync();
                Assert.NotNull(result);
                Assert.True(result.Count == 0);
                result = await testQueryable.ToListAsync();
                Assert.NotNull(result);
                Assert.True(result.Count == 0);
            }
#endif

            if (ValidateTimeSpan)
            {
                // TimeSpan
                serviceQuery = ServiceQueryRequestBuilder.New().IsNull(nameof(TestClass.NullTimeSpanVal)).Build();
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
                result = await testQueryable.ToListAsync();
                Assert.NotNull(result);
                Assert.True(result.Count == 0);
                result = await testQueryable.ToListAsync();
                Assert.NotNull(result);
                Assert.True(result.Count == 0);
            }
#if NET7_0_OR_GREATER
            if (ValidateUInt128)
            {
                // UInt128
                serviceQuery = ServiceQueryRequestBuilder.New().IsNull(nameof(TestClass.NullUInt128Val)).Build();
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
                result = await testQueryable.ToListAsync();
                Assert.NotNull(result);
                Assert.True(result.Count == 0);
                result = await testQueryable.ToListAsync();
                Assert.NotNull(result);
                Assert.True(result.Count == 0);
            }
#endif
            if (ValidateUInt64)
            {
                // UInt64
                serviceQuery = ServiceQueryRequestBuilder.New().IsNull(nameof(TestClass.NullUInt64Val)).Build();
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
                result = await testQueryable.ToListAsync();
                Assert.NotNull(result);
                Assert.True(result.Count == 0);
                result = await testQueryable.ToListAsync();
                Assert.NotNull(result);
                Assert.True(result.Count == 0);
            }

            // UInt32
            serviceQuery = ServiceQueryRequestBuilder.New().IsNull(nameof(TestClass.NullUInt32Val)).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);

            // UInt16
            serviceQuery = ServiceQueryRequestBuilder.New().IsNull(nameof(TestClass.NullUInt16Val)).Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
        }
    }
}
using MongoDB.Driver;

namespace ServiceQuery.Xunit
{
    public class AggregateMaximumTestsAsyncMongoDb : AggregateMaximumTestsAsyncMongoDb<TestClass>
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

    public abstract class AggregateMaximumTestsAsyncMongoDb<T> : BaseTest<T> where T : class
    {
        [Fact]
        public async Task MaximumStandardTest()
        {
            var sourceQueryable = await GetTestListAsync();
            if (sourceQueryable == null)
                return;
            IServiceQuery serviceQuery;
            double? result;

            // Boolean
            serviceQuery = ServiceQueryBuilder.New().Maximum(nameof(TestClass.BoolVal)).Build();
            await Assert.ThrowsAsync<ServiceQueryException>(async () =>
            {
                result = await serviceQuery.MongoDbExecuteAggregateAsync<T>(sourceQueryable);
            });
            await Assert.ThrowsAsync<ServiceQueryException>(async () =>
            {
                result = await serviceQuery.MongoDbExecuteAggregateAsync<T>(sourceQueryable);
            });

            // ByteArray
            var tempbyteArray = new byte[1] { 1 };
            serviceQuery = ServiceQueryBuilder.New().Maximum(nameof(TestClass.ByteArrayVal)).Build();
            await Assert.ThrowsAsync<ServiceQueryException>(async () =>
            {
                result = await serviceQuery.MongoDbExecuteAggregateAsync<T>(sourceQueryable);
            });

            // Byte
            serviceQuery = ServiceQueryBuilder.New().Maximum(nameof(TestClass.ByteVal)).Build();
            await Assert.ThrowsAsync<ServiceQueryException>(async () =>
            {
                result = await serviceQuery.MongoDbExecuteAggregateAsync<T>(sourceQueryable);
            });

            // Char
            serviceQuery = ServiceQueryBuilder.New().Maximum(nameof(TestClass.CharVal)).Build();
            await Assert.ThrowsAsync<ServiceQueryException>(async () =>
            {
                result = await serviceQuery.MongoDbExecuteAggregateAsync<T>(sourceQueryable);
            });

            if (ValidateDateTimeOffset)
            {
                // DateTimeOffset
                serviceQuery = ServiceQueryBuilder.New().Maximum(nameof(TestClass.DateTimeOffsetVal)).Build();
                await Assert.ThrowsAsync<ServiceQueryException>(async () =>
                {
                    result = await serviceQuery.MongoDbExecuteAggregateAsync<T>(sourceQueryable);
                });
            }

#if NET6_0_OR_GREATER

            // DateOnly
            serviceQuery = ServiceQueryBuilder.New().Maximum(nameof(TestClass.DateOnlyVal)).Build();
            await Assert.ThrowsAsync<ServiceQueryException>(async () =>
            {
                result = await serviceQuery.MongoDbExecuteAggregateAsync<T>(sourceQueryable);
            });
#endif

            // DateTime
            serviceQuery = ServiceQueryBuilder.New().Maximum(nameof(TestClass.DateTimeVal)).Build();
            await Assert.ThrowsAsync<ServiceQueryException>(async () =>
            {
                result = await serviceQuery.MongoDbExecuteAggregateAsync<T>(sourceQueryable);
            });

            // Decimal
            if (ValidateDecimal)
            {
                serviceQuery = ServiceQueryBuilder.New().Maximum(nameof(TestClass.DecimalVal)).Build();
                result = await serviceQuery.MongoDbExecuteAggregateAsync<T>(sourceQueryable);
                Assert.True(result == 3);
            }

            // Double
            serviceQuery = ServiceQueryBuilder.New().Maximum(nameof(TestClass.DoubleVal)).Build();
            result = await serviceQuery.MongoDbExecuteAggregateAsync<T>(sourceQueryable);
            Assert.True(result == 3);
            result = await serviceQuery.MongoDbExecuteAggregateAsync<T>(sourceQueryable);
            Assert.True(result == 3);

            // Float
            serviceQuery = ServiceQueryBuilder.New().Maximum(nameof(TestClass.FloatVal)).Build();
            result = await serviceQuery.MongoDbExecuteAggregateAsync<T>(sourceQueryable);
            Assert.True(result == 3);

            // Guid
            serviceQuery = ServiceQueryBuilder.New().Maximum(nameof(TestClass.GuidVal)).Build();
            await Assert.ThrowsAsync<ServiceQueryException>(async () =>
            {
                result = await serviceQuery.MongoDbExecuteAggregateAsync<T>(sourceQueryable);
            });

            // Int
            serviceQuery = ServiceQueryBuilder.New().Maximum(nameof(TestClass.IntVal)).Build();
            result = await serviceQuery.MongoDbExecuteAggregateAsync<T>(sourceQueryable);
            Assert.True(result == 3);

            // Long
            serviceQuery = ServiceQueryBuilder.New().Maximum(nameof(TestClass.LongVal)).Build();
            result = await serviceQuery.MongoDbExecuteAggregateAsync<T>(sourceQueryable);
            Assert.True(result == 3);

            // SByte
            serviceQuery = ServiceQueryBuilder.New().Maximum(nameof(TestClass.SByteVal)).Build();
            result = await serviceQuery.MongoDbExecuteAggregateAsync<T>(sourceQueryable);
            Assert.True(result == 3);

            // Short
            serviceQuery = ServiceQueryBuilder.New().Maximum(nameof(TestClass.ShortVal)).Build();
            result = await serviceQuery.MongoDbExecuteAggregateAsync<T>(sourceQueryable);
            Assert.True(result == 3);

            // Single
            serviceQuery = ServiceQueryBuilder.New().Maximum(nameof(TestClass.SingleVal)).Build();
            result = await serviceQuery.MongoDbExecuteAggregateAsync<T>(sourceQueryable);
            Assert.True(result == 3);

            // String
            serviceQuery = ServiceQueryBuilder.New().Maximum(nameof(TestClass.StringVal)).Build();
            await Assert.ThrowsAsync<ServiceQueryException>(async () =>
            {
                result = await serviceQuery.MongoDbExecuteAggregateAsync<T>(sourceQueryable);
            });

#if NET6_0_OR_GREATER

            if (ValidateTimeOnly)
            {
                // TimeOnly
                serviceQuery = ServiceQueryBuilder.New().Maximum(nameof(TestClass.TimeOnlyVal)).Build();
                await Assert.ThrowsAsync<ServiceQueryException>(async () =>
                {
                    result = await serviceQuery.MongoDbExecuteAggregateAsync<T>(sourceQueryable);
                });
            }
#endif

            if (ValidateTimeSpan)
            {
                // TimeSpan
                serviceQuery = ServiceQueryBuilder.New().Maximum(nameof(TestClass.TimeSpanVal)).Build();
                await Assert.ThrowsAsync<ServiceQueryException>(async () =>
                {
                    result = await serviceQuery.MongoDbExecuteAggregateAsync<T>(sourceQueryable);
                });
            }

#if NET7_0_OR_GREATER
            if (ValidateUInt128)
            {
                // UInt128
                serviceQuery = ServiceQueryBuilder.New().Maximum(nameof(TestClass.UInt128Val)).Build();
                result = await serviceQuery.MongoDbExecuteAggregateAsync<T>(sourceQueryable);
                Assert.True(result == 3);
            }
#endif

            if (ValidateUInt64)
            {
                // UInt64
                serviceQuery = ServiceQueryBuilder.New().Maximum(nameof(TestClass.UInt64Val)).Build();
                result = await serviceQuery.MongoDbExecuteAggregateAsync<T>(sourceQueryable);
                Assert.True(result == 3);
            }

            // UInt32
            serviceQuery = ServiceQueryBuilder.New().Maximum(nameof(TestClass.UInt32Val)).Build();
            result = await serviceQuery.MongoDbExecuteAggregateAsync<T>(sourceQueryable);
            Assert.True(result == 3);

            // UInt16
            serviceQuery = ServiceQueryBuilder.New().Maximum(nameof(TestClass.UInt16Val)).Build();
            result = await serviceQuery.MongoDbExecuteAggregateAsync<T>(sourceQueryable);
            Assert.True(result == 3);
        }

        [Fact]
        public async Task MaximumRequestTest()
        {
            var sourceQueryable = await GetTestListAsync();
            if (sourceQueryable == null)
                return;
            IServiceQueryRequest serviceQuery;
            double? result;

            // Boolean
            serviceQuery = ServiceQueryRequestBuilder.New().Maximum(nameof(TestClass.BoolVal)).Build();
            await Assert.ThrowsAsync<ServiceQueryException>(async () =>
            {
                result = await serviceQuery.GetServiceQuery().MongoDbExecuteAggregateAsync<T>(sourceQueryable);
            });
            await Assert.ThrowsAsync<ServiceQueryException>(async () =>
            {
                result = await serviceQuery.GetServiceQuery().MongoDbExecuteAggregateAsync<T>(sourceQueryable);
            });

            // ByteArray
            var tempbyteArray = new byte[1] { 1 };
            serviceQuery = ServiceQueryRequestBuilder.New().Maximum(nameof(TestClass.ByteArrayVal)).Build();
            await Assert.ThrowsAsync<ServiceQueryException>(async () =>
            {
                result = await serviceQuery.GetServiceQuery().MongoDbExecuteAggregateAsync<T>(sourceQueryable);
            });

            // Byte
            serviceQuery = ServiceQueryRequestBuilder.New().Maximum(nameof(TestClass.ByteVal)).Build();
            await Assert.ThrowsAsync<ServiceQueryException>(async () =>
            {
                result = await serviceQuery.GetServiceQuery().MongoDbExecuteAggregateAsync<T>(sourceQueryable);
            });

            // Char
            serviceQuery = ServiceQueryRequestBuilder.New().Maximum(nameof(TestClass.CharVal)).Build();
            await Assert.ThrowsAsync<ServiceQueryException>(async () =>
            {
                result = await serviceQuery.GetServiceQuery().MongoDbExecuteAggregateAsync<T>(sourceQueryable);
            });

            if (ValidateDateTimeOffset)
            {
                // DateTimeOffset
                serviceQuery = ServiceQueryRequestBuilder.New().Maximum(nameof(TestClass.DateTimeOffsetVal)).Build();
                await Assert.ThrowsAsync<ServiceQueryException>(async () =>
                {
                    result = await serviceQuery.GetServiceQuery().MongoDbExecuteAggregateAsync<T>(sourceQueryable);
                });
            }

#if NET6_0_OR_GREATER
            // DateOnly
            serviceQuery = ServiceQueryRequestBuilder.New().Maximum(nameof(TestClass.DateOnlyVal)).Build();
            await Assert.ThrowsAsync<ServiceQueryException>(async () =>
            {
                result = await serviceQuery.GetServiceQuery().MongoDbExecuteAggregateAsync<T>(sourceQueryable);
            });
#endif

            // DateTime
            serviceQuery = ServiceQueryRequestBuilder.New().Maximum(nameof(TestClass.DateTimeVal)).Build();
            await Assert.ThrowsAsync<ServiceQueryException>(async () =>
            {
                result = await serviceQuery.GetServiceQuery().MongoDbExecuteAggregateAsync<T>(sourceQueryable);
            });

            // Decimal
            if (ValidateDecimal)
            {
                serviceQuery = ServiceQueryRequestBuilder.New().Maximum(nameof(TestClass.DecimalVal)).Build();
                result = await serviceQuery.GetServiceQuery().MongoDbExecuteAggregateAsync<T>(sourceQueryable);
                Assert.True(result == 3);
            }

            // Double
            serviceQuery = ServiceQueryRequestBuilder.New().Maximum(nameof(TestClass.DoubleVal)).Build();
            result = await serviceQuery.GetServiceQuery().MongoDbExecuteAggregateAsync<T>(sourceQueryable);
            Assert.True(result == 3);
            result = await serviceQuery.GetServiceQuery().MongoDbExecuteAggregateAsync<T>(sourceQueryable);
            Assert.True(result == 3);

            // Float
            serviceQuery = ServiceQueryRequestBuilder.New().Maximum(nameof(TestClass.FloatVal)).Build();
            result = await serviceQuery.GetServiceQuery().MongoDbExecuteAggregateAsync<T>(sourceQueryable);
            Assert.True(result == 3);

            // Guid
            serviceQuery = ServiceQueryRequestBuilder.New().Maximum(nameof(TestClass.GuidVal)).Build();
            await Assert.ThrowsAsync<ServiceQueryException>(async () =>
            {
                result = await serviceQuery.GetServiceQuery().MongoDbExecuteAggregateAsync<T>(sourceQueryable);
            });

            // Int
            serviceQuery = ServiceQueryRequestBuilder.New().Maximum(nameof(TestClass.IntVal)).Build();
            result = await serviceQuery.GetServiceQuery().MongoDbExecuteAggregateAsync<T>(sourceQueryable);
            Assert.True(result == 3);

            // Long
            serviceQuery = ServiceQueryRequestBuilder.New().Maximum(nameof(TestClass.LongVal)).Build();
            result = await serviceQuery.GetServiceQuery().MongoDbExecuteAggregateAsync<T>(sourceQueryable);
            Assert.True(result == 3);

            // SByte
            serviceQuery = ServiceQueryRequestBuilder.New().Maximum(nameof(TestClass.SByteVal)).Build();
            result = await serviceQuery.GetServiceQuery().MongoDbExecuteAggregateAsync<T>(sourceQueryable);
            Assert.True(result == 3);

            // Short
            serviceQuery = ServiceQueryRequestBuilder.New().Maximum(nameof(TestClass.ShortVal)).Build();
            result = await serviceQuery.GetServiceQuery().MongoDbExecuteAggregateAsync<T>(sourceQueryable);
            Assert.True(result == 3);

            // Single
            serviceQuery = ServiceQueryRequestBuilder.New().Maximum(nameof(TestClass.SingleVal)).Build();
            result = await serviceQuery.GetServiceQuery().MongoDbExecuteAggregateAsync<T>(sourceQueryable);
            Assert.True(result == 3);

            // String
            serviceQuery = ServiceQueryRequestBuilder.New().Maximum(nameof(TestClass.StringVal)).Build();
            await Assert.ThrowsAsync<ServiceQueryException>(async () =>
            {
                result = await serviceQuery.GetServiceQuery().MongoDbExecuteAggregateAsync<T>(sourceQueryable);
            });

#if NET6_0_OR_GREATER
            if (ValidateTimeOnly)
            {
                // TimeOnly
                serviceQuery = ServiceQueryRequestBuilder.New().Maximum(nameof(TestClass.TimeOnlyVal)).Build();
                await Assert.ThrowsAsync<ServiceQueryException>(async () =>
                {
                    result = await serviceQuery.GetServiceQuery().MongoDbExecuteAggregateAsync<T>(sourceQueryable);
                });
            }
#endif

            if (ValidateTimeSpan)
            {
                // TimeSpan
                serviceQuery = ServiceQueryRequestBuilder.New().Maximum(nameof(TestClass.TimeSpanVal)).Build();
                await Assert.ThrowsAsync<ServiceQueryException>(async () =>
                {
                    result = await serviceQuery.GetServiceQuery().MongoDbExecuteAggregateAsync<T>(sourceQueryable);
                });
            }

#if NET7_0_OR_GREATER
            if (ValidateUInt128)
            {
                // UInt128
                serviceQuery = ServiceQueryRequestBuilder.New().Maximum(nameof(TestClass.UInt128Val)).Build();
                result = await serviceQuery.GetServiceQuery().MongoDbExecuteAggregateAsync<T>(sourceQueryable);
                Assert.True(result == 3);
            }
#endif

            if (ValidateUInt64)
            {
                // UInt64
                serviceQuery = ServiceQueryRequestBuilder.New().Maximum(nameof(TestClass.UInt64Val)).Build();
                result = await serviceQuery.GetServiceQuery().MongoDbExecuteAggregateAsync<T>(sourceQueryable);
                Assert.True(result == 3);
            }

            // UInt32
            serviceQuery = ServiceQueryRequestBuilder.New().Maximum(nameof(TestClass.UInt32Val)).Build();
            result = await serviceQuery.GetServiceQuery().MongoDbExecuteAggregateAsync<T>(sourceQueryable);
            Assert.True(result == 3);

            // UInt16
            serviceQuery = ServiceQueryRequestBuilder.New().Maximum(nameof(TestClass.UInt16Val)).Build();
            result = await serviceQuery.GetServiceQuery().MongoDbExecuteAggregateAsync<T>(sourceQueryable);
            Assert.True(result == 3);
        }

        [Fact]
        public async Task NullMaximumStandardTest()
        {
            var sourceQueryable = await GetTestListAsync();
            if (sourceQueryable == null)
                return;
            IServiceQuery serviceQuery;
            double? result;

            // Boolean
            serviceQuery = ServiceQueryBuilder.New().Maximum(nameof(TestClass.NullBoolVal)).Build();
            await Assert.ThrowsAsync<ServiceQueryException>(async () =>
            {
                result = await serviceQuery.MongoDbExecuteAggregateAsync<T>(sourceQueryable);
            });
            await Assert.ThrowsAsync<ServiceQueryException>(async () =>
            {
                result = await serviceQuery.MongoDbExecuteAggregateAsync<T>(sourceQueryable);
            });

            // ByteArray
            var tempbyteArray = new byte[1] { 1 };
            serviceQuery = ServiceQueryBuilder.New().Maximum(nameof(TestClass.NullByteArrayVal)).Build();
            await Assert.ThrowsAsync<ServiceQueryException>(async () =>
            {
                result = await serviceQuery.MongoDbExecuteAggregateAsync<T>(sourceQueryable);
            });

            // Byte
            serviceQuery = ServiceQueryBuilder.New().Maximum(nameof(TestClass.NullByteVal)).Build();
            await Assert.ThrowsAsync<ServiceQueryException>(async () =>
            {
                result = await serviceQuery.MongoDbExecuteAggregateAsync<T>(sourceQueryable);
            });

            // Char
            serviceQuery = ServiceQueryBuilder.New().Maximum(nameof(TestClass.NullCharVal)).Build();
            await Assert.ThrowsAsync<ServiceQueryException>(async () =>
            {
                result = await serviceQuery.MongoDbExecuteAggregateAsync<T>(sourceQueryable);
            });

            if (ValidateDateTimeOffset)
            {
                // DateTimeOffset
                serviceQuery = ServiceQueryBuilder.New().Maximum(nameof(TestClass.NullDateTimeOffsetVal)).Build();
                await Assert.ThrowsAsync<ServiceQueryException>(async () =>
                {
                    result = await serviceQuery.MongoDbExecuteAggregateAsync<T>(sourceQueryable);
                });
            }

#if NET6_0_OR_GREATER
            // DateOnly
            serviceQuery = ServiceQueryBuilder.New().Maximum(nameof(TestClass.NullDateOnlyVal)).Build();
            await Assert.ThrowsAsync<ServiceQueryException>(async () =>
            {
                result = await serviceQuery.MongoDbExecuteAggregateAsync<T>(sourceQueryable);
            });
#endif

            // DateTime
            serviceQuery = ServiceQueryBuilder.New().Maximum(nameof(TestClass.NullDateTimeVal)).Build();
            await Assert.ThrowsAsync<ServiceQueryException>(async () =>
            {
                result = await serviceQuery.MongoDbExecuteAggregateAsync<T>(sourceQueryable);
            });

            // Decimal
            if (ValidateDecimal)
            {
                serviceQuery = ServiceQueryBuilder.New().Maximum(nameof(TestClass.NullDecimalVal)).Build();
                result = await serviceQuery.MongoDbExecuteAggregateAsync<T>(sourceQueryable);
                Assert.True(result == null);
            }

            // Double
            serviceQuery = ServiceQueryBuilder.New().Maximum(nameof(TestClass.NullDoubleVal)).Build();
            result = await serviceQuery.MongoDbExecuteAggregateAsync<T>(sourceQueryable);
            Assert.True(result == null);
            result = await serviceQuery.MongoDbExecuteAggregateAsync<T>(sourceQueryable);
            Assert.True(result == null);

            // Float
            serviceQuery = ServiceQueryBuilder.New().Maximum(nameof(TestClass.NullFloatVal)).Build();
            result = await serviceQuery.MongoDbExecuteAggregateAsync<T>(sourceQueryable);
            Assert.True(result == null);

            // Guid
            serviceQuery = ServiceQueryBuilder.New().Maximum(nameof(TestClass.NullGuidVal)).Build();
            await Assert.ThrowsAsync<ServiceQueryException>(async () =>
            {
                result = await serviceQuery.MongoDbExecuteAggregateAsync<T>(sourceQueryable);
            });

            // Int
            serviceQuery = ServiceQueryBuilder.New().Maximum(nameof(TestClass.NullIntVal)).Build();
            result = await serviceQuery.MongoDbExecuteAggregateAsync<T>(sourceQueryable);
            Assert.True(result == null);

            // Long
            serviceQuery = ServiceQueryBuilder.New().Maximum(nameof(TestClass.NullLongVal)).Build();
            result = await serviceQuery.MongoDbExecuteAggregateAsync<T>(sourceQueryable);
            Assert.True(result == null);

            // SByte
            serviceQuery = ServiceQueryBuilder.New().Maximum(nameof(TestClass.NullSByteVal)).Build();
            result = await serviceQuery.MongoDbExecuteAggregateAsync<T>(sourceQueryable);
            Assert.True(result == null);

            // Short
            serviceQuery = ServiceQueryBuilder.New().Maximum(nameof(TestClass.NullShortVal)).Build();
            result = await serviceQuery.MongoDbExecuteAggregateAsync<T>(sourceQueryable);
            Assert.True(result == null);

            // Single
            serviceQuery = ServiceQueryBuilder.New().Maximum(nameof(TestClass.NullSingleVal)).Build();
            result = await serviceQuery.MongoDbExecuteAggregateAsync<T>(sourceQueryable);
            Assert.True(result == null);

            // String
            serviceQuery = ServiceQueryBuilder.New().Maximum(nameof(TestClass.NullStringVal)).Build();
            await Assert.ThrowsAsync<ServiceQueryException>(async () =>
            {
                result = await serviceQuery.MongoDbExecuteAggregateAsync<T>(sourceQueryable);
            });

#if NET6_0_OR_GREATER
            if (ValidateTimeOnly)
            {
                // TimeOnly
                serviceQuery = ServiceQueryBuilder.New().Maximum(nameof(TestClass.NullTimeOnlyVal)).Build();
                await Assert.ThrowsAsync<ServiceQueryException>(async () =>
                {
                    result = await serviceQuery.MongoDbExecuteAggregateAsync<T>(sourceQueryable);
                });
            }
#endif

            if (ValidateTimeSpan)
            {
                // TimeSpan
                serviceQuery = ServiceQueryBuilder.New().Maximum(nameof(TestClass.NullTimeSpanVal)).Build();
                await Assert.ThrowsAsync<ServiceQueryException>(async () =>
                {
                    result = await serviceQuery.MongoDbExecuteAggregateAsync<T>(sourceQueryable);
                });
            }

#if NET7_0_OR_GREATER
            if (ValidateUInt128)
            {
                // UInt128
                serviceQuery = ServiceQueryBuilder.New().Maximum(nameof(TestClass.NullUInt128Val)).Build();
                result = await serviceQuery.MongoDbExecuteAggregateAsync<T>(sourceQueryable);
                Assert.True(result == null);
            }
#endif

            if (ValidateUInt64)
            {
                // UInt64
                serviceQuery = ServiceQueryBuilder.New().Maximum(nameof(TestClass.NullUInt64Val)).Build();
                result = await serviceQuery.MongoDbExecuteAggregateAsync<T>(sourceQueryable);
                Assert.True(result == null);
            }

            // UInt32
            serviceQuery = ServiceQueryBuilder.New().Maximum(nameof(TestClass.NullUInt32Val)).Build();
            result = await serviceQuery.MongoDbExecuteAggregateAsync<T>(sourceQueryable);
            Assert.True(result == null);

            // UInt16
            serviceQuery = ServiceQueryBuilder.New().Maximum(nameof(TestClass.NullUInt16Val)).Build();
            result = await serviceQuery.MongoDbExecuteAggregateAsync<T>(sourceQueryable);
            Assert.True(result == null);
        }

        [Fact]
        public async Task NullMaximumRequestTest()
        {
            var sourceQueryable = await GetTestListAsync();
            if (sourceQueryable == null)
                return;
            IServiceQueryRequest serviceQuery;
            double? result;

            // Boolean
            serviceQuery = ServiceQueryRequestBuilder.New().Maximum(nameof(TestClass.NullBoolVal)).Build();
            await Assert.ThrowsAsync<ServiceQueryException>(async () =>
            {
                result = await serviceQuery.GetServiceQuery().MongoDbExecuteAggregateAsync<T>(sourceQueryable);
            });
            await Assert.ThrowsAsync<ServiceQueryException>(async () =>
            {
                result = await serviceQuery.GetServiceQuery().MongoDbExecuteAggregateAsync<T>(sourceQueryable);
            });

            // ByteArray
            var tempbyteArray = new byte[1] { 1 };
            serviceQuery = ServiceQueryRequestBuilder.New().Maximum(nameof(TestClass.NullByteArrayVal)).Build();
            await Assert.ThrowsAsync<ServiceQueryException>(async () =>
            {
                result = await serviceQuery.GetServiceQuery().MongoDbExecuteAggregateAsync<T>(sourceQueryable);
            });

            // Byte
            serviceQuery = ServiceQueryRequestBuilder.New().Maximum(nameof(TestClass.NullByteVal)).Build();
            await Assert.ThrowsAsync<ServiceQueryException>(async () =>
            {
                result = await serviceQuery.GetServiceQuery().MongoDbExecuteAggregateAsync<T>(sourceQueryable);
            });

            // Char
            serviceQuery = ServiceQueryRequestBuilder.New().Maximum(nameof(TestClass.NullCharVal)).Build();
            await Assert.ThrowsAsync<ServiceQueryException>(async () =>
            {
                result = await serviceQuery.GetServiceQuery().MongoDbExecuteAggregateAsync<T>(sourceQueryable);
            });

            if (ValidateDateTimeOffset)
            {
                // DateTimeOffset
                serviceQuery = ServiceQueryRequestBuilder.New().Maximum(nameof(TestClass.NullDateTimeOffsetVal)).Build();
                await Assert.ThrowsAsync<ServiceQueryException>(async () =>
                {
                    result = await serviceQuery.GetServiceQuery().MongoDbExecuteAggregateAsync<T>(sourceQueryable);
                });
            }

#if NET6_0_OR_GREATER
            // DateOnly
            serviceQuery = ServiceQueryRequestBuilder.New().Maximum(nameof(TestClass.NullDateOnlyVal)).Build();
            await Assert.ThrowsAsync<ServiceQueryException>(async () =>
            {
                result = await serviceQuery.GetServiceQuery().MongoDbExecuteAggregateAsync<T>(sourceQueryable);
            });
#endif

            // DateTime
            serviceQuery = ServiceQueryRequestBuilder.New().Maximum(nameof(TestClass.NullDateTimeVal)).Build();
            await Assert.ThrowsAsync<ServiceQueryException>(async () =>
            {
                result = await serviceQuery.GetServiceQuery().MongoDbExecuteAggregateAsync<T>(sourceQueryable);
            });

            // Decimal
            if (ValidateDecimal)
            {
                serviceQuery = ServiceQueryRequestBuilder.New().Maximum(nameof(TestClass.NullDecimalVal)).Build();
                result = await serviceQuery.GetServiceQuery().MongoDbExecuteAggregateAsync<T>(sourceQueryable);
                Assert.True(result == null);
            }

            // Double
            serviceQuery = ServiceQueryRequestBuilder.New().Maximum(nameof(TestClass.NullDoubleVal)).Build();
            result = await serviceQuery.GetServiceQuery().MongoDbExecuteAggregateAsync<T>(sourceQueryable);
            Assert.True(result == null);
            result = await serviceQuery.GetServiceQuery().MongoDbExecuteAggregateAsync<T>(sourceQueryable);
            Assert.True(result == null);

            // Float
            serviceQuery = ServiceQueryRequestBuilder.New().Maximum(nameof(TestClass.NullFloatVal)).Build();
            result = await serviceQuery.GetServiceQuery().MongoDbExecuteAggregateAsync<T>(sourceQueryable);
            Assert.True(result == null);

            // Guid
            serviceQuery = ServiceQueryRequestBuilder.New().Maximum(nameof(TestClass.NullGuidVal)).Build();
            await Assert.ThrowsAsync<ServiceQueryException>(async () =>
            {
                result = await serviceQuery.GetServiceQuery().MongoDbExecuteAggregateAsync<T>(sourceQueryable);
            });

            // Int
            serviceQuery = ServiceQueryRequestBuilder.New().Maximum(nameof(TestClass.NullIntVal)).Build();
            result = await serviceQuery.GetServiceQuery().MongoDbExecuteAggregateAsync<T>(sourceQueryable);
            Assert.True(result == null);

            // Long
            serviceQuery = ServiceQueryRequestBuilder.New().Maximum(nameof(TestClass.NullLongVal)).Build();
            result = await serviceQuery.GetServiceQuery().MongoDbExecuteAggregateAsync<T>(sourceQueryable);
            Assert.True(result == null);

            // SByte
            serviceQuery = ServiceQueryRequestBuilder.New().Maximum(nameof(TestClass.NullSByteVal)).Build();
            result = await serviceQuery.GetServiceQuery().MongoDbExecuteAggregateAsync<T>(sourceQueryable);
            Assert.True(result == null);

            // Short
            serviceQuery = ServiceQueryRequestBuilder.New().Maximum(nameof(TestClass.NullShortVal)).Build();
            result = await serviceQuery.GetServiceQuery().MongoDbExecuteAggregateAsync<T>(sourceQueryable);
            Assert.True(result == null);

            // Single
            serviceQuery = ServiceQueryRequestBuilder.New().Maximum(nameof(TestClass.NullSingleVal)).Build();
            result = await serviceQuery.GetServiceQuery().MongoDbExecuteAggregateAsync<T>(sourceQueryable);
            Assert.True(result == null);

            // String
            serviceQuery = ServiceQueryRequestBuilder.New().Maximum(nameof(TestClass.NullStringVal)).Build();
            await Assert.ThrowsAsync<ServiceQueryException>(async () =>
            {
                result = await serviceQuery.GetServiceQuery().MongoDbExecuteAggregateAsync<T>(sourceQueryable);
            });

#if NET6_0_OR_GREATER
            if (ValidateTimeOnly)
            {
                // TimeOnly
                serviceQuery = ServiceQueryRequestBuilder.New().Maximum(nameof(TestClass.NullTimeOnlyVal)).Build();
                await Assert.ThrowsAsync<ServiceQueryException>(async () =>
                {
                    result = await serviceQuery.GetServiceQuery().MongoDbExecuteAggregateAsync<T>(sourceQueryable);
                });
            }
#endif

            if (ValidateTimeSpan)
            {
                // TimeSpan
                serviceQuery = ServiceQueryRequestBuilder.New().Maximum(nameof(TestClass.NullTimeSpanVal)).Build();
                await Assert.ThrowsAsync<ServiceQueryException>(async () =>
                {
                    result = await serviceQuery.GetServiceQuery().MongoDbExecuteAggregateAsync<T>(sourceQueryable);
                });
            }

#if NET7_0_OR_GREATER
            if (ValidateUInt128)
            {
                // UInt128
                serviceQuery = ServiceQueryRequestBuilder.New().Maximum(nameof(TestClass.NullUInt128Val)).Build();
                result = await serviceQuery.GetServiceQuery().MongoDbExecuteAggregateAsync<T>(sourceQueryable);
                Assert.True(result == null);
            }
#endif

            if (ValidateUInt64)
            {
                // UInt64
                serviceQuery = ServiceQueryRequestBuilder.New().Maximum(nameof(TestClass.NullUInt64Val)).Build();
                result = await serviceQuery.GetServiceQuery().MongoDbExecuteAggregateAsync<T>(sourceQueryable);
                Assert.True(result == null);
            }

            // UInt32
            serviceQuery = ServiceQueryRequestBuilder.New().Maximum(nameof(TestClass.NullUInt32Val)).Build();
            result = await serviceQuery.GetServiceQuery().MongoDbExecuteAggregateAsync<T>(sourceQueryable);
            Assert.True(result == null);

            // UInt16
            serviceQuery = ServiceQueryRequestBuilder.New().Maximum(nameof(TestClass.NullUInt16Val)).Build();
            result = await serviceQuery.GetServiceQuery().MongoDbExecuteAggregateAsync<T>(sourceQueryable);
            Assert.True(result == null);
        }

        [Fact]
        public async Task NullCopyMaximumStandardTest()
        {
            var sourceQueryable = await GetTestNullCopyListAsync();
            if (sourceQueryable == null)
                return;
            IServiceQuery serviceQuery;
            double? result;

            // Boolean
            serviceQuery = ServiceQueryBuilder.New().Maximum(nameof(TestClass.NullBoolVal)).Build();
            await Assert.ThrowsAsync<ServiceQueryException>(async () =>
            {
                result = await serviceQuery.MongoDbExecuteAggregateAsync<T>(sourceQueryable);
            });
            await Assert.ThrowsAsync<ServiceQueryException>(async () =>
            {
                result = await serviceQuery.MongoDbExecuteAggregateAsync<T>(sourceQueryable);
            });

            // ByteArray
            var tempbyteArray = new byte[1] { 1 };
            serviceQuery = ServiceQueryBuilder.New().Maximum(nameof(TestClass.NullByteArrayVal)).Build();
            await Assert.ThrowsAsync<ServiceQueryException>(async () =>
            {
                result = await serviceQuery.MongoDbExecuteAggregateAsync<T>(sourceQueryable);
            });

            // Byte
            serviceQuery = ServiceQueryBuilder.New().Maximum(nameof(TestClass.NullByteVal)).Build();
            await Assert.ThrowsAsync<ServiceQueryException>(async () =>
            {
                result = await serviceQuery.MongoDbExecuteAggregateAsync<T>(sourceQueryable);
            });

            // Char
            serviceQuery = ServiceQueryBuilder.New().Maximum(nameof(TestClass.NullCharVal)).Build();
            await Assert.ThrowsAsync<ServiceQueryException>(async () =>
            {
                result = await serviceQuery.MongoDbExecuteAggregateAsync<T>(sourceQueryable);
            });

            if (ValidateDateTimeOffset)
            {
                // DateTimeOffset
                serviceQuery = ServiceQueryBuilder.New().Maximum(nameof(TestClass.NullDateTimeOffsetVal)).Build();
                await Assert.ThrowsAsync<ServiceQueryException>(async () =>
                {
                    result = await serviceQuery.MongoDbExecuteAggregateAsync<T>(sourceQueryable);
                });
            }

#if NET6_0_OR_GREATER
            // DateOnly
            serviceQuery = ServiceQueryBuilder.New().Maximum(nameof(TestClass.NullDateOnlyVal)).Build();
            await Assert.ThrowsAsync<ServiceQueryException>(async () =>
            {
                result = await serviceQuery.MongoDbExecuteAggregateAsync<T>(sourceQueryable);
            });
#endif

            // DateTime
            serviceQuery = ServiceQueryBuilder.New().Maximum(nameof(TestClass.NullDateTimeVal)).Build();
            await Assert.ThrowsAsync<ServiceQueryException>(async () =>
            {
                result = await serviceQuery.MongoDbExecuteAggregateAsync<T>(sourceQueryable);
            });

            // Decimal
            if (ValidateDecimal)
            {
                serviceQuery = ServiceQueryBuilder.New().Maximum(nameof(TestClass.NullDecimalVal)).Build();
                result = await serviceQuery.MongoDbExecuteAggregateAsync<T>(sourceQueryable);
                Assert.True(result == 3);
            }

            // Double
            serviceQuery = ServiceQueryBuilder.New().Maximum(nameof(TestClass.NullDoubleVal)).Build();
            result = await serviceQuery.MongoDbExecuteAggregateAsync<T>(sourceQueryable);
            Assert.True(result == 3);
            result = await serviceQuery.MongoDbExecuteAggregateAsync<T>(sourceQueryable);
            Assert.True(result == 3);

            // Float
            serviceQuery = ServiceQueryBuilder.New().Maximum(nameof(TestClass.NullFloatVal)).Build();
            result = await serviceQuery.MongoDbExecuteAggregateAsync<T>(sourceQueryable);
            Assert.True(result == 3);

            // Guid
            serviceQuery = ServiceQueryBuilder.New().Maximum(nameof(TestClass.NullGuidVal)).Build();
            await Assert.ThrowsAsync<ServiceQueryException>(async () =>
            {
                result = await serviceQuery.MongoDbExecuteAggregateAsync<T>(sourceQueryable);
            });

            // Int
            serviceQuery = ServiceQueryBuilder.New().Maximum(nameof(TestClass.NullIntVal)).Build();
            result = await serviceQuery.MongoDbExecuteAggregateAsync<T>(sourceQueryable);
            Assert.True(result == 3);

            // Long
            serviceQuery = ServiceQueryBuilder.New().Maximum(nameof(TestClass.NullLongVal)).Build();
            result = await serviceQuery.MongoDbExecuteAggregateAsync<T>(sourceQueryable);
            Assert.True(result == 3);

            // SByte
            serviceQuery = ServiceQueryBuilder.New().Maximum(nameof(TestClass.NullSByteVal)).Build();
            result = await serviceQuery.MongoDbExecuteAggregateAsync<T>(sourceQueryable);
            Assert.True(result == 3);

            // Short
            serviceQuery = ServiceQueryBuilder.New().Maximum(nameof(TestClass.NullShortVal)).Build();
            result = await serviceQuery.MongoDbExecuteAggregateAsync<T>(sourceQueryable);
            Assert.True(result == 3);

            // Single
            serviceQuery = ServiceQueryBuilder.New().Maximum(nameof(TestClass.NullSingleVal)).Build();
            result = await serviceQuery.MongoDbExecuteAggregateAsync<T>(sourceQueryable);
            Assert.True(result == 3);

            // String
            serviceQuery = ServiceQueryBuilder.New().Maximum(nameof(TestClass.NullStringVal)).Build();
            await Assert.ThrowsAsync<ServiceQueryException>(async () =>
            {
                result = await serviceQuery.MongoDbExecuteAggregateAsync<T>(sourceQueryable);
            });

#if NET6_0_OR_GREATER
            if (ValidateTimeOnly)
            {
                // TimeOnly
                serviceQuery = ServiceQueryBuilder.New().Maximum(nameof(TestClass.NullTimeOnlyVal)).Build();
                await Assert.ThrowsAsync<ServiceQueryException>(async () =>
                {
                    result = await serviceQuery.MongoDbExecuteAggregateAsync<T>(sourceQueryable);
                });
            }
#endif

            if (ValidateTimeSpan)
            {
                // TimeSpan
                serviceQuery = ServiceQueryBuilder.New().Maximum(nameof(TestClass.NullTimeSpanVal)).Build();
                await Assert.ThrowsAsync<ServiceQueryException>(async () =>
                {
                    result = await serviceQuery.MongoDbExecuteAggregateAsync<T>(sourceQueryable);
                });
            }

#if NET7_0_OR_GREATER
            if (ValidateUInt128)
            {
                // UInt128
                serviceQuery = ServiceQueryBuilder.New().Maximum(nameof(TestClass.NullUInt128Val)).Build();
                result = await serviceQuery.MongoDbExecuteAggregateAsync<T>(sourceQueryable);
                Assert.True(result == 3);
            }
#endif

            if (ValidateUInt64)
            {
                // UInt64
                serviceQuery = ServiceQueryBuilder.New().Maximum(nameof(TestClass.NullUInt64Val)).Build();
                result = await serviceQuery.MongoDbExecuteAggregateAsync<T>(sourceQueryable);
                Assert.True(result == 3);
            }

            // UInt32
            serviceQuery = ServiceQueryBuilder.New().Maximum(nameof(TestClass.NullUInt32Val)).Build();
            result = await serviceQuery.MongoDbExecuteAggregateAsync<T>(sourceQueryable);
            Assert.True(result == 3);

            // UInt16
            serviceQuery = ServiceQueryBuilder.New().Maximum(nameof(TestClass.NullUInt16Val)).Build();
            result = await serviceQuery.MongoDbExecuteAggregateAsync<T>(sourceQueryable);
            Assert.True(result == 3);
        }

        [Fact]
        public async Task NullCopyMaximumRequestTest()
        {
            var sourceQueryable = await GetTestNullCopyListAsync();
            if (sourceQueryable == null)
                return;
            IServiceQueryRequest serviceQuery;
            double? result;

            // Boolean
            serviceQuery = ServiceQueryRequestBuilder.New().Maximum(nameof(TestClass.NullBoolVal)).Build();
            await Assert.ThrowsAsync<ServiceQueryException>(async () =>
            {
                result = await serviceQuery.GetServiceQuery().MongoDbExecuteAggregateAsync<T>(sourceQueryable);
            });
            await Assert.ThrowsAsync<ServiceQueryException>(async () =>
            {
                result = await serviceQuery.GetServiceQuery().MongoDbExecuteAggregateAsync<T>(sourceQueryable);
            });

            // ByteArray
            var tempbyteArray = new byte[1] { 1 };
            serviceQuery = ServiceQueryRequestBuilder.New().Maximum(nameof(TestClass.NullByteArrayVal)).Build();
            await Assert.ThrowsAsync<ServiceQueryException>(async () =>
            {
                result = await serviceQuery.GetServiceQuery().MongoDbExecuteAggregateAsync<T>(sourceQueryable);
            });

            // Byte
            serviceQuery = ServiceQueryRequestBuilder.New().Maximum(nameof(TestClass.NullByteVal)).Build();
            await Assert.ThrowsAsync<ServiceQueryException>(async () =>
            {
                result = await serviceQuery.GetServiceQuery().MongoDbExecuteAggregateAsync<T>(sourceQueryable);
            });

            // Char
            serviceQuery = ServiceQueryRequestBuilder.New().Maximum(nameof(TestClass.NullCharVal)).Build();
            await Assert.ThrowsAsync<ServiceQueryException>(async () =>
            {
                result = await serviceQuery.GetServiceQuery().MongoDbExecuteAggregateAsync<T>(sourceQueryable);
            });

            if (ValidateDateTimeOffset)
            {
                // DateTimeOffset
                serviceQuery = ServiceQueryRequestBuilder.New().Maximum(nameof(TestClass.NullDateTimeOffsetVal)).Build();
                await Assert.ThrowsAsync<ServiceQueryException>(async () =>
                {
                    result = await serviceQuery.GetServiceQuery().MongoDbExecuteAggregateAsync<T>(sourceQueryable);
                });
            }

#if NET6_0_OR_GREATER
            // DateOnly
            serviceQuery = ServiceQueryRequestBuilder.New().Maximum(nameof(TestClass.NullDateOnlyVal)).Build();
            await Assert.ThrowsAsync<ServiceQueryException>(async () =>
            {
                result = await serviceQuery.GetServiceQuery().MongoDbExecuteAggregateAsync<T>(sourceQueryable);
            });
#endif

            // DateTime
            serviceQuery = ServiceQueryRequestBuilder.New().Maximum(nameof(TestClass.NullDateTimeVal)).Build();
            await Assert.ThrowsAsync<ServiceQueryException>(async () =>
            {
                result = await serviceQuery.GetServiceQuery().MongoDbExecuteAggregateAsync<T>(sourceQueryable);
            });

            // Decimal
            if (ValidateDecimal)
            {
                serviceQuery = ServiceQueryRequestBuilder.New().Maximum(nameof(TestClass.NullDecimalVal)).Build();
                result = await serviceQuery.GetServiceQuery().MongoDbExecuteAggregateAsync<T>(sourceQueryable);
                Assert.True(result == 3);
            }

            // Double
            serviceQuery = ServiceQueryRequestBuilder.New().Maximum(nameof(TestClass.NullDoubleVal)).Build();
            result = await serviceQuery.GetServiceQuery().MongoDbExecuteAggregateAsync<T>(sourceQueryable);
            Assert.True(result == 3);
            result = await serviceQuery.GetServiceQuery().MongoDbExecuteAggregateAsync<T>(sourceQueryable);
            Assert.True(result == 3);

            // Float
            serviceQuery = ServiceQueryRequestBuilder.New().Maximum(nameof(TestClass.NullFloatVal)).Build();
            result = await serviceQuery.GetServiceQuery().MongoDbExecuteAggregateAsync<T>(sourceQueryable);
            Assert.True(result == 3);

            // Guid
            serviceQuery = ServiceQueryRequestBuilder.New().Maximum(nameof(TestClass.NullGuidVal)).Build();
            await Assert.ThrowsAsync<ServiceQueryException>(async () =>
            {
                result = await serviceQuery.GetServiceQuery().MongoDbExecuteAggregateAsync<T>(sourceQueryable);
            });

            // Int
            serviceQuery = ServiceQueryRequestBuilder.New().Maximum(nameof(TestClass.NullIntVal)).Build();
            result = await serviceQuery.GetServiceQuery().MongoDbExecuteAggregateAsync<T>(sourceQueryable);
            Assert.True(result == 3);

            // Long
            serviceQuery = ServiceQueryRequestBuilder.New().Maximum(nameof(TestClass.NullLongVal)).Build();
            result = await serviceQuery.GetServiceQuery().MongoDbExecuteAggregateAsync<T>(sourceQueryable);
            Assert.True(result == 3);

            // SByte
            serviceQuery = ServiceQueryRequestBuilder.New().Maximum(nameof(TestClass.NullSByteVal)).Build();
            result = await serviceQuery.GetServiceQuery().MongoDbExecuteAggregateAsync<T>(sourceQueryable);
            Assert.True(result == 3);

            // Short
            serviceQuery = ServiceQueryRequestBuilder.New().Maximum(nameof(TestClass.NullShortVal)).Build();
            result = await serviceQuery.GetServiceQuery().MongoDbExecuteAggregateAsync<T>(sourceQueryable);
            Assert.True(result == 3);

            // Single
            serviceQuery = ServiceQueryRequestBuilder.New().Maximum(nameof(TestClass.NullSingleVal)).Build();
            result = await serviceQuery.GetServiceQuery().MongoDbExecuteAggregateAsync<T>(sourceQueryable);
            Assert.True(result == 3);

            // String
            serviceQuery = ServiceQueryRequestBuilder.New().Maximum(nameof(TestClass.NullStringVal)).Build();
            await Assert.ThrowsAsync<ServiceQueryException>(async () =>
            {
                result = await serviceQuery.GetServiceQuery().MongoDbExecuteAggregateAsync<T>(sourceQueryable);
            });

#if NET6_0_OR_GREATER
            if (ValidateTimeOnly)
            {
                // TimeOnly
                serviceQuery = ServiceQueryRequestBuilder.New().Maximum(nameof(TestClass.NullTimeOnlyVal)).Build();
                await Assert.ThrowsAsync<ServiceQueryException>(async () =>
                {
                    result = await serviceQuery.GetServiceQuery().MongoDbExecuteAggregateAsync<T>(sourceQueryable);
                });
            }
#endif

            if (ValidateTimeSpan)
            {
                // TimeSpan
                serviceQuery = ServiceQueryRequestBuilder.New().Maximum(nameof(TestClass.NullTimeSpanVal)).Build();
                await Assert.ThrowsAsync<ServiceQueryException>(async () =>
                {
                    result = await serviceQuery.GetServiceQuery().MongoDbExecuteAggregateAsync<T>(sourceQueryable);
                });
            }

#if NET7_0_OR_GREATER
            if (ValidateUInt128)
            {
                // UInt128
                serviceQuery = ServiceQueryRequestBuilder.New().Maximum(nameof(TestClass.NullUInt128Val)).Build();
                result = await serviceQuery.GetServiceQuery().MongoDbExecuteAggregateAsync<T>(sourceQueryable);
                Assert.True(result == 3);
            }
#endif

            if (ValidateUInt64)
            {
                // UInt64
                serviceQuery = ServiceQueryRequestBuilder.New().Maximum(nameof(TestClass.NullUInt64Val)).Build();
                result = await serviceQuery.GetServiceQuery().MongoDbExecuteAggregateAsync<T>(sourceQueryable);
                Assert.True(result == 3);
            }

            // UInt32
            serviceQuery = ServiceQueryRequestBuilder.New().Maximum(nameof(TestClass.NullUInt32Val)).Build();
            result = await serviceQuery.GetServiceQuery().MongoDbExecuteAggregateAsync<T>(sourceQueryable);
            Assert.True(result == 3);

            // UInt16
            serviceQuery = ServiceQueryRequestBuilder.New().Maximum(nameof(TestClass.NullUInt16Val)).Build();
            result = await serviceQuery.GetServiceQuery().MongoDbExecuteAggregateAsync<T>(sourceQueryable);
            Assert.True(result == 3);
        }
    }
}
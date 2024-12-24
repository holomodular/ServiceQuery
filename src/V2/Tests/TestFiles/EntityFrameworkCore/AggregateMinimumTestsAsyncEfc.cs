namespace ServiceQuery.Xunit
{
    public class AggregateMinimumTestsAsyncEfc : AggregateMinimumTestsAsyncEfc<TestClass>
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

    public abstract class AggregateMinimumTestsAsyncEfc<T> : BaseTest<T> where T : class
    {
        [Fact]
        public async Task MinimumStandardTest()
        {
            var sourceQueryable = await GetTestListAsync();
            IServiceQuery serviceQuery;
            double? result;

            // Boolean
            serviceQuery = ServiceQueryBuilder.New().Minimum(nameof(TestClass.BoolVal)).Build();
            await Assert.ThrowsAsync<ServiceQueryException>(async () =>
            {
                result = await serviceQuery.ExecuteAggregateAsync<T>(sourceQueryable);
            });

            // ByteArray
            var tempbyteArray = new byte[1] { 1 };
            serviceQuery = ServiceQueryBuilder.New().Minimum(nameof(TestClass.ByteArrayVal)).Build();
            await Assert.ThrowsAsync<ServiceQueryException>(async () =>
            {
                result = await serviceQuery.ExecuteAggregateAsync<T>(sourceQueryable);
            });

            // Byte
            serviceQuery = ServiceQueryBuilder.New().Minimum(nameof(TestClass.ByteVal)).Build();
            await Assert.ThrowsAsync<ServiceQueryException>(async () =>
            {
                result = await serviceQuery.ExecuteAggregateAsync<T>(sourceQueryable);
            });

            // Char
            serviceQuery = ServiceQueryBuilder.New().Minimum(nameof(TestClass.CharVal)).Build();
            await Assert.ThrowsAsync<ServiceQueryException>(async () =>
            {
                result = await serviceQuery.ExecuteAggregateAsync<T>(sourceQueryable);
            });

            if (ValidateDateTimeOffset)
            {
                // DateTimeOffset
                serviceQuery = ServiceQueryBuilder.New().Minimum(nameof(TestClass.DateTimeOffsetVal)).Build();
                await Assert.ThrowsAsync<ServiceQueryException>(async () =>
                {
                    result = await serviceQuery.ExecuteAggregateAsync<T>(sourceQueryable);
                });
            }

#if NET6_0_OR_GREATER
            // DateOnly
            serviceQuery = ServiceQueryBuilder.New().Minimum(nameof(TestClass.DateOnlyVal)).Build();
            await Assert.ThrowsAsync<ServiceQueryException>(async () =>
            {
                result = await serviceQuery.ExecuteAggregateAsync<T>(sourceQueryable);
            });
#endif

            // DateTime
            serviceQuery = ServiceQueryBuilder.New().Minimum(nameof(TestClass.DateTimeVal)).Build();
            await Assert.ThrowsAsync<ServiceQueryException>(async () =>
            {
                result = await serviceQuery.ExecuteAggregateAsync<T>(sourceQueryable);
            });

            // Decimal
            if (ValidateDecimal)
            {
                serviceQuery = ServiceQueryBuilder.New().Minimum(nameof(TestClass.DecimalVal)).Build();
                result = await serviceQuery.ExecuteAggregateAsync<T>(sourceQueryable);
                Assert.True(result == 0);
            }

            // Double
            serviceQuery = ServiceQueryBuilder.New().Minimum(nameof(TestClass.DoubleVal)).Build();
            result = await serviceQuery.ExecuteAggregateAsync<T>(sourceQueryable);
            Assert.True(result == 0);
            result = await serviceQuery.ExecuteAggregateAsync<T>(sourceQueryable);
            Assert.True(result == 0);

            // Float
            serviceQuery = ServiceQueryBuilder.New().Minimum(nameof(TestClass.FloatVal)).Build();
            result = await serviceQuery.ExecuteAggregateAsync<T>(sourceQueryable);
            Assert.True(result == 0);

            // Guid
            serviceQuery = ServiceQueryBuilder.New().Minimum(nameof(TestClass.GuidVal)).Build();
            await Assert.ThrowsAsync<ServiceQueryException>(async () =>
            {
                result = await serviceQuery.ExecuteAggregateAsync<T>(sourceQueryable);
            });

            // Int
            serviceQuery = ServiceQueryBuilder.New().Minimum(nameof(TestClass.IntVal)).Build();
            result = await serviceQuery.ExecuteAggregateAsync<T>(sourceQueryable);
            Assert.True(result == 0);

            // Long
            serviceQuery = ServiceQueryBuilder.New().Minimum(nameof(TestClass.LongVal)).Build();
            result = await serviceQuery.ExecuteAggregateAsync<T>(sourceQueryable);
            Assert.True(result == 0);

            // SByte
            serviceQuery = ServiceQueryBuilder.New().Minimum(nameof(TestClass.SByteVal)).Build();
            result = await serviceQuery.ExecuteAggregateAsync<T>(sourceQueryable);
            Assert.True(result == 0);

            // Short
            serviceQuery = ServiceQueryBuilder.New().Minimum(nameof(TestClass.ShortVal)).Build();
            result = await serviceQuery.ExecuteAggregateAsync<T>(sourceQueryable);
            Assert.True(result == 0);

            // Single
            serviceQuery = ServiceQueryBuilder.New().Minimum(nameof(TestClass.SingleVal)).Build();
            result = await serviceQuery.ExecuteAggregateAsync<T>(sourceQueryable);
            Assert.True(result == 0);

            // String
            serviceQuery = ServiceQueryBuilder.New().Minimum(nameof(TestClass.StringVal)).Build();
            await Assert.ThrowsAsync<ServiceQueryException>(async () =>
            {
                result = await serviceQuery.ExecuteAggregateAsync<T>(sourceQueryable);
            });

#if NET6_0_OR_GREATER
            if (ValidateTimeOnly)
            {
                // TimeOnly
                serviceQuery = ServiceQueryBuilder.New().Minimum(nameof(TestClass.TimeOnlyVal)).Build();
                await Assert.ThrowsAsync<ServiceQueryException>(async () =>
                {
                    result = await serviceQuery.ExecuteAggregateAsync<T>(sourceQueryable);
                });
            }
#endif

            if (ValidateTimeSpan)
            {
                // TimeSpan
                serviceQuery = ServiceQueryBuilder.New().Minimum(nameof(TestClass.TimeSpanVal)).Build();
                await Assert.ThrowsAsync<ServiceQueryException>(async () =>
                {
                    result = await serviceQuery.ExecuteAggregateAsync<T>(sourceQueryable);
                });
            }

#if NET7_0_OR_GREATER
            if (ValidateUInt128)
            {
                // UInt128
                serviceQuery = ServiceQueryBuilder.New().Minimum(nameof(TestClass.UInt128Val)).Build();
                result = await serviceQuery.ExecuteAggregateAsync<T>(sourceQueryable);
                Assert.True(result == 0);
            }
#endif

            if (ValidateUInt64)
            {
                // UInt64
                serviceQuery = ServiceQueryBuilder.New().Minimum(nameof(TestClass.UInt64Val)).Build();
                result = await serviceQuery.ExecuteAggregateAsync<T>(sourceQueryable);
                Assert.True(result == 0);
            }

            // UInt32
            serviceQuery = ServiceQueryBuilder.New().Minimum(nameof(TestClass.UInt32Val)).Build();
            result = await serviceQuery.ExecuteAggregateAsync<T>(sourceQueryable);
            Assert.True(result == 0);

            // UInt16
            serviceQuery = ServiceQueryBuilder.New().Minimum(nameof(TestClass.UInt16Val)).Build();
            result = await serviceQuery.ExecuteAggregateAsync<T>(sourceQueryable);
            Assert.True(result == 0);
        }

        [Fact]
        public async Task MinimumRequestTest()
        {
            var sourceQueryable = await GetTestListAsync();
            IServiceQueryRequest serviceQuery;
            double? result;

            // Boolean
            serviceQuery = ServiceQueryRequestBuilder.New().Minimum(nameof(TestClass.BoolVal)).Build();
            await Assert.ThrowsAsync<ServiceQueryException>(async () =>
            {
                result = await serviceQuery.GetServiceQuery().ExecuteAggregateAsync<T>(sourceQueryable);
            });
            await Assert.ThrowsAsync<ServiceQueryException>(async () =>
            {
                result = await serviceQuery.GetServiceQuery().ExecuteAggregateAsync<T>(sourceQueryable);
            });

            // ByteArray
            var tempbyteArray = new byte[1] { 1 };
            serviceQuery = ServiceQueryRequestBuilder.New().Minimum(nameof(TestClass.ByteArrayVal)).Build();
            await Assert.ThrowsAsync<ServiceQueryException>(async () =>
            {
                result = await serviceQuery.GetServiceQuery().ExecuteAggregateAsync<T>(sourceQueryable);
            });

            // Byte
            serviceQuery = ServiceQueryRequestBuilder.New().Minimum(nameof(TestClass.ByteVal)).Build();
            await Assert.ThrowsAsync<ServiceQueryException>(async () =>
            {
                result = await serviceQuery.GetServiceQuery().ExecuteAggregateAsync<T>(sourceQueryable);
            });

            // Char
            serviceQuery = ServiceQueryRequestBuilder.New().Minimum(nameof(TestClass.CharVal)).Build();
            await Assert.ThrowsAsync<ServiceQueryException>(async () =>
            {
                result = await serviceQuery.GetServiceQuery().ExecuteAggregateAsync<T>(sourceQueryable);
            });

            if (ValidateDateTimeOffset)
            {
                // DateTimeOffset
                serviceQuery = ServiceQueryRequestBuilder.New().Minimum(nameof(TestClass.DateTimeOffsetVal)).Build();
                await Assert.ThrowsAsync<ServiceQueryException>(async () =>
                {
                    result = await serviceQuery.GetServiceQuery().ExecuteAggregateAsync<T>(sourceQueryable);
                });
            }

#if NET6_0_OR_GREATER
            // DateOnly
            serviceQuery = ServiceQueryRequestBuilder.New().Minimum(nameof(TestClass.DateOnlyVal)).Build();
            await Assert.ThrowsAsync<ServiceQueryException>(async () =>
            {
                result = await serviceQuery.GetServiceQuery().ExecuteAggregateAsync<T>(sourceQueryable);
            });
#endif

            // DateTime
            serviceQuery = ServiceQueryRequestBuilder.New().Minimum(nameof(TestClass.DateTimeVal)).Build();
            await Assert.ThrowsAsync<ServiceQueryException>(async () =>
            {
                result = await serviceQuery.GetServiceQuery().ExecuteAggregateAsync<T>(sourceQueryable);
            });

            // Decimal
            if (ValidateDecimal)
            {
                serviceQuery = ServiceQueryRequestBuilder.New().Minimum(nameof(TestClass.DecimalVal)).Build();
                result = await serviceQuery.GetServiceQuery().ExecuteAggregateAsync<T>(sourceQueryable);
                Assert.True(result == 0);
            }

            // Double
            serviceQuery = ServiceQueryRequestBuilder.New().Minimum(nameof(TestClass.DoubleVal)).Build();
            result = await serviceQuery.GetServiceQuery().ExecuteAggregateAsync<T>(sourceQueryable);
            Assert.True(result == 0);
            result = await serviceQuery.GetServiceQuery().ExecuteAggregateAsync<T>(sourceQueryable);
            Assert.True(result == 0);

            // Float
            serviceQuery = ServiceQueryRequestBuilder.New().Minimum(nameof(TestClass.FloatVal)).Build();
            result = await serviceQuery.GetServiceQuery().ExecuteAggregateAsync<T>(sourceQueryable);
            Assert.True(result == 0);

            // Guid
            serviceQuery = ServiceQueryRequestBuilder.New().Minimum(nameof(TestClass.GuidVal)).Build();
            await Assert.ThrowsAsync<ServiceQueryException>(async () =>
            {
                result = await serviceQuery.GetServiceQuery().ExecuteAggregateAsync<T>(sourceQueryable);
            });

            // Int
            serviceQuery = ServiceQueryRequestBuilder.New().Minimum(nameof(TestClass.IntVal)).Build();
            result = await serviceQuery.GetServiceQuery().ExecuteAggregateAsync<T>(sourceQueryable);
            Assert.True(result == 0);

            // Long
            serviceQuery = ServiceQueryRequestBuilder.New().Minimum(nameof(TestClass.LongVal)).Build();
            result = await serviceQuery.GetServiceQuery().ExecuteAggregateAsync<T>(sourceQueryable);
            Assert.True(result == 0);

            // SByte
            serviceQuery = ServiceQueryRequestBuilder.New().Minimum(nameof(TestClass.SByteVal)).Build();
            result = await serviceQuery.GetServiceQuery().ExecuteAggregateAsync<T>(sourceQueryable);
            Assert.True(result == 0);

            // Short
            serviceQuery = ServiceQueryRequestBuilder.New().Minimum(nameof(TestClass.ShortVal)).Build();
            result = await serviceQuery.GetServiceQuery().ExecuteAggregateAsync<T>(sourceQueryable);
            Assert.True(result == 0);

            // Single
            serviceQuery = ServiceQueryRequestBuilder.New().Minimum(nameof(TestClass.SingleVal)).Build();
            result = await serviceQuery.GetServiceQuery().ExecuteAggregateAsync<T>(sourceQueryable);
            Assert.True(result == 0);

            // String
            serviceQuery = ServiceQueryRequestBuilder.New().Minimum(nameof(TestClass.StringVal)).Build();
            await Assert.ThrowsAsync<ServiceQueryException>(async () =>
            {
                result = await serviceQuery.GetServiceQuery().ExecuteAggregateAsync<T>(sourceQueryable);
            });

#if NET6_0_OR_GREATER
            if (ValidateTimeOnly)
            {
                // TimeOnly
                serviceQuery = ServiceQueryRequestBuilder.New().Minimum(nameof(TestClass.TimeOnlyVal)).Build();
                await Assert.ThrowsAsync<ServiceQueryException>(async () =>
                {
                    result = await serviceQuery.GetServiceQuery().ExecuteAggregateAsync<T>(sourceQueryable);
                });
            }
#endif

            if (ValidateTimeSpan)
            {
                // TimeSpan
                serviceQuery = ServiceQueryRequestBuilder.New().Minimum(nameof(TestClass.TimeSpanVal)).Build();
                await Assert.ThrowsAsync<ServiceQueryException>(async () =>
                {
                    result = await serviceQuery.GetServiceQuery().ExecuteAggregateAsync<T>(sourceQueryable);
                });
            }

#if NET7_0_OR_GREATER
            if (ValidateUInt128)
            {
                // UInt128
                serviceQuery = ServiceQueryRequestBuilder.New().Minimum(nameof(TestClass.UInt128Val)).Build();
                result = await serviceQuery.GetServiceQuery().ExecuteAggregateAsync<T>(sourceQueryable);
                Assert.True(result == 0);
            }
#endif
            if (ValidateUInt64)
            {
                // UInt64
                serviceQuery = ServiceQueryRequestBuilder.New().Minimum(nameof(TestClass.UInt64Val)).Build();
                result = await serviceQuery.GetServiceQuery().ExecuteAggregateAsync<T>(sourceQueryable);
                Assert.True(result == 0);
            }

            // UInt32
            serviceQuery = ServiceQueryRequestBuilder.New().Minimum(nameof(TestClass.UInt32Val)).Build();
            result = await serviceQuery.GetServiceQuery().ExecuteAggregateAsync<T>(sourceQueryable);
            Assert.True(result == 0);

            // UInt16
            serviceQuery = ServiceQueryRequestBuilder.New().Minimum(nameof(TestClass.UInt16Val)).Build();
            result = await serviceQuery.GetServiceQuery().ExecuteAggregateAsync<T>(sourceQueryable);
            Assert.True(result == 0);
        }

        [Fact]
        public async Task NullMinimumStandardTest()
        {
            var sourceQueryable = await GetTestListAsync();
            IServiceQuery serviceQuery;
            double? result;

            // Boolean
            serviceQuery = ServiceQueryBuilder.New().Minimum(nameof(TestClass.NullBoolVal)).Build();
            await Assert.ThrowsAsync<ServiceQueryException>(async () =>
            {
                result = await serviceQuery.ExecuteAggregateAsync<T>(sourceQueryable);
            });
            await Assert.ThrowsAsync<ServiceQueryException>(async () =>
            {
                result = await serviceQuery.ExecuteAggregateAsync<T>(sourceQueryable);
            });

            // ByteArray
            var tempbyteArray = new byte[1] { 1 };
            serviceQuery = ServiceQueryBuilder.New().Minimum(nameof(TestClass.NullByteArrayVal)).Build();
            await Assert.ThrowsAsync<ServiceQueryException>(async () =>
            {
                result = await serviceQuery.ExecuteAggregateAsync<T>(sourceQueryable);
            });

            // Byte
            serviceQuery = ServiceQueryBuilder.New().Minimum(nameof(TestClass.NullByteVal)).Build();
            await Assert.ThrowsAsync<ServiceQueryException>(async () =>
            {
                result = await serviceQuery.ExecuteAggregateAsync<T>(sourceQueryable);
            });

            // Char
            serviceQuery = ServiceQueryBuilder.New().Minimum(nameof(TestClass.NullCharVal)).Build();
            await Assert.ThrowsAsync<ServiceQueryException>(async () =>
            {
                result = await serviceQuery.ExecuteAggregateAsync<T>(sourceQueryable);
            });

            if (ValidateDateTimeOffset)
            {
                // DateTimeOffset
                serviceQuery = ServiceQueryBuilder.New().Minimum(nameof(TestClass.NullDateTimeOffsetVal)).Build();
                await Assert.ThrowsAsync<ServiceQueryException>(async () =>
                {
                    result = await serviceQuery.ExecuteAggregateAsync<T>(sourceQueryable);
                });
            }

#if NET6_0_OR_GREATER
            // DateOnly
            serviceQuery = ServiceQueryBuilder.New().Minimum(nameof(TestClass.NullDateOnlyVal)).Build();
            await Assert.ThrowsAsync<ServiceQueryException>(async () =>
            {
                result = await serviceQuery.ExecuteAggregateAsync<T>(sourceQueryable);
            });
#endif

            // DateTime
            serviceQuery = ServiceQueryBuilder.New().Minimum(nameof(TestClass.NullDateTimeVal)).Build();
            await Assert.ThrowsAsync<ServiceQueryException>(async () =>
            {
                result = await serviceQuery.ExecuteAggregateAsync<T>(sourceQueryable);
            });

            // Decimal
            if (ValidateDecimal)
            {
                serviceQuery = ServiceQueryBuilder.New().Minimum(nameof(TestClass.NullDecimalVal)).Build();
                result = await serviceQuery.ExecuteAggregateAsync<T>(sourceQueryable);
                Assert.True(result == null);
            }

            // Double
            serviceQuery = ServiceQueryBuilder.New().Minimum(nameof(TestClass.NullDoubleVal)).Build();
            result = await serviceQuery.ExecuteAggregateAsync<T>(sourceQueryable);
            Assert.True(result == null);
            result = await serviceQuery.ExecuteAggregateAsync<T>(sourceQueryable);
            Assert.True(result == null);

            // Float
            serviceQuery = ServiceQueryBuilder.New().Minimum(nameof(TestClass.NullFloatVal)).Build();
            result = await serviceQuery.ExecuteAggregateAsync<T>(sourceQueryable);
            Assert.True(result == null);

            // Guid
            serviceQuery = ServiceQueryBuilder.New().Minimum(nameof(TestClass.NullGuidVal)).Build();
            await Assert.ThrowsAsync<ServiceQueryException>(async () =>
            {
                result = await serviceQuery.ExecuteAggregateAsync<T>(sourceQueryable);
            });

            // Int
            serviceQuery = ServiceQueryBuilder.New().Minimum(nameof(TestClass.NullIntVal)).Build();
            result = await serviceQuery.ExecuteAggregateAsync<T>(sourceQueryable);
            Assert.True(result == null);

            // Long
            serviceQuery = ServiceQueryBuilder.New().Minimum(nameof(TestClass.NullLongVal)).Build();
            result = await serviceQuery.ExecuteAggregateAsync<T>(sourceQueryable);
            Assert.True(result == null);

            // SByte
            serviceQuery = ServiceQueryBuilder.New().Minimum(nameof(TestClass.NullSByteVal)).Build();
            result = await serviceQuery.ExecuteAggregateAsync<T>(sourceQueryable);
            Assert.True(result == null);

            // Short
            serviceQuery = ServiceQueryBuilder.New().Minimum(nameof(TestClass.NullShortVal)).Build();
            result = await serviceQuery.ExecuteAggregateAsync<T>(sourceQueryable);
            Assert.True(result == null);

            // Single
            serviceQuery = ServiceQueryBuilder.New().Minimum(nameof(TestClass.NullSingleVal)).Build();
            result = await serviceQuery.ExecuteAggregateAsync<T>(sourceQueryable);
            Assert.True(result == null);

            // String
            serviceQuery = ServiceQueryBuilder.New().Minimum(nameof(TestClass.NullStringVal)).Build();
            await Assert.ThrowsAsync<ServiceQueryException>(async () =>
            {
                result = await serviceQuery.ExecuteAggregateAsync<T>(sourceQueryable);
            });

#if NET6_0_OR_GREATER
            if (ValidateTimeOnly)
            {
                // TimeOnly
                serviceQuery = ServiceQueryBuilder.New().Minimum(nameof(TestClass.NullTimeOnlyVal)).Build();
                await Assert.ThrowsAsync<ServiceQueryException>(async () =>
                {
                    result = await serviceQuery.ExecuteAggregateAsync<T>(sourceQueryable);
                });
            }
#endif

            if (ValidateTimeSpan)
            {
                // TimeSpan
                serviceQuery = ServiceQueryBuilder.New().Minimum(nameof(TestClass.NullTimeSpanVal)).Build();
                await Assert.ThrowsAsync<ServiceQueryException>(async () =>
                {
                    result = await serviceQuery.ExecuteAggregateAsync<T>(sourceQueryable);
                });
            }

#if NET7_0_OR_GREATER
            if (ValidateUInt128)
            {
                // UInt128
                serviceQuery = ServiceQueryBuilder.New().Minimum(nameof(TestClass.NullUInt128Val)).Build();
                result = await serviceQuery.ExecuteAggregateAsync<T>(sourceQueryable);
                Assert.True(result == null);
            }
#endif
            if (ValidateUInt64)
            {
                // UInt64
                serviceQuery = ServiceQueryBuilder.New().Minimum(nameof(TestClass.NullUInt64Val)).Build();
                result = await serviceQuery.ExecuteAggregateAsync<T>(sourceQueryable);
                Assert.True(result == null);
            }

            // UInt32
            serviceQuery = ServiceQueryBuilder.New().Minimum(nameof(TestClass.NullUInt32Val)).Build();
            result = await serviceQuery.ExecuteAggregateAsync<T>(sourceQueryable);
            Assert.True(result == null);

            // UInt16
            serviceQuery = ServiceQueryBuilder.New().Minimum(nameof(TestClass.NullUInt16Val)).Build();
            result = await serviceQuery.ExecuteAggregateAsync<T>(sourceQueryable);
            Assert.True(result == null);
        }

        [Fact]
        public async Task NullMinimumRequestTest()
        {
            var sourceQueryable = await GetTestListAsync();
            IServiceQueryRequest serviceQuery;
            double? result;

            // Boolean
            serviceQuery = ServiceQueryRequestBuilder.New().Minimum(nameof(TestClass.NullBoolVal)).Build();
            await Assert.ThrowsAsync<ServiceQueryException>(async () =>
            {
                result = await serviceQuery.GetServiceQuery().ExecuteAggregateAsync<T>(sourceQueryable);
            });
            await Assert.ThrowsAsync<ServiceQueryException>(async () =>
            {
                result = await serviceQuery.GetServiceQuery().ExecuteAggregateAsync<T>(sourceQueryable);
            });

            // ByteArray
            var tempbyteArray = new byte[1] { 1 };
            serviceQuery = ServiceQueryRequestBuilder.New().Minimum(nameof(TestClass.NullByteArrayVal)).Build();
            await Assert.ThrowsAsync<ServiceQueryException>(async () =>
            {
                result = await serviceQuery.GetServiceQuery().ExecuteAggregateAsync<T>(sourceQueryable);
            });

            // Byte
            serviceQuery = ServiceQueryRequestBuilder.New().Minimum(nameof(TestClass.NullByteVal)).Build();
            await Assert.ThrowsAsync<ServiceQueryException>(async () =>
            {
                result = await serviceQuery.GetServiceQuery().ExecuteAggregateAsync<T>(sourceQueryable);
            });

            // Char
            serviceQuery = ServiceQueryRequestBuilder.New().Minimum(nameof(TestClass.NullCharVal)).Build();
            await Assert.ThrowsAsync<ServiceQueryException>(async () =>
            {
                result = await serviceQuery.GetServiceQuery().ExecuteAggregateAsync<T>(sourceQueryable);
            });

            if (ValidateDateTimeOffset)
            {
                // DateTimeOffset
                serviceQuery = ServiceQueryRequestBuilder.New().Minimum(nameof(TestClass.NullDateTimeOffsetVal)).Build();
                await Assert.ThrowsAsync<ServiceQueryException>(async () =>
                {
                    result = await serviceQuery.GetServiceQuery().ExecuteAggregateAsync<T>(sourceQueryable);
                });
            }

#if NET6_0_OR_GREATER
            // DateOnly
            serviceQuery = ServiceQueryRequestBuilder.New().Minimum(nameof(TestClass.NullDateOnlyVal)).Build();
            await Assert.ThrowsAsync<ServiceQueryException>(async () =>
            {
                result = await serviceQuery.GetServiceQuery().ExecuteAggregateAsync<T>(sourceQueryable);
            });
#endif

            // DateTime
            serviceQuery = ServiceQueryRequestBuilder.New().Minimum(nameof(TestClass.NullDateTimeVal)).Build();
            await Assert.ThrowsAsync<ServiceQueryException>(async () =>
            {
                result = await serviceQuery.GetServiceQuery().ExecuteAggregateAsync<T>(sourceQueryable);
            });

            // Decimal
            if (ValidateDecimal)
            {
                serviceQuery = ServiceQueryRequestBuilder.New().Minimum(nameof(TestClass.NullDecimalVal)).Build();
                result = await serviceQuery.GetServiceQuery().ExecuteAggregateAsync<T>(sourceQueryable);
                Assert.True(result == null);
            }

            // Double
            serviceQuery = ServiceQueryRequestBuilder.New().Minimum(nameof(TestClass.NullDoubleVal)).Build();
            result = await serviceQuery.GetServiceQuery().ExecuteAggregateAsync<T>(sourceQueryable);
            Assert.True(result == null);
            result = await serviceQuery.GetServiceQuery().ExecuteAggregateAsync<T>(sourceQueryable);
            Assert.True(result == null);

            // Float
            serviceQuery = ServiceQueryRequestBuilder.New().Minimum(nameof(TestClass.NullFloatVal)).Build();
            result = await serviceQuery.GetServiceQuery().ExecuteAggregateAsync<T>(sourceQueryable);
            Assert.True(result == null);

            // Guid
            serviceQuery = ServiceQueryRequestBuilder.New().Minimum(nameof(TestClass.NullGuidVal)).Build();
            await Assert.ThrowsAsync<ServiceQueryException>(async () =>
            {
                result = await serviceQuery.GetServiceQuery().ExecuteAggregateAsync<T>(sourceQueryable);
            });

            // Int
            serviceQuery = ServiceQueryRequestBuilder.New().Minimum(nameof(TestClass.NullIntVal)).Build();
            result = await serviceQuery.GetServiceQuery().ExecuteAggregateAsync<T>(sourceQueryable);
            Assert.True(result == null);

            // Long
            serviceQuery = ServiceQueryRequestBuilder.New().Minimum(nameof(TestClass.NullLongVal)).Build();
            result = await serviceQuery.GetServiceQuery().ExecuteAggregateAsync<T>(sourceQueryable);
            Assert.True(result == null);

            // SByte
            serviceQuery = ServiceQueryRequestBuilder.New().Minimum(nameof(TestClass.NullSByteVal)).Build();
            result = await serviceQuery.GetServiceQuery().ExecuteAggregateAsync<T>(sourceQueryable);
            Assert.True(result == null);

            // Short
            serviceQuery = ServiceQueryRequestBuilder.New().Minimum(nameof(TestClass.NullShortVal)).Build();
            result = await serviceQuery.GetServiceQuery().ExecuteAggregateAsync<T>(sourceQueryable);
            Assert.True(result == null);

            // Single
            serviceQuery = ServiceQueryRequestBuilder.New().Minimum(nameof(TestClass.NullSingleVal)).Build();
            result = await serviceQuery.GetServiceQuery().ExecuteAggregateAsync<T>(sourceQueryable);
            Assert.True(result == null);

            // String
            serviceQuery = ServiceQueryRequestBuilder.New().Minimum(nameof(TestClass.NullStringVal)).Build();
            await Assert.ThrowsAsync<ServiceQueryException>(async () =>
            {
                result = await serviceQuery.GetServiceQuery().ExecuteAggregateAsync<T>(sourceQueryable);
            });

#if NET6_0_OR_GREATER
            if (ValidateTimeOnly)
            {
                // TimeOnly
                serviceQuery = ServiceQueryRequestBuilder.New().Minimum(nameof(TestClass.NullTimeOnlyVal)).Build();
                await Assert.ThrowsAsync<ServiceQueryException>(async () =>
                {
                    result = await serviceQuery.GetServiceQuery().ExecuteAggregateAsync<T>(sourceQueryable);
                });
            }
#endif

            if (ValidateTimeSpan)
            {
                // TimeSpan
                serviceQuery = ServiceQueryRequestBuilder.New().Minimum(nameof(TestClass.NullTimeSpanVal)).Build();
                await Assert.ThrowsAsync<ServiceQueryException>(async () =>
                {
                    result = await serviceQuery.GetServiceQuery().ExecuteAggregateAsync<T>(sourceQueryable);
                });
            }

#if NET7_0_OR_GREATER
            if (ValidateUInt128)
            {
                // UInt128
                serviceQuery = ServiceQueryRequestBuilder.New().Minimum(nameof(TestClass.NullUInt128Val)).Build();
                result = await serviceQuery.GetServiceQuery().ExecuteAggregateAsync<T>(sourceQueryable);
                Assert.True(result == null);
            }
#endif

            if (ValidateUInt64)
            {
                // UInt64
                serviceQuery = ServiceQueryRequestBuilder.New().Minimum(nameof(TestClass.NullUInt64Val)).Build();
                result = await serviceQuery.GetServiceQuery().ExecuteAggregateAsync<T>(sourceQueryable);
                Assert.True(result == null);
            }

            // UInt32
            serviceQuery = ServiceQueryRequestBuilder.New().Minimum(nameof(TestClass.NullUInt32Val)).Build();
            result = await serviceQuery.GetServiceQuery().ExecuteAggregateAsync<T>(sourceQueryable);
            Assert.True(result == null);

            // UInt16
            serviceQuery = ServiceQueryRequestBuilder.New().Minimum(nameof(TestClass.NullUInt16Val)).Build();
            result = await serviceQuery.GetServiceQuery().ExecuteAggregateAsync<T>(sourceQueryable);
            Assert.True(result == null);
        }

        [Fact]
        public async Task NullCopyMinimumStandardTest()
        {
            var sourceQueryable = await GetTestNullCopyListAsync();
            IServiceQuery serviceQuery;
            double? result;

            // Boolean
            serviceQuery = ServiceQueryBuilder.New().Minimum(nameof(TestClass.NullBoolVal)).Build();
            await Assert.ThrowsAsync<ServiceQueryException>(async () =>
            {
                result = await serviceQuery.ExecuteAggregateAsync<T>(sourceQueryable);
            });
            await Assert.ThrowsAsync<ServiceQueryException>(async () =>
            {
                result = await serviceQuery.ExecuteAggregateAsync<T>(sourceQueryable);
            });

            // ByteArray
            var tempbyteArray = new byte[1] { 1 };
            serviceQuery = ServiceQueryBuilder.New().Minimum(nameof(TestClass.NullByteArrayVal)).Build();
            await Assert.ThrowsAsync<ServiceQueryException>(async () =>
            {
                result = await serviceQuery.ExecuteAggregateAsync<T>(sourceQueryable);
            });

            // Byte
            serviceQuery = ServiceQueryBuilder.New().Minimum(nameof(TestClass.NullByteVal)).Build();
            await Assert.ThrowsAsync<ServiceQueryException>(async () =>
            {
                result = await serviceQuery.ExecuteAggregateAsync<T>(sourceQueryable);
            });

            // Char
            serviceQuery = ServiceQueryBuilder.New().Minimum(nameof(TestClass.NullCharVal)).Build();
            await Assert.ThrowsAsync<ServiceQueryException>(async () =>
            {
                result = await serviceQuery.ExecuteAggregateAsync<T>(sourceQueryable);
            });

            if (ValidateDateTimeOffset)
            {
                // DateTimeOffset
                serviceQuery = ServiceQueryBuilder.New().Minimum(nameof(TestClass.NullDateTimeOffsetVal)).Build();
                await Assert.ThrowsAsync<ServiceQueryException>(async () =>
                {
                    result = await serviceQuery.ExecuteAggregateAsync<T>(sourceQueryable);
                });
            }

#if NET6_0_OR_GREATER
            // DateOnly
            serviceQuery = ServiceQueryBuilder.New().Minimum(nameof(TestClass.NullDateOnlyVal)).Build();
            await Assert.ThrowsAsync<ServiceQueryException>(async () =>
            {
                result = await serviceQuery.ExecuteAggregateAsync<T>(sourceQueryable);
            });
#endif

            // DateTime
            serviceQuery = ServiceQueryBuilder.New().Minimum(nameof(TestClass.NullDateTimeVal)).Build();
            await Assert.ThrowsAsync<ServiceQueryException>(async () =>
            {
                result = await serviceQuery.ExecuteAggregateAsync<T>(sourceQueryable);
            });

            // Decimal
            if (ValidateDecimal)
            {
                serviceQuery = ServiceQueryBuilder.New().Minimum(nameof(TestClass.NullDecimalVal)).Build();
                result = await serviceQuery.ExecuteAggregateAsync<T>(sourceQueryable);
                Assert.True(result == 0);
            }

            // Double
            serviceQuery = ServiceQueryBuilder.New().Minimum(nameof(TestClass.NullDoubleVal)).Build();
            result = await serviceQuery.ExecuteAggregateAsync<T>(sourceQueryable);
            Assert.True(result == 0);
            result = await serviceQuery.ExecuteAggregateAsync<T>(sourceQueryable);
            Assert.True(result == 0);

            // Float
            serviceQuery = ServiceQueryBuilder.New().Minimum(nameof(TestClass.NullFloatVal)).Build();
            result = await serviceQuery.ExecuteAggregateAsync<T>(sourceQueryable);
            Assert.True(result == 0);

            // Guid
            serviceQuery = ServiceQueryBuilder.New().Minimum(nameof(TestClass.NullGuidVal)).Build();
            await Assert.ThrowsAsync<ServiceQueryException>(async () =>
            {
                result = await serviceQuery.ExecuteAggregateAsync<T>(sourceQueryable);
            });

            // Int
            serviceQuery = ServiceQueryBuilder.New().Minimum(nameof(TestClass.NullIntVal)).Build();
            result = await serviceQuery.ExecuteAggregateAsync<T>(sourceQueryable);
            Assert.True(result == 0);

            // Long
            serviceQuery = ServiceQueryBuilder.New().Minimum(nameof(TestClass.NullLongVal)).Build();
            result = await serviceQuery.ExecuteAggregateAsync<T>(sourceQueryable);
            Assert.True(result == 0);

            // SByte
            serviceQuery = ServiceQueryBuilder.New().Minimum(nameof(TestClass.NullSByteVal)).Build();
            result = await serviceQuery.ExecuteAggregateAsync<T>(sourceQueryable);
            Assert.True(result == 0);

            // Short
            serviceQuery = ServiceQueryBuilder.New().Minimum(nameof(TestClass.NullShortVal)).Build();
            result = await serviceQuery.ExecuteAggregateAsync<T>(sourceQueryable);
            Assert.True(result == 0);

            // Single
            serviceQuery = ServiceQueryBuilder.New().Minimum(nameof(TestClass.NullSingleVal)).Build();
            result = await serviceQuery.ExecuteAggregateAsync<T>(sourceQueryable);
            Assert.True(result == 0);

            // String
            serviceQuery = ServiceQueryBuilder.New().Minimum(nameof(TestClass.NullStringVal)).Build();
            await Assert.ThrowsAsync<ServiceQueryException>(async () =>
            {
                result = await serviceQuery.ExecuteAggregateAsync<T>(sourceQueryable);
            });

#if NET6_0_OR_GREATER
            if (ValidateTimeOnly)
            {
                // TimeOnly
                serviceQuery = ServiceQueryBuilder.New().Minimum(nameof(TestClass.NullTimeOnlyVal)).Build();
                await Assert.ThrowsAsync<ServiceQueryException>(async () =>
                {
                    result = await serviceQuery.ExecuteAggregateAsync<T>(sourceQueryable);
                });
            }
#endif

            if (ValidateTimeSpan)
            {
                // TimeSpan
                serviceQuery = ServiceQueryBuilder.New().Minimum(nameof(TestClass.NullTimeSpanVal)).Build();
                await Assert.ThrowsAsync<ServiceQueryException>(async () =>
                {
                    result = await serviceQuery.ExecuteAggregateAsync<T>(sourceQueryable);
                });
            }

#if NET7_0_OR_GREATER
            if (ValidateUInt128)
            {
                // UInt128
                serviceQuery = ServiceQueryBuilder.New().Minimum(nameof(TestClass.NullUInt128Val)).Build();
                result = await serviceQuery.ExecuteAggregateAsync<T>(sourceQueryable);
                Assert.True(result == 0);
            }
#endif
            if (ValidateUInt64)
            {
                // UInt64
                serviceQuery = ServiceQueryBuilder.New().Minimum(nameof(TestClass.NullUInt64Val)).Build();
                result = await serviceQuery.ExecuteAggregateAsync<T>(sourceQueryable);
                Assert.True(result == 0);
            }

            // UInt32
            serviceQuery = ServiceQueryBuilder.New().Minimum(nameof(TestClass.NullUInt32Val)).Build();
            result = await serviceQuery.ExecuteAggregateAsync<T>(sourceQueryable);
            Assert.True(result == 0);

            // UInt16
            serviceQuery = ServiceQueryBuilder.New().Minimum(nameof(TestClass.NullUInt16Val)).Build();
            result = await serviceQuery.ExecuteAggregateAsync<T>(sourceQueryable);
            Assert.True(result == 0);
        }

        [Fact]
        public async Task NullCopyMinimumRequestTest()
        {
            var sourceQueryable = await GetTestNullCopyListAsync();
            IServiceQueryRequest serviceQuery;
            double? result;

            // Boolean
            serviceQuery = ServiceQueryRequestBuilder.New().Minimum(nameof(TestClass.NullBoolVal)).Build();
            await Assert.ThrowsAsync<ServiceQueryException>(async () =>
            {
                result = await serviceQuery.GetServiceQuery().ExecuteAggregateAsync<T>(sourceQueryable);
            });
            await Assert.ThrowsAsync<ServiceQueryException>(async () =>
            {
                result = await serviceQuery.GetServiceQuery().ExecuteAggregateAsync<T>(sourceQueryable);
            });

            // ByteArray
            var tempbyteArray = new byte[1] { 1 };
            serviceQuery = ServiceQueryRequestBuilder.New().Minimum(nameof(TestClass.NullByteArrayVal)).Build();
            await Assert.ThrowsAsync<ServiceQueryException>(async () =>
            {
                result = await serviceQuery.GetServiceQuery().ExecuteAggregateAsync<T>(sourceQueryable);
            });

            // Byte
            serviceQuery = ServiceQueryRequestBuilder.New().Minimum(nameof(TestClass.NullByteVal)).Build();
            await Assert.ThrowsAsync<ServiceQueryException>(async () =>
            {
                result = await serviceQuery.GetServiceQuery().ExecuteAggregateAsync<T>(sourceQueryable);
            });

            // Char
            serviceQuery = ServiceQueryRequestBuilder.New().Minimum(nameof(TestClass.NullCharVal)).Build();
            await Assert.ThrowsAsync<ServiceQueryException>(async () =>
            {
                result = await serviceQuery.GetServiceQuery().ExecuteAggregateAsync<T>(sourceQueryable);
            });

            if (ValidateDateTimeOffset)
            {
                // DateTimeOffset
                serviceQuery = ServiceQueryRequestBuilder.New().Minimum(nameof(TestClass.NullDateTimeOffsetVal)).Build();
                await Assert.ThrowsAsync<ServiceQueryException>(async () =>
                {
                    result = await serviceQuery.GetServiceQuery().ExecuteAggregateAsync<T>(sourceQueryable);
                });
            }

#if NET6_0_OR_GREATER
            // DateOnly
            serviceQuery = ServiceQueryRequestBuilder.New().Minimum(nameof(TestClass.NullDateOnlyVal)).Build();
            await Assert.ThrowsAsync<ServiceQueryException>(async () =>
            {
                result = await serviceQuery.GetServiceQuery().ExecuteAggregateAsync<T>(sourceQueryable);
            });
#endif

            // DateTime
            serviceQuery = ServiceQueryRequestBuilder.New().Minimum(nameof(TestClass.NullDateTimeVal)).Build();
            await Assert.ThrowsAsync<ServiceQueryException>(async () =>
            {
                result = await serviceQuery.GetServiceQuery().ExecuteAggregateAsync<T>(sourceQueryable);
            });

            // Decimal
            if (ValidateDecimal)
            {
                serviceQuery = ServiceQueryRequestBuilder.New().Minimum(nameof(TestClass.NullDecimalVal)).Build();
                result = await serviceQuery.GetServiceQuery().ExecuteAggregateAsync<T>(sourceQueryable);
                Assert.True(result == 0);
            }

            // Double
            serviceQuery = ServiceQueryRequestBuilder.New().Minimum(nameof(TestClass.NullDoubleVal)).Build();
            result = await serviceQuery.GetServiceQuery().ExecuteAggregateAsync<T>(sourceQueryable);
            Assert.True(result == 0);
            result = await serviceQuery.GetServiceQuery().ExecuteAggregateAsync<T>(sourceQueryable);
            Assert.True(result == 0);

            // Float
            serviceQuery = ServiceQueryRequestBuilder.New().Minimum(nameof(TestClass.NullFloatVal)).Build();
            result = await serviceQuery.GetServiceQuery().ExecuteAggregateAsync<T>(sourceQueryable);
            Assert.True(result == 0);

            // Guid
            serviceQuery = ServiceQueryRequestBuilder.New().Minimum(nameof(TestClass.NullGuidVal)).Build();
            await Assert.ThrowsAsync<ServiceQueryException>(async () =>
            {
                result = await serviceQuery.GetServiceQuery().ExecuteAggregateAsync<T>(sourceQueryable);
            });

            // Int
            serviceQuery = ServiceQueryRequestBuilder.New().Minimum(nameof(TestClass.NullIntVal)).Build();
            result = await serviceQuery.GetServiceQuery().ExecuteAggregateAsync<T>(sourceQueryable);
            Assert.True(result == 0);

            // Long
            serviceQuery = ServiceQueryRequestBuilder.New().Minimum(nameof(TestClass.NullLongVal)).Build();
            result = await serviceQuery.GetServiceQuery().ExecuteAggregateAsync<T>(sourceQueryable);
            Assert.True(result == 0);

            // SByte
            serviceQuery = ServiceQueryRequestBuilder.New().Minimum(nameof(TestClass.NullSByteVal)).Build();
            result = await serviceQuery.GetServiceQuery().ExecuteAggregateAsync<T>(sourceQueryable);
            Assert.True(result == 0);

            // Short
            serviceQuery = ServiceQueryRequestBuilder.New().Minimum(nameof(TestClass.NullShortVal)).Build();
            result = await serviceQuery.GetServiceQuery().ExecuteAggregateAsync<T>(sourceQueryable);
            Assert.True(result == 0);

            // Single
            serviceQuery = ServiceQueryRequestBuilder.New().Minimum(nameof(TestClass.NullSingleVal)).Build();
            result = await serviceQuery.GetServiceQuery().ExecuteAggregateAsync<T>(sourceQueryable);
            Assert.True(result == 0);

            // String
            serviceQuery = ServiceQueryRequestBuilder.New().Minimum(nameof(TestClass.NullStringVal)).Build();
            await Assert.ThrowsAsync<ServiceQueryException>(async () =>
            {
                result = await serviceQuery.GetServiceQuery().ExecuteAggregateAsync<T>(sourceQueryable);
            });

#if NET6_0_OR_GREATER
            if (ValidateTimeOnly)
            {
                // TimeOnly
                serviceQuery = ServiceQueryRequestBuilder.New().Minimum(nameof(TestClass.NullTimeOnlyVal)).Build();
                await Assert.ThrowsAsync<ServiceQueryException>(async () =>
                {
                    result = await serviceQuery.GetServiceQuery().ExecuteAggregateAsync<T>(sourceQueryable);
                });
            }
#endif

            if (ValidateTimeSpan)
            {
                // TimeSpan
                serviceQuery = ServiceQueryRequestBuilder.New().Minimum(nameof(TestClass.NullTimeSpanVal)).Build();
                await Assert.ThrowsAsync<ServiceQueryException>(async () =>
                {
                    result = await serviceQuery.GetServiceQuery().ExecuteAggregateAsync<T>(sourceQueryable);
                });
            }

#if NET7_0_OR_GREATER
            if (ValidateUInt128)
            {
                // UInt128
                serviceQuery = ServiceQueryRequestBuilder.New().Minimum(nameof(TestClass.NullUInt128Val)).Build();
                result = await serviceQuery.GetServiceQuery().ExecuteAggregateAsync<T>(sourceQueryable);
                Assert.True(result == 0);
            }
#endif

            if (ValidateUInt64)
            {
                // UInt64
                serviceQuery = ServiceQueryRequestBuilder.New().Minimum(nameof(TestClass.NullUInt64Val)).Build();
                result = await serviceQuery.GetServiceQuery().ExecuteAggregateAsync<T>(sourceQueryable);
                Assert.True(result == 0);
            }

            // UInt32
            serviceQuery = ServiceQueryRequestBuilder.New().Minimum(nameof(TestClass.NullUInt32Val)).Build();
            result = await serviceQuery.GetServiceQuery().ExecuteAggregateAsync<T>(sourceQueryable);
            Assert.True(result == 0);

            // UInt16
            serviceQuery = ServiceQueryRequestBuilder.New().Minimum(nameof(TestClass.NullUInt16Val)).Build();
            result = await serviceQuery.GetServiceQuery().ExecuteAggregateAsync<T>(sourceQueryable);
            Assert.True(result == 0);
        }
    }
}
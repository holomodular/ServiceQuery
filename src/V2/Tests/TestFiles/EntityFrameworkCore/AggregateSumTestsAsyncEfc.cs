namespace ServiceQuery.Xunit
{
    public class AggregateSumTestsAsyncEfc : AggregateSumTestsAsyncEfc<TestClass>
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

    public abstract class AggregateSumTestsAsyncEfc<T> : BaseTest<T> where T : class
    {
        public bool NullSumIsNull = false;
        public bool CosmosSequenceNoElementsError = false;

        [Fact]
        public async Task SumStandardTestAsync()
        {
            var sourceQueryable = await GetTestListAsync();
            IServiceQuery serviceQuery;
            double? result;

            // Boolean
            serviceQuery = ServiceQueryBuilder.New().Sum(nameof(TestClass.BoolVal)).Build();
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
            serviceQuery = ServiceQueryBuilder.New().Sum(nameof(TestClass.ByteArrayVal)).Build();
            await Assert.ThrowsAsync<ServiceQueryException>(async () =>
            {
                result = await serviceQuery.ExecuteAggregateAsync<T>(sourceQueryable);
            });

            // Byte
            serviceQuery = ServiceQueryBuilder.New().Sum(nameof(TestClass.ByteVal)).Build();
            await Assert.ThrowsAsync<ServiceQueryException>(async () =>
            {
                result = await serviceQuery.ExecuteAggregateAsync<T>(sourceQueryable);
            });

            // Char
            serviceQuery = ServiceQueryBuilder.New().Sum(nameof(TestClass.CharVal)).Build();
            await Assert.ThrowsAsync<ServiceQueryException>(async () =>
            {
                result = await serviceQuery.ExecuteAggregateAsync<T>(sourceQueryable);
            });

            if (ValidateDateTimeOffset)
            {
                // DateTimeOffset
                serviceQuery = ServiceQueryBuilder.New().Sum(nameof(TestClass.DateTimeOffsetVal)).Build();
                await Assert.ThrowsAsync<ServiceQueryException>(async () =>
                {
                    result = await serviceQuery.ExecuteAggregateAsync<T>(sourceQueryable);
                });
            }

#if NET6_0_OR_GREATER
            // DateOnly
            serviceQuery = ServiceQueryBuilder.New().Sum(nameof(TestClass.DateOnlyVal)).Build();
            await Assert.ThrowsAsync<ServiceQueryException>(async () =>
            {
                result = await serviceQuery.ExecuteAggregateAsync<T>(sourceQueryable);
            });
#endif

            // DateTime
            serviceQuery = ServiceQueryBuilder.New().Sum(nameof(TestClass.DateTimeVal)).Build();
            await Assert.ThrowsAsync<ServiceQueryException>(async () =>
            {
                result = await serviceQuery.ExecuteAggregateAsync<T>(sourceQueryable);
            });

            // Decimal
            if (ValidateDecimal)
            {
                serviceQuery = ServiceQueryBuilder.New().Sum(nameof(TestClass.DecimalVal)).Build();
                result = await serviceQuery.ExecuteAggregateAsync<T>(sourceQueryable);
                Assert.True(result == 6);
            }

            // Double
            serviceQuery = ServiceQueryBuilder.New().Sum(nameof(TestClass.DoubleVal)).Build();
            result = await serviceQuery.ExecuteAggregateAsync<T>(sourceQueryable);
            Assert.True(result == 6);
            result = await serviceQuery.ExecuteAggregateAsync<T>(sourceQueryable);
            Assert.True(result == 6);

            // Float
            serviceQuery = ServiceQueryBuilder.New().Sum(nameof(TestClass.FloatVal)).Build();
            result = await serviceQuery.ExecuteAggregateAsync<T>(sourceQueryable);
            Assert.True(result == 6);

            // Guid
            serviceQuery = ServiceQueryBuilder.New().Sum(nameof(TestClass.GuidVal)).Build();
            await Assert.ThrowsAsync<ServiceQueryException>(async () =>
            {
                result = await serviceQuery.ExecuteAggregateAsync<T>(sourceQueryable);
            });

            // Int
            serviceQuery = ServiceQueryBuilder.New().Sum(nameof(TestClass.IntVal)).Build();
            result = await serviceQuery.ExecuteAggregateAsync<T>(sourceQueryable);
            Assert.True(result == 6);

            // Long
            serviceQuery = ServiceQueryBuilder.New().Sum(nameof(TestClass.LongVal)).Build();
            result = await serviceQuery.ExecuteAggregateAsync<T>(sourceQueryable);
            Assert.True(result == 6);

            // SByte
            serviceQuery = ServiceQueryBuilder.New().Sum(nameof(TestClass.SByteVal)).Build();
            await Assert.ThrowsAsync<ServiceQueryException>(async () =>
            {
                result = await serviceQuery.ExecuteAggregateAsync<T>(sourceQueryable);
            });

            // Short
            serviceQuery = ServiceQueryBuilder.New().Sum(nameof(TestClass.ShortVal)).Build();
            await Assert.ThrowsAsync<ServiceQueryException>(async () =>
            {
                result = await serviceQuery.ExecuteAggregateAsync<T>(sourceQueryable);
            });

            // Single
            serviceQuery = ServiceQueryBuilder.New().Sum(nameof(TestClass.SingleVal)).Build();
            result = await serviceQuery.ExecuteAggregateAsync<T>(sourceQueryable);
            Assert.True(result == 6);

            // String
            serviceQuery = ServiceQueryBuilder.New().Sum(nameof(TestClass.StringVal)).Build();
            await Assert.ThrowsAsync<ServiceQueryException>(async () =>
            {
                result = await serviceQuery.ExecuteAggregateAsync<T>(sourceQueryable);
            });

#if NET6_0_OR_GREATER
            // TimeOnly
            serviceQuery = ServiceQueryBuilder.New().Sum(nameof(TestClass.TimeOnlyVal)).Build();
            await Assert.ThrowsAsync<ServiceQueryException>(async () =>
            {
                result = await serviceQuery.ExecuteAggregateAsync<T>(sourceQueryable);
            });
#endif

            if (ValidateTimeSpan)
            {
                // TimeSpan
                serviceQuery = ServiceQueryBuilder.New().Sum(nameof(TestClass.TimeSpanVal)).Build();
                await Assert.ThrowsAsync<ServiceQueryException>(async () =>
                {
                    result = await serviceQuery.ExecuteAggregateAsync<T>(sourceQueryable);
                });
            }
#if NET7_0_OR_GREATER
            if (ValidateUInt128)
            {
                // UInt128
                serviceQuery = ServiceQueryBuilder.New().Sum(nameof(TestClass.UInt128Val)).Build();
                await Assert.ThrowsAsync<ServiceQueryException>(async () =>
                {
                    result = await serviceQuery.ExecuteAggregateAsync<T>(sourceQueryable);
                });
            }
#endif
            if (ValidateUInt64)
            {
                // UInt64
                serviceQuery = ServiceQueryBuilder.New().Sum(nameof(TestClass.UInt64Val)).Build();
                await Assert.ThrowsAsync<ServiceQueryException>(async () =>
                {
                    result = await serviceQuery.ExecuteAggregateAsync<T>(sourceQueryable);
                });
            }

            // UInt32
            serviceQuery = ServiceQueryBuilder.New().Sum(nameof(TestClass.UInt32Val)).Build();
            await Assert.ThrowsAsync<ServiceQueryException>(async () =>
            {
                result = await serviceQuery.ExecuteAggregateAsync<T>(sourceQueryable);
            });

            // UInt16
            serviceQuery = ServiceQueryBuilder.New().Sum(nameof(TestClass.UInt16Val)).Build();
            await Assert.ThrowsAsync<ServiceQueryException>(async () =>
            {
                result = await serviceQuery.ExecuteAggregateAsync<T>(sourceQueryable);
            });
        }

        [Fact]
        public async Task SumRequestTest()
        {
            var sourceQueryable = await GetTestListAsync();
            IServiceQueryRequest serviceQuery;
            double? result;

            // Boolean
            serviceQuery = ServiceQueryRequestBuilder.New().Sum(nameof(TestClass.BoolVal)).Build();
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
            serviceQuery = ServiceQueryRequestBuilder.New().Sum(nameof(TestClass.ByteArrayVal)).Build();
            await Assert.ThrowsAsync<ServiceQueryException>(async () =>
            {
                result = await serviceQuery.GetServiceQuery().ExecuteAggregateAsync<T>(sourceQueryable);
            });

            // Byte
            serviceQuery = ServiceQueryRequestBuilder.New().Sum(nameof(TestClass.ByteVal)).Build();
            await Assert.ThrowsAsync<ServiceQueryException>(async () =>
            {
                result = await serviceQuery.GetServiceQuery().ExecuteAggregateAsync<T>(sourceQueryable);
            });

            // Char
            serviceQuery = ServiceQueryRequestBuilder.New().Sum(nameof(TestClass.CharVal)).Build();
            await Assert.ThrowsAsync<ServiceQueryException>(async () =>
            {
                result = await serviceQuery.GetServiceQuery().ExecuteAggregateAsync<T>(sourceQueryable);
            });

            if (ValidateDateTimeOffset)
            {
                // DateTimeOffset
                serviceQuery = ServiceQueryRequestBuilder.New().Sum(nameof(TestClass.DateTimeOffsetVal)).Build();
                await Assert.ThrowsAsync<ServiceQueryException>(async () =>
                {
                    result = await serviceQuery.GetServiceQuery().ExecuteAggregateAsync<T>(sourceQueryable);
                });
            }

#if NET6_0_OR_GREATER
            // DateOnly
            serviceQuery = ServiceQueryRequestBuilder.New().Sum(nameof(TestClass.DateOnlyVal)).Build();
            await Assert.ThrowsAsync<ServiceQueryException>(async () =>
            {
                result = await serviceQuery.GetServiceQuery().ExecuteAggregateAsync<T>(sourceQueryable);
            });
#endif

            // DateTime
            serviceQuery = ServiceQueryRequestBuilder.New().Sum(nameof(TestClass.DateTimeVal)).Build();
            await Assert.ThrowsAsync<ServiceQueryException>(async () =>
            {
                result = await serviceQuery.GetServiceQuery().ExecuteAggregateAsync<T>(sourceQueryable);
            });

            // Decimal
            if (ValidateDecimal)
            {
                serviceQuery = ServiceQueryRequestBuilder.New().Sum(nameof(TestClass.DecimalVal)).Build();
                result = await serviceQuery.GetServiceQuery().ExecuteAggregateAsync<T>(sourceQueryable);
                Assert.True(result == 6);
            }

            // Double
            serviceQuery = ServiceQueryRequestBuilder.New().Sum(nameof(TestClass.DoubleVal)).Build();
            result = await serviceQuery.GetServiceQuery().ExecuteAggregateAsync<T>(sourceQueryable);
            Assert.True(result == 6);
            result = await serviceQuery.GetServiceQuery().ExecuteAggregateAsync<T>(sourceQueryable);
            Assert.True(result == 6);

            // Float
            serviceQuery = ServiceQueryRequestBuilder.New().Sum(nameof(TestClass.FloatVal)).Build();
            result = await serviceQuery.GetServiceQuery().ExecuteAggregateAsync<T>(sourceQueryable);
            Assert.True(result == 6);

            // Guid
            serviceQuery = ServiceQueryRequestBuilder.New().Sum(nameof(TestClass.GuidVal)).Build();
            await Assert.ThrowsAsync<ServiceQueryException>(async () =>
            {
                result = await serviceQuery.GetServiceQuery().ExecuteAggregateAsync<T>(sourceQueryable);
            });

            // Int
            serviceQuery = ServiceQueryRequestBuilder.New().Sum(nameof(TestClass.IntVal)).Build();
            result = await serviceQuery.GetServiceQuery().ExecuteAggregateAsync<T>(sourceQueryable);
            Assert.True(result == 6);

            // Long
            serviceQuery = ServiceQueryRequestBuilder.New().Sum(nameof(TestClass.LongVal)).Build();
            result = await serviceQuery.GetServiceQuery().ExecuteAggregateAsync<T>(sourceQueryable);
            Assert.True(result == 6);

            // SByte
            serviceQuery = ServiceQueryRequestBuilder.New().Sum(nameof(TestClass.SByteVal)).Build();
            await Assert.ThrowsAsync<ServiceQueryException>(async () =>
            {
                result = await serviceQuery.GetServiceQuery().ExecuteAggregateAsync<T>(sourceQueryable);
            });

            // Short
            serviceQuery = ServiceQueryRequestBuilder.New().Sum(nameof(TestClass.ShortVal)).Build();
            await Assert.ThrowsAsync<ServiceQueryException>(async () =>
            {
                result = await serviceQuery.GetServiceQuery().ExecuteAggregateAsync<T>(sourceQueryable);
            });

            // Single
            serviceQuery = ServiceQueryRequestBuilder.New().Sum(nameof(TestClass.SingleVal)).Build();
            result = await serviceQuery.GetServiceQuery().ExecuteAggregateAsync<T>(sourceQueryable);
            Assert.True(result == 6);

            // String
            serviceQuery = ServiceQueryRequestBuilder.New().Sum(nameof(TestClass.StringVal)).Build();
            await Assert.ThrowsAsync<ServiceQueryException>(async () =>
            {
                result = await serviceQuery.GetServiceQuery().ExecuteAggregateAsync<T>(sourceQueryable);
            });

#if NET6_0_OR_GREATER
            // TimeOnly
            serviceQuery = ServiceQueryRequestBuilder.New().Sum(nameof(TestClass.TimeOnlyVal)).Build();
            await Assert.ThrowsAsync<ServiceQueryException>(async () =>
            {
                result = await serviceQuery.GetServiceQuery().ExecuteAggregateAsync<T>(sourceQueryable);
            });
#endif

            if (ValidateTimeSpan)
            {
                // TimeSpan
                serviceQuery = ServiceQueryRequestBuilder.New().Sum(nameof(TestClass.TimeSpanVal)).Build();
                await Assert.ThrowsAsync<ServiceQueryException>(async () =>
                {
                    result = await serviceQuery.GetServiceQuery().ExecuteAggregateAsync<T>(sourceQueryable);
                });
            }
#if NET7_0_OR_GREATER
            if (ValidateUInt128)
            {
                // UInt128
                serviceQuery = ServiceQueryRequestBuilder.New().Sum(nameof(TestClass.UInt128Val)).Build();
                await Assert.ThrowsAsync<ServiceQueryException>(async () =>
                {
                    result = await serviceQuery.GetServiceQuery().ExecuteAggregateAsync<T>(sourceQueryable);
                });
            }
#endif
            if (ValidateUInt64)
            {
                // UInt64
                serviceQuery = ServiceQueryRequestBuilder.New().Sum(nameof(TestClass.UInt64Val)).Build();
                await Assert.ThrowsAsync<ServiceQueryException>(async () =>
                {
                    result = await serviceQuery.GetServiceQuery().ExecuteAggregateAsync<T>(sourceQueryable);
                });
            }

            // UInt32
            serviceQuery = ServiceQueryRequestBuilder.New().Sum(nameof(TestClass.UInt32Val)).Build();
            await Assert.ThrowsAsync<ServiceQueryException>(async () =>
            {
                result = await serviceQuery.GetServiceQuery().ExecuteAggregateAsync<T>(sourceQueryable);
            });

            // UInt16
            serviceQuery = ServiceQueryRequestBuilder.New().Sum(nameof(TestClass.UInt16Val)).Build();
            await Assert.ThrowsAsync<ServiceQueryException>(async () =>
            {
                result = await serviceQuery.GetServiceQuery().ExecuteAggregateAsync<T>(sourceQueryable);
            });
        }

        [Fact]
        public async Task NullSumStandardTest()
        {
            var sourceQueryable = await GetTestListAsync();
            IServiceQuery serviceQuery;
            double? result;

            // Boolean
            serviceQuery = ServiceQueryBuilder.New().Sum(nameof(TestClass.NullBoolVal)).Build();
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
            serviceQuery = ServiceQueryBuilder.New().Sum(nameof(TestClass.NullByteArrayVal)).Build();
            await Assert.ThrowsAsync<ServiceQueryException>(async () =>
            {
                result = await serviceQuery.ExecuteAggregateAsync<T>(sourceQueryable);
            });

            // Byte
            serviceQuery = ServiceQueryBuilder.New().Sum(nameof(TestClass.NullByteVal)).Build();
            await Assert.ThrowsAsync<ServiceQueryException>(async () =>
            {
                result = await serviceQuery.ExecuteAggregateAsync<T>(sourceQueryable);
            });

            // Char
            serviceQuery = ServiceQueryBuilder.New().Sum(nameof(TestClass.NullCharVal)).Build();
            await Assert.ThrowsAsync<ServiceQueryException>(async () =>
            {
                result = await serviceQuery.ExecuteAggregateAsync<T>(sourceQueryable);
            });

            if (ValidateDateTimeOffset)
            {
                // DateTimeOffset
                serviceQuery = ServiceQueryBuilder.New().Sum(nameof(TestClass.NullDateTimeOffsetVal)).Build();
                await Assert.ThrowsAsync<ServiceQueryException>(async () =>
                {
                    result = await serviceQuery.ExecuteAggregateAsync<T>(sourceQueryable);
                });
            }

#if NET6_0_OR_GREATER
            // DateOnly
            serviceQuery = ServiceQueryBuilder.New().Sum(nameof(TestClass.NullDateOnlyVal)).Build();
            await Assert.ThrowsAsync<ServiceQueryException>(async () =>
            {
                result = await serviceQuery.ExecuteAggregateAsync<T>(sourceQueryable);
            });
#endif

            // DateTime
            serviceQuery = ServiceQueryBuilder.New().Sum(nameof(TestClass.NullDateTimeVal)).Build();
            await Assert.ThrowsAsync<ServiceQueryException>(async () =>
            {
                result = await serviceQuery.ExecuteAggregateAsync<T>(sourceQueryable);
            });

            // Decimal
            if (ValidateDecimal)
            {
                if (CosmosSequenceNoElementsError)
                {
                    await Assert.ThrowsAsync<ServiceQueryException>(async () =>
                    {
                        serviceQuery = ServiceQueryBuilder.New().Sum(nameof(TestClass.NullDecimalVal)).Build();
                        result = await serviceQuery.ExecuteAggregateAsync<T>(sourceQueryable);
                    });
                }
                else
                {
                    serviceQuery = ServiceQueryBuilder.New().Sum(nameof(TestClass.NullDecimalVal)).Build();
                    result = await serviceQuery.ExecuteAggregateAsync<T>(sourceQueryable);
                    if (NullSumIsNull)
                        Assert.True(result == null);
                    else
                        Assert.True(result == 0);
                }
            }

            // Double
            if (CosmosSequenceNoElementsError)
            {
                await Assert.ThrowsAsync<ServiceQueryException>(async () =>
                {
                    serviceQuery = ServiceQueryBuilder.New().Sum(nameof(TestClass.NullDoubleVal)).Build();
                    result = await serviceQuery.ExecuteAggregateAsync<T>(sourceQueryable);
                });
            }
            else
            {
                serviceQuery = ServiceQueryBuilder.New().Sum(nameof(TestClass.NullDoubleVal)).Build();
                result = await serviceQuery.ExecuteAggregateAsync<T>(sourceQueryable);
                if (NullSumIsNull)
                    Assert.True(result == null);
                else
                    Assert.True(result == 0);
                result = await serviceQuery.ExecuteAggregateAsync<T>(sourceQueryable);
                if (NullSumIsNull)
                    Assert.True(result == null);
                else
                    Assert.True(result == 0);
            }

            // Float
            if (CosmosSequenceNoElementsError)
            {
                await Assert.ThrowsAsync<ServiceQueryException>(async () =>
                {
                    serviceQuery = ServiceQueryBuilder.New().Sum(nameof(TestClass.NullFloatVal)).Build();
                    result = await serviceQuery.ExecuteAggregateAsync<T>(sourceQueryable);
                });
            }
            else
            {
                serviceQuery = ServiceQueryBuilder.New().Sum(nameof(TestClass.NullFloatVal)).Build();
                result = await serviceQuery.ExecuteAggregateAsync<T>(sourceQueryable);
                if (NullSumIsNull)
                    Assert.True(result == null);
                else
                    Assert.True(result == 0);
            }
            // Guid
            serviceQuery = ServiceQueryBuilder.New().Sum(nameof(TestClass.NullGuidVal)).Build();
            await Assert.ThrowsAsync<ServiceQueryException>(async () =>
            {
                result = await serviceQuery.ExecuteAggregateAsync<T>(sourceQueryable);
            });

            // Int
            if (CosmosSequenceNoElementsError)
            {
                await Assert.ThrowsAsync<ServiceQueryException>(async () =>
                {
                    serviceQuery = ServiceQueryBuilder.New().Sum(nameof(TestClass.NullIntVal)).Build();
                    result = await serviceQuery.ExecuteAggregateAsync<T>(sourceQueryable);
                });
            }
            else
            {
                serviceQuery = ServiceQueryBuilder.New().Sum(nameof(TestClass.NullIntVal)).Build();
                result = await serviceQuery.ExecuteAggregateAsync<T>(sourceQueryable);
                if (NullSumIsNull)
                    Assert.True(result == null);
                else
                    Assert.True(result == 0);
            }

            // Long
            if (CosmosSequenceNoElementsError)
            {
                await Assert.ThrowsAsync<ServiceQueryException>(async () =>
                {
                    serviceQuery = ServiceQueryBuilder.New().Sum(nameof(TestClass.NullLongVal)).Build();
                    result = await serviceQuery.ExecuteAggregateAsync<T>(sourceQueryable);
                });
            }
            else
            {
                serviceQuery = ServiceQueryBuilder.New().Sum(nameof(TestClass.NullLongVal)).Build();
                result = await serviceQuery.ExecuteAggregateAsync<T>(sourceQueryable);
                if (NullSumIsNull)
                    Assert.True(result == null);
                else
                    Assert.True(result == 0);
            }

            // SByte
            serviceQuery = ServiceQueryBuilder.New().Sum(nameof(TestClass.NullSByteVal)).Build();
            await Assert.ThrowsAsync<ServiceQueryException>(async () =>
            {
                result = await serviceQuery.ExecuteAggregateAsync<T>(sourceQueryable);
            });

            // Short
            serviceQuery = ServiceQueryBuilder.New().Sum(nameof(TestClass.NullShortVal)).Build();
            await Assert.ThrowsAsync<ServiceQueryException>(async () =>
            {
                result = await serviceQuery.ExecuteAggregateAsync<T>(sourceQueryable);
            });

            // Single
            if (CosmosSequenceNoElementsError)
            {
                await Assert.ThrowsAsync<ServiceQueryException>(async () =>
                {
                    serviceQuery = ServiceQueryBuilder.New().Sum(nameof(TestClass.NullSingleVal)).Build();
                    result = await serviceQuery.ExecuteAggregateAsync<T>(sourceQueryable);
                });
            }
            else
            {
                serviceQuery = ServiceQueryBuilder.New().Sum(nameof(TestClass.NullSingleVal)).Build();
                result = await serviceQuery.ExecuteAggregateAsync<T>(sourceQueryable);
                if (NullSumIsNull)
                    Assert.True(result == null);
                else
                    Assert.True(result == 0);
            }

            // String
            serviceQuery = ServiceQueryBuilder.New().Sum(nameof(TestClass.NullStringVal)).Build();
            await Assert.ThrowsAsync<ServiceQueryException>(async () =>
            {
                result = await serviceQuery.ExecuteAggregateAsync<T>(sourceQueryable);
            });

#if NET6_0_OR_GREATER
            // TimeOnly
            serviceQuery = ServiceQueryBuilder.New().Sum(nameof(TestClass.NullTimeOnlyVal)).Build();
            await Assert.ThrowsAsync<ServiceQueryException>(async () =>
            {
                result = await serviceQuery.ExecuteAggregateAsync<T>(sourceQueryable);
            });
#endif

            if (ValidateTimeSpan)
            {
                // TimeSpan
                serviceQuery = ServiceQueryBuilder.New().Sum(nameof(TestClass.NullTimeSpanVal)).Build();
                await Assert.ThrowsAsync<ServiceQueryException>(async () =>
                {
                    result = await serviceQuery.ExecuteAggregateAsync<T>(sourceQueryable);
                });
            }
#if NET7_0_OR_GREATER
            if (ValidateUInt128)
            {
                // UInt128
                serviceQuery = ServiceQueryBuilder.New().Sum(nameof(TestClass.NullUInt128Val)).Build();
                await Assert.ThrowsAsync<ServiceQueryException>(async () =>
                {
                    result = await serviceQuery.ExecuteAggregateAsync<T>(sourceQueryable);
                });
            }
#endif
            if (ValidateUInt64)
            {
                // UInt64
                serviceQuery = ServiceQueryBuilder.New().Sum(nameof(TestClass.NullUInt64Val)).Build();
                await Assert.ThrowsAsync<ServiceQueryException>(async () =>
                {
                    result = await serviceQuery.ExecuteAggregateAsync<T>(sourceQueryable);
                });
            }

            // UInt32
            serviceQuery = ServiceQueryBuilder.New().Sum(nameof(TestClass.NullUInt32Val)).Build();
            await Assert.ThrowsAsync<ServiceQueryException>(async () =>
            {
                result = await serviceQuery.ExecuteAggregateAsync<T>(sourceQueryable);
            });

            // UInt16
            serviceQuery = ServiceQueryBuilder.New().Sum(nameof(TestClass.NullUInt16Val)).Build();
            await Assert.ThrowsAsync<ServiceQueryException>(async () =>
            {
                result = await serviceQuery.ExecuteAggregateAsync<T>(sourceQueryable);
            });
        }

        [Fact]
        public async Task NullSumRequestTest()
        {
            var sourceQueryable = await GetTestListAsync();
            IServiceQueryRequest serviceQuery;
            double? result;

            // Boolean
            serviceQuery = ServiceQueryRequestBuilder.New().Sum(nameof(TestClass.NullBoolVal)).Build();
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
            serviceQuery = ServiceQueryRequestBuilder.New().Sum(nameof(TestClass.NullByteArrayVal)).Build();
            await Assert.ThrowsAsync<ServiceQueryException>(async () =>
            {
                result = await serviceQuery.GetServiceQuery().ExecuteAggregateAsync<T>(sourceQueryable);
            });

            // Byte
            serviceQuery = ServiceQueryRequestBuilder.New().Sum(nameof(TestClass.NullByteVal)).Build();
            await Assert.ThrowsAsync<ServiceQueryException>(async () =>
            {
                result = await serviceQuery.GetServiceQuery().ExecuteAggregateAsync<T>(sourceQueryable);
            });

            // Char
            serviceQuery = ServiceQueryRequestBuilder.New().Sum(nameof(TestClass.NullCharVal)).Build();
            await Assert.ThrowsAsync<ServiceQueryException>(async () =>
            {
                result = await serviceQuery.GetServiceQuery().ExecuteAggregateAsync<T>(sourceQueryable);
            });

            if (ValidateDateTimeOffset)
            {
                // DateTimeOffset
                serviceQuery = ServiceQueryRequestBuilder.New().Sum(nameof(TestClass.NullDateTimeOffsetVal)).Build();
                await Assert.ThrowsAsync<ServiceQueryException>(async () =>
                {
                    result = await serviceQuery.GetServiceQuery().ExecuteAggregateAsync<T>(sourceQueryable);
                });
            }

#if NET6_0_OR_GREATER
            // DateOnly
            serviceQuery = ServiceQueryRequestBuilder.New().Sum(nameof(TestClass.NullDateOnlyVal)).Build();
            await Assert.ThrowsAsync<ServiceQueryException>(async () =>
            {
                result = await serviceQuery.GetServiceQuery().ExecuteAggregateAsync<T>(sourceQueryable);
            });
#endif

            // DateTime
            serviceQuery = ServiceQueryRequestBuilder.New().Sum(nameof(TestClass.NullDateTimeVal)).Build();
            await Assert.ThrowsAsync<ServiceQueryException>(async () =>
            {
                result = await serviceQuery.GetServiceQuery().ExecuteAggregateAsync<T>(sourceQueryable);
            });

            // Decimal
            if (ValidateDecimal)
            {
                if (CosmosSequenceNoElementsError)
                {
                    await Assert.ThrowsAsync<ServiceQueryException>(async () =>
                    {
                        serviceQuery = ServiceQueryRequestBuilder.New().Sum(nameof(TestClass.NullDecimalVal)).Build();
                        result = await serviceQuery.GetServiceQuery().ExecuteAggregateAsync<T>(sourceQueryable);
                    });
                }
                else
                {
                    serviceQuery = ServiceQueryRequestBuilder.New().Sum(nameof(TestClass.NullDecimalVal)).Build();
                    result = await serviceQuery.GetServiceQuery().ExecuteAggregateAsync<T>(sourceQueryable);
                    if (NullSumIsNull)
                        Assert.True(result == null);
                    else
                        Assert.True(result == 0);
                }
            }

            // Double
            if (CosmosSequenceNoElementsError)
            {
                await Assert.ThrowsAsync<ServiceQueryException>(async () =>
                {
                    serviceQuery = ServiceQueryRequestBuilder.New().Sum(nameof(TestClass.NullDoubleVal)).Build();
                    result = await serviceQuery.GetServiceQuery().ExecuteAggregateAsync<T>(sourceQueryable);
                });
            }
            else
            {
                serviceQuery = ServiceQueryRequestBuilder.New().Sum(nameof(TestClass.NullDoubleVal)).Build();
                result = await serviceQuery.GetServiceQuery().ExecuteAggregateAsync<T>(sourceQueryable);
                if (NullSumIsNull)
                    Assert.True(result == null);
                else
                    Assert.True(result == 0);
                result = await serviceQuery.GetServiceQuery().ExecuteAggregateAsync<T>(sourceQueryable);
                if (NullSumIsNull)
                    Assert.True(result == null);
                else
                    Assert.True(result == 0);
            }

            // Float
            if (CosmosSequenceNoElementsError)
            {
                await Assert.ThrowsAsync<ServiceQueryException>(async () =>
                {
                    serviceQuery = ServiceQueryRequestBuilder.New().Sum(nameof(TestClass.NullFloatVal)).Build();
                    result = await serviceQuery.GetServiceQuery().ExecuteAggregateAsync<T>(sourceQueryable);
                });
            }
            else
            {
                serviceQuery = ServiceQueryRequestBuilder.New().Sum(nameof(TestClass.NullFloatVal)).Build();
                result = await serviceQuery.GetServiceQuery().ExecuteAggregateAsync<T>(sourceQueryable);
                if (NullSumIsNull)
                    Assert.True(result == null);
                else
                    Assert.True(result == 0);
            }

            // Guid
            serviceQuery = ServiceQueryRequestBuilder.New().Sum(nameof(TestClass.NullGuidVal)).Build();
            await Assert.ThrowsAsync<ServiceQueryException>(async () =>
            {
                result = await serviceQuery.GetServiceQuery().ExecuteAggregateAsync<T>(sourceQueryable);
            });

            // Int
            if (CosmosSequenceNoElementsError)
            {
                await Assert.ThrowsAsync<ServiceQueryException>(async () =>
                {
                    serviceQuery = ServiceQueryRequestBuilder.New().Sum(nameof(TestClass.NullIntVal)).Build();
                    result = await serviceQuery.GetServiceQuery().ExecuteAggregateAsync<T>(sourceQueryable);
                });
            }
            else
            {
                serviceQuery = ServiceQueryRequestBuilder.New().Sum(nameof(TestClass.NullIntVal)).Build();
                result = await serviceQuery.GetServiceQuery().ExecuteAggregateAsync<T>(sourceQueryable);
                if (NullSumIsNull)
                    Assert.True(result == null);
                else
                    Assert.True(result == 0);
            }

            // Long
            if (CosmosSequenceNoElementsError)
            {
                await Assert.ThrowsAsync<ServiceQueryException>(async () =>
                {
                    serviceQuery = ServiceQueryRequestBuilder.New().Sum(nameof(TestClass.NullLongVal)).Build();
                    result = await serviceQuery.GetServiceQuery().ExecuteAggregateAsync<T>(sourceQueryable);
                });
            }
            else
            {
                serviceQuery = ServiceQueryRequestBuilder.New().Sum(nameof(TestClass.NullLongVal)).Build();
                result = await serviceQuery.GetServiceQuery().ExecuteAggregateAsync<T>(sourceQueryable);
                if (NullSumIsNull)
                    Assert.True(result == null);
                else
                    Assert.True(result == 0);
            }

            // SByte
            serviceQuery = ServiceQueryRequestBuilder.New().Sum(nameof(TestClass.NullSByteVal)).Build();
            await Assert.ThrowsAsync<ServiceQueryException>(async () =>
            {
                result = await serviceQuery.GetServiceQuery().ExecuteAggregateAsync<T>(sourceQueryable);
            });

            // Short
            serviceQuery = ServiceQueryRequestBuilder.New().Sum(nameof(TestClass.NullShortVal)).Build();
            await Assert.ThrowsAsync<ServiceQueryException>(async () =>
            {
                result = await serviceQuery.GetServiceQuery().ExecuteAggregateAsync<T>(sourceQueryable);
            });

            // Single
            if (CosmosSequenceNoElementsError)
            {
                await Assert.ThrowsAsync<ServiceQueryException>(async () =>
                {
                    serviceQuery = ServiceQueryRequestBuilder.New().Sum(nameof(TestClass.NullSingleVal)).Build();
                    result = await serviceQuery.GetServiceQuery().ExecuteAggregateAsync<T>(sourceQueryable);
                });
            }
            else
            {
                serviceQuery = ServiceQueryRequestBuilder.New().Sum(nameof(TestClass.NullSingleVal)).Build();
                result = await serviceQuery.GetServiceQuery().ExecuteAggregateAsync<T>(sourceQueryable);
                if (NullSumIsNull)
                    Assert.True(result == null);
                else
                    Assert.True(result == 0);
            }

            // String
            serviceQuery = ServiceQueryRequestBuilder.New().Sum(nameof(TestClass.NullStringVal)).Build();
            await Assert.ThrowsAsync<ServiceQueryException>(async () =>
            {
                result = await serviceQuery.GetServiceQuery().ExecuteAggregateAsync<T>(sourceQueryable);
            });

#if NET6_0_OR_GREATER
            // TimeOnly
            serviceQuery = ServiceQueryRequestBuilder.New().Sum(nameof(TestClass.NullTimeOnlyVal)).Build();
            await Assert.ThrowsAsync<ServiceQueryException>(async () =>
            {
                result = await serviceQuery.GetServiceQuery().ExecuteAggregateAsync<T>(sourceQueryable);
            });
#endif

            if (ValidateTimeSpan)
            {
                // TimeSpan
                serviceQuery = ServiceQueryRequestBuilder.New().Sum(nameof(TestClass.NullTimeSpanVal)).Build();
                await Assert.ThrowsAsync<ServiceQueryException>(async () =>
                {
                    result = await serviceQuery.GetServiceQuery().ExecuteAggregateAsync<T>(sourceQueryable);
                });
            }
#if NET7_0_OR_GREATER
            if (ValidateUInt128)
            {
                // UInt128
                serviceQuery = ServiceQueryRequestBuilder.New().Sum(nameof(TestClass.NullUInt128Val)).Build();
                await Assert.ThrowsAsync<ServiceQueryException>(async () =>
                {
                    result = await serviceQuery.GetServiceQuery().ExecuteAggregateAsync<T>(sourceQueryable);
                });
            }
#endif
            if (ValidateUInt64)
            {
                // UInt64
                serviceQuery = ServiceQueryRequestBuilder.New().Sum(nameof(TestClass.NullUInt64Val)).Build();
                await Assert.ThrowsAsync<ServiceQueryException>(async () =>
                {
                    result = await serviceQuery.GetServiceQuery().ExecuteAggregateAsync<T>(sourceQueryable);
                });
            }

            // UInt32
            serviceQuery = ServiceQueryRequestBuilder.New().Sum(nameof(TestClass.NullUInt32Val)).Build();
            await Assert.ThrowsAsync<ServiceQueryException>(async () =>
            {
                result = await serviceQuery.GetServiceQuery().ExecuteAggregateAsync<T>(sourceQueryable);
            });

            // UInt16
            serviceQuery = ServiceQueryRequestBuilder.New().Sum(nameof(TestClass.NullUInt16Val)).Build();
            await Assert.ThrowsAsync<ServiceQueryException>(async () =>
            {
                result = await serviceQuery.GetServiceQuery().ExecuteAggregateAsync<T>(sourceQueryable);
            });
        }
    }
}
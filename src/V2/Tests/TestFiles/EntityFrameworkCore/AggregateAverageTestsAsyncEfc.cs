namespace ServiceQuery.Xunit
{
    public class AggregateAverageTestsAsyncEfc : AggregateAverageTestsAsyncEfc<TestClass>
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

    public abstract class AggregateAverageTestsAsyncEfc<T> : BaseTest<T> where T : class
    {
        public bool ValidateNullLong = true;

        public bool CosmosIntLongRounding = false;
        public bool CosmosSequenceNoElementsError = false;

        [Fact]
        public async Task AverageStandardTest()
        {
            IQueryable<T> sourceQueryable = await GetTestListAsync();
            IServiceQuery serviceQuery;
            double? result;

            // Boolean
            serviceQuery = ServiceQueryBuilder.New().Average(nameof(TestClass.BoolVal)).Build();
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
            serviceQuery = ServiceQueryBuilder.New().Average(nameof(TestClass.ByteArrayVal)).Build();
            await Assert.ThrowsAsync<ServiceQueryException>(async () =>
            {
                result = await serviceQuery.ExecuteAggregateAsync<T>(sourceQueryable);
            });
            await Assert.ThrowsAsync<ServiceQueryException>(async () =>
            {
                result = await serviceQuery.ExecuteAggregateAsync<T>(sourceQueryable);
            });

            // Byte
            serviceQuery = ServiceQueryBuilder.New().Average(nameof(TestClass.ByteVal)).Build();
            await Assert.ThrowsAsync<ServiceQueryException>(async () =>
            {
                result = await serviceQuery.ExecuteAggregateAsync<T>(sourceQueryable);
            });
            await Assert.ThrowsAsync<ServiceQueryException>(async () =>
            {
                result = await serviceQuery.ExecuteAggregateAsync<T>(sourceQueryable);
            });

            // Char
            serviceQuery = ServiceQueryBuilder.New().Average(nameof(TestClass.CharVal)).Build();
            await Assert.ThrowsAsync<ServiceQueryException>(async () =>
            {
                result = await serviceQuery.ExecuteAggregateAsync<T>(sourceQueryable);
            });
            await Assert.ThrowsAsync<ServiceQueryException>(async () =>
            {
                result = await serviceQuery.ExecuteAggregateAsync<T>(sourceQueryable);
            });

            if (ValidateDateTimeOffset)
            {
                // DateTimeOffset
                serviceQuery = ServiceQueryBuilder.New().Average(nameof(TestClass.DateTimeOffsetVal)).Build();
                await Assert.ThrowsAsync<ServiceQueryException>(async () =>
                {
                    result = await serviceQuery.ExecuteAggregateAsync<T>(sourceQueryable);
                });
                await Assert.ThrowsAsync<ServiceQueryException>(async () =>
                {
                    result = await serviceQuery.ExecuteAggregateAsync<T>(sourceQueryable);
                });
            }

#if NET6_0_OR_GREATER
            // DateOnly
            serviceQuery = ServiceQueryBuilder.New().Average(nameof(TestClass.DateOnlyVal)).Build();
            await Assert.ThrowsAsync<ServiceQueryException>(async () =>
            {
                result = await serviceQuery.ExecuteAggregateAsync<T>(sourceQueryable);
            });
            await Assert.ThrowsAsync<ServiceQueryException>(async () =>
            {
                result = await serviceQuery.ExecuteAggregateAsync<T>(sourceQueryable);
            });
#endif

            // DateTime
            serviceQuery = ServiceQueryBuilder.New().Average(nameof(TestClass.DateTimeVal)).Build();
            await Assert.ThrowsAsync<ServiceQueryException>(async () =>
            {
                result = await serviceQuery.ExecuteAggregateAsync<T>(sourceQueryable);
            });
            await Assert.ThrowsAsync<ServiceQueryException>(async () =>
            {
                result = await serviceQuery.ExecuteAggregateAsync<T>(sourceQueryable);
            });

            // Decimal
            if (ValidateDecimal)
            {
                serviceQuery = ServiceQueryBuilder.New().Average(nameof(TestClass.DecimalVal)).Build();
                result = await serviceQuery.ExecuteAggregateAsync<T>(sourceQueryable);
                Assert.True(result == (double)6 / 4);
                result = await serviceQuery.ExecuteAggregateAsync<T>(sourceQueryable);
                Assert.True(result == (double)6 / 4);
            }

            // Double
            serviceQuery = ServiceQueryBuilder.New().Average(nameof(TestClass.DoubleVal)).Build();
            result = await serviceQuery.ExecuteAggregateAsync<T>(sourceQueryable);
            Assert.True(result == (double)6 / 4);
            serviceQuery = ServiceQueryBuilder.New().Average(nameof(TestClass.DoubleVal)).Build();
            result = await serviceQuery.ExecuteAggregateAsync<T>(sourceQueryable);
            Assert.True(result == (double)6 / 4);

            // Float
            serviceQuery = ServiceQueryBuilder.New().Average(nameof(TestClass.FloatVal)).Build();
            result = await serviceQuery.ExecuteAggregateAsync<T>(sourceQueryable);
            Assert.True(result == (double)6 / 4);
            result = await serviceQuery.ExecuteAggregateAsync<T>(sourceQueryable);
            Assert.True(result == (double)6 / 4);

            // Guid
            serviceQuery = ServiceQueryBuilder.New().Average(nameof(TestClass.GuidVal)).Build();
            await Assert.ThrowsAsync<ServiceQueryException>(async () =>
            {
                result = await serviceQuery.ExecuteAggregateAsync<T>(sourceQueryable);
            });
            await Assert.ThrowsAsync<ServiceQueryException>(async () =>
            {
                result = await serviceQuery.ExecuteAggregateAsync<T>(sourceQueryable);
            });

            if (CosmosIntLongRounding)
            {
                // Int
                serviceQuery = ServiceQueryBuilder.New().Average(nameof(TestClass.IntVal)).Build();
                result = await serviceQuery.ExecuteAggregateAsync<T>(sourceQueryable);
                Assert.True(result == Math.Round((double)6 / 4));

                // Long
                serviceQuery = ServiceQueryBuilder.New().Average(nameof(TestClass.LongVal)).Build();
                result = await serviceQuery.ExecuteAggregateAsync<T>(sourceQueryable);
                Assert.True(result == Math.Round((double)6 / 4));
            }
            else
            {
                // Int
                serviceQuery = ServiceQueryBuilder.New().Average(nameof(TestClass.IntVal)).Build();
                result = await serviceQuery.ExecuteAggregateAsync<T>(sourceQueryable);
                Assert.True(result == (double)6 / 4);

                // Long
                serviceQuery = ServiceQueryBuilder.New().Average(nameof(TestClass.LongVal)).Build();
                result = await serviceQuery.ExecuteAggregateAsync<T>(sourceQueryable);
                Assert.True(result == (double)6 / 4);
            }

            // SByte
            serviceQuery = ServiceQueryBuilder.New().Average(nameof(TestClass.SByteVal)).Build();
            await Assert.ThrowsAsync<ServiceQueryException>(async () =>
            {
                result = await serviceQuery.ExecuteAggregateAsync<T>(sourceQueryable);
            });
            await Assert.ThrowsAsync<ServiceQueryException>(async () =>
            {
                result = await serviceQuery.ExecuteAggregateAsync<T>(sourceQueryable);
            });

            // Short
            serviceQuery = ServiceQueryBuilder.New().Average(nameof(TestClass.ShortVal)).Build();
            await Assert.ThrowsAsync<ServiceQueryException>(async () =>
            {
                result = await serviceQuery.ExecuteAggregateAsync<T>(sourceQueryable);
            });
            await Assert.ThrowsAsync<ServiceQueryException>(async () =>
            {
                result = await serviceQuery.ExecuteAggregateAsync<T>(sourceQueryable);
            });

            // Single
            serviceQuery = ServiceQueryBuilder.New().Average(nameof(TestClass.SingleVal)).Build();
            result = await serviceQuery.ExecuteAggregateAsync<T>(sourceQueryable);
            Assert.True(result == (double)6 / 4);

            // String
            serviceQuery = ServiceQueryBuilder.New().Average(nameof(TestClass.StringVal)).Build();
            await Assert.ThrowsAsync<ServiceQueryException>(async () =>
            {
                result = await serviceQuery.ExecuteAggregateAsync<T>(sourceQueryable);
            });
            await Assert.ThrowsAsync<ServiceQueryException>(async () =>
            {
                result = await serviceQuery.ExecuteAggregateAsync<T>(sourceQueryable);
            });

#if NET6_0_OR_GREATER
            // TimeOnly
            serviceQuery = ServiceQueryBuilder.New().Average(nameof(TestClass.TimeOnlyVal)).Build();
            await Assert.ThrowsAsync<ServiceQueryException>(async () =>
            {
                result = await serviceQuery.ExecuteAggregateAsync<T>(sourceQueryable);
            });
            await Assert.ThrowsAsync<ServiceQueryException>(async () =>
            {
                result = await serviceQuery.ExecuteAggregateAsync<T>(sourceQueryable);
            });
#endif

            if (ValidateTimeSpan)
            {
                // TimeSpan
                serviceQuery = ServiceQueryBuilder.New().Average(nameof(TestClass.TimeSpanVal)).Build();
                await Assert.ThrowsAsync<ServiceQueryException>(async () =>
                {
                    result = await serviceQuery.ExecuteAggregateAsync<T>(sourceQueryable);
                });
                await Assert.ThrowsAsync<ServiceQueryException>(async () =>
                {
                    result = await serviceQuery.ExecuteAggregateAsync<T>(sourceQueryable);
                });
            }

#if NET7_0_OR_GREATER
            if (ValidateUInt128)
            {
                // UInt128
                serviceQuery = ServiceQueryBuilder.New().Average(nameof(TestClass.UInt128Val)).Build();
                await Assert.ThrowsAsync<ServiceQueryException>(async () =>
                {
                    result = await serviceQuery.ExecuteAggregateAsync<T>(sourceQueryable);
                });
                await Assert.ThrowsAsync<ServiceQueryException>(async () =>
                {
                    result = await serviceQuery.ExecuteAggregateAsync<T>(sourceQueryable);
                });
            }
#endif

            if (ValidateUInt64)
            {
                // UInt64
                serviceQuery = ServiceQueryBuilder.New().Average(nameof(TestClass.UInt64Val)).Build();
                await Assert.ThrowsAsync<ServiceQueryException>(async () =>
                {
                    result = await serviceQuery.ExecuteAggregateAsync<T>(sourceQueryable);
                });
                await Assert.ThrowsAsync<ServiceQueryException>(async () =>
                {
                    result = await serviceQuery.ExecuteAggregateAsync<T>(sourceQueryable);
                });
            }

            // UInt32
            serviceQuery = ServiceQueryBuilder.New().Average(nameof(TestClass.UInt32Val)).Build();
            await Assert.ThrowsAsync<ServiceQueryException>(async () =>
            {
                result = await serviceQuery.ExecuteAggregateAsync<T>(sourceQueryable);
            });
            await Assert.ThrowsAsync<ServiceQueryException>(async () =>
            {
                result = await serviceQuery.ExecuteAggregateAsync<T>(sourceQueryable);
            });

            // UInt16
            serviceQuery = ServiceQueryBuilder.New().Average(nameof(TestClass.UInt16Val)).Build();
            await Assert.ThrowsAsync<ServiceQueryException>(async () =>
            {
                result = await serviceQuery.ExecuteAggregateAsync<T>(sourceQueryable);
            });
            await Assert.ThrowsAsync<ServiceQueryException>(async () =>
            {
                result = await serviceQuery.ExecuteAggregateAsync<T>(sourceQueryable);
            });
        }

        [Fact]
        public async Task AverageRequestTest()
        {
            var sourceQueryable = await GetTestListAsync();
            IServiceQueryRequest serviceQuery;
            double? result;

            // Boolean
            serviceQuery = ServiceQueryRequestBuilder.New().Average(nameof(TestClass.BoolVal)).Build();
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
            serviceQuery = ServiceQueryRequestBuilder.New().Average(nameof(TestClass.ByteArrayVal)).Build();
            await Assert.ThrowsAsync<ServiceQueryException>(async () =>
            {
                result = await serviceQuery.GetServiceQuery().ExecuteAggregateAsync<T>(sourceQueryable);
            });
            await Assert.ThrowsAsync<ServiceQueryException>(async () =>
            {
                result = await serviceQuery.GetServiceQuery().ExecuteAggregateAsync<T>(sourceQueryable);
            });

            // Byte
            serviceQuery = ServiceQueryRequestBuilder.New().Average(nameof(TestClass.ByteVal)).Build();
            await Assert.ThrowsAsync<ServiceQueryException>(async () =>
            {
                result = await serviceQuery.GetServiceQuery().ExecuteAggregateAsync<T>(sourceQueryable);
            });
            await Assert.ThrowsAsync<ServiceQueryException>(async () =>
            {
                result = await serviceQuery.GetServiceQuery().ExecuteAggregateAsync<T>(sourceQueryable);
            });

            // Char
            serviceQuery = ServiceQueryRequestBuilder.New().Average(nameof(TestClass.CharVal)).Build();
            await Assert.ThrowsAsync<ServiceQueryException>(async () =>
            {
                result = await serviceQuery.GetServiceQuery().ExecuteAggregateAsync<T>(sourceQueryable);
            });
            await Assert.ThrowsAsync<ServiceQueryException>(async () =>
            {
                result = await serviceQuery.GetServiceQuery().ExecuteAggregateAsync<T>(sourceQueryable);
            });

            if (ValidateDateTimeOffset)
            {
                // DateTimeOffset
                serviceQuery = ServiceQueryRequestBuilder.New().Average(nameof(TestClass.DateTimeOffsetVal)).Build();
                await Assert.ThrowsAsync<ServiceQueryException>(async () =>
                {
                    result = await serviceQuery.GetServiceQuery().ExecuteAggregateAsync<T>(sourceQueryable);
                });
                await Assert.ThrowsAsync<ServiceQueryException>(async () =>
                {
                    result = await serviceQuery.GetServiceQuery().ExecuteAggregateAsync<T>(sourceQueryable);
                });
            }

#if NET6_0_OR_GREATER

            // DateOnly
            serviceQuery = ServiceQueryRequestBuilder.New().Average(nameof(TestClass.DateOnlyVal)).Build();
            await Assert.ThrowsAsync<ServiceQueryException>(async () =>
            {
                result = await serviceQuery.GetServiceQuery().ExecuteAggregateAsync<T>(sourceQueryable);
            });
            await Assert.ThrowsAsync<ServiceQueryException>(async () =>
            {
                result = await serviceQuery.GetServiceQuery().ExecuteAggregateAsync<T>(sourceQueryable);
            });
#endif

            // DateTime
            serviceQuery = ServiceQueryRequestBuilder.New().Average(nameof(TestClass.DateTimeVal)).Build();
            await Assert.ThrowsAsync<ServiceQueryException>(async () =>
            {
                result = await serviceQuery.GetServiceQuery().ExecuteAggregateAsync<T>(sourceQueryable);
            });
            await Assert.ThrowsAsync<ServiceQueryException>(async () =>
            {
                result = await serviceQuery.GetServiceQuery().ExecuteAggregateAsync<T>(sourceQueryable);
            });

            // Decimal
            if (ValidateDecimal)
            {
                serviceQuery = ServiceQueryRequestBuilder.New().Average(nameof(TestClass.DecimalVal)).Build();
                result = await serviceQuery.GetServiceQuery().ExecuteAggregateAsync<T>(sourceQueryable);
                Assert.True(result == (double)6 / 4);
            }

            // Double
            serviceQuery = ServiceQueryRequestBuilder.New().Average(nameof(TestClass.DoubleVal)).Build();
            result = await serviceQuery.GetServiceQuery().ExecuteAggregateAsync<T>(sourceQueryable);
            Assert.True(result == (double)6 / 4);
            result = await serviceQuery.GetServiceQuery().ExecuteAggregateAsync<T>(sourceQueryable);
            Assert.True(result == (double)6 / 4);

            // Float
            serviceQuery = ServiceQueryRequestBuilder.New().Average(nameof(TestClass.FloatVal)).Build();
            result = await serviceQuery.GetServiceQuery().ExecuteAggregateAsync<T>(sourceQueryable);
            Assert.True(result == (double)6 / 4);

            // Guid
            serviceQuery = ServiceQueryRequestBuilder.New().Average(nameof(TestClass.GuidVal)).Build();
            await Assert.ThrowsAsync<ServiceQueryException>(async () =>
            {
                result = await serviceQuery.GetServiceQuery().ExecuteAggregateAsync<T>(sourceQueryable);
            });

            if (CosmosIntLongRounding)
            {
                // Int
                serviceQuery = ServiceQueryRequestBuilder.New().Average(nameof(TestClass.IntVal)).Build();
                result = await serviceQuery.GetServiceQuery().ExecuteAggregateAsync<T>(sourceQueryable);
                Assert.True(result == Math.Round((double)6 / 4));

                // Long
                serviceQuery = ServiceQueryRequestBuilder.New().Average(nameof(TestClass.LongVal)).Build();
                result = await serviceQuery.GetServiceQuery().ExecuteAggregateAsync<T>(sourceQueryable);
                Assert.True(result == Math.Round((double)6 / 4));
            }
            else
            {
                // Int
                serviceQuery = ServiceQueryRequestBuilder.New().Average(nameof(TestClass.IntVal)).Build();
                result = await serviceQuery.GetServiceQuery().ExecuteAggregateAsync<T>(sourceQueryable);
                Assert.True(result == (double)6 / 4);

                // Long
                serviceQuery = ServiceQueryRequestBuilder.New().Average(nameof(TestClass.LongVal)).Build();
                result = await serviceQuery.GetServiceQuery().ExecuteAggregateAsync<T>(sourceQueryable);
                Assert.True(result == (double)6 / 4);
            }

            // SByte
            serviceQuery = ServiceQueryRequestBuilder.New().Average(nameof(TestClass.SByteVal)).Build();
            await Assert.ThrowsAsync<ServiceQueryException>(async () =>
            {
                result = await serviceQuery.GetServiceQuery().ExecuteAggregateAsync<T>(sourceQueryable);
            });
            await Assert.ThrowsAsync<ServiceQueryException>(async () =>
            {
                result = await serviceQuery.GetServiceQuery().ExecuteAggregateAsync<T>(sourceQueryable);
            });

            // Short
            serviceQuery = ServiceQueryRequestBuilder.New().Average(nameof(TestClass.ShortVal)).Build();
            await Assert.ThrowsAsync<ServiceQueryException>(async () =>
            {
                result = await serviceQuery.GetServiceQuery().ExecuteAggregateAsync<T>(sourceQueryable);
            });
            await Assert.ThrowsAsync<ServiceQueryException>(async () =>
            {
                result = await serviceQuery.GetServiceQuery().ExecuteAggregateAsync<T>(sourceQueryable);
            });

            // Single
            serviceQuery = ServiceQueryRequestBuilder.New().Average(nameof(TestClass.SingleVal)).Build();
            result = await serviceQuery.GetServiceQuery().ExecuteAggregateAsync<T>(sourceQueryable);
            Assert.True(result == (double)6 / 4);

            // String
            serviceQuery = ServiceQueryRequestBuilder.New().Average(nameof(TestClass.StringVal)).Build();
            await Assert.ThrowsAsync<ServiceQueryException>(async () =>
            {
                result = await serviceQuery.GetServiceQuery().ExecuteAggregateAsync<T>(sourceQueryable);
            });
            await Assert.ThrowsAsync<ServiceQueryException>(async () =>
            {
                result = await serviceQuery.GetServiceQuery().ExecuteAggregateAsync<T>(sourceQueryable);
            });

#if NET6_0_OR_GREATER

            // TimeOnly
            serviceQuery = ServiceQueryRequestBuilder.New().Average(nameof(TestClass.TimeOnlyVal)).Build();
            await Assert.ThrowsAsync<ServiceQueryException>(async () =>
            {
                result = await serviceQuery.GetServiceQuery().ExecuteAggregateAsync<T>(sourceQueryable);
            });
            await Assert.ThrowsAsync<ServiceQueryException>(async () =>
            {
                result = await serviceQuery.GetServiceQuery().ExecuteAggregateAsync<T>(sourceQueryable);
            });
#endif

            if (ValidateTimeSpan)
            {
                // TimeSpan
                serviceQuery = ServiceQueryRequestBuilder.New().Average(nameof(TestClass.TimeSpanVal)).Build();
                await Assert.ThrowsAsync<ServiceQueryException>(async () =>
                {
                    result = await serviceQuery.GetServiceQuery().ExecuteAggregateAsync<T>(sourceQueryable);
                });
                await Assert.ThrowsAsync<ServiceQueryException>(async () =>
                {
                    result = await serviceQuery.GetServiceQuery().ExecuteAggregateAsync<T>(sourceQueryable);
                });
            }

#if NET7_0_OR_GREATER

            if (ValidateUInt128)
            {
                // UInt128
                serviceQuery = ServiceQueryRequestBuilder.New().Average(nameof(TestClass.UInt128Val)).Build();
                await Assert.ThrowsAsync<ServiceQueryException>(async () =>
                {
                    result = await serviceQuery.GetServiceQuery().ExecuteAggregateAsync<T>(sourceQueryable);
                });
                await Assert.ThrowsAsync<ServiceQueryException>(async () =>
                {
                    result = await serviceQuery.GetServiceQuery().ExecuteAggregateAsync<T>(sourceQueryable);
                });
            }
#endif

            if (ValidateUInt64)
            {
                // UInt64
                serviceQuery = ServiceQueryRequestBuilder.New().Average(nameof(TestClass.UInt64Val)).Build();
                await Assert.ThrowsAsync<ServiceQueryException>(async () =>
                {
                    result = await serviceQuery.GetServiceQuery().ExecuteAggregateAsync<T>(sourceQueryable);
                });
                await Assert.ThrowsAsync<ServiceQueryException>(async () =>
                {
                    result = await serviceQuery.GetServiceQuery().ExecuteAggregateAsync<T>(sourceQueryable);
                });
            }

            // UInt32
            serviceQuery = ServiceQueryRequestBuilder.New().Average(nameof(TestClass.UInt32Val)).Build();
            await Assert.ThrowsAsync<ServiceQueryException>(async () =>
            {
                result = await serviceQuery.GetServiceQuery().ExecuteAggregateAsync<T>(sourceQueryable);
            });
            await Assert.ThrowsAsync<ServiceQueryException>(async () =>
            {
                result = await serviceQuery.GetServiceQuery().ExecuteAggregateAsync<T>(sourceQueryable);
            });

            // UInt16
            serviceQuery = ServiceQueryRequestBuilder.New().Average(nameof(TestClass.UInt16Val)).Build();
            await Assert.ThrowsAsync<ServiceQueryException>(async () =>
            {
                result = await serviceQuery.GetServiceQuery().ExecuteAggregateAsync<T>(sourceQueryable);
            });
            await Assert.ThrowsAsync<ServiceQueryException>(async () =>
            {
                result = await serviceQuery.GetServiceQuery().ExecuteAggregateAsync<T>(sourceQueryable);
            });
        }

        [Fact]
        public async Task NullAverageStandardTest()
        {
            var sourceQueryable = await GetTestListAsync();
            IServiceQuery serviceQuery;
            double? result;

            // Boolean
            serviceQuery = ServiceQueryBuilder.New().Average(nameof(TestClass.NullBoolVal)).Build();
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
            serviceQuery = ServiceQueryBuilder.New().Average(nameof(TestClass.NullByteArrayVal)).Build();
            await Assert.ThrowsAsync<ServiceQueryException>(async () =>
            {
                result = await serviceQuery.ExecuteAggregateAsync<T>(sourceQueryable);
            });

            // Byte
            serviceQuery = ServiceQueryBuilder.New().Average(nameof(TestClass.NullByteVal)).Build();
            await Assert.ThrowsAsync<ServiceQueryException>(async () =>
            {
                result = await serviceQuery.ExecuteAggregateAsync<T>(sourceQueryable);
            });

            // Char
            serviceQuery = ServiceQueryBuilder.New().Average(nameof(TestClass.NullCharVal)).Build();
            await Assert.ThrowsAsync<ServiceQueryException>(async () =>
            {
                result = await serviceQuery.ExecuteAggregateAsync<T>(sourceQueryable);
            });

            if (ValidateDateTimeOffset)
            {
                // DateTimeOffset
                serviceQuery = ServiceQueryBuilder.New().Average(nameof(TestClass.NullDateTimeOffsetVal)).Build();
                await Assert.ThrowsAsync<ServiceQueryException>(async () =>
                {
                    result = await serviceQuery.ExecuteAggregateAsync<T>(sourceQueryable);
                });
            }

#if NET6_0_OR_GREATER
            // DateOnly
            serviceQuery = ServiceQueryBuilder.New().Average(nameof(TestClass.NullDateOnlyVal)).Build();
            await Assert.ThrowsAsync<ServiceQueryException>(async () =>
            {
                result = await serviceQuery.ExecuteAggregateAsync<T>(sourceQueryable);
            });
#endif

            // DateTime
            serviceQuery = ServiceQueryBuilder.New().Average(nameof(TestClass.NullDateTimeVal)).Build();
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
                        serviceQuery = ServiceQueryBuilder.New().Average(nameof(TestClass.NullDecimalVal)).Build();
                        result = await serviceQuery.ExecuteAggregateAsync<T>(sourceQueryable);
                    });
                }
                else
                {
                    serviceQuery = ServiceQueryBuilder.New().Average(nameof(TestClass.NullDecimalVal)).Build();
                    result = await serviceQuery.ExecuteAggregateAsync<T>(sourceQueryable);
                    Assert.True(result == null);
                }
            }

            // Double
            if (CosmosSequenceNoElementsError)
            {
                await Assert.ThrowsAsync<ServiceQueryException>(async () =>
                {
                    serviceQuery = ServiceQueryBuilder.New().Average(nameof(TestClass.NullDoubleVal)).Build();
                    result = await serviceQuery.ExecuteAggregateAsync<T>(sourceQueryable);
                });
            }
            else
            {
                serviceQuery = ServiceQueryBuilder.New().Average(nameof(TestClass.NullDoubleVal)).Build();
                result = await serviceQuery.ExecuteAggregateAsync<T>(sourceQueryable);
                Assert.True(result == null);
                result = await serviceQuery.ExecuteAggregateAsync<T>(sourceQueryable);
                Assert.True(result == null);
            }

            // Float
            if (CosmosSequenceNoElementsError)
            {
                await Assert.ThrowsAsync<ServiceQueryException>(async () =>
                {
                    serviceQuery = ServiceQueryBuilder.New().Average(nameof(TestClass.NullFloatVal)).Build();
                    result = await serviceQuery.ExecuteAggregateAsync<T>(sourceQueryable);
                });
            }
            else
            {
                serviceQuery = ServiceQueryBuilder.New().Average(nameof(TestClass.NullFloatVal)).Build();
                result = await serviceQuery.ExecuteAggregateAsync<T>(sourceQueryable);
                Assert.True(result == null);
            }

            // Guid
            serviceQuery = ServiceQueryBuilder.New().Average(nameof(TestClass.NullGuidVal)).Build();
            await Assert.ThrowsAsync<ServiceQueryException>(async () =>
            {
                result = await serviceQuery.ExecuteAggregateAsync<T>(sourceQueryable);
            });

            // Int

            if (CosmosSequenceNoElementsError)
            {
                await Assert.ThrowsAsync<ServiceQueryException>(async () =>
                {
                    serviceQuery = ServiceQueryBuilder.New().Average(nameof(TestClass.NullIntVal)).Build();
                    result = await serviceQuery.ExecuteAggregateAsync<T>(sourceQueryable);
                });
            }
            else
            {
                serviceQuery = ServiceQueryBuilder.New().Average(nameof(TestClass.NullIntVal)).Build();
                result = await serviceQuery.ExecuteAggregateAsync<T>(sourceQueryable);
                Assert.True(result == null);
            }

            // Long
            if (ValidateNullLong)
            {
                if (CosmosSequenceNoElementsError)
                {
                    await Assert.ThrowsAsync<ServiceQueryException>(async () =>
                    {
                        serviceQuery = ServiceQueryBuilder.New().Average(nameof(TestClass.NullLongVal)).Build();
                        result = await serviceQuery.ExecuteAggregateAsync<T>(sourceQueryable);
                    });
                }
                else
                {
                    serviceQuery = ServiceQueryBuilder.New().Average(nameof(TestClass.NullLongVal)).Build();
                    result = await serviceQuery.ExecuteAggregateAsync<T>(sourceQueryable);
                    Assert.True(result == null);
                }
            }

            // SByte
            serviceQuery = ServiceQueryBuilder.New().Average(nameof(TestClass.NullSByteVal)).Build();
            await Assert.ThrowsAsync<ServiceQueryException>(async () =>
            {
                result = await serviceQuery.ExecuteAggregateAsync<T>(sourceQueryable);
            });

            // Short
            serviceQuery = ServiceQueryBuilder.New().Average(nameof(TestClass.NullShortVal)).Build();
            await Assert.ThrowsAsync<ServiceQueryException>(async () =>
            {
                result = await serviceQuery.ExecuteAggregateAsync<T>(sourceQueryable);
            });

            // Single
            if (CosmosSequenceNoElementsError)
            {
                await Assert.ThrowsAsync<ServiceQueryException>(async () =>
                {
                    serviceQuery = ServiceQueryBuilder.New().Average(nameof(TestClass.NullSingleVal)).Build();
                    result = await serviceQuery.ExecuteAggregateAsync<T>(sourceQueryable);
                });
            }
            else
            {
                serviceQuery = ServiceQueryBuilder.New().Average(nameof(TestClass.NullSingleVal)).Build();
                result = await serviceQuery.ExecuteAggregateAsync<T>(sourceQueryable);
                Assert.True(result == null);
            }
            // String
            serviceQuery = ServiceQueryBuilder.New().Average(nameof(TestClass.NullStringVal)).Build();
            await Assert.ThrowsAsync<ServiceQueryException>(async () =>
            {
                result = await serviceQuery.ExecuteAggregateAsync<T>(sourceQueryable);
            });

#if NET6_0_OR_GREATER
            // TimeOnly
            serviceQuery = ServiceQueryBuilder.New().Average(nameof(TestClass.NullTimeOnlyVal)).Build();
            await Assert.ThrowsAsync<ServiceQueryException>(async () =>
            {
                result = await serviceQuery.ExecuteAggregateAsync<T>(sourceQueryable);
            });
#endif

            if (ValidateTimeSpan)
            {
                // TimeSpan
                serviceQuery = ServiceQueryBuilder.New().Average(nameof(TestClass.NullTimeSpanVal)).Build();
                await Assert.ThrowsAsync<ServiceQueryException>(async () =>
                {
                    result = await serviceQuery.ExecuteAggregateAsync<T>(sourceQueryable);
                });
            }

#if NET7_0_OR_GREATER
            if (ValidateUInt128)
            {
                // UInt128
                serviceQuery = ServiceQueryBuilder.New().Average(nameof(TestClass.NullUInt128Val)).Build();
                await Assert.ThrowsAsync<ServiceQueryException>(async () =>
                {
                    result = await serviceQuery.ExecuteAggregateAsync<T>(sourceQueryable);
                });
            }
#endif

            if (ValidateUInt64)
            {
                // UInt64
                serviceQuery = ServiceQueryBuilder.New().Average(nameof(TestClass.NullUInt64Val)).Build();
                await Assert.ThrowsAsync<ServiceQueryException>(async () =>
                {
                    result = await serviceQuery.ExecuteAggregateAsync<T>(sourceQueryable);
                });
            }

            // UInt32
            serviceQuery = ServiceQueryBuilder.New().Average(nameof(TestClass.NullUInt32Val)).Build();
            await Assert.ThrowsAsync<ServiceQueryException>(async () =>
            {
                result = await serviceQuery.ExecuteAggregateAsync<T>(sourceQueryable);
            });

            // UInt16
            serviceQuery = ServiceQueryBuilder.New().Average(nameof(TestClass.NullUInt16Val)).Build();
            await Assert.ThrowsAsync<ServiceQueryException>(async () =>
            {
                result = await serviceQuery.ExecuteAggregateAsync<T>(sourceQueryable);
            });
        }

        [Fact]
        public async Task NullAverageRequestTest()
        {
            var sourceQueryable = await GetTestListAsync();
            IServiceQueryRequest serviceQuery;
            double? result;

            // Boolean
            serviceQuery = ServiceQueryRequestBuilder.New().Average(nameof(TestClass.NullBoolVal)).Build();
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
            serviceQuery = ServiceQueryRequestBuilder.New().Average(nameof(TestClass.NullByteArrayVal)).Build();
            await Assert.ThrowsAsync<ServiceQueryException>(async () =>
            {
                result = await serviceQuery.GetServiceQuery().ExecuteAggregateAsync<T>(sourceQueryable);
            });

            // Byte
            serviceQuery = ServiceQueryRequestBuilder.New().Average(nameof(TestClass.NullByteVal)).Build();
            await Assert.ThrowsAsync<ServiceQueryException>(async () =>
            {
                result = await serviceQuery.GetServiceQuery().ExecuteAggregateAsync<T>(sourceQueryable);
            });

            // Char
            serviceQuery = ServiceQueryRequestBuilder.New().Average(nameof(TestClass.NullCharVal)).Build();
            await Assert.ThrowsAsync<ServiceQueryException>(async () =>
            {
                result = await serviceQuery.GetServiceQuery().ExecuteAggregateAsync<T>(sourceQueryable);
            });

            if (ValidateDateTimeOffset)
            {
                // DateTimeOffset
                serviceQuery = ServiceQueryRequestBuilder.New().Average(nameof(TestClass.NullDateTimeOffsetVal)).Build();
                await Assert.ThrowsAsync<ServiceQueryException>(async () =>
                {
                    result = await serviceQuery.GetServiceQuery().ExecuteAggregateAsync<T>(sourceQueryable);
                });
            }

#if NET6_0_OR_GREATER
            // DateOnly
            serviceQuery = ServiceQueryRequestBuilder.New().Average(nameof(TestClass.NullDateOnlyVal)).Build();
            await Assert.ThrowsAsync<ServiceQueryException>(async () =>
            {
                result = await serviceQuery.GetServiceQuery().ExecuteAggregateAsync<T>(sourceQueryable);
            });
#endif

            // DateTime
            serviceQuery = ServiceQueryRequestBuilder.New().Average(nameof(TestClass.NullDateTimeVal)).Build();
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
                        serviceQuery = ServiceQueryRequestBuilder.New().Average(nameof(TestClass.NullDecimalVal)).Build();
                        result = await serviceQuery.GetServiceQuery().ExecuteAggregateAsync<T>(sourceQueryable);
                    });
                }
                else
                {
                    serviceQuery = ServiceQueryRequestBuilder.New().Average(nameof(TestClass.NullDecimalVal)).Build();
                    result = await serviceQuery.GetServiceQuery().ExecuteAggregateAsync<T>(sourceQueryable);
                    Assert.True(result == null);
                }
            }

            // Double
            if (CosmosSequenceNoElementsError)
            {
                await Assert.ThrowsAsync<ServiceQueryException>(async () =>
                {
                    serviceQuery = ServiceQueryRequestBuilder.New().Average(nameof(TestClass.NullDoubleVal)).Build();
                    result = await serviceQuery.GetServiceQuery().ExecuteAggregateAsync<T>(sourceQueryable);
                });
            }
            else
            {
                serviceQuery = ServiceQueryRequestBuilder.New().Average(nameof(TestClass.NullDoubleVal)).Build();
                result = await serviceQuery.GetServiceQuery().ExecuteAggregateAsync<T>(sourceQueryable);
                Assert.True(result == null);
                result = await serviceQuery.GetServiceQuery().ExecuteAggregateAsync<T>(sourceQueryable);
                Assert.True(result == null);
            }

            // Float
            if (CosmosSequenceNoElementsError)
            {
                await Assert.ThrowsAsync<ServiceQueryException>(async () =>
                {
                    serviceQuery = ServiceQueryRequestBuilder.New().Average(nameof(TestClass.NullFloatVal)).Build();
                    result = await serviceQuery.GetServiceQuery().ExecuteAggregateAsync<T>(sourceQueryable);
                });
            }
            else
            {
                serviceQuery = ServiceQueryRequestBuilder.New().Average(nameof(TestClass.NullFloatVal)).Build();
                result = await serviceQuery.GetServiceQuery().ExecuteAggregateAsync<T>(sourceQueryable);
                Assert.True(result == null);
            }

            // Guid
            serviceQuery = ServiceQueryRequestBuilder.New().Average(nameof(TestClass.NullGuidVal)).Build();
            await Assert.ThrowsAsync<ServiceQueryException>(async () =>
            {
                result = await serviceQuery.GetServiceQuery().ExecuteAggregateAsync<T>(sourceQueryable);
            });

            // Int
            if (CosmosSequenceNoElementsError)
            {
                await Assert.ThrowsAsync<ServiceQueryException>(async () =>
                {
                    serviceQuery = ServiceQueryRequestBuilder.New().Average(nameof(TestClass.NullIntVal)).Build();
                    result = await serviceQuery.GetServiceQuery().ExecuteAggregateAsync<T>(sourceQueryable);
                });
            }
            else
            {
                serviceQuery = ServiceQueryRequestBuilder.New().Average(nameof(TestClass.NullIntVal)).Build();
                result = await serviceQuery.GetServiceQuery().ExecuteAggregateAsync<T>(sourceQueryable);
                Assert.True(result == null);
            }

            // Long
            if (ValidateNullLong)
            {
                if (CosmosSequenceNoElementsError)
                {
                    await Assert.ThrowsAsync<ServiceQueryException>(async () =>
                    {
                        serviceQuery = ServiceQueryRequestBuilder.New().Average(nameof(TestClass.NullLongVal)).Build();
                        result = await serviceQuery.GetServiceQuery().ExecuteAggregateAsync<T>(sourceQueryable);
                    });
                }
                else
                {
                    serviceQuery = ServiceQueryRequestBuilder.New().Average(nameof(TestClass.NullLongVal)).Build();
                    result = await serviceQuery.GetServiceQuery().ExecuteAggregateAsync<T>(sourceQueryable);
                    Assert.True(result == null);
                }
            }

            // SByte
            serviceQuery = ServiceQueryRequestBuilder.New().Average(nameof(TestClass.NullSByteVal)).Build();
            await Assert.ThrowsAsync<ServiceQueryException>(async () =>
            {
                result = await serviceQuery.GetServiceQuery().ExecuteAggregateAsync<T>(sourceQueryable);
            });

            // Short
            serviceQuery = ServiceQueryRequestBuilder.New().Average(nameof(TestClass.NullShortVal)).Build();
            await Assert.ThrowsAsync<ServiceQueryException>(async () =>
            {
                result = await serviceQuery.GetServiceQuery().ExecuteAggregateAsync<T>(sourceQueryable);
            });

            // Single
            if (CosmosSequenceNoElementsError)
            {
                await Assert.ThrowsAsync<ServiceQueryException>(async () =>
                {
                    serviceQuery = ServiceQueryRequestBuilder.New().Average(nameof(TestClass.NullSingleVal)).Build();
                    result = await serviceQuery.GetServiceQuery().ExecuteAggregateAsync<T>(sourceQueryable);
                });
            }
            else
            {
                serviceQuery = ServiceQueryRequestBuilder.New().Average(nameof(TestClass.NullSingleVal)).Build();
                result = await serviceQuery.GetServiceQuery().ExecuteAggregateAsync<T>(sourceQueryable);
                Assert.True(result == null);
            }

            // String
            serviceQuery = ServiceQueryRequestBuilder.New().Average(nameof(TestClass.NullStringVal)).Build();
            await Assert.ThrowsAsync<ServiceQueryException>(async () =>
            {
                result = await serviceQuery.GetServiceQuery().ExecuteAggregateAsync<T>(sourceQueryable);
            });

#if NET6_0_OR_GREATER
            // TimeOnly
            serviceQuery = ServiceQueryRequestBuilder.New().Average(nameof(TestClass.NullTimeOnlyVal)).Build();
            await Assert.ThrowsAsync<ServiceQueryException>(async () =>
            {
                result = await serviceQuery.GetServiceQuery().ExecuteAggregateAsync<T>(sourceQueryable);
            });
#endif

            if (ValidateTimeSpan)
            {
                // TimeSpan
                serviceQuery = ServiceQueryRequestBuilder.New().Average(nameof(TestClass.NullTimeSpanVal)).Build();
                await Assert.ThrowsAsync<ServiceQueryException>(async () =>
                {
                    result = await serviceQuery.GetServiceQuery().ExecuteAggregateAsync<T>(sourceQueryable);
                });
            }

#if NET7_0_OR_GREATER
            if (ValidateUInt128)
            {
                // UInt128
                serviceQuery = ServiceQueryRequestBuilder.New().Average(nameof(TestClass.NullUInt128Val)).Build();
                await Assert.ThrowsAsync<ServiceQueryException>(async () =>
                {
                    result = await serviceQuery.GetServiceQuery().ExecuteAggregateAsync<T>(sourceQueryable);
                });
            }
#endif

            if (ValidateUInt64)
            {
                // UInt64
                serviceQuery = ServiceQueryRequestBuilder.New().Average(nameof(TestClass.NullUInt64Val)).Build();
                await Assert.ThrowsAsync<ServiceQueryException>(async () =>
                {
                    result = await serviceQuery.GetServiceQuery().ExecuteAggregateAsync<T>(sourceQueryable);
                });
            }

            // UInt32
            serviceQuery = ServiceQueryRequestBuilder.New().Average(nameof(TestClass.NullUInt32Val)).Build();
            await Assert.ThrowsAsync<ServiceQueryException>(async () =>
            {
                result = await serviceQuery.GetServiceQuery().ExecuteAggregateAsync<T>(sourceQueryable);
            });

            // UInt16
            serviceQuery = ServiceQueryRequestBuilder.New().Average(nameof(TestClass.NullUInt16Val)).Build();
            await Assert.ThrowsAsync<ServiceQueryException>(async () =>
            {
                result = await serviceQuery.GetServiceQuery().ExecuteAggregateAsync<T>(sourceQueryable);
            });
        }
    }
}
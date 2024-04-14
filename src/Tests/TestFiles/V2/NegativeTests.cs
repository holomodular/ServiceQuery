namespace ServiceQuery.Xunit
{
    public class NegativeTests : NegativeTests<TestClass>
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
    }

    public abstract class NegativeTests<T> : BaseTest<T> where T : class, ITestClass, new()
    {
        [Fact]
        public void EndBeforeBegin()
        {
            var sourceQueryable = GetTestList();
            var sq = ServiceQueryRequestBuilder.New()
                .End()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .Begin()
                .Build().GetServiceQuery();
            Assert.Throws<ServiceQueryException>(() =>
            {
                var err = sq.Execute(sourceQueryable);
            });

            sq = ServiceQueryRequestBuilder.New()
                .End()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .Begin()
                .Build().GetServiceQuery();
            Assert.Throws<ServiceQueryException>(() =>
            {
                var err = sq.Execute(sourceQueryable);
            });
        }

        [Fact]
        public void MissingEnd()
        {
            var sourceQueryable = GetTestList();
            var sq = ServiceQueryRequestBuilder.New()
                .Begin()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .Build().GetServiceQuery();
            Assert.Throws<ServiceQueryException>(() =>
            {
                var err = sq.Execute(sourceQueryable);
            });
        }

        [Fact]
        public void MultipleAndOr()
        {
            var sourceQueryable = GetTestList();
            var sq = ServiceQueryRequestBuilder.New()
                .And().Build().GetServiceQuery();
            Assert.Throws<ServiceQueryException>(() =>
            {
                var err = sq.Execute(sourceQueryable);
            });

            sq = ServiceQueryRequestBuilder.New()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .And()
                .And().Build().GetServiceQuery();
            Assert.Throws<ServiceQueryException>(() =>
            {
                var err = sq.Execute(sourceQueryable);
            });

            sq = ServiceQueryRequestBuilder.New()
                .Or().Build().GetServiceQuery();
            Assert.Throws<ServiceQueryException>(() =>
            {
                var err = sq.Execute(sourceQueryable);
            });

            sq = ServiceQueryRequestBuilder.New()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .Or()
                .Or().Build().GetServiceQuery();
            Assert.Throws<ServiceQueryException>(() =>
            {
                var err = sq.Execute(sourceQueryable);
            });
        }

        [Fact]
        public void PropertyNotFound()
        {
            var sourceQueryable = GetTestList();
            var sq = ServiceQueryRequestBuilder.New()
                .Select("zzzzz")
                .Build().GetServiceQuery();
            Assert.Throws<ServiceQueryException>(() =>
            {
                var err = sq.Execute(sourceQueryable);
            });

            var options = new ServiceQueryOptions() { PropertyNameCaseSensitive = true };

            sq = ServiceQueryRequestBuilder.New()
                .Select("INTVAL")
                .Build().GetServiceQuery();

            Assert.Throws<ServiceQueryException>(() =>
            {
                var err = sq.Execute(sourceQueryable, options);
            });

            sq = ServiceQueryRequestBuilder.New()
                .Select("zzz")
                .Build().GetServiceQuery();

            Assert.Throws<ServiceQueryException>(() =>
            {
                var err = sq.Execute(sourceQueryable, options);
            });
        }

        [Fact]
        public void PageSizeNotANumber()
        {
            var sourceQueryable = GetTestList();
            //IQueryable<T> testQueryable;
            IServiceQueryRequest serviceQuery;
            //List<T> result;

            serviceQuery = ServiceQueryRequestBuilder.New().Build();
            serviceQuery.Filters.Add(new ServiceQueryServiceFilter()
            {
                FilterType = "pagesize",
                Properties = new List<string>() { nameof(TestClass.IntVal) },
                Values = new List<string>() { "abc" }
            });
            Assert.Throws<ServiceQueryException>(() =>
            {
                var err = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            });

            serviceQuery = ServiceQueryRequestBuilder.New().Build();
            serviceQuery.Filters.Add(new ServiceQueryServiceFilter()
            {
                FilterType = "pagesize",
                Properties = new List<string>() { nameof(TestClass.IntVal) },
                Values = new List<string>() { "" }
            });
            Assert.Throws<ServiceQueryException>(() =>
            {
                var err = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            });

            serviceQuery = ServiceQueryRequestBuilder.New().Build();
            serviceQuery.Filters.Add(new ServiceQueryServiceFilter()
            {
                FilterType = "pagenumber",
                Properties = new List<string>() { nameof(TestClass.IntVal) },
                Values = new List<string>() { "" }
            });
            Assert.Throws<ServiceQueryException>(() =>
            {
                var err = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            });

            serviceQuery = ServiceQueryRequestBuilder.New().Build();
            serviceQuery.Filters.Add(new ServiceQueryServiceFilter()
            {
                FilterType = "includecount",
                Properties = new List<string>() { nameof(TestClass.IntVal) },
                Values = new List<string>() { "asd" }
            });
            Assert.Throws<ServiceQueryException>(() =>
            {
                var err = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            });

            serviceQuery = ServiceQueryRequestBuilder.New().Build();
            serviceQuery.Filters.Add(new ServiceQueryServiceFilter()
            {
                FilterType = "includecount",
                Properties = new List<string>() { nameof(TestClass.IntVal) },
                Values = new List<string>() { "" }
            });
            var templist = serviceQuery.Execute(sourceQueryable);
        }

        [Fact]
        public void RequestBeginEnd()
        {
            var sourceQueryable = GetTestList();
            IQueryable<T> testQueryable;
            IServiceQueryRequest serviceQuery;
            //List<T> result;

            serviceQuery = ServiceQueryRequestBuilder.New()
                .Begin()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .End()
                .Build();

            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            var list = testQueryable.ToList();
        }

        [Fact]
        public void IncludeCountTwice()
        {
            var sourceQueryable = GetTestList();
            IQueryable<T> testQueryable;
            IServiceQueryRequest serviceQuery;
            //List<T> result;

            serviceQuery = ServiceQueryRequestBuilder.New()
                .IncludeCount()
                .IncludeCount()
                .Build();

            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            var list = testQueryable.ToList();

            serviceQuery = ServiceQueryRequestBuilder.New()
                .IncludeCount()
                .Paging(1, 1000, true)
                .Build();

            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            list = testQueryable.ToList();
        }

        [Fact]
        public void IsAggregateNullCheck()
        {
            IServiceQueryRequest serviceQuery;

            serviceQuery = ServiceQueryRequestBuilder.New().Build();

            var sq = serviceQuery.GetServiceQuery();
            sq.Filters = null;
            Assert.True(sq.IsAggregate() == false);
        }

        [Fact]
        public void FilterTypeNotDefined()
        {
            IServiceQueryRequest serviceQuery;

            serviceQuery = ServiceQueryRequestBuilder.New().Build();
            serviceQuery.Filters.Add(new ServiceQueryServiceFilter()
            {
                FilterType = "",
            });

            Assert.Throws<ServiceQueryException>(() =>
            {
                var sq = serviceQuery.GetServiceQuery();
            });
        }

        [Fact]
        public void MissingProperty()
        {
            var sourceQueryable = GetTestList();
            IServiceQueryRequest serviceQuery;

            serviceQuery = ServiceQueryRequestBuilder.New().Build();
            serviceQuery.Filters.Add(new ServiceQueryServiceFilter()
            {
                FilterType = "select",
                Properties = new List<string>() { }
            });

            //Assert.Throws<ServiceQueryException>(() =>
            //{
            //    var sq = serviceQuery.GetServiceQuery();
            //});

            serviceQuery = ServiceQueryRequestBuilder.New().Build();
            serviceQuery.Filters.Add(new ServiceQueryServiceFilter()
            {
                FilterType = "greaterthan",
                Properties = new List<string>() { }
            });

            Assert.Throws<ServiceQueryException>(() =>
            {
                var sq = serviceQuery.GetServiceQuery().Execute(sourceQueryable);
            });
            Assert.Throws<ServiceQueryException>(() =>
            {
                var sq = ServiceQueryRequestBuilder.New().Build().GetServiceQuery();
                sq.Filters.Add(new ServiceQueryFilter()
                {
                    FilterType = ServiceQueryFilterType.Compare,
                    CompareType = ServiceQueryCompareType.GreaterThan,
                    Properties = new List<string>() { }
                });
                var respsq = sq.Execute(sourceQueryable);
            });

            serviceQuery = ServiceQueryRequestBuilder.New().Build();
            serviceQuery.Filters.Add(new ServiceQueryServiceFilter()
            {
                FilterType = "greaterthanorequal",
                Properties = new List<string>() { }
            });

            Assert.Throws<ServiceQueryException>(() =>
            {
                var sq = serviceQuery.GetServiceQuery().Execute(sourceQueryable);
            });
            Assert.Throws<ServiceQueryException>(() =>
            {
                var sq = ServiceQueryRequestBuilder.New().Build().GetServiceQuery();
                sq.Filters.Add(new ServiceQueryFilter()
                {
                    FilterType = ServiceQueryFilterType.Compare,
                    CompareType = ServiceQueryCompareType.GreaterThanEqual,
                    Properties = new List<string>() { }
                });
                var respsq = sq.Execute(sourceQueryable);
            });

            serviceQuery = ServiceQueryRequestBuilder.New().Build();
            serviceQuery.Filters.Add(new ServiceQueryServiceFilter()
            {
                FilterType = "lessthan",
                Properties = new List<string>() { }
            });

            Assert.Throws<ServiceQueryException>(() =>
            {
                var sq = serviceQuery.GetServiceQuery().Execute(sourceQueryable);
            });
            Assert.Throws<ServiceQueryException>(() =>
            {
                var sq = ServiceQueryRequestBuilder.New().Build().GetServiceQuery();
                sq.Filters.Add(new ServiceQueryFilter()
                {
                    FilterType = ServiceQueryFilterType.Compare,
                    CompareType = ServiceQueryCompareType.LessThan,
                    Properties = new List<string>() { }
                });
                var respsq = sq.Execute(sourceQueryable);
            });

            serviceQuery = ServiceQueryRequestBuilder.New().Build();
            serviceQuery.Filters.Add(new ServiceQueryServiceFilter()
            {
                FilterType = "lessthanorequal",
                Properties = new List<string>() { }
            });

            Assert.Throws<ServiceQueryException>(() =>
            {
                var sq = serviceQuery.GetServiceQuery().Execute(sourceQueryable);
            });
            Assert.Throws<ServiceQueryException>(() =>
            {
                var sq = ServiceQueryRequestBuilder.New().Build().GetServiceQuery();
                sq.Filters.Add(new ServiceQueryFilter()
                {
                    FilterType = ServiceQueryFilterType.Compare,
                    CompareType = ServiceQueryCompareType.LessThanEqual,
                    Properties = new List<string>() { }
                });
                var respsq = sq.Execute(sourceQueryable);
            });

            serviceQuery = ServiceQueryRequestBuilder.New().Build();
            serviceQuery.Filters.Add(new ServiceQueryServiceFilter()
            {
                FilterType = "equal",
                Properties = new List<string>() { }
            });

            Assert.Throws<ServiceQueryException>(() =>
            {
                var sq = serviceQuery.GetServiceQuery().Execute(sourceQueryable);
            });
            Assert.Throws<ServiceQueryException>(() =>
            {
                var sq = ServiceQueryRequestBuilder.New().Build().GetServiceQuery();
                sq.Filters.Add(new ServiceQueryFilter()
                {
                    FilterType = ServiceQueryFilterType.Compare,
                    CompareType = ServiceQueryCompareType.Equal,
                    Properties = new List<string>() { }
                });
                var respsq = sq.Execute(sourceQueryable);
            });

            serviceQuery = ServiceQueryRequestBuilder.New().Build();
            serviceQuery.Filters.Add(new ServiceQueryServiceFilter()
            {
                FilterType = "notequal",
                Properties = new List<string>() { }
            });

            Assert.Throws<ServiceQueryException>(() =>
            {
                var sq = serviceQuery.GetServiceQuery().Execute(sourceQueryable);
            });
            Assert.Throws<ServiceQueryException>(() =>
            {
                var sq = ServiceQueryRequestBuilder.New().Build().GetServiceQuery();
                sq.Filters.Add(new ServiceQueryFilter()
                {
                    FilterType = ServiceQueryFilterType.Compare,
                    CompareType = ServiceQueryCompareType.NotEqual,
                    Properties = new List<string>() { }
                });
                var respsq = sq.Execute(sourceQueryable);
            });

            serviceQuery = ServiceQueryRequestBuilder.New().Build();
            serviceQuery.Filters.Add(new ServiceQueryServiceFilter()
            {
                FilterType = "inset",
                Properties = new List<string>() { }
            });

            Assert.Throws<ServiceQueryException>(() =>
            {
                var sq = serviceQuery.GetServiceQuery().Execute(sourceQueryable);
            });
            Assert.Throws<ServiceQueryException>(() =>
            {
                var sq = ServiceQueryRequestBuilder.New().Build().GetServiceQuery();
                sq.Filters.Add(new ServiceQueryFilter()
                {
                    FilterType = ServiceQueryFilterType.Set,
                    IncludeType = ServiceQueryIncludeType.Include,
                    Properties = new List<string>() { }
                });
                var respsq = sq.Execute(sourceQueryable);
            });

            serviceQuery = ServiceQueryRequestBuilder.New().Build();
            serviceQuery.Filters.Add(new ServiceQueryServiceFilter()
            {
                FilterType = "notinset",
                Properties = new List<string>() { }
            });

            Assert.Throws<ServiceQueryException>(() =>
            {
                var sq = serviceQuery.GetServiceQuery().Execute(sourceQueryable);
            });
            Assert.Throws<ServiceQueryException>(() =>
            {
                var sq = ServiceQueryRequestBuilder.New().Build().GetServiceQuery();
                sq.Filters.Add(new ServiceQueryFilter()
                {
                    FilterType = ServiceQueryFilterType.Set,
                    IncludeType = ServiceQueryIncludeType.NotInclude,
                    Properties = new List<string>() { }
                });
                var respsq = sq.Execute(sourceQueryable);
            });

            serviceQuery = ServiceQueryRequestBuilder.New().Build();
            serviceQuery.Filters.Add(new ServiceQueryServiceFilter()
            {
                FilterType = "isnull",
                Properties = new List<string>() { }
            });

            Assert.Throws<ServiceQueryException>(() =>
            {
                var sq = serviceQuery.GetServiceQuery().Execute(sourceQueryable);
            });
            Assert.Throws<ServiceQueryException>(() =>
            {
                var sq = ServiceQueryRequestBuilder.New().Build().GetServiceQuery();
                sq.Filters.Add(new ServiceQueryFilter()
                {
                    FilterType = ServiceQueryFilterType.Null,
                    IncludeType = ServiceQueryIncludeType.Include,
                    Properties = new List<string>() { }
                });
                var respsq = sq.Execute(sourceQueryable);
            });

            serviceQuery = ServiceQueryRequestBuilder.New().Build();
            serviceQuery.Filters.Add(new ServiceQueryServiceFilter()
            {
                FilterType = "isnotnull",
                Properties = new List<string>() { }
            });

            Assert.Throws<ServiceQueryException>(() =>
            {
                var sq = serviceQuery.GetServiceQuery().Execute(sourceQueryable);
            });
            Assert.Throws<ServiceQueryException>(() =>
            {
                var sq = ServiceQueryRequestBuilder.New().Build().GetServiceQuery();
                sq.Filters.Add(new ServiceQueryFilter()
                {
                    FilterType = ServiceQueryFilterType.Null,
                    IncludeType = ServiceQueryIncludeType.NotInclude,
                    Properties = new List<string>() { }
                });
                var respsq = sq.Execute(sourceQueryable);
            });

            serviceQuery = ServiceQueryRequestBuilder.New().Build();
            serviceQuery.Filters.Add(new ServiceQueryServiceFilter()
            {
                FilterType = "minimum",
                Properties = new List<string>() { }
            });

            Assert.Throws<ServiceQueryException>(() =>
            {
                var sq = serviceQuery.GetServiceQuery().Execute(sourceQueryable);
            });
            Assert.Throws<ServiceQueryException>(() =>
            {
                var sq = ServiceQueryRequestBuilder.New().Build().GetServiceQuery();
                sq.Filters.Add(new ServiceQueryFilter()
                {
                    FilterType = ServiceQueryFilterType.Aggregate,
                    AggregateType = ServiceQueryAggregateType.Minimum,
                    Properties = new List<string>() { }
                });
                var respsq = sq.Execute(sourceQueryable);
            });

            serviceQuery = ServiceQueryRequestBuilder.New().Build();
            serviceQuery.Filters.Add(new ServiceQueryServiceFilter()
            {
                FilterType = "maximum",
                Properties = new List<string>() { }
            });

            Assert.Throws<ServiceQueryException>(() =>
            {
                var sq = serviceQuery.GetServiceQuery().Execute(sourceQueryable);
            });
            Assert.Throws<ServiceQueryException>(() =>
            {
                var sq = ServiceQueryRequestBuilder.New().Build().GetServiceQuery();
                sq.Filters.Add(new ServiceQueryFilter()
                {
                    FilterType = ServiceQueryFilterType.Aggregate,
                    AggregateType = ServiceQueryAggregateType.Maximum,
                    Properties = new List<string>() { }
                });
                var respsq = sq.Execute(sourceQueryable);
            });

            serviceQuery = ServiceQueryRequestBuilder.New().Build();
            serviceQuery.Filters.Add(new ServiceQueryServiceFilter()
            {
                FilterType = "startswith",
                Properties = new List<string>() { }
            });

            Assert.Throws<ServiceQueryException>(() =>
            {
                var sq = serviceQuery.GetServiceQuery().Execute(sourceQueryable);
            });
            Assert.Throws<ServiceQueryException>(() =>
            {
                var sq = ServiceQueryRequestBuilder.New().Build().GetServiceQuery();
                sq.Filters.Add(new ServiceQueryFilter()
                {
                    FilterType = ServiceQueryFilterType.Compare,
                    CompareType = ServiceQueryCompareType.StartsWith,
                    Properties = new List<string>() { }
                });
                var respsq = sq.Execute(sourceQueryable);
            });

            serviceQuery = ServiceQueryRequestBuilder.New().Build();
            serviceQuery.Filters.Add(new ServiceQueryServiceFilter()
            {
                FilterType = "endswith",
                Properties = new List<string>() { }
            });

            Assert.Throws<ServiceQueryException>(() =>
            {
                var sq = serviceQuery.GetServiceQuery().Execute(sourceQueryable);
            });
            Assert.Throws<ServiceQueryException>(() =>
            {
                var sq = ServiceQueryRequestBuilder.New().Build().GetServiceQuery();
                sq.Filters.Add(new ServiceQueryFilter()
                {
                    FilterType = ServiceQueryFilterType.Compare,
                    CompareType = ServiceQueryCompareType.EndsWith,
                    Properties = new List<string>() { }
                });
                var respsq = sq.Execute(sourceQueryable);
            });

            serviceQuery = ServiceQueryRequestBuilder.New().Build();
            serviceQuery.Filters.Add(new ServiceQueryServiceFilter()
            {
                FilterType = "contains",
                Properties = new List<string>() { }
            });

            Assert.Throws<ServiceQueryException>(() =>
            {
                var sq = serviceQuery.GetServiceQuery().Execute(sourceQueryable);
            });
            Assert.Throws<ServiceQueryException>(() =>
            {
                var sq = ServiceQueryRequestBuilder.New().Build().GetServiceQuery();
                sq.Filters.Add(new ServiceQueryFilter()
                {
                    FilterType = ServiceQueryFilterType.Compare,
                    CompareType = ServiceQueryCompareType.Contains,
                    Properties = new List<string>() { }
                });
                var respsq = sq.Execute(sourceQueryable);
            });

            serviceQuery = ServiceQueryRequestBuilder.New().Build();
            serviceQuery.Filters.Add(new ServiceQueryServiceFilter()
            {
                FilterType = "sum",
                Properties = new List<string>() { }
            });

            Assert.Throws<ServiceQueryException>(() =>
            {
                var sq = serviceQuery.GetServiceQuery().Execute(sourceQueryable);
            });
            Assert.Throws<ServiceQueryException>(() =>
            {
                var sq = ServiceQueryRequestBuilder.New().Build().GetServiceQuery();
                sq.Filters.Add(new ServiceQueryFilter()
                {
                    FilterType = ServiceQueryFilterType.Aggregate,
                    AggregateType = ServiceQueryAggregateType.Sum,
                    Properties = new List<string>() { }
                });
                var respsq = sq.Execute(sourceQueryable);
            });

            serviceQuery = ServiceQueryRequestBuilder.New().Build();
            serviceQuery.Filters.Add(new ServiceQueryServiceFilter()
            {
                FilterType = "sortasc",
                Properties = new List<string>() { }
            });

            Assert.Throws<ServiceQueryException>(() =>
            {
                var sq = serviceQuery.GetServiceQuery().Execute(sourceQueryable);
            });
            //Assert.Throws<ServiceQueryException>(() =>
            //{
            //    var sq = ServiceQueryRequestBuilder.New().Build().GetServiceQuery();
            //    sq.Filters.Add(new ServiceQueryFilter()
            //    {
            //        FilterType = ServiceQueryFilterType.Sort,
            //        SortType = ServiceQuerySortType.Ascending,
            //        Properties = new List<string>() { }
            //    });
            //    var respsq = sq.Execute(sourceQueryable);
            //});

            serviceQuery = ServiceQueryRequestBuilder.New().Build();
            serviceQuery.Filters.Add(new ServiceQueryServiceFilter()
            {
                FilterType = "sortdesc",
                Properties = new List<string>() { }
            });

            Assert.Throws<ServiceQueryException>(() =>
            {
                var sq = serviceQuery.GetServiceQuery().Execute(sourceQueryable);
            });
            //Assert.Throws<ServiceQueryException>(() =>
            //{
            //    var sq = ServiceQueryRequestBuilder.New().Build().GetServiceQuery();
            //    sq.Filters.Add(new ServiceQueryFilter()
            //    {
            //        FilterType = ServiceQueryFilterType.Sort,
            //        SortType = ServiceQuerySortType.Descending,
            //        Properties = new List<string>() { }
            //    });
            //    var respsq = sq.Execute(sourceQueryable);
            //});
        }

        [Fact]
        public void ExecuteNull()
        {
            IServiceQueryRequest serviceQuery;

            serviceQuery = ServiceQueryRequestBuilder.New().Build();

            var result = serviceQuery.GetServiceQuery().Execute<TestClass>(null);
            Assert.True(result == null);
        }

        [Fact]
        public void SyncNegativeIsEqualStandardTest()
        {
            var sourceQueryable = GetTestList();
            IQueryable<T> testQueryable;
            IServiceQuery serviceQuery;
            //List<T> result;

            // Boolean
            serviceQuery = ServiceQueryBuilder.New().IsEqual(nameof(TestClass.BoolVal), Guid.NewGuid().ToString()).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.Apply(sourceQueryable);
            });

            // ByteArray
            //var tempbyteArray = new byte[1] { 1 };
            //serviceQuery = ServiceQueryBuilder.New().IsEqual(nameof(TestClass.ByteArrayVal), System.Text.Encoding.UTF8.GetString(tempbyteArray)).Build();
            //testQueryable = serviceQuery.GetQueryable(sourceQueryable);
            //result = testQueryable.ToList();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 1);
            //result = testQueryable.ToList();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 1);

            // Byte
            serviceQuery = ServiceQueryBuilder.New().IsEqual(nameof(TestClass.ByteVal), Guid.NewGuid().ToString()).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.Apply(sourceQueryable);
            });

            // Char
            serviceQuery = ServiceQueryBuilder.New().IsEqual(nameof(TestClass.CharVal), Guid.NewGuid().ToString()).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.Apply(sourceQueryable);
            });

            if (ValidateDateTimeOffset)
            {
                // DateTimeOffset
                var tempDateTimeOffset = new T().GetDefault1Record().DateTimeOffsetVal;
                serviceQuery = ServiceQueryBuilder.New().IsEqual(nameof(TestClass.DateTimeOffsetVal), Guid.NewGuid().ToString()).Build();
                Assert.Throws<ServiceQueryException>(() =>
                {
                    testQueryable = serviceQuery.Apply(sourceQueryable);
                });
            }

#if NET6_0_OR_GREATER
            // DateOnly
            var tempDateOnly = new T().GetDefault1Record().DateOnlyVal;
            serviceQuery = ServiceQueryBuilder.New().IsEqual(nameof(TestClass.DateOnlyVal), Guid.NewGuid().ToString()).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.Apply(sourceQueryable);
            });
#endif

            // DateTime
            var tempDateTime = new T().GetDefault1Record().DateTimeVal;
            serviceQuery = ServiceQueryBuilder.New().IsEqual(nameof(TestClass.DateTimeVal), Guid.NewGuid().ToString()).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.Apply(sourceQueryable);
            });

            // Decimal
            serviceQuery = ServiceQueryBuilder.New().IsEqual(nameof(TestClass.DecimalVal), Guid.NewGuid().ToString()).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.Apply(sourceQueryable);
            });

            // Double
            serviceQuery = ServiceQueryBuilder.New().IsEqual(nameof(TestClass.DoubleVal), Guid.NewGuid().ToString()).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.Apply(sourceQueryable);
            });

            // Float
            serviceQuery = ServiceQueryBuilder.New().IsEqual(nameof(TestClass.FloatVal), Guid.NewGuid().ToString()).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.Apply(sourceQueryable);
            });

            // Guid
            serviceQuery = ServiceQueryBuilder.New().IsEqual(nameof(TestClass.GuidVal), "zzzzz").Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.Apply(sourceQueryable);
            });

            // Int
            serviceQuery = ServiceQueryBuilder.New().IsEqual(nameof(TestClass.IntVal), Guid.NewGuid().ToString()).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.Apply(sourceQueryable);
            });

            // Long
            serviceQuery = ServiceQueryBuilder.New().IsEqual(nameof(TestClass.LongVal), Guid.NewGuid().ToString()).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.Apply(sourceQueryable);
            });

            // SByte
            serviceQuery = ServiceQueryBuilder.New().IsEqual(nameof(TestClass.SByteVal), Guid.NewGuid().ToString()).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.Apply(sourceQueryable);
            });

            // Short
            serviceQuery = ServiceQueryBuilder.New().IsEqual(nameof(TestClass.ShortVal), Guid.NewGuid().ToString()).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.Apply(sourceQueryable);
            });

            // Single
            serviceQuery = ServiceQueryBuilder.New().IsEqual(nameof(TestClass.SingleVal), Guid.NewGuid().ToString()).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.Apply(sourceQueryable);
            });

            // String
            //serviceQuery = ServiceQueryBuilder.New().IsEqual(nameof(TestClass.StringVal), null).Build();
            //Assert.Throws<ServiceQueryException>(() =>
            //{
            //    testQueryable = serviceQuery.Apply(sourceQueryable);
            //});

#if NET6_0_OR_GREATER
            // TimeOnly
            var tempTimeOnly = new T().GetDefault1Record().TimeOnlyVal;
            serviceQuery = ServiceQueryBuilder.New().IsEqual(nameof(TestClass.TimeOnlyVal), Guid.NewGuid().ToString()).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.Apply(sourceQueryable);
            });
#endif

            if (ValidateTimeSpan)
            {
                // TimeSpan
                serviceQuery = ServiceQueryBuilder.New().IsEqual(nameof(TestClass.TimeSpanVal), Guid.NewGuid().ToString()).Build();
                Assert.Throws<ServiceQueryException>(() =>
                {
                    testQueryable = serviceQuery.Apply(sourceQueryable);
                });
            }
#if NET7_0_OR_GREATER
            if (ValidateUInt128)
            {
                // UInt128
                serviceQuery = ServiceQueryBuilder.New().IsEqual(nameof(TestClass.UInt128Val), Guid.NewGuid().ToString()).Build();
                Assert.Throws<ServiceQueryException>(() =>
                {
                    testQueryable = serviceQuery.Apply(sourceQueryable);
                });
            }
#endif
            if (ValidateUInt64)
            {
                // UInt64
                serviceQuery = ServiceQueryBuilder.New().IsEqual(nameof(TestClass.UInt64Val), Guid.NewGuid().ToString()).Build();
                Assert.Throws<ServiceQueryException>(() =>
                {
                    testQueryable = serviceQuery.Apply(sourceQueryable);
                });
            }

            // UInt32
            serviceQuery = ServiceQueryBuilder.New().IsEqual(nameof(TestClass.UInt32Val), Guid.NewGuid().ToString()).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.Apply(sourceQueryable);
            });

            // UInt16
            serviceQuery = ServiceQueryBuilder.New().IsEqual(nameof(TestClass.UInt16Val), Guid.NewGuid().ToString()).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.Apply(sourceQueryable);
            });
        }

        [Fact]
        public void SyncIsEqualRequestTest()
        {
            var sourceQueryable = GetTestList();
            IQueryable<T> testQueryable;
            IServiceQueryRequest serviceQuery;
            //List<T> result;

            // Boolean
            serviceQuery = ServiceQueryRequestBuilder.New().IsEqual(nameof(TestClass.BoolVal), Guid.NewGuid().ToString()).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            });

            // ByteArray
            //var tempbyteArray = new byte[1] { 1 };
            //serviceQuery = ServiceQueryRequestBuilder.New().IsEqual(nameof(TestClass.ByteArrayVal), System.Text.Encoding.UTF8.GetString(tempbyteArray)).Build();
            //testQueryable = serviceQuery.GetQueryable(sourceQueryable);
            //result = testQueryable.ToList();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 1);
            //result = testQueryable.ToList();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 1);

            // Byte
            serviceQuery = ServiceQueryRequestBuilder.New().IsEqual(nameof(TestClass.ByteVal), Guid.NewGuid().ToString()).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            });

            // Char
            serviceQuery = ServiceQueryRequestBuilder.New().IsEqual(nameof(TestClass.CharVal), Guid.NewGuid().ToString()).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            });

            if (ValidateDateTimeOffset)
            {
                // DateTimeOffset
                var tempDateTimeOffset = new T().GetDefault1Record().DateTimeOffsetVal;
                serviceQuery = ServiceQueryRequestBuilder.New().IsEqual(nameof(TestClass.DateTimeOffsetVal), Guid.NewGuid().ToString()).Build();
                Assert.Throws<ServiceQueryException>(() =>
                {
                    testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
                });
            }

#if NET6_0_OR_GREATER
            // DateOnly
            var tempDateOnly = new T().GetDefault1Record().DateOnlyVal;
            serviceQuery = ServiceQueryRequestBuilder.New().IsEqual(nameof(TestClass.DateOnlyVal), Guid.NewGuid().ToString()).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            });
#endif

            // DateTime
            var tempDateTime = new T().GetDefault1Record().DateTimeVal;
            serviceQuery = ServiceQueryRequestBuilder.New().IsEqual(nameof(TestClass.DateTimeVal), Guid.NewGuid().ToString()).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            });

            // Decimal
            serviceQuery = ServiceQueryRequestBuilder.New().IsEqual(nameof(TestClass.DecimalVal), Guid.NewGuid().ToString()).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            });

            // Double
            serviceQuery = ServiceQueryRequestBuilder.New().IsEqual(nameof(TestClass.DoubleVal), Guid.NewGuid().ToString()).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            });

            // Float
            serviceQuery = ServiceQueryRequestBuilder.New().IsEqual(nameof(TestClass.FloatVal), Guid.NewGuid().ToString()).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            });

            // Guid
            serviceQuery = ServiceQueryRequestBuilder.New().IsEqual(nameof(TestClass.GuidVal), "zzzzz").Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            });

            // Int
            serviceQuery = ServiceQueryRequestBuilder.New().IsEqual(nameof(TestClass.IntVal), Guid.NewGuid().ToString()).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            });

            // Long
            serviceQuery = ServiceQueryRequestBuilder.New().IsEqual(nameof(TestClass.LongVal), Guid.NewGuid().ToString()).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            });

            // SByte
            serviceQuery = ServiceQueryRequestBuilder.New().IsEqual(nameof(TestClass.SByteVal), Guid.NewGuid().ToString()).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            });

            // Short
            serviceQuery = ServiceQueryRequestBuilder.New().IsEqual(nameof(TestClass.ShortVal), Guid.NewGuid().ToString()).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            });

            // Single
            serviceQuery = ServiceQueryRequestBuilder.New().IsEqual(nameof(TestClass.SingleVal), Guid.NewGuid().ToString()).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            });

            // String
            //serviceQuery = ServiceQueryRequestBuilder.New().IsEqual(nameof(TestClass.StringVal), null).Build();
            //Assert.Throws<ServiceQueryException>(() =>
            //{
            //    testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            //});

#if NET6_0_OR_GREATER
            // TimeOnly
            var tempTimeOnly = new T().GetDefault1Record().TimeOnlyVal;
            serviceQuery = ServiceQueryRequestBuilder.New().IsEqual(nameof(TestClass.TimeOnlyVal), Guid.NewGuid().ToString()).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            });
#endif

            if (ValidateTimeSpan)
            {
                // TimeSpan
                serviceQuery = ServiceQueryRequestBuilder.New().IsEqual(nameof(TestClass.TimeSpanVal), Guid.NewGuid().ToString()).Build();
                Assert.Throws<ServiceQueryException>(() =>
                {
                    testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
                });
            }
#if NET7_0_OR_GREATER
            if (ValidateUInt128)
            {
                // UInt128
                serviceQuery = ServiceQueryRequestBuilder.New().IsEqual(nameof(TestClass.UInt128Val), Guid.NewGuid().ToString()).Build();
                Assert.Throws<ServiceQueryException>(() =>
                {
                    testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
                });
            }
#endif
            if (ValidateUInt64)
            {
                // UInt64
                serviceQuery = ServiceQueryRequestBuilder.New().IsEqual(nameof(TestClass.UInt64Val), Guid.NewGuid().ToString()).Build();
                Assert.Throws<ServiceQueryException>(() =>
                {
                    testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
                });
            }

            // UInt32
            serviceQuery = ServiceQueryRequestBuilder.New().IsEqual(nameof(TestClass.UInt32Val), Guid.NewGuid().ToString()).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            });

            // UInt16
            serviceQuery = ServiceQueryRequestBuilder.New().IsEqual(nameof(TestClass.UInt16Val), Guid.NewGuid().ToString()).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            });
        }

        [Fact]
        public void SyncNullIsEqualStandardTest()
        {
            var sourceQueryable = GetTestList();
            IQueryable<T> testQueryable;
            IServiceQuery serviceQuery;
            //List<T> result;

            // Boolean
            serviceQuery = ServiceQueryBuilder.New().IsEqual(nameof(TestClass.NullBoolVal), Guid.NewGuid().ToString()).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.Apply(sourceQueryable);
            });

            // ByteArray
            //var tempbyteArray = new byte[1] { 1 };
            //serviceQuery = ServiceQueryBuilder.New().IsEqual(nameof(TestClass.NullByteArrayVal), System.Text.Encoding.UTF8.GetString(tempbyteArray)).Build();
            //testQueryable = serviceQuery.GetQueryable(sourceQueryable);
            //result = testQueryable.ToList();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 0);
            //result = testQueryable.ToList();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 0);

            // Byte
            serviceQuery = ServiceQueryBuilder.New().IsEqual(nameof(TestClass.NullByteVal), Guid.NewGuid().ToString()).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.Apply(sourceQueryable);
            });

            // Char
            serviceQuery = ServiceQueryBuilder.New().IsEqual(nameof(TestClass.NullCharVal), Guid.NewGuid().ToString()).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.Apply(sourceQueryable);
            });

            if (ValidateDateTimeOffset)
            {
                // DateTimeOffset
                var tempDateTimeOffset = new T().GetDefault1Record().DateTimeOffsetVal;
                serviceQuery = ServiceQueryBuilder.New().IsEqual(nameof(TestClass.NullDateTimeOffsetVal), Guid.NewGuid().ToString()).Build();
                Assert.Throws<ServiceQueryException>(() =>
                {
                    testQueryable = serviceQuery.Apply(sourceQueryable);
                });
            }

#if NET6_0_OR_GREATER
            // DateOnly
            var tempDateOnly = new T().GetDefault1Record().DateOnlyVal;
            serviceQuery = ServiceQueryBuilder.New().IsEqual(nameof(TestClass.NullDateOnlyVal), Guid.NewGuid().ToString()).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.Apply(sourceQueryable);
            });
#endif

            // DateTime
            var tempDateTime = new T().GetDefault1Record().DateTimeVal;
            serviceQuery = ServiceQueryBuilder.New().IsEqual(nameof(TestClass.NullDateTimeVal), Guid.NewGuid().ToString()).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.Apply(sourceQueryable);
            });

            // Decimal
            serviceQuery = ServiceQueryBuilder.New().IsEqual(nameof(TestClass.NullDecimalVal), Guid.NewGuid().ToString()).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.Apply(sourceQueryable);
            });

            // Double
            serviceQuery = ServiceQueryBuilder.New().IsEqual(nameof(TestClass.NullDoubleVal), Guid.NewGuid().ToString()).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.Apply(sourceQueryable);
            });

            // Float
            serviceQuery = ServiceQueryBuilder.New().IsEqual(nameof(TestClass.NullFloatVal), Guid.NewGuid().ToString()).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.Apply(sourceQueryable);
            });

            // Guid
            serviceQuery = ServiceQueryBuilder.New().IsEqual(nameof(TestClass.NullGuidVal), "zzzzz").Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.Apply(sourceQueryable);
            });

            // Int
            serviceQuery = ServiceQueryBuilder.New().IsEqual(nameof(TestClass.NullIntVal), Guid.NewGuid().ToString()).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.Apply(sourceQueryable);
            });

            // Long
            serviceQuery = ServiceQueryBuilder.New().IsEqual(nameof(TestClass.NullLongVal), Guid.NewGuid().ToString()).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.Apply(sourceQueryable);
            });

            // SByte
            serviceQuery = ServiceQueryBuilder.New().IsEqual(nameof(TestClass.NullSByteVal), Guid.NewGuid().ToString()).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.Apply(sourceQueryable);
            });

            // Short
            serviceQuery = ServiceQueryBuilder.New().IsEqual(nameof(TestClass.NullShortVal), Guid.NewGuid().ToString()).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.Apply(sourceQueryable);
            });

            // Single
            serviceQuery = ServiceQueryBuilder.New().IsEqual(nameof(TestClass.NullSingleVal), Guid.NewGuid().ToString()).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.Apply(sourceQueryable);
            });

            // String
            //serviceQuery = ServiceQueryBuilder.New().IsEqual(nameof(TestClass.NullStringVal), null).Build();
            //Assert.Throws<ServiceQueryException>(() =>
            //{
            //    testQueryable = serviceQuery.Apply(sourceQueryable);
            //});

#if NET6_0_OR_GREATER
            // TimeOnly
            var tempTimeOnly = new T().GetDefault1Record().TimeOnlyVal;
            serviceQuery = ServiceQueryBuilder.New().IsEqual(nameof(TestClass.NullTimeOnlyVal), Guid.NewGuid().ToString()).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.Apply(sourceQueryable);
            });
#endif

            if (ValidateTimeSpan)
            {
                // TimeSpan
                serviceQuery = ServiceQueryBuilder.New().IsEqual(nameof(TestClass.NullTimeSpanVal), Guid.NewGuid().ToString()).Build();
                Assert.Throws<ServiceQueryException>(() =>
                {
                    testQueryable = serviceQuery.Apply(sourceQueryable);
                });
            }
#if NET7_0_OR_GREATER
            if (ValidateUInt128)
            {
                // UInt128
                serviceQuery = ServiceQueryBuilder.New().IsEqual(nameof(TestClass.NullUInt128Val), Guid.NewGuid().ToString()).Build();
                Assert.Throws<ServiceQueryException>(() =>
                {
                    testQueryable = serviceQuery.Apply(sourceQueryable);
                });
            }
#endif
            if (ValidateUInt64)
            {
                // UInt64
                serviceQuery = ServiceQueryBuilder.New().IsEqual(nameof(TestClass.NullUInt64Val), Guid.NewGuid().ToString()).Build();
                Assert.Throws<ServiceQueryException>(() =>
                {
                    testQueryable = serviceQuery.Apply(sourceQueryable);
                });
            }

            // UInt32
            serviceQuery = ServiceQueryBuilder.New().IsEqual(nameof(TestClass.NullUInt32Val), Guid.NewGuid().ToString()).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.Apply(sourceQueryable);
            });

            // UInt16
            serviceQuery = ServiceQueryBuilder.New().IsEqual(nameof(TestClass.NullUInt16Val), Guid.NewGuid().ToString()).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.Apply(sourceQueryable);
            });
        }

        [Fact]
        public void SyncNullIsEqualRequestTest()
        {
            var sourceQueryable = GetTestList();
            IQueryable<T> testQueryable;
            IServiceQueryRequest serviceQuery;
            //List<T> result;

            // Boolean
            serviceQuery = ServiceQueryRequestBuilder.New().IsEqual(nameof(TestClass.NullBoolVal), Guid.NewGuid().ToString()).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            });

            // ByteArray
            //var tempbyteArray = new byte[1] { 1 };
            //serviceQuery = ServiceQueryRequestBuilder.New().IsEqual(nameof(TestClass.NullByteArrayVal), System.Text.Encoding.UTF8.GetString(tempbyteArray)).Build();
            //testQueryable = serviceQuery.GetQueryable(sourceQueryable);
            //result = testQueryable.ToList();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 0);
            //result = testQueryable.ToList();
            //Assert.NotNull(result);
            //Assert.True(result.Count == 0);

            // Byte
            serviceQuery = ServiceQueryRequestBuilder.New().IsEqual(nameof(TestClass.NullByteVal), Guid.NewGuid().ToString()).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            });

            // Char
            serviceQuery = ServiceQueryRequestBuilder.New().IsEqual(nameof(TestClass.NullCharVal), Guid.NewGuid().ToString()).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            });

            if (ValidateDateTimeOffset)
            {
                // DateTimeOffset
                var tempDateTimeOffset = new T().GetDefault1Record().DateTimeOffsetVal;
                serviceQuery = ServiceQueryRequestBuilder.New().IsEqual(nameof(TestClass.NullDateTimeOffsetVal), Guid.NewGuid().ToString()).Build();
                Assert.Throws<ServiceQueryException>(() =>
                {
                    testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
                });
            }

#if NET6_0_OR_GREATER
            // DateOnly
            var tempDateOnly = new T().GetDefault1Record().DateOnlyVal;
            serviceQuery = ServiceQueryRequestBuilder.New().IsEqual(nameof(TestClass.NullDateOnlyVal), Guid.NewGuid().ToString()).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            });
#endif

            // DateTime
            var tempDateTime = new T().GetDefault1Record().DateTimeVal;
            serviceQuery = ServiceQueryRequestBuilder.New().IsEqual(nameof(TestClass.NullDateTimeVal), Guid.NewGuid().ToString()).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            });

            // Decimal
            serviceQuery = ServiceQueryRequestBuilder.New().IsEqual(nameof(TestClass.NullDecimalVal), Guid.NewGuid().ToString()).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            });

            // Double
            serviceQuery = ServiceQueryRequestBuilder.New().IsEqual(nameof(TestClass.NullDoubleVal), Guid.NewGuid().ToString()).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            });

            // Float
            serviceQuery = ServiceQueryRequestBuilder.New().IsEqual(nameof(TestClass.NullFloatVal), Guid.NewGuid().ToString()).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            });

            // Guid
            serviceQuery = ServiceQueryRequestBuilder.New().IsEqual(nameof(TestClass.NullGuidVal), "zzzz").Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            });

            // Int
            serviceQuery = ServiceQueryRequestBuilder.New().IsEqual(nameof(TestClass.NullIntVal), Guid.NewGuid().ToString()).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            });

            // Long
            serviceQuery = ServiceQueryRequestBuilder.New().IsEqual(nameof(TestClass.NullLongVal), Guid.NewGuid().ToString()).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            });

            // SByte
            serviceQuery = ServiceQueryRequestBuilder.New().IsEqual(nameof(TestClass.NullSByteVal), Guid.NewGuid().ToString()).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            });

            // Short
            serviceQuery = ServiceQueryRequestBuilder.New().IsEqual(nameof(TestClass.NullShortVal), Guid.NewGuid().ToString()).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            });

            // Single
            serviceQuery = ServiceQueryRequestBuilder.New().IsEqual(nameof(TestClass.NullSingleVal), Guid.NewGuid().ToString()).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            });

            // String
            //serviceQuery = ServiceQueryRequestBuilder.New().IsEqual(nameof(TestClass.NullStringVal), Guid.NewGuid().ToString()).Build();
            //Assert.Throws<ServiceQueryException>(() =>
            //{
            //    testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            //});

#if NET6_0_OR_GREATER
            // TimeOnly
            var tempTimeOnly = new T().GetDefault1Record().TimeOnlyVal;
            serviceQuery = ServiceQueryRequestBuilder.New().IsEqual(nameof(TestClass.NullTimeOnlyVal), Guid.NewGuid().ToString()).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            });
#endif

            if (ValidateTimeSpan)
            {
                // TimeSpan
                serviceQuery = ServiceQueryRequestBuilder.New().IsEqual(nameof(TestClass.NullTimeSpanVal), Guid.NewGuid().ToString()).Build();
                Assert.Throws<ServiceQueryException>(() =>
                {
                    testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
                });
            }
#if NET7_0_OR_GREATER
            if (ValidateUInt128)
            {
                // UInt128
                serviceQuery = ServiceQueryRequestBuilder.New().IsEqual(nameof(TestClass.NullUInt128Val), Guid.NewGuid().ToString()).Build();
                Assert.Throws<ServiceQueryException>(() =>
                {
                    testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
                });
            }
#endif
            if (ValidateUInt64)
            {
                // UInt64
                serviceQuery = ServiceQueryRequestBuilder.New().IsEqual(nameof(TestClass.NullUInt64Val), Guid.NewGuid().ToString()).Build();
                Assert.Throws<ServiceQueryException>(() =>
                {
                    testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
                });
            }

            // UInt32
            serviceQuery = ServiceQueryRequestBuilder.New().IsEqual(nameof(TestClass.NullUInt32Val), Guid.NewGuid().ToString()).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            });

            // UInt16
            serviceQuery = ServiceQueryRequestBuilder.New().IsEqual(nameof(TestClass.NullUInt16Val), Guid.NewGuid().ToString()).Build();
            Assert.Throws<ServiceQueryException>(() =>
            {
                testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            });
        }
    }
}
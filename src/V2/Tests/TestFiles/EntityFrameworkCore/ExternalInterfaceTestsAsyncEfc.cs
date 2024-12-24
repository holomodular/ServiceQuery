using Microsoft.EntityFrameworkCore;

namespace ServiceQuery.Xunit
{
    public class ExternalInterfaceTestsAsyncEfc : BaseTest
    {
        protected void ValidateSort(List<TestClass> sortedList, bool asc)
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
        public async Task ExternalInterfaceStandardTest()
        {
            var sourceQueryable = GetTestList();

            IServiceQuery serviceQuery;
            List<TestClass> result;
            TestClass retRecord;
            ITestClass oneRecord;

            // select
            serviceQuery = ServiceQueryBuilder.New().Build();
            var selectExp = serviceQuery.BuildSelectExpression<TestClass>();
            Assert.Null(selectExp);

            var ps = serviceQuery.GetSelectProperties<TestClass>();
            Assert.Null(selectExp);

            // where
            serviceQuery = ServiceQueryBuilder.New().Build();
            var whereExp = serviceQuery.BuildWhereExpression<TestClass>();
            Assert.Null(whereExp);

            serviceQuery = ServiceQueryBuilder.New()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .Build();
            whereExp = serviceQuery.BuildWhereExpression<TestClass>();
            Assert.NotNull(whereExp);
            result = sourceQueryable.Where(whereExp).ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            Assert.True(result[0].LongVal == 1);
            result = sourceQueryable.Where(whereExp).ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);

            // select, where, order
            serviceQuery = ServiceQueryBuilder.New()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .Select(nameof(TestClass.BoolVal))
                .Sort(nameof(TestClass.BoolVal), true)
                .Build();
            ps = serviceQuery.GetSelectProperties<TestClass>();
            Assert.NotNull(ps);
            Assert.True(ps.Count == 1);

            selectExp = serviceQuery.BuildSelectExpression<TestClass>();
            Assert.NotNull(selectExp);
            whereExp = serviceQuery.BuildWhereExpression<TestClass>();
            result = sourceQueryable.Where(whereExp).Select(selectExp).ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            result = sourceQueryable.Where(whereExp).Select(selectExp).ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            retRecord = result[0];
            oneRecord = new TestClass().GetDefault1Record(new TestClass());
            Assert.True(retRecord.BoolVal == oneRecord.BoolVal);
            Assert.True(retRecord.ByteArrayVal != oneRecord.ByteArrayVal);
            Assert.True(retRecord.ByteVal != oneRecord.ByteVal);
            Assert.True(retRecord.CharVal != oneRecord.CharVal);
            Assert.True(retRecord.DateTimeOffsetVal != oneRecord.DateTimeOffsetVal);
            Assert.True(retRecord.DateTimeVal != oneRecord.DateTimeVal);
            Assert.True(retRecord.DecimalVal != oneRecord.DecimalVal);
            Assert.True(retRecord.DoubleVal != oneRecord.DoubleVal);
            Assert.True(retRecord.FloatVal != oneRecord.FloatVal);
            Assert.True(retRecord.GuidVal != oneRecord.GuidVal);
            Assert.True(retRecord.IntVal != oneRecord.IntVal);
            Assert.True(retRecord.LongVal != oneRecord.LongVal);
            Assert.True(retRecord.SByteVal != oneRecord.SByteVal);
            Assert.True(retRecord.ShortVal != oneRecord.ShortVal);
            Assert.True(retRecord.SingleVal != oneRecord.SingleVal);
            Assert.True(retRecord.StringVal != oneRecord.StringVal);
            Assert.True(retRecord.TimeSpanVal != oneRecord.TimeSpanVal);
            Assert.True(retRecord.UInt16Val != oneRecord.UInt16Val);
            Assert.True(retRecord.UInt32Val != oneRecord.UInt32Val);
            Assert.True(retRecord.UInt64Val != oneRecord.UInt64Val);

            // Sort
            serviceQuery = ServiceQueryBuilder.New().Build();
            var sortExp = serviceQuery.BuildOrderByExpression<TestClass>(sourceQueryable);
            Assert.Null(sortExp);

            serviceQuery = ServiceQueryBuilder.New().Sort(nameof(TestClass.IntVal), true).Build();
            sortExp = serviceQuery.BuildOrderByExpression<TestClass>(sourceQueryable);
            Assert.NotNull(sortExp);
            result = sourceQueryable.OrderBy(sortExp).ToList();
            ValidateSort(result, true);
        }

        [Fact]
        public async Task NullExternalInterfaceStandardTest()
        {
            var sourceQueryable = GetTestList();

            IServiceQuery serviceQuery;
            List<TestClass> result;
            TestClass retRecord;
            ITestClass oneRecord;

            // select
            serviceQuery = ServiceQueryBuilder.New().Build();
            var selectExp = serviceQuery.BuildSelectExpression<TestClass>();
            Assert.Null(selectExp);

            var ps = serviceQuery.GetSelectProperties<TestClass>();
            Assert.Null(selectExp);

            // where
            serviceQuery = ServiceQueryBuilder.New().Build();
            var whereExp = serviceQuery.BuildWhereExpression<TestClass>();
            Assert.Null(whereExp);

            serviceQuery = ServiceQueryBuilder.New()
                .IsEqual(nameof(TestClass.NullIntVal), null)
                .Build();
            whereExp = serviceQuery.BuildWhereExpression<TestClass>();
            Assert.NotNull(whereExp);
            result = sourceQueryable.Where(whereExp).ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
            //Assert.True(result[0].LongVal == 1);
            result = sourceQueryable.Where(whereExp).ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);

            // select
            serviceQuery = ServiceQueryBuilder.New()
                .IsEqual(nameof(TestClass.NullIntVal), null)
                .Select(nameof(TestClass.NullBoolVal))
                .Build();
            ps = serviceQuery.GetSelectProperties<TestClass>();
            Assert.NotNull(ps);
            Assert.True(ps.Count == 1);

            selectExp = serviceQuery.BuildSelectExpression<TestClass>();
            Assert.NotNull(selectExp);
            whereExp = serviceQuery.BuildWhereExpression<TestClass>();
            result = sourceQueryable.Where(whereExp).Select(selectExp).OrderByDescending(x => x.IntVal).ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
            result = sourceQueryable.Where(whereExp).Select(selectExp).ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
            retRecord = result[0];
            oneRecord = new TestClass().GetDefault3Record(new TestClass());
            Assert.True(retRecord.NullBoolVal == oneRecord.NullBoolVal);
            Assert.True(retRecord.BoolVal != oneRecord.BoolVal);
            Assert.True(retRecord.ByteArrayVal != oneRecord.ByteArrayVal);
            Assert.True(retRecord.ByteVal != oneRecord.ByteVal);
            Assert.True(retRecord.CharVal != oneRecord.CharVal);
            Assert.True(retRecord.DateTimeOffsetVal != oneRecord.DateTimeOffsetVal);
            Assert.True(retRecord.DateTimeVal != oneRecord.DateTimeVal);
            Assert.True(retRecord.DecimalVal != oneRecord.DecimalVal);
            Assert.True(retRecord.DoubleVal != oneRecord.DoubleVal);
            Assert.True(retRecord.FloatVal != oneRecord.FloatVal);
            Assert.True(retRecord.GuidVal != oneRecord.GuidVal);
            Assert.True(retRecord.IntVal != oneRecord.IntVal);
            Assert.True(retRecord.LongVal != oneRecord.LongVal);
            Assert.True(retRecord.SByteVal != oneRecord.SByteVal);
            Assert.True(retRecord.ShortVal != oneRecord.ShortVal);
            Assert.True(retRecord.SingleVal != oneRecord.SingleVal);
            Assert.True(retRecord.StringVal != oneRecord.StringVal);
            Assert.True(retRecord.TimeSpanVal != oneRecord.TimeSpanVal);
            Assert.True(retRecord.UInt16Val != oneRecord.UInt16Val);
            Assert.True(retRecord.UInt32Val != oneRecord.UInt32Val);
            Assert.True(retRecord.UInt64Val != oneRecord.UInt64Val);

            // Sort
            serviceQuery = ServiceQueryBuilder.New().Build();
            var sortExp = serviceQuery.BuildOrderByExpression<TestClass>(sourceQueryable);
            Assert.Null(sortExp);

            serviceQuery = ServiceQueryBuilder.New().Sort(nameof(TestClass.IntVal), true).Build();
            sortExp = serviceQuery.BuildOrderByExpression<TestClass>(sourceQueryable);
            Assert.NotNull(sortExp);
            result = sourceQueryable.OrderBy(sortExp).ToList();
            ValidateSort(result, true);
        }

        [Fact]
        public async Task ExternalInterfaceRequestTest()
        {
            var sourceQueryable = GetTestList();

            IServiceQueryRequest serviceQuery;
            List<TestClass> result;
            TestClass retRecord;
            ITestClass oneRecord;

            // select
            serviceQuery = ServiceQueryRequestBuilder.New().Build();
            var selectExp = serviceQuery.GetServiceQuery().BuildSelectExpression<TestClass>();
            Assert.Null(selectExp);

            var ps = serviceQuery.GetServiceQuery().GetSelectProperties<TestClass>();
            Assert.Null(selectExp);

            // where
            serviceQuery = ServiceQueryRequestBuilder.New().Build();
            var whereExp = serviceQuery.GetServiceQuery().BuildWhereExpression<TestClass>();
            Assert.Null(whereExp);

            serviceQuery = ServiceQueryRequestBuilder.New()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .Build();
            whereExp = serviceQuery.GetServiceQuery().BuildWhereExpression<TestClass>();
            Assert.NotNull(whereExp);
            result = sourceQueryable.Where(whereExp).ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            Assert.True(result[0].LongVal == 1);
            result = sourceQueryable.Where(whereExp).ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);

            // select, where, order
            serviceQuery = ServiceQueryRequestBuilder.New()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .Select(nameof(TestClass.BoolVal))
                .Sort(nameof(TestClass.BoolVal), true)
                .Build();
            ps = serviceQuery.GetServiceQuery().GetSelectProperties<TestClass>();
            Assert.NotNull(ps);
            Assert.True(ps.Count == 1);

            selectExp = serviceQuery.GetServiceQuery().BuildSelectExpression<TestClass>();
            Assert.NotNull(selectExp);
            whereExp = serviceQuery.GetServiceQuery().BuildWhereExpression<TestClass>();
            result = sourceQueryable.Where(whereExp).Select(selectExp).ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            result = sourceQueryable.Where(whereExp).Select(selectExp).ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            retRecord = result[0];
            oneRecord = new TestClass().GetDefault1Record(new TestClass());
            Assert.True(retRecord.BoolVal == oneRecord.BoolVal);
            Assert.True(retRecord.ByteArrayVal != oneRecord.ByteArrayVal);
            Assert.True(retRecord.ByteVal != oneRecord.ByteVal);
            Assert.True(retRecord.CharVal != oneRecord.CharVal);
            Assert.True(retRecord.DateTimeOffsetVal != oneRecord.DateTimeOffsetVal);
            Assert.True(retRecord.DateTimeVal != oneRecord.DateTimeVal);
            Assert.True(retRecord.DecimalVal != oneRecord.DecimalVal);
            Assert.True(retRecord.DoubleVal != oneRecord.DoubleVal);
            Assert.True(retRecord.FloatVal != oneRecord.FloatVal);
            Assert.True(retRecord.GuidVal != oneRecord.GuidVal);
            Assert.True(retRecord.IntVal != oneRecord.IntVal);
            Assert.True(retRecord.LongVal != oneRecord.LongVal);
            Assert.True(retRecord.SByteVal != oneRecord.SByteVal);
            Assert.True(retRecord.ShortVal != oneRecord.ShortVal);
            Assert.True(retRecord.SingleVal != oneRecord.SingleVal);
            Assert.True(retRecord.StringVal != oneRecord.StringVal);
            Assert.True(retRecord.TimeSpanVal != oneRecord.TimeSpanVal);
            Assert.True(retRecord.UInt16Val != oneRecord.UInt16Val);
            Assert.True(retRecord.UInt32Val != oneRecord.UInt32Val);
            Assert.True(retRecord.UInt64Val != oneRecord.UInt64Val);

            // Sort
            serviceQuery = ServiceQueryRequestBuilder.New().Build();
            var sortExp = serviceQuery.GetServiceQuery().BuildOrderByExpression<TestClass>(sourceQueryable);
            Assert.Null(sortExp);

            serviceQuery = ServiceQueryRequestBuilder.New().Sort(nameof(TestClass.IntVal), true).Build();
            sortExp = serviceQuery.GetServiceQuery().BuildOrderByExpression<TestClass>(sourceQueryable);
            Assert.NotNull(sortExp);
            result = sourceQueryable.OrderBy(sortExp).ToList();
            ValidateSort(result, true);
        }

        [Fact]
        public async Task NullExternalInterfaceRequestTest()
        {
            var sourceQueryable = GetTestList();

            IServiceQueryRequest serviceQuery;
            List<TestClass> result;
            TestClass retRecord;
            ITestClass oneRecord;

            // select
            serviceQuery = ServiceQueryRequestBuilder.New().Build();
            var selectExp = serviceQuery.GetServiceQuery().BuildSelectExpression<TestClass>();
            Assert.Null(selectExp);

            var ps = serviceQuery.GetServiceQuery().GetSelectProperties<TestClass>();
            Assert.Null(selectExp);

            // where
            serviceQuery = ServiceQueryRequestBuilder.New().Build();
            var whereExp = serviceQuery.GetServiceQuery().BuildWhereExpression<TestClass>();
            Assert.Null(whereExp);

            serviceQuery = ServiceQueryRequestBuilder.New()
                .IsEqual(nameof(TestClass.NullIntVal), null)
                .Build();
            whereExp = serviceQuery.GetServiceQuery().BuildWhereExpression<TestClass>();
            Assert.NotNull(whereExp);
            result = sourceQueryable.Where(whereExp).ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
            //Assert.True(result[0].LongVal == 1);
            result = sourceQueryable.Where(whereExp).ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);

            // select
            serviceQuery = ServiceQueryRequestBuilder.New()
                .IsEqual(nameof(TestClass.NullIntVal), null)
                .Select(nameof(TestClass.NullBoolVal))
                .Build();
            ps = serviceQuery.GetServiceQuery().GetSelectProperties<TestClass>();
            Assert.NotNull(ps);
            Assert.True(ps.Count == 1);

            selectExp = serviceQuery.GetServiceQuery().BuildSelectExpression<TestClass>();
            Assert.NotNull(selectExp);
            whereExp = serviceQuery.GetServiceQuery().BuildWhereExpression<TestClass>();
            result = sourceQueryable.Where(whereExp).Select(selectExp).OrderByDescending(x => x.IntVal).ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
            result = sourceQueryable.Where(whereExp).Select(selectExp).ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
            retRecord = result[0];
            oneRecord = new TestClass().GetDefault3Record(new TestClass());
            Assert.True(retRecord.NullBoolVal == oneRecord.NullBoolVal);
            Assert.True(retRecord.BoolVal != oneRecord.BoolVal);
            Assert.True(retRecord.ByteArrayVal != oneRecord.ByteArrayVal);
            Assert.True(retRecord.ByteVal != oneRecord.ByteVal);
            Assert.True(retRecord.CharVal != oneRecord.CharVal);
            Assert.True(retRecord.DateTimeOffsetVal != oneRecord.DateTimeOffsetVal);
            Assert.True(retRecord.DateTimeVal != oneRecord.DateTimeVal);
            Assert.True(retRecord.DecimalVal != oneRecord.DecimalVal);
            Assert.True(retRecord.DoubleVal != oneRecord.DoubleVal);
            Assert.True(retRecord.FloatVal != oneRecord.FloatVal);
            Assert.True(retRecord.GuidVal != oneRecord.GuidVal);
            Assert.True(retRecord.IntVal != oneRecord.IntVal);
            Assert.True(retRecord.LongVal != oneRecord.LongVal);
            Assert.True(retRecord.SByteVal != oneRecord.SByteVal);
            Assert.True(retRecord.ShortVal != oneRecord.ShortVal);
            Assert.True(retRecord.SingleVal != oneRecord.SingleVal);
            Assert.True(retRecord.StringVal != oneRecord.StringVal);
            Assert.True(retRecord.TimeSpanVal != oneRecord.TimeSpanVal);
            Assert.True(retRecord.UInt16Val != oneRecord.UInt16Val);
            Assert.True(retRecord.UInt32Val != oneRecord.UInt32Val);
            Assert.True(retRecord.UInt64Val != oneRecord.UInt64Val);

            // Sort
            serviceQuery = ServiceQueryRequestBuilder.New().Build();
            var sortExp = serviceQuery.GetServiceQuery().BuildOrderByExpression<TestClass>(sourceQueryable);
            Assert.Null(sortExp);

            serviceQuery = ServiceQueryRequestBuilder.New().Sort(nameof(TestClass.IntVal), true).Build();
            sortExp = serviceQuery.GetServiceQuery().BuildOrderByExpression<TestClass>(sourceQueryable);
            Assert.NotNull(sortExp);
            result = sourceQueryable.OrderBy(sortExp).ToList();
            ValidateSort(result, true);
        }

        [Fact]
        public async Task ExternalInterfaceRequestExecuteTest()
        {
            var sourceQueryable = GetTestList();

            IServiceQueryRequest serviceQuery;
            ServiceQueryResponse<TestClass> result;

            IQueryable<TestClass> nullquery = null;
            serviceQuery = ServiceQueryRequestBuilder.New().Build();
            result = serviceQuery.Execute(nullquery);
            Assert.Null(result);
            result = serviceQuery.Execute(nullquery);
            Assert.Null(result);

            serviceQuery = ServiceQueryRequestBuilder.New().Build();
            result = serviceQuery.Execute(sourceQueryable);
            Assert.NotNull(result);
            Assert.Null(result.Aggregate);
            Assert.True(result.Count == 0);
            Assert.NotNull(result.List);
            Assert.True(result.List.Count == 4);
            result = serviceQuery.Execute(sourceQueryable);
            Assert.NotNull(result);
            Assert.Null(result.Aggregate);
            Assert.True(result.Count == 0);
            Assert.NotNull(result.List);
            Assert.True(result.List.Count == 4);

            serviceQuery = ServiceQueryRequestBuilder.New().Paging(1, 1000, true).Build();
            result = serviceQuery.Execute(sourceQueryable);
            Assert.NotNull(result);
            Assert.Null(result.Aggregate);
            Assert.True(result.Count == 4);
            Assert.NotNull(result.List);
            Assert.True(result.List.Count == 4);
            result = serviceQuery.Execute(sourceQueryable);
            Assert.NotNull(result);
            Assert.Null(result.Aggregate);
            Assert.True(result.Count == 4);
            Assert.NotNull(result.List);
            Assert.True(result.List.Count == 4);

            serviceQuery = ServiceQueryRequestBuilder.New()
                .IncludeCount()
                .Build();
            result = serviceQuery.Execute(sourceQueryable);
            Assert.NotNull(result);
            Assert.Null(result.Aggregate);
            Assert.True(result.Count == 4);
            Assert.NotNull(result.List);
            Assert.True(result.List.Count == 4);
            result = serviceQuery.Execute(sourceQueryable);
            Assert.NotNull(result);
            Assert.Null(result.Aggregate);
            Assert.True(result.Count == 4);
            Assert.NotNull(result.List);
            Assert.True(result.List.Count == 4);

            // select, where, order
            serviceQuery = ServiceQueryRequestBuilder.New()
                .IsEqual(nameof(TestClass.SharedParentKey), "1")
                .Select(nameof(TestClass.BoolVal))
                .SortAsc(nameof(TestClass.BoolVal))
                .IncludeCount()
                .Build();
            result = serviceQuery.Execute(sourceQueryable);
            Assert.NotNull(result);
            Assert.Null(result.Aggregate);
            Assert.True(result.Count == 4);
            Assert.NotNull(result.List);
            Assert.True(result.List.Count == 4);
            result = serviceQuery.Execute(sourceQueryable);
            Assert.NotNull(result);
            Assert.Null(result.Aggregate);
            Assert.True(result.Count == 4);
            Assert.NotNull(result.List);
            Assert.True(result.List.Count == 4);

            // Sort
            serviceQuery = ServiceQueryRequestBuilder.New().Sort(nameof(TestClass.IntVal), true).Build();
            result = serviceQuery.Execute(sourceQueryable);
            Assert.NotNull(result);
            Assert.Null(result.Aggregate);
            Assert.True(result.Count == 0);
            Assert.NotNull(result.List);
            Assert.True(result.List.Count == 4);
            ValidateSort(result.List, true);
            result = serviceQuery.Execute(sourceQueryable);
            Assert.NotNull(result);
            Assert.Null(result.Aggregate);
            Assert.True(result.Count == 0);
            Assert.NotNull(result.List);
            Assert.True(result.List.Count == 4);

            // Aggregate
            serviceQuery = ServiceQueryRequestBuilder.New().Maximum(nameof(TestClass.IntVal)).Build();
            result = serviceQuery.Execute(sourceQueryable);
            Assert.NotNull(result);
            Assert.NotNull(result.Aggregate);
            Assert.True(result.Aggregate.Value == 3);
            Assert.True(result.Count == null);
            Assert.NotNull(result.List);
            Assert.True(result.List.Count == 0);
            result = serviceQuery.Execute(sourceQueryable);
            Assert.NotNull(result);
            Assert.NotNull(result.Aggregate);
            Assert.True(result.Aggregate.Value == 3);
            Assert.True(result.Count == null);
            Assert.NotNull(result.List);
            Assert.True(result.List.Count == 0);

            // Aggregate includecount
            serviceQuery = ServiceQueryRequestBuilder.New().Count().IncludeCount().Build();
            result = serviceQuery.Execute(sourceQueryable);
            Assert.NotNull(result);
            Assert.NotNull(result.Aggregate);
            Assert.True(result.Aggregate.Value == 4);
            Assert.True(result.Count == 4);
            Assert.NotNull(result.List);
            Assert.True(result.List.Count == 0);
            result = serviceQuery.Execute(sourceQueryable);
            Assert.NotNull(result);
            Assert.NotNull(result.Aggregate);
            Assert.True(result.Aggregate.Value == 4);
            Assert.True(result.Count == 4);
            Assert.NotNull(result.List);
            Assert.True(result.List.Count == 0);
        }
    }
}
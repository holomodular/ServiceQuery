using System.Data.Entity;

namespace ServiceQuery.Xunit
{
    public class SelectTests : SelectTests<TestClass>
    {
        public override IQueryable<TestClass> GetTestList()
        {
            return new TestClass().GetDefaultList().AsQueryable();
        }
    }

    public abstract class SelectTests<T> : BaseTest<T> where T : class, ITestClass, new()
    {
        [Fact]
        public async Task SelectStandardTests()
        {
            var sourceQueryable = GetTestList();
            IQueryable<T> testQueryable;
            IServiceQuery serviceQuery;
            List<T> result;
            T retRecord;
            ITestClass oneRecord;

            // No select
            serviceQuery = ServiceQueryBuilder.New()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            retRecord = result[0];
            oneRecord = new T().GetDefault1Record();
            Assert.True(retRecord.BoolVal == oneRecord.BoolVal);
            Assert.True(retRecord.ByteArrayVal[0] == oneRecord.ByteArrayVal[0]);
            Assert.True(retRecord.ByteVal == oneRecord.ByteVal);
            Assert.True(retRecord.CharVal == oneRecord.CharVal);
            Assert.True(retRecord.DateTimeOffsetVal == oneRecord.DateTimeOffsetVal);
            Assert.True(retRecord.DateTimeVal == oneRecord.DateTimeVal);
            Assert.True(retRecord.DecimalVal == oneRecord.DecimalVal);
            Assert.True(retRecord.DoubleVal == oneRecord.DoubleVal);
            Assert.True(retRecord.FloatVal == oneRecord.FloatVal);
            Assert.True(retRecord.GuidVal == oneRecord.GuidVal);
            Assert.True(retRecord.IntVal == oneRecord.IntVal);
            Assert.True(retRecord.LongVal == oneRecord.LongVal);
            Assert.True(retRecord.SByteVal == oneRecord.SByteVal);
            Assert.True(retRecord.ShortVal == oneRecord.ShortVal);
            Assert.True(retRecord.SingleVal == oneRecord.SingleVal);
            Assert.True(retRecord.StringVal == oneRecord.StringVal);
            Assert.True(retRecord.TimeSpanVal == oneRecord.TimeSpanVal);
            Assert.True(retRecord.UInt16Val == oneRecord.UInt16Val);
            Assert.True(retRecord.UInt32Val == oneRecord.UInt32Val);
            Assert.True(retRecord.UInt64Val == oneRecord.UInt64Val);

            // 1 select
            serviceQuery = ServiceQueryBuilder.New()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .Select(nameof(TestClass.BoolVal))
                .Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            retRecord = result[0];
            oneRecord = new T().GetDefault1Record();
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

            // 2 select
            serviceQuery = ServiceQueryBuilder.New()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .Select(nameof(TestClass.BoolVal))
                .Select(nameof(TestClass.ByteArrayVal))
                .Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            retRecord = result[0];
            oneRecord = new T().GetDefault1Record();
            Assert.True(retRecord.BoolVal == oneRecord.BoolVal);
            Assert.True(retRecord.ByteArrayVal[0] == oneRecord.ByteArrayVal[0]);
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

            // 3 select
            serviceQuery = ServiceQueryBuilder.New()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .Select(nameof(TestClass.BoolVal))
                .Select(nameof(TestClass.ByteArrayVal))
                .Select(nameof(TestClass.ByteVal))
                .Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            retRecord = result[0];
            oneRecord = new T().GetDefault1Record();
            Assert.True(retRecord.BoolVal == oneRecord.BoolVal);
            Assert.True(retRecord.ByteArrayVal[0] == oneRecord.ByteArrayVal[0]);
            Assert.True(retRecord.ByteVal == oneRecord.ByteVal);
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

            // 4 select
            serviceQuery = ServiceQueryBuilder.New()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .Select(nameof(TestClass.BoolVal))
                .Select(nameof(TestClass.ByteArrayVal))
                .Select(nameof(TestClass.ByteVal))
                .Select(nameof(TestClass.CharVal))
                .Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            retRecord = result[0];
            oneRecord = new T().GetDefault1Record();
            Assert.True(retRecord.BoolVal == oneRecord.BoolVal);
            Assert.True(retRecord.ByteArrayVal[0] == oneRecord.ByteArrayVal[0]);
            Assert.True(retRecord.ByteVal == oneRecord.ByteVal);
            Assert.True(retRecord.CharVal == oneRecord.CharVal);
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

            // 5 select
            serviceQuery = ServiceQueryBuilder.New()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .Select(nameof(TestClass.BoolVal))
                .Select(nameof(TestClass.ByteArrayVal))
                .Select(nameof(TestClass.ByteVal))
                .Select(nameof(TestClass.CharVal))
                .Select(nameof(TestClass.DateTimeOffsetVal))
                .Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            retRecord = result[0];
            oneRecord = new T().GetDefault1Record();
            Assert.True(retRecord.BoolVal == oneRecord.BoolVal);
            Assert.True(retRecord.ByteArrayVal[0] == oneRecord.ByteArrayVal[0]);
            Assert.True(retRecord.ByteVal == oneRecord.ByteVal);
            Assert.True(retRecord.CharVal == oneRecord.CharVal);
            Assert.True(retRecord.DateTimeOffsetVal == oneRecord.DateTimeOffsetVal);
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

            // 6 select
            serviceQuery = ServiceQueryBuilder.New()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .Select(nameof(TestClass.BoolVal))
                .Select(nameof(TestClass.ByteArrayVal))
                .Select(nameof(TestClass.ByteVal))
                .Select(nameof(TestClass.CharVal))
                .Select(nameof(TestClass.DateTimeOffsetVal))
                .Select(nameof(TestClass.DateTimeVal))
                .Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            retRecord = result[0];
            oneRecord = new T().GetDefault1Record();
            Assert.True(retRecord.BoolVal == oneRecord.BoolVal);
            Assert.True(retRecord.ByteArrayVal[0] == oneRecord.ByteArrayVal[0]);
            Assert.True(retRecord.ByteVal == oneRecord.ByteVal);
            Assert.True(retRecord.CharVal == oneRecord.CharVal);
            Assert.True(retRecord.DateTimeOffsetVal == oneRecord.DateTimeOffsetVal);
            Assert.True(retRecord.DateTimeVal == oneRecord.DateTimeVal);
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

            // 7 select
            serviceQuery = ServiceQueryBuilder.New()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .Select(nameof(TestClass.BoolVal))
                .Select(nameof(TestClass.ByteArrayVal))
                .Select(nameof(TestClass.ByteVal))
                .Select(nameof(TestClass.CharVal))
                .Select(nameof(TestClass.DateTimeOffsetVal))
                .Select(nameof(TestClass.DateTimeVal))
                .Select(nameof(TestClass.DecimalVal))
                .Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            retRecord = result[0];
            oneRecord = new T().GetDefault1Record();
            Assert.True(retRecord.BoolVal == oneRecord.BoolVal);
            Assert.True(retRecord.ByteArrayVal[0] == oneRecord.ByteArrayVal[0]);
            Assert.True(retRecord.ByteVal == oneRecord.ByteVal);
            Assert.True(retRecord.CharVal == oneRecord.CharVal);
            Assert.True(retRecord.DateTimeOffsetVal == oneRecord.DateTimeOffsetVal);
            Assert.True(retRecord.DateTimeVal == oneRecord.DateTimeVal);
            Assert.True(retRecord.DecimalVal == oneRecord.DecimalVal);
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

            // 8 select
            serviceQuery = ServiceQueryBuilder.New()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .Select(nameof(TestClass.BoolVal))
                .Select(nameof(TestClass.ByteArrayVal))
                .Select(nameof(TestClass.ByteVal))
                .Select(nameof(TestClass.CharVal))
                .Select(nameof(TestClass.DateTimeOffsetVal))
                .Select(nameof(TestClass.DateTimeVal))
                .Select(nameof(TestClass.DecimalVal))
                .Select(nameof(TestClass.DoubleVal))
                .Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            retRecord = result[0];
            oneRecord = new T().GetDefault1Record();
            Assert.True(retRecord.BoolVal == oneRecord.BoolVal);
            Assert.True(retRecord.ByteArrayVal[0] == oneRecord.ByteArrayVal[0]);
            Assert.True(retRecord.ByteVal == oneRecord.ByteVal);
            Assert.True(retRecord.CharVal == oneRecord.CharVal);
            Assert.True(retRecord.DateTimeOffsetVal == oneRecord.DateTimeOffsetVal);
            Assert.True(retRecord.DateTimeVal == oneRecord.DateTimeVal);
            Assert.True(retRecord.DecimalVal == oneRecord.DecimalVal);
            Assert.True(retRecord.DoubleVal == oneRecord.DoubleVal);
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

            // 9 select
            serviceQuery = ServiceQueryBuilder.New()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .Select(nameof(TestClass.BoolVal))
                .Select(nameof(TestClass.ByteArrayVal))
                .Select(nameof(TestClass.ByteVal))
                .Select(nameof(TestClass.CharVal))
                .Select(nameof(TestClass.DateTimeOffsetVal))
                .Select(nameof(TestClass.DateTimeVal))
                .Select(nameof(TestClass.DecimalVal))
                .Select(nameof(TestClass.DoubleVal))
                .Select(nameof(TestClass.FloatVal))
                .Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            retRecord = result[0];
            oneRecord = new T().GetDefault1Record();
            Assert.True(retRecord.BoolVal == oneRecord.BoolVal);
            Assert.True(retRecord.ByteArrayVal[0] == oneRecord.ByteArrayVal[0]);
            Assert.True(retRecord.ByteVal == oneRecord.ByteVal);
            Assert.True(retRecord.CharVal == oneRecord.CharVal);
            Assert.True(retRecord.DateTimeOffsetVal == oneRecord.DateTimeOffsetVal);
            Assert.True(retRecord.DateTimeVal == oneRecord.DateTimeVal);
            Assert.True(retRecord.DecimalVal == oneRecord.DecimalVal);
            Assert.True(retRecord.DoubleVal == oneRecord.DoubleVal);
            Assert.True(retRecord.FloatVal == oneRecord.FloatVal);
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

            // 10 select
            serviceQuery = ServiceQueryBuilder.New()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .Select(nameof(TestClass.BoolVal))
                .Select(nameof(TestClass.ByteArrayVal))
                .Select(nameof(TestClass.ByteVal))
                .Select(nameof(TestClass.CharVal))
                .Select(nameof(TestClass.DateTimeOffsetVal))
                .Select(nameof(TestClass.DateTimeVal))
                .Select(nameof(TestClass.DecimalVal))
                .Select(nameof(TestClass.DoubleVal))
                .Select(nameof(TestClass.FloatVal))
                .Select(nameof(TestClass.GuidVal))
                .Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            retRecord = result[0];
            oneRecord = new T().GetDefault1Record();
            Assert.True(retRecord.BoolVal == oneRecord.BoolVal);
            Assert.True(retRecord.ByteArrayVal[0] == oneRecord.ByteArrayVal[0]);
            Assert.True(retRecord.ByteVal == oneRecord.ByteVal);
            Assert.True(retRecord.CharVal == oneRecord.CharVal);
            Assert.True(retRecord.DateTimeOffsetVal == oneRecord.DateTimeOffsetVal);
            Assert.True(retRecord.DateTimeVal == oneRecord.DateTimeVal);
            Assert.True(retRecord.DecimalVal == oneRecord.DecimalVal);
            Assert.True(retRecord.DoubleVal == oneRecord.DoubleVal);
            Assert.True(retRecord.FloatVal == oneRecord.FloatVal);
            Assert.True(retRecord.GuidVal == oneRecord.GuidVal);
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

            // 11 select
            serviceQuery = ServiceQueryBuilder.New()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .Select(nameof(TestClass.BoolVal))
                .Select(nameof(TestClass.ByteArrayVal))
                .Select(nameof(TestClass.ByteVal))
                .Select(nameof(TestClass.CharVal))
                .Select(nameof(TestClass.DateTimeOffsetVal))
                .Select(nameof(TestClass.DateTimeVal))
                .Select(nameof(TestClass.DecimalVal))
                .Select(nameof(TestClass.DoubleVal))
                .Select(nameof(TestClass.FloatVal))
                .Select(nameof(TestClass.GuidVal))
                .Select(nameof(TestClass.IntVal))
                .Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            retRecord = result[0];
            oneRecord = new T().GetDefault1Record();
            Assert.True(retRecord.BoolVal == oneRecord.BoolVal);
            Assert.True(retRecord.ByteArrayVal[0] == oneRecord.ByteArrayVal[0]);
            Assert.True(retRecord.ByteVal == oneRecord.ByteVal);
            Assert.True(retRecord.CharVal == oneRecord.CharVal);
            Assert.True(retRecord.DateTimeOffsetVal == oneRecord.DateTimeOffsetVal);
            Assert.True(retRecord.DateTimeVal == oneRecord.DateTimeVal);
            Assert.True(retRecord.DecimalVal == oneRecord.DecimalVal);
            Assert.True(retRecord.DoubleVal == oneRecord.DoubleVal);
            Assert.True(retRecord.FloatVal == oneRecord.FloatVal);
            Assert.True(retRecord.GuidVal == oneRecord.GuidVal);
            Assert.True(retRecord.IntVal == oneRecord.IntVal);
            Assert.True(retRecord.LongVal != oneRecord.LongVal);
            Assert.True(retRecord.SByteVal != oneRecord.SByteVal);
            Assert.True(retRecord.ShortVal != oneRecord.ShortVal);
            Assert.True(retRecord.SingleVal != oneRecord.SingleVal);
            Assert.True(retRecord.StringVal != oneRecord.StringVal);
            Assert.True(retRecord.TimeSpanVal != oneRecord.TimeSpanVal);
            Assert.True(retRecord.UInt16Val != oneRecord.UInt16Val);
            Assert.True(retRecord.UInt32Val != oneRecord.UInt32Val);
            Assert.True(retRecord.UInt64Val != oneRecord.UInt64Val);

            // 12 select
            serviceQuery = ServiceQueryBuilder.New()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .Select(nameof(TestClass.BoolVal))
                .Select(nameof(TestClass.ByteArrayVal))
                .Select(nameof(TestClass.ByteVal))
                .Select(nameof(TestClass.CharVal))
                .Select(nameof(TestClass.DateTimeOffsetVal))
                .Select(nameof(TestClass.DateTimeVal))
                .Select(nameof(TestClass.DecimalVal))
                .Select(nameof(TestClass.DoubleVal))
                .Select(nameof(TestClass.FloatVal))
                .Select(nameof(TestClass.GuidVal))
                .Select(nameof(TestClass.IntVal))
                .Select(nameof(TestClass.LongVal))
                .Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            retRecord = result[0];
            oneRecord = new T().GetDefault1Record();
            Assert.True(retRecord.BoolVal == oneRecord.BoolVal);
            Assert.True(retRecord.ByteArrayVal[0] == oneRecord.ByteArrayVal[0]);
            Assert.True(retRecord.ByteVal == oneRecord.ByteVal);
            Assert.True(retRecord.CharVal == oneRecord.CharVal);
            Assert.True(retRecord.DateTimeOffsetVal == oneRecord.DateTimeOffsetVal);
            Assert.True(retRecord.DateTimeVal == oneRecord.DateTimeVal);
            Assert.True(retRecord.DecimalVal == oneRecord.DecimalVal);
            Assert.True(retRecord.DoubleVal == oneRecord.DoubleVal);
            Assert.True(retRecord.FloatVal == oneRecord.FloatVal);
            Assert.True(retRecord.GuidVal == oneRecord.GuidVal);
            Assert.True(retRecord.IntVal == oneRecord.IntVal);
            Assert.True(retRecord.LongVal == oneRecord.LongVal);
            Assert.True(retRecord.SByteVal != oneRecord.SByteVal);
            Assert.True(retRecord.ShortVal != oneRecord.ShortVal);
            Assert.True(retRecord.SingleVal != oneRecord.SingleVal);
            Assert.True(retRecord.StringVal != oneRecord.StringVal);
            Assert.True(retRecord.TimeSpanVal != oneRecord.TimeSpanVal);
            Assert.True(retRecord.UInt16Val != oneRecord.UInt16Val);
            Assert.True(retRecord.UInt32Val != oneRecord.UInt32Val);
            Assert.True(retRecord.UInt64Val != oneRecord.UInt64Val);

            // 13 select
            serviceQuery = ServiceQueryBuilder.New()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .Select(nameof(TestClass.BoolVal))
                .Select(nameof(TestClass.ByteArrayVal))
                .Select(nameof(TestClass.ByteVal))
                .Select(nameof(TestClass.CharVal))
                .Select(nameof(TestClass.DateTimeOffsetVal))
                .Select(nameof(TestClass.DateTimeVal))
                .Select(nameof(TestClass.DecimalVal))
                .Select(nameof(TestClass.DoubleVal))
                .Select(nameof(TestClass.FloatVal))
                .Select(nameof(TestClass.GuidVal))
                .Select(nameof(TestClass.IntVal))
                .Select(nameof(TestClass.LongVal))
                .Select(nameof(TestClass.SByteVal))
                .Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            retRecord = result[0];
            oneRecord = new T().GetDefault1Record();
            Assert.True(retRecord.BoolVal == oneRecord.BoolVal);
            Assert.True(retRecord.ByteArrayVal[0] == oneRecord.ByteArrayVal[0]);
            Assert.True(retRecord.ByteVal == oneRecord.ByteVal);
            Assert.True(retRecord.CharVal == oneRecord.CharVal);
            Assert.True(retRecord.DateTimeOffsetVal == oneRecord.DateTimeOffsetVal);
            Assert.True(retRecord.DateTimeVal == oneRecord.DateTimeVal);
            Assert.True(retRecord.DecimalVal == oneRecord.DecimalVal);
            Assert.True(retRecord.DoubleVal == oneRecord.DoubleVal);
            Assert.True(retRecord.FloatVal == oneRecord.FloatVal);
            Assert.True(retRecord.GuidVal == oneRecord.GuidVal);
            Assert.True(retRecord.IntVal == oneRecord.IntVal);
            Assert.True(retRecord.LongVal == oneRecord.LongVal);
            Assert.True(retRecord.SByteVal == oneRecord.SByteVal);
            Assert.True(retRecord.ShortVal != oneRecord.ShortVal);
            Assert.True(retRecord.SingleVal != oneRecord.SingleVal);
            Assert.True(retRecord.StringVal != oneRecord.StringVal);
            Assert.True(retRecord.TimeSpanVal != oneRecord.TimeSpanVal);
            Assert.True(retRecord.UInt16Val != oneRecord.UInt16Val);
            Assert.True(retRecord.UInt32Val != oneRecord.UInt32Val);
            Assert.True(retRecord.UInt64Val != oneRecord.UInt64Val);

            // 14 select
            serviceQuery = ServiceQueryBuilder.New()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .Select(nameof(TestClass.BoolVal))
                .Select(nameof(TestClass.ByteArrayVal))
                .Select(nameof(TestClass.ByteVal))
                .Select(nameof(TestClass.CharVal))
                .Select(nameof(TestClass.DateTimeOffsetVal))
                .Select(nameof(TestClass.DateTimeVal))
                .Select(nameof(TestClass.DecimalVal))
                .Select(nameof(TestClass.DoubleVal))
                .Select(nameof(TestClass.FloatVal))
                .Select(nameof(TestClass.GuidVal))
                .Select(nameof(TestClass.IntVal))
                .Select(nameof(TestClass.LongVal))
                .Select(nameof(TestClass.SByteVal))
                .Select(nameof(TestClass.ShortVal))
                .Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            retRecord = result[0];
            oneRecord = new T().GetDefault1Record();
            Assert.True(retRecord.BoolVal == oneRecord.BoolVal);
            Assert.True(retRecord.ByteArrayVal[0] == oneRecord.ByteArrayVal[0]);
            Assert.True(retRecord.ByteVal == oneRecord.ByteVal);
            Assert.True(retRecord.CharVal == oneRecord.CharVal);
            Assert.True(retRecord.DateTimeOffsetVal == oneRecord.DateTimeOffsetVal);
            Assert.True(retRecord.DateTimeVal == oneRecord.DateTimeVal);
            Assert.True(retRecord.DecimalVal == oneRecord.DecimalVal);
            Assert.True(retRecord.DoubleVal == oneRecord.DoubleVal);
            Assert.True(retRecord.FloatVal == oneRecord.FloatVal);
            Assert.True(retRecord.GuidVal == oneRecord.GuidVal);
            Assert.True(retRecord.IntVal == oneRecord.IntVal);
            Assert.True(retRecord.LongVal == oneRecord.LongVal);
            Assert.True(retRecord.SByteVal == oneRecord.SByteVal);
            Assert.True(retRecord.ShortVal == oneRecord.ShortVal);
            Assert.True(retRecord.SingleVal != oneRecord.SingleVal);
            Assert.True(retRecord.StringVal != oneRecord.StringVal);
            Assert.True(retRecord.TimeSpanVal != oneRecord.TimeSpanVal);
            Assert.True(retRecord.UInt16Val != oneRecord.UInt16Val);
            Assert.True(retRecord.UInt32Val != oneRecord.UInt32Val);
            Assert.True(retRecord.UInt64Val != oneRecord.UInt64Val);

            // 15 select
            serviceQuery = ServiceQueryBuilder.New()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .Select(nameof(TestClass.BoolVal))
                .Select(nameof(TestClass.ByteArrayVal))
                .Select(nameof(TestClass.ByteVal))
                .Select(nameof(TestClass.CharVal))
                .Select(nameof(TestClass.DateTimeOffsetVal))
                .Select(nameof(TestClass.DateTimeVal))
                .Select(nameof(TestClass.DecimalVal))
                .Select(nameof(TestClass.DoubleVal))
                .Select(nameof(TestClass.FloatVal))
                .Select(nameof(TestClass.GuidVal))
                .Select(nameof(TestClass.IntVal))
                .Select(nameof(TestClass.LongVal))
                .Select(nameof(TestClass.SByteVal))
                .Select(nameof(TestClass.ShortVal))
                .Select(nameof(TestClass.SingleVal))
                .Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            retRecord = result[0];
            oneRecord = new T().GetDefault1Record();
            Assert.True(retRecord.BoolVal == oneRecord.BoolVal);
            Assert.True(retRecord.ByteArrayVal[0] == oneRecord.ByteArrayVal[0]);
            Assert.True(retRecord.ByteVal == oneRecord.ByteVal);
            Assert.True(retRecord.CharVal == oneRecord.CharVal);
            Assert.True(retRecord.DateTimeOffsetVal == oneRecord.DateTimeOffsetVal);
            Assert.True(retRecord.DateTimeVal == oneRecord.DateTimeVal);
            Assert.True(retRecord.DecimalVal == oneRecord.DecimalVal);
            Assert.True(retRecord.DoubleVal == oneRecord.DoubleVal);
            Assert.True(retRecord.FloatVal == oneRecord.FloatVal);
            Assert.True(retRecord.GuidVal == oneRecord.GuidVal);
            Assert.True(retRecord.IntVal == oneRecord.IntVal);
            Assert.True(retRecord.LongVal == oneRecord.LongVal);
            Assert.True(retRecord.SByteVal == oneRecord.SByteVal);
            Assert.True(retRecord.ShortVal == oneRecord.ShortVal);
            Assert.True(retRecord.SingleVal == oneRecord.SingleVal);
            Assert.True(retRecord.StringVal != oneRecord.StringVal);
            Assert.True(retRecord.TimeSpanVal != oneRecord.TimeSpanVal);
            Assert.True(retRecord.UInt16Val != oneRecord.UInt16Val);
            Assert.True(retRecord.UInt32Val != oneRecord.UInt32Val);
            Assert.True(retRecord.UInt64Val != oneRecord.UInt64Val);

            // 16 select
            serviceQuery = ServiceQueryBuilder.New()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .Select(nameof(TestClass.BoolVal))
                .Select(nameof(TestClass.ByteArrayVal))
                .Select(nameof(TestClass.ByteVal))
                .Select(nameof(TestClass.CharVal))
                .Select(nameof(TestClass.DateTimeOffsetVal))
                .Select(nameof(TestClass.DateTimeVal))
                .Select(nameof(TestClass.DecimalVal))
                .Select(nameof(TestClass.DoubleVal))
                .Select(nameof(TestClass.FloatVal))
                .Select(nameof(TestClass.GuidVal))
                .Select(nameof(TestClass.IntVal))
                .Select(nameof(TestClass.LongVal))
                .Select(nameof(TestClass.SByteVal))
                .Select(nameof(TestClass.ShortVal))
                .Select(nameof(TestClass.SingleVal))
                .Select(nameof(TestClass.StringVal))
                .Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            retRecord = result[0];
            oneRecord = new T().GetDefault1Record();
            Assert.True(retRecord.BoolVal == oneRecord.BoolVal);
            Assert.True(retRecord.ByteArrayVal[0] == oneRecord.ByteArrayVal[0]);
            Assert.True(retRecord.ByteVal == oneRecord.ByteVal);
            Assert.True(retRecord.CharVal == oneRecord.CharVal);
            Assert.True(retRecord.DateTimeOffsetVal == oneRecord.DateTimeOffsetVal);
            Assert.True(retRecord.DateTimeVal == oneRecord.DateTimeVal);
            Assert.True(retRecord.DecimalVal == oneRecord.DecimalVal);
            Assert.True(retRecord.DoubleVal == oneRecord.DoubleVal);
            Assert.True(retRecord.FloatVal == oneRecord.FloatVal);
            Assert.True(retRecord.GuidVal == oneRecord.GuidVal);
            Assert.True(retRecord.IntVal == oneRecord.IntVal);
            Assert.True(retRecord.LongVal == oneRecord.LongVal);
            Assert.True(retRecord.SByteVal == oneRecord.SByteVal);
            Assert.True(retRecord.ShortVal == oneRecord.ShortVal);
            Assert.True(retRecord.SingleVal == oneRecord.SingleVal);
            Assert.True(retRecord.StringVal == oneRecord.StringVal);
            Assert.True(retRecord.TimeSpanVal != oneRecord.TimeSpanVal);
            Assert.True(retRecord.UInt16Val != oneRecord.UInt16Val);
            Assert.True(retRecord.UInt32Val != oneRecord.UInt32Val);
            Assert.True(retRecord.UInt64Val != oneRecord.UInt64Val);

            // 17 select
            serviceQuery = ServiceQueryBuilder.New()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .Select(nameof(TestClass.BoolVal))
                .Select(nameof(TestClass.ByteArrayVal))
                .Select(nameof(TestClass.ByteVal))
                .Select(nameof(TestClass.CharVal))
                .Select(nameof(TestClass.DateTimeOffsetVal))
                .Select(nameof(TestClass.DateTimeVal))
                .Select(nameof(TestClass.DecimalVal))
                .Select(nameof(TestClass.DoubleVal))
                .Select(nameof(TestClass.FloatVal))
                .Select(nameof(TestClass.GuidVal))
                .Select(nameof(TestClass.IntVal))
                .Select(nameof(TestClass.LongVal))
                .Select(nameof(TestClass.SByteVal))
                .Select(nameof(TestClass.ShortVal))
                .Select(nameof(TestClass.SingleVal))
                .Select(nameof(TestClass.StringVal))
                .Select(nameof(TestClass.TimeSpanVal))
                .Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            retRecord = result[0];
            oneRecord = new T().GetDefault1Record();
            Assert.True(retRecord.BoolVal == oneRecord.BoolVal);
            Assert.True(retRecord.ByteArrayVal[0] == oneRecord.ByteArrayVal[0]);
            Assert.True(retRecord.ByteVal == oneRecord.ByteVal);
            Assert.True(retRecord.CharVal == oneRecord.CharVal);
            Assert.True(retRecord.DateTimeOffsetVal == oneRecord.DateTimeOffsetVal);
            Assert.True(retRecord.DateTimeVal == oneRecord.DateTimeVal);
            Assert.True(retRecord.DecimalVal == oneRecord.DecimalVal);
            Assert.True(retRecord.DoubleVal == oneRecord.DoubleVal);
            Assert.True(retRecord.FloatVal == oneRecord.FloatVal);
            Assert.True(retRecord.GuidVal == oneRecord.GuidVal);
            Assert.True(retRecord.IntVal == oneRecord.IntVal);
            Assert.True(retRecord.LongVal == oneRecord.LongVal);
            Assert.True(retRecord.SByteVal == oneRecord.SByteVal);
            Assert.True(retRecord.ShortVal == oneRecord.ShortVal);
            Assert.True(retRecord.SingleVal == oneRecord.SingleVal);
            Assert.True(retRecord.StringVal == oneRecord.StringVal);
            Assert.True(retRecord.TimeSpanVal == oneRecord.TimeSpanVal);
            Assert.True(retRecord.UInt16Val != oneRecord.UInt16Val);
            Assert.True(retRecord.UInt32Val != oneRecord.UInt32Val);
            Assert.True(retRecord.UInt64Val != oneRecord.UInt64Val);

            // 18 select
            serviceQuery = ServiceQueryBuilder.New()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .Select(nameof(TestClass.BoolVal))
                .Select(nameof(TestClass.ByteArrayVal))
                .Select(nameof(TestClass.ByteVal))
                .Select(nameof(TestClass.CharVal))
                .Select(nameof(TestClass.DateTimeOffsetVal))
                .Select(nameof(TestClass.DateTimeVal))
                .Select(nameof(TestClass.DecimalVal))
                .Select(nameof(TestClass.DoubleVal))
                .Select(nameof(TestClass.FloatVal))
                .Select(nameof(TestClass.GuidVal))
                .Select(nameof(TestClass.IntVal))
                .Select(nameof(TestClass.LongVal))
                .Select(nameof(TestClass.SByteVal))
                .Select(nameof(TestClass.ShortVal))
                .Select(nameof(TestClass.SingleVal))
                .Select(nameof(TestClass.StringVal))
                .Select(nameof(TestClass.TimeSpanVal))
                .Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            retRecord = result[0];
            oneRecord = new T().GetDefault1Record();
            Assert.True(retRecord.BoolVal == oneRecord.BoolVal);
            Assert.True(retRecord.ByteArrayVal[0] == oneRecord.ByteArrayVal[0]);
            Assert.True(retRecord.ByteVal == oneRecord.ByteVal);
            Assert.True(retRecord.CharVal == oneRecord.CharVal);
            Assert.True(retRecord.DateTimeOffsetVal == oneRecord.DateTimeOffsetVal);
            Assert.True(retRecord.DateTimeVal == oneRecord.DateTimeVal);
            Assert.True(retRecord.DecimalVal == oneRecord.DecimalVal);
            Assert.True(retRecord.DoubleVal == oneRecord.DoubleVal);
            Assert.True(retRecord.FloatVal == oneRecord.FloatVal);
            Assert.True(retRecord.GuidVal == oneRecord.GuidVal);
            Assert.True(retRecord.IntVal == oneRecord.IntVal);
            Assert.True(retRecord.LongVal == oneRecord.LongVal);
            Assert.True(retRecord.SByteVal == oneRecord.SByteVal);
            Assert.True(retRecord.ShortVal == oneRecord.ShortVal);
            Assert.True(retRecord.SingleVal == oneRecord.SingleVal);
            Assert.True(retRecord.StringVal == oneRecord.StringVal);
            Assert.True(retRecord.TimeSpanVal == oneRecord.TimeSpanVal);
            Assert.True(retRecord.UInt16Val != oneRecord.UInt16Val);
            Assert.True(retRecord.UInt32Val != oneRecord.UInt32Val);
            Assert.True(retRecord.UInt64Val != oneRecord.UInt64Val);

            // 19 select
            serviceQuery = ServiceQueryBuilder.New()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .Select(nameof(TestClass.BoolVal))
                .Select(nameof(TestClass.ByteArrayVal))
                .Select(nameof(TestClass.ByteVal))
                .Select(nameof(TestClass.CharVal))
                .Select(nameof(TestClass.DateTimeOffsetVal))
                .Select(nameof(TestClass.DateTimeVal))
                .Select(nameof(TestClass.DecimalVal))
                .Select(nameof(TestClass.DoubleVal))
                .Select(nameof(TestClass.FloatVal))
                .Select(nameof(TestClass.GuidVal))
                .Select(nameof(TestClass.IntVal))
                .Select(nameof(TestClass.LongVal))
                .Select(nameof(TestClass.SByteVal))
                .Select(nameof(TestClass.ShortVal))
                .Select(nameof(TestClass.SingleVal))
                .Select(nameof(TestClass.StringVal))
                .Select(nameof(TestClass.TimeSpanVal))
                .Select(nameof(TestClass.UInt16Val))
                .Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            retRecord = result[0];
            oneRecord = new T().GetDefault1Record();
            Assert.True(retRecord.BoolVal == oneRecord.BoolVal);
            Assert.True(retRecord.ByteArrayVal[0] == oneRecord.ByteArrayVal[0]);
            Assert.True(retRecord.ByteVal == oneRecord.ByteVal);
            Assert.True(retRecord.CharVal == oneRecord.CharVal);
            Assert.True(retRecord.DateTimeOffsetVal == oneRecord.DateTimeOffsetVal);
            Assert.True(retRecord.DateTimeVal == oneRecord.DateTimeVal);
            Assert.True(retRecord.DecimalVal == oneRecord.DecimalVal);
            Assert.True(retRecord.DoubleVal == oneRecord.DoubleVal);
            Assert.True(retRecord.FloatVal == oneRecord.FloatVal);
            Assert.True(retRecord.GuidVal == oneRecord.GuidVal);
            Assert.True(retRecord.IntVal == oneRecord.IntVal);
            Assert.True(retRecord.LongVal == oneRecord.LongVal);
            Assert.True(retRecord.SByteVal == oneRecord.SByteVal);
            Assert.True(retRecord.ShortVal == oneRecord.ShortVal);
            Assert.True(retRecord.SingleVal == oneRecord.SingleVal);
            Assert.True(retRecord.StringVal == oneRecord.StringVal);
            Assert.True(retRecord.TimeSpanVal == oneRecord.TimeSpanVal);
            Assert.True(retRecord.UInt16Val == oneRecord.UInt16Val);
            Assert.True(retRecord.UInt32Val != oneRecord.UInt32Val);
            Assert.True(retRecord.UInt64Val != oneRecord.UInt64Val);

            // 20 select
            serviceQuery = ServiceQueryBuilder.New()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .Select(nameof(TestClass.BoolVal))
                .Select(nameof(TestClass.ByteArrayVal))
                .Select(nameof(TestClass.ByteVal))
                .Select(nameof(TestClass.CharVal))
                .Select(nameof(TestClass.DateTimeOffsetVal))
                .Select(nameof(TestClass.DateTimeVal))
                .Select(nameof(TestClass.DecimalVal))
                .Select(nameof(TestClass.DoubleVal))
                .Select(nameof(TestClass.FloatVal))
                .Select(nameof(TestClass.GuidVal))
                .Select(nameof(TestClass.IntVal))
                .Select(nameof(TestClass.LongVal))
                .Select(nameof(TestClass.SByteVal))
                .Select(nameof(TestClass.ShortVal))
                .Select(nameof(TestClass.SingleVal))
                .Select(nameof(TestClass.StringVal))
                .Select(nameof(TestClass.TimeSpanVal))
                .Select(nameof(TestClass.UInt16Val))
                .Select(nameof(TestClass.UInt32Val))
                .Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            retRecord = result[0];
            oneRecord = new T().GetDefault1Record();
            Assert.True(retRecord.BoolVal == oneRecord.BoolVal);
            Assert.True(retRecord.ByteArrayVal[0] == oneRecord.ByteArrayVal[0]);
            Assert.True(retRecord.ByteVal == oneRecord.ByteVal);
            Assert.True(retRecord.CharVal == oneRecord.CharVal);
            Assert.True(retRecord.DateTimeOffsetVal == oneRecord.DateTimeOffsetVal);
            Assert.True(retRecord.DateTimeVal == oneRecord.DateTimeVal);
            Assert.True(retRecord.DecimalVal == oneRecord.DecimalVal);
            Assert.True(retRecord.DoubleVal == oneRecord.DoubleVal);
            Assert.True(retRecord.FloatVal == oneRecord.FloatVal);
            Assert.True(retRecord.GuidVal == oneRecord.GuidVal);
            Assert.True(retRecord.IntVal == oneRecord.IntVal);
            Assert.True(retRecord.LongVal == oneRecord.LongVal);
            Assert.True(retRecord.SByteVal == oneRecord.SByteVal);
            Assert.True(retRecord.ShortVal == oneRecord.ShortVal);
            Assert.True(retRecord.SingleVal == oneRecord.SingleVal);
            Assert.True(retRecord.StringVal == oneRecord.StringVal);
            Assert.True(retRecord.TimeSpanVal == oneRecord.TimeSpanVal);
            Assert.True(retRecord.UInt16Val == oneRecord.UInt16Val);
            Assert.True(retRecord.UInt32Val == oneRecord.UInt32Val);
            Assert.True(retRecord.UInt64Val != oneRecord.UInt64Val);

            // 21 select
            serviceQuery = ServiceQueryBuilder.New()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .Select(nameof(TestClass.BoolVal))
                .Select(nameof(TestClass.ByteArrayVal))
                .Select(nameof(TestClass.ByteVal))
                .Select(nameof(TestClass.CharVal))
                .Select(nameof(TestClass.DateTimeOffsetVal))
                .Select(nameof(TestClass.DateTimeVal))
                .Select(nameof(TestClass.DecimalVal))
                .Select(nameof(TestClass.DoubleVal))
                .Select(nameof(TestClass.FloatVal))
                .Select(nameof(TestClass.GuidVal))
                .Select(nameof(TestClass.IntVal))
                .Select(nameof(TestClass.LongVal))
                .Select(nameof(TestClass.SByteVal))
                .Select(nameof(TestClass.ShortVal))
                .Select(nameof(TestClass.SingleVal))
                .Select(nameof(TestClass.StringVal))
                .Select(nameof(TestClass.TimeSpanVal))
                .Select(nameof(TestClass.UInt16Val))
                .Select(nameof(TestClass.UInt32Val))
                .Select(nameof(TestClass.UInt64Val))
                .Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            retRecord = result[0];
            oneRecord = new T().GetDefault1Record();
            Assert.True(retRecord.BoolVal == oneRecord.BoolVal);
            Assert.True(retRecord.ByteArrayVal[0] == oneRecord.ByteArrayVal[0]);
            Assert.True(retRecord.ByteVal == oneRecord.ByteVal);
            Assert.True(retRecord.CharVal == oneRecord.CharVal);
            Assert.True(retRecord.DateTimeOffsetVal == oneRecord.DateTimeOffsetVal);
            Assert.True(retRecord.DateTimeVal == oneRecord.DateTimeVal);
            Assert.True(retRecord.DecimalVal == oneRecord.DecimalVal);
            Assert.True(retRecord.DoubleVal == oneRecord.DoubleVal);
            Assert.True(retRecord.FloatVal == oneRecord.FloatVal);
            Assert.True(retRecord.GuidVal == oneRecord.GuidVal);
            Assert.True(retRecord.IntVal == oneRecord.IntVal);
            Assert.True(retRecord.LongVal == oneRecord.LongVal);
            Assert.True(retRecord.SByteVal == oneRecord.SByteVal);
            Assert.True(retRecord.ShortVal == oneRecord.ShortVal);
            Assert.True(retRecord.SingleVal == oneRecord.SingleVal);
            Assert.True(retRecord.StringVal == oneRecord.StringVal);
            Assert.True(retRecord.TimeSpanVal == oneRecord.TimeSpanVal);
            Assert.True(retRecord.UInt16Val == oneRecord.UInt16Val);
            Assert.True(retRecord.UInt32Val == oneRecord.UInt32Val);
            Assert.True(retRecord.UInt64Val == oneRecord.UInt64Val);
        }

        [Fact]
        public async Task SelectRequestTests()
        {
            var sourceQueryable = GetTestList();
            IQueryable<T> testQueryable;
            IServiceQueryRequest serviceQuery;
            List<T> result;
            T retRecord;
            ITestClass oneRecord;

            // No select
            serviceQuery = ServiceQueryRequestBuilder.New()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            retRecord = result[0];
            oneRecord = new T().GetDefault1Record();
            Assert.True(retRecord.BoolVal == oneRecord.BoolVal);
            Assert.True(retRecord.ByteArrayVal[0] == oneRecord.ByteArrayVal[0]);
            Assert.True(retRecord.ByteVal == oneRecord.ByteVal);
            Assert.True(retRecord.CharVal == oneRecord.CharVal);
            Assert.True(retRecord.DateTimeOffsetVal == oneRecord.DateTimeOffsetVal);
            Assert.True(retRecord.DateTimeVal == oneRecord.DateTimeVal);
            Assert.True(retRecord.DecimalVal == oneRecord.DecimalVal);
            Assert.True(retRecord.DoubleVal == oneRecord.DoubleVal);
            Assert.True(retRecord.FloatVal == oneRecord.FloatVal);
            Assert.True(retRecord.GuidVal == oneRecord.GuidVal);
            Assert.True(retRecord.IntVal == oneRecord.IntVal);
            Assert.True(retRecord.LongVal == oneRecord.LongVal);
            Assert.True(retRecord.SByteVal == oneRecord.SByteVal);
            Assert.True(retRecord.ShortVal == oneRecord.ShortVal);
            Assert.True(retRecord.SingleVal == oneRecord.SingleVal);
            Assert.True(retRecord.StringVal == oneRecord.StringVal);
            Assert.True(retRecord.TimeSpanVal == oneRecord.TimeSpanVal);
            Assert.True(retRecord.UInt16Val == oneRecord.UInt16Val);
            Assert.True(retRecord.UInt32Val == oneRecord.UInt32Val);
            Assert.True(retRecord.UInt64Val == oneRecord.UInt64Val);

            // 1 select
            serviceQuery = ServiceQueryRequestBuilder.New()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .Select(nameof(TestClass.BoolVal))
                .Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            retRecord = result[0];
            oneRecord = new T().GetDefault1Record();
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

            // 2 select
            serviceQuery = ServiceQueryRequestBuilder.New()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .Select(nameof(TestClass.BoolVal))
                .Select(nameof(TestClass.ByteArrayVal))
                .Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            retRecord = result[0];
            oneRecord = new T().GetDefault1Record();
            Assert.True(retRecord.BoolVal == oneRecord.BoolVal);
            Assert.True(retRecord.ByteArrayVal[0] == oneRecord.ByteArrayVal[0]);
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

            // 3 select
            serviceQuery = ServiceQueryRequestBuilder.New()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .Select(nameof(TestClass.BoolVal))
                .Select(nameof(TestClass.ByteArrayVal))
                .Select(nameof(TestClass.ByteVal))
                .Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            retRecord = result[0];
            oneRecord = new T().GetDefault1Record();
            Assert.True(retRecord.BoolVal == oneRecord.BoolVal);
            Assert.True(retRecord.ByteArrayVal[0] == oneRecord.ByteArrayVal[0]);
            Assert.True(retRecord.ByteVal == oneRecord.ByteVal);
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

            // 4 select
            serviceQuery = ServiceQueryRequestBuilder.New()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .Select(nameof(TestClass.BoolVal))
                .Select(nameof(TestClass.ByteArrayVal))
                .Select(nameof(TestClass.ByteVal))
                .Select(nameof(TestClass.CharVal))
                .Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            retRecord = result[0];
            oneRecord = new T().GetDefault1Record();
            Assert.True(retRecord.BoolVal == oneRecord.BoolVal);
            Assert.True(retRecord.ByteArrayVal[0] == oneRecord.ByteArrayVal[0]);
            Assert.True(retRecord.ByteVal == oneRecord.ByteVal);
            Assert.True(retRecord.CharVal == oneRecord.CharVal);
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

            // 5 select
            serviceQuery = ServiceQueryRequestBuilder.New()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .Select(nameof(TestClass.BoolVal))
                .Select(nameof(TestClass.ByteArrayVal))
                .Select(nameof(TestClass.ByteVal))
                .Select(nameof(TestClass.CharVal))
                .Select(nameof(TestClass.DateTimeOffsetVal))
                .Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            retRecord = result[0];
            oneRecord = new T().GetDefault1Record();
            Assert.True(retRecord.BoolVal == oneRecord.BoolVal);
            Assert.True(retRecord.ByteArrayVal[0] == oneRecord.ByteArrayVal[0]);
            Assert.True(retRecord.ByteVal == oneRecord.ByteVal);
            Assert.True(retRecord.CharVal == oneRecord.CharVal);
            Assert.True(retRecord.DateTimeOffsetVal == oneRecord.DateTimeOffsetVal);
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

            // 6 select
            serviceQuery = ServiceQueryRequestBuilder.New()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .Select(nameof(TestClass.BoolVal))
                .Select(nameof(TestClass.ByteArrayVal))
                .Select(nameof(TestClass.ByteVal))
                .Select(nameof(TestClass.CharVal))
                .Select(nameof(TestClass.DateTimeOffsetVal))
                .Select(nameof(TestClass.DateTimeVal))
                .Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            retRecord = result[0];
            oneRecord = new T().GetDefault1Record();
            Assert.True(retRecord.BoolVal == oneRecord.BoolVal);
            Assert.True(retRecord.ByteArrayVal[0] == oneRecord.ByteArrayVal[0]);
            Assert.True(retRecord.ByteVal == oneRecord.ByteVal);
            Assert.True(retRecord.CharVal == oneRecord.CharVal);
            Assert.True(retRecord.DateTimeOffsetVal == oneRecord.DateTimeOffsetVal);
            Assert.True(retRecord.DateTimeVal == oneRecord.DateTimeVal);
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

            // 7 select
            serviceQuery = ServiceQueryRequestBuilder.New()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .Select(nameof(TestClass.BoolVal))
                .Select(nameof(TestClass.ByteArrayVal))
                .Select(nameof(TestClass.ByteVal))
                .Select(nameof(TestClass.CharVal))
                .Select(nameof(TestClass.DateTimeOffsetVal))
                .Select(nameof(TestClass.DateTimeVal))
                .Select(nameof(TestClass.DecimalVal))
                .Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            retRecord = result[0];
            oneRecord = new T().GetDefault1Record();
            Assert.True(retRecord.BoolVal == oneRecord.BoolVal);
            Assert.True(retRecord.ByteArrayVal[0] == oneRecord.ByteArrayVal[0]);
            Assert.True(retRecord.ByteVal == oneRecord.ByteVal);
            Assert.True(retRecord.CharVal == oneRecord.CharVal);
            Assert.True(retRecord.DateTimeOffsetVal == oneRecord.DateTimeOffsetVal);
            Assert.True(retRecord.DateTimeVal == oneRecord.DateTimeVal);
            Assert.True(retRecord.DecimalVal == oneRecord.DecimalVal);
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

            // 8 select
            serviceQuery = ServiceQueryRequestBuilder.New()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .Select(nameof(TestClass.BoolVal))
                .Select(nameof(TestClass.ByteArrayVal))
                .Select(nameof(TestClass.ByteVal))
                .Select(nameof(TestClass.CharVal))
                .Select(nameof(TestClass.DateTimeOffsetVal))
                .Select(nameof(TestClass.DateTimeVal))
                .Select(nameof(TestClass.DecimalVal))
                .Select(nameof(TestClass.DoubleVal))
                .Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            retRecord = result[0];
            oneRecord = new T().GetDefault1Record();
            Assert.True(retRecord.BoolVal == oneRecord.BoolVal);
            Assert.True(retRecord.ByteArrayVal[0] == oneRecord.ByteArrayVal[0]);
            Assert.True(retRecord.ByteVal == oneRecord.ByteVal);
            Assert.True(retRecord.CharVal == oneRecord.CharVal);
            Assert.True(retRecord.DateTimeOffsetVal == oneRecord.DateTimeOffsetVal);
            Assert.True(retRecord.DateTimeVal == oneRecord.DateTimeVal);
            Assert.True(retRecord.DecimalVal == oneRecord.DecimalVal);
            Assert.True(retRecord.DoubleVal == oneRecord.DoubleVal);
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

            // 9 select
            serviceQuery = ServiceQueryRequestBuilder.New()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .Select(nameof(TestClass.BoolVal))
                .Select(nameof(TestClass.ByteArrayVal))
                .Select(nameof(TestClass.ByteVal))
                .Select(nameof(TestClass.CharVal))
                .Select(nameof(TestClass.DateTimeOffsetVal))
                .Select(nameof(TestClass.DateTimeVal))
                .Select(nameof(TestClass.DecimalVal))
                .Select(nameof(TestClass.DoubleVal))
                .Select(nameof(TestClass.FloatVal))
                .Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            retRecord = result[0];
            oneRecord = new T().GetDefault1Record();
            Assert.True(retRecord.BoolVal == oneRecord.BoolVal);
            Assert.True(retRecord.ByteArrayVal[0] == oneRecord.ByteArrayVal[0]);
            Assert.True(retRecord.ByteVal == oneRecord.ByteVal);
            Assert.True(retRecord.CharVal == oneRecord.CharVal);
            Assert.True(retRecord.DateTimeOffsetVal == oneRecord.DateTimeOffsetVal);
            Assert.True(retRecord.DateTimeVal == oneRecord.DateTimeVal);
            Assert.True(retRecord.DecimalVal == oneRecord.DecimalVal);
            Assert.True(retRecord.DoubleVal == oneRecord.DoubleVal);
            Assert.True(retRecord.FloatVal == oneRecord.FloatVal);
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

            // 10 select
            serviceQuery = ServiceQueryRequestBuilder.New()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .Select(nameof(TestClass.BoolVal))
                .Select(nameof(TestClass.ByteArrayVal))
                .Select(nameof(TestClass.ByteVal))
                .Select(nameof(TestClass.CharVal))
                .Select(nameof(TestClass.DateTimeOffsetVal))
                .Select(nameof(TestClass.DateTimeVal))
                .Select(nameof(TestClass.DecimalVal))
                .Select(nameof(TestClass.DoubleVal))
                .Select(nameof(TestClass.FloatVal))
                .Select(nameof(TestClass.GuidVal))
                .Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            retRecord = result[0];
            oneRecord = new T().GetDefault1Record();
            Assert.True(retRecord.BoolVal == oneRecord.BoolVal);
            Assert.True(retRecord.ByteArrayVal[0] == oneRecord.ByteArrayVal[0]);
            Assert.True(retRecord.ByteVal == oneRecord.ByteVal);
            Assert.True(retRecord.CharVal == oneRecord.CharVal);
            Assert.True(retRecord.DateTimeOffsetVal == oneRecord.DateTimeOffsetVal);
            Assert.True(retRecord.DateTimeVal == oneRecord.DateTimeVal);
            Assert.True(retRecord.DecimalVal == oneRecord.DecimalVal);
            Assert.True(retRecord.DoubleVal == oneRecord.DoubleVal);
            Assert.True(retRecord.FloatVal == oneRecord.FloatVal);
            Assert.True(retRecord.GuidVal == oneRecord.GuidVal);
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

            // 11 select
            serviceQuery = ServiceQueryRequestBuilder.New()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .Select(nameof(TestClass.BoolVal))
                .Select(nameof(TestClass.ByteArrayVal))
                .Select(nameof(TestClass.ByteVal))
                .Select(nameof(TestClass.CharVal))
                .Select(nameof(TestClass.DateTimeOffsetVal))
                .Select(nameof(TestClass.DateTimeVal))
                .Select(nameof(TestClass.DecimalVal))
                .Select(nameof(TestClass.DoubleVal))
                .Select(nameof(TestClass.FloatVal))
                .Select(nameof(TestClass.GuidVal))
                .Select(nameof(TestClass.IntVal))
                .Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            retRecord = result[0];
            oneRecord = new T().GetDefault1Record();
            Assert.True(retRecord.BoolVal == oneRecord.BoolVal);
            Assert.True(retRecord.ByteArrayVal[0] == oneRecord.ByteArrayVal[0]);
            Assert.True(retRecord.ByteVal == oneRecord.ByteVal);
            Assert.True(retRecord.CharVal == oneRecord.CharVal);
            Assert.True(retRecord.DateTimeOffsetVal == oneRecord.DateTimeOffsetVal);
            Assert.True(retRecord.DateTimeVal == oneRecord.DateTimeVal);
            Assert.True(retRecord.DecimalVal == oneRecord.DecimalVal);
            Assert.True(retRecord.DoubleVal == oneRecord.DoubleVal);
            Assert.True(retRecord.FloatVal == oneRecord.FloatVal);
            Assert.True(retRecord.GuidVal == oneRecord.GuidVal);
            Assert.True(retRecord.IntVal == oneRecord.IntVal);
            Assert.True(retRecord.LongVal != oneRecord.LongVal);
            Assert.True(retRecord.SByteVal != oneRecord.SByteVal);
            Assert.True(retRecord.ShortVal != oneRecord.ShortVal);
            Assert.True(retRecord.SingleVal != oneRecord.SingleVal);
            Assert.True(retRecord.StringVal != oneRecord.StringVal);
            Assert.True(retRecord.TimeSpanVal != oneRecord.TimeSpanVal);
            Assert.True(retRecord.UInt16Val != oneRecord.UInt16Val);
            Assert.True(retRecord.UInt32Val != oneRecord.UInt32Val);
            Assert.True(retRecord.UInt64Val != oneRecord.UInt64Val);

            // 12 select
            serviceQuery = ServiceQueryRequestBuilder.New()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .Select(nameof(TestClass.BoolVal))
                .Select(nameof(TestClass.ByteArrayVal))
                .Select(nameof(TestClass.ByteVal))
                .Select(nameof(TestClass.CharVal))
                .Select(nameof(TestClass.DateTimeOffsetVal))
                .Select(nameof(TestClass.DateTimeVal))
                .Select(nameof(TestClass.DecimalVal))
                .Select(nameof(TestClass.DoubleVal))
                .Select(nameof(TestClass.FloatVal))
                .Select(nameof(TestClass.GuidVal))
                .Select(nameof(TestClass.IntVal))
                .Select(nameof(TestClass.LongVal))
                .Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            retRecord = result[0];
            oneRecord = new T().GetDefault1Record();
            Assert.True(retRecord.BoolVal == oneRecord.BoolVal);
            Assert.True(retRecord.ByteArrayVal[0] == oneRecord.ByteArrayVal[0]);
            Assert.True(retRecord.ByteVal == oneRecord.ByteVal);
            Assert.True(retRecord.CharVal == oneRecord.CharVal);
            Assert.True(retRecord.DateTimeOffsetVal == oneRecord.DateTimeOffsetVal);
            Assert.True(retRecord.DateTimeVal == oneRecord.DateTimeVal);
            Assert.True(retRecord.DecimalVal == oneRecord.DecimalVal);
            Assert.True(retRecord.DoubleVal == oneRecord.DoubleVal);
            Assert.True(retRecord.FloatVal == oneRecord.FloatVal);
            Assert.True(retRecord.GuidVal == oneRecord.GuidVal);
            Assert.True(retRecord.IntVal == oneRecord.IntVal);
            Assert.True(retRecord.LongVal == oneRecord.LongVal);
            Assert.True(retRecord.SByteVal != oneRecord.SByteVal);
            Assert.True(retRecord.ShortVal != oneRecord.ShortVal);
            Assert.True(retRecord.SingleVal != oneRecord.SingleVal);
            Assert.True(retRecord.StringVal != oneRecord.StringVal);
            Assert.True(retRecord.TimeSpanVal != oneRecord.TimeSpanVal);
            Assert.True(retRecord.UInt16Val != oneRecord.UInt16Val);
            Assert.True(retRecord.UInt32Val != oneRecord.UInt32Val);
            Assert.True(retRecord.UInt64Val != oneRecord.UInt64Val);

            // 13 select
            serviceQuery = ServiceQueryRequestBuilder.New()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .Select(nameof(TestClass.BoolVal))
                .Select(nameof(TestClass.ByteArrayVal))
                .Select(nameof(TestClass.ByteVal))
                .Select(nameof(TestClass.CharVal))
                .Select(nameof(TestClass.DateTimeOffsetVal))
                .Select(nameof(TestClass.DateTimeVal))
                .Select(nameof(TestClass.DecimalVal))
                .Select(nameof(TestClass.DoubleVal))
                .Select(nameof(TestClass.FloatVal))
                .Select(nameof(TestClass.GuidVal))
                .Select(nameof(TestClass.IntVal))
                .Select(nameof(TestClass.LongVal))
                .Select(nameof(TestClass.SByteVal))
                .Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            retRecord = result[0];
            oneRecord = new T().GetDefault1Record();
            Assert.True(retRecord.BoolVal == oneRecord.BoolVal);
            Assert.True(retRecord.ByteArrayVal[0] == oneRecord.ByteArrayVal[0]);
            Assert.True(retRecord.ByteVal == oneRecord.ByteVal);
            Assert.True(retRecord.CharVal == oneRecord.CharVal);
            Assert.True(retRecord.DateTimeOffsetVal == oneRecord.DateTimeOffsetVal);
            Assert.True(retRecord.DateTimeVal == oneRecord.DateTimeVal);
            Assert.True(retRecord.DecimalVal == oneRecord.DecimalVal);
            Assert.True(retRecord.DoubleVal == oneRecord.DoubleVal);
            Assert.True(retRecord.FloatVal == oneRecord.FloatVal);
            Assert.True(retRecord.GuidVal == oneRecord.GuidVal);
            Assert.True(retRecord.IntVal == oneRecord.IntVal);
            Assert.True(retRecord.LongVal == oneRecord.LongVal);
            Assert.True(retRecord.SByteVal == oneRecord.SByteVal);
            Assert.True(retRecord.ShortVal != oneRecord.ShortVal);
            Assert.True(retRecord.SingleVal != oneRecord.SingleVal);
            Assert.True(retRecord.StringVal != oneRecord.StringVal);
            Assert.True(retRecord.TimeSpanVal != oneRecord.TimeSpanVal);
            Assert.True(retRecord.UInt16Val != oneRecord.UInt16Val);
            Assert.True(retRecord.UInt32Val != oneRecord.UInt32Val);
            Assert.True(retRecord.UInt64Val != oneRecord.UInt64Val);

            // 14 select
            serviceQuery = ServiceQueryRequestBuilder.New()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .Select(nameof(TestClass.BoolVal))
                .Select(nameof(TestClass.ByteArrayVal))
                .Select(nameof(TestClass.ByteVal))
                .Select(nameof(TestClass.CharVal))
                .Select(nameof(TestClass.DateTimeOffsetVal))
                .Select(nameof(TestClass.DateTimeVal))
                .Select(nameof(TestClass.DecimalVal))
                .Select(nameof(TestClass.DoubleVal))
                .Select(nameof(TestClass.FloatVal))
                .Select(nameof(TestClass.GuidVal))
                .Select(nameof(TestClass.IntVal))
                .Select(nameof(TestClass.LongVal))
                .Select(nameof(TestClass.SByteVal))
                .Select(nameof(TestClass.ShortVal))
                .Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            retRecord = result[0];
            oneRecord = new T().GetDefault1Record();
            Assert.True(retRecord.BoolVal == oneRecord.BoolVal);
            Assert.True(retRecord.ByteArrayVal[0] == oneRecord.ByteArrayVal[0]);
            Assert.True(retRecord.ByteVal == oneRecord.ByteVal);
            Assert.True(retRecord.CharVal == oneRecord.CharVal);
            Assert.True(retRecord.DateTimeOffsetVal == oneRecord.DateTimeOffsetVal);
            Assert.True(retRecord.DateTimeVal == oneRecord.DateTimeVal);
            Assert.True(retRecord.DecimalVal == oneRecord.DecimalVal);
            Assert.True(retRecord.DoubleVal == oneRecord.DoubleVal);
            Assert.True(retRecord.FloatVal == oneRecord.FloatVal);
            Assert.True(retRecord.GuidVal == oneRecord.GuidVal);
            Assert.True(retRecord.IntVal == oneRecord.IntVal);
            Assert.True(retRecord.LongVal == oneRecord.LongVal);
            Assert.True(retRecord.SByteVal == oneRecord.SByteVal);
            Assert.True(retRecord.ShortVal == oneRecord.ShortVal);
            Assert.True(retRecord.SingleVal != oneRecord.SingleVal);
            Assert.True(retRecord.StringVal != oneRecord.StringVal);
            Assert.True(retRecord.TimeSpanVal != oneRecord.TimeSpanVal);
            Assert.True(retRecord.UInt16Val != oneRecord.UInt16Val);
            Assert.True(retRecord.UInt32Val != oneRecord.UInt32Val);
            Assert.True(retRecord.UInt64Val != oneRecord.UInt64Val);

            // 15 select
            serviceQuery = ServiceQueryRequestBuilder.New()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .Select(nameof(TestClass.BoolVal))
                .Select(nameof(TestClass.ByteArrayVal))
                .Select(nameof(TestClass.ByteVal))
                .Select(nameof(TestClass.CharVal))
                .Select(nameof(TestClass.DateTimeOffsetVal))
                .Select(nameof(TestClass.DateTimeVal))
                .Select(nameof(TestClass.DecimalVal))
                .Select(nameof(TestClass.DoubleVal))
                .Select(nameof(TestClass.FloatVal))
                .Select(nameof(TestClass.GuidVal))
                .Select(nameof(TestClass.IntVal))
                .Select(nameof(TestClass.LongVal))
                .Select(nameof(TestClass.SByteVal))
                .Select(nameof(TestClass.ShortVal))
                .Select(nameof(TestClass.SingleVal))
                .Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            retRecord = result[0];
            oneRecord = new T().GetDefault1Record();
            Assert.True(retRecord.BoolVal == oneRecord.BoolVal);
            Assert.True(retRecord.ByteArrayVal[0] == oneRecord.ByteArrayVal[0]);
            Assert.True(retRecord.ByteVal == oneRecord.ByteVal);
            Assert.True(retRecord.CharVal == oneRecord.CharVal);
            Assert.True(retRecord.DateTimeOffsetVal == oneRecord.DateTimeOffsetVal);
            Assert.True(retRecord.DateTimeVal == oneRecord.DateTimeVal);
            Assert.True(retRecord.DecimalVal == oneRecord.DecimalVal);
            Assert.True(retRecord.DoubleVal == oneRecord.DoubleVal);
            Assert.True(retRecord.FloatVal == oneRecord.FloatVal);
            Assert.True(retRecord.GuidVal == oneRecord.GuidVal);
            Assert.True(retRecord.IntVal == oneRecord.IntVal);
            Assert.True(retRecord.LongVal == oneRecord.LongVal);
            Assert.True(retRecord.SByteVal == oneRecord.SByteVal);
            Assert.True(retRecord.ShortVal == oneRecord.ShortVal);
            Assert.True(retRecord.SingleVal == oneRecord.SingleVal);
            Assert.True(retRecord.StringVal != oneRecord.StringVal);
            Assert.True(retRecord.TimeSpanVal != oneRecord.TimeSpanVal);
            Assert.True(retRecord.UInt16Val != oneRecord.UInt16Val);
            Assert.True(retRecord.UInt32Val != oneRecord.UInt32Val);
            Assert.True(retRecord.UInt64Val != oneRecord.UInt64Val);

            // 16 select
            serviceQuery = ServiceQueryRequestBuilder.New()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .Select(nameof(TestClass.BoolVal))
                .Select(nameof(TestClass.ByteArrayVal))
                .Select(nameof(TestClass.ByteVal))
                .Select(nameof(TestClass.CharVal))
                .Select(nameof(TestClass.DateTimeOffsetVal))
                .Select(nameof(TestClass.DateTimeVal))
                .Select(nameof(TestClass.DecimalVal))
                .Select(nameof(TestClass.DoubleVal))
                .Select(nameof(TestClass.FloatVal))
                .Select(nameof(TestClass.GuidVal))
                .Select(nameof(TestClass.IntVal))
                .Select(nameof(TestClass.LongVal))
                .Select(nameof(TestClass.SByteVal))
                .Select(nameof(TestClass.ShortVal))
                .Select(nameof(TestClass.SingleVal))
                .Select(nameof(TestClass.StringVal))
                .Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            retRecord = result[0];
            oneRecord = new T().GetDefault1Record();
            Assert.True(retRecord.BoolVal == oneRecord.BoolVal);
            Assert.True(retRecord.ByteArrayVal[0] == oneRecord.ByteArrayVal[0]);
            Assert.True(retRecord.ByteVal == oneRecord.ByteVal);
            Assert.True(retRecord.CharVal == oneRecord.CharVal);
            Assert.True(retRecord.DateTimeOffsetVal == oneRecord.DateTimeOffsetVal);
            Assert.True(retRecord.DateTimeVal == oneRecord.DateTimeVal);
            Assert.True(retRecord.DecimalVal == oneRecord.DecimalVal);
            Assert.True(retRecord.DoubleVal == oneRecord.DoubleVal);
            Assert.True(retRecord.FloatVal == oneRecord.FloatVal);
            Assert.True(retRecord.GuidVal == oneRecord.GuidVal);
            Assert.True(retRecord.IntVal == oneRecord.IntVal);
            Assert.True(retRecord.LongVal == oneRecord.LongVal);
            Assert.True(retRecord.SByteVal == oneRecord.SByteVal);
            Assert.True(retRecord.ShortVal == oneRecord.ShortVal);
            Assert.True(retRecord.SingleVal == oneRecord.SingleVal);
            Assert.True(retRecord.StringVal == oneRecord.StringVal);
            Assert.True(retRecord.TimeSpanVal != oneRecord.TimeSpanVal);
            Assert.True(retRecord.UInt16Val != oneRecord.UInt16Val);
            Assert.True(retRecord.UInt32Val != oneRecord.UInt32Val);
            Assert.True(retRecord.UInt64Val != oneRecord.UInt64Val);

            // 17 select
            serviceQuery = ServiceQueryRequestBuilder.New()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .Select(nameof(TestClass.BoolVal))
                .Select(nameof(TestClass.ByteArrayVal))
                .Select(nameof(TestClass.ByteVal))
                .Select(nameof(TestClass.CharVal))
                .Select(nameof(TestClass.DateTimeOffsetVal))
                .Select(nameof(TestClass.DateTimeVal))
                .Select(nameof(TestClass.DecimalVal))
                .Select(nameof(TestClass.DoubleVal))
                .Select(nameof(TestClass.FloatVal))
                .Select(nameof(TestClass.GuidVal))
                .Select(nameof(TestClass.IntVal))
                .Select(nameof(TestClass.LongVal))
                .Select(nameof(TestClass.SByteVal))
                .Select(nameof(TestClass.ShortVal))
                .Select(nameof(TestClass.SingleVal))
                .Select(nameof(TestClass.StringVal))
                .Select(nameof(TestClass.TimeSpanVal))
                .Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            retRecord = result[0];
            oneRecord = new T().GetDefault1Record();
            Assert.True(retRecord.BoolVal == oneRecord.BoolVal);
            Assert.True(retRecord.ByteArrayVal[0] == oneRecord.ByteArrayVal[0]);
            Assert.True(retRecord.ByteVal == oneRecord.ByteVal);
            Assert.True(retRecord.CharVal == oneRecord.CharVal);
            Assert.True(retRecord.DateTimeOffsetVal == oneRecord.DateTimeOffsetVal);
            Assert.True(retRecord.DateTimeVal == oneRecord.DateTimeVal);
            Assert.True(retRecord.DecimalVal == oneRecord.DecimalVal);
            Assert.True(retRecord.DoubleVal == oneRecord.DoubleVal);
            Assert.True(retRecord.FloatVal == oneRecord.FloatVal);
            Assert.True(retRecord.GuidVal == oneRecord.GuidVal);
            Assert.True(retRecord.IntVal == oneRecord.IntVal);
            Assert.True(retRecord.LongVal == oneRecord.LongVal);
            Assert.True(retRecord.SByteVal == oneRecord.SByteVal);
            Assert.True(retRecord.ShortVal == oneRecord.ShortVal);
            Assert.True(retRecord.SingleVal == oneRecord.SingleVal);
            Assert.True(retRecord.StringVal == oneRecord.StringVal);
            Assert.True(retRecord.TimeSpanVal == oneRecord.TimeSpanVal);
            Assert.True(retRecord.UInt16Val != oneRecord.UInt16Val);
            Assert.True(retRecord.UInt32Val != oneRecord.UInt32Val);
            Assert.True(retRecord.UInt64Val != oneRecord.UInt64Val);

            // 18 select
            serviceQuery = ServiceQueryRequestBuilder.New()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .Select(nameof(TestClass.BoolVal))
                .Select(nameof(TestClass.ByteArrayVal))
                .Select(nameof(TestClass.ByteVal))
                .Select(nameof(TestClass.CharVal))
                .Select(nameof(TestClass.DateTimeOffsetVal))
                .Select(nameof(TestClass.DateTimeVal))
                .Select(nameof(TestClass.DecimalVal))
                .Select(nameof(TestClass.DoubleVal))
                .Select(nameof(TestClass.FloatVal))
                .Select(nameof(TestClass.GuidVal))
                .Select(nameof(TestClass.IntVal))
                .Select(nameof(TestClass.LongVal))
                .Select(nameof(TestClass.SByteVal))
                .Select(nameof(TestClass.ShortVal))
                .Select(nameof(TestClass.SingleVal))
                .Select(nameof(TestClass.StringVal))
                .Select(nameof(TestClass.TimeSpanVal))
                .Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            retRecord = result[0];
            oneRecord = new T().GetDefault1Record();
            Assert.True(retRecord.BoolVal == oneRecord.BoolVal);
            Assert.True(retRecord.ByteArrayVal[0] == oneRecord.ByteArrayVal[0]);
            Assert.True(retRecord.ByteVal == oneRecord.ByteVal);
            Assert.True(retRecord.CharVal == oneRecord.CharVal);
            Assert.True(retRecord.DateTimeOffsetVal == oneRecord.DateTimeOffsetVal);
            Assert.True(retRecord.DateTimeVal == oneRecord.DateTimeVal);
            Assert.True(retRecord.DecimalVal == oneRecord.DecimalVal);
            Assert.True(retRecord.DoubleVal == oneRecord.DoubleVal);
            Assert.True(retRecord.FloatVal == oneRecord.FloatVal);
            Assert.True(retRecord.GuidVal == oneRecord.GuidVal);
            Assert.True(retRecord.IntVal == oneRecord.IntVal);
            Assert.True(retRecord.LongVal == oneRecord.LongVal);
            Assert.True(retRecord.SByteVal == oneRecord.SByteVal);
            Assert.True(retRecord.ShortVal == oneRecord.ShortVal);
            Assert.True(retRecord.SingleVal == oneRecord.SingleVal);
            Assert.True(retRecord.StringVal == oneRecord.StringVal);
            Assert.True(retRecord.TimeSpanVal == oneRecord.TimeSpanVal);
            Assert.True(retRecord.UInt16Val != oneRecord.UInt16Val);
            Assert.True(retRecord.UInt32Val != oneRecord.UInt32Val);
            Assert.True(retRecord.UInt64Val != oneRecord.UInt64Val);

            // 19 select
            serviceQuery = ServiceQueryRequestBuilder.New()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .Select(nameof(TestClass.BoolVal))
                .Select(nameof(TestClass.ByteArrayVal))
                .Select(nameof(TestClass.ByteVal))
                .Select(nameof(TestClass.CharVal))
                .Select(nameof(TestClass.DateTimeOffsetVal))
                .Select(nameof(TestClass.DateTimeVal))
                .Select(nameof(TestClass.DecimalVal))
                .Select(nameof(TestClass.DoubleVal))
                .Select(nameof(TestClass.FloatVal))
                .Select(nameof(TestClass.GuidVal))
                .Select(nameof(TestClass.IntVal))
                .Select(nameof(TestClass.LongVal))
                .Select(nameof(TestClass.SByteVal))
                .Select(nameof(TestClass.ShortVal))
                .Select(nameof(TestClass.SingleVal))
                .Select(nameof(TestClass.StringVal))
                .Select(nameof(TestClass.TimeSpanVal))
                .Select(nameof(TestClass.UInt16Val))
                .Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            retRecord = result[0];
            oneRecord = new T().GetDefault1Record();
            Assert.True(retRecord.BoolVal == oneRecord.BoolVal);
            Assert.True(retRecord.ByteArrayVal[0] == oneRecord.ByteArrayVal[0]);
            Assert.True(retRecord.ByteVal == oneRecord.ByteVal);
            Assert.True(retRecord.CharVal == oneRecord.CharVal);
            Assert.True(retRecord.DateTimeOffsetVal == oneRecord.DateTimeOffsetVal);
            Assert.True(retRecord.DateTimeVal == oneRecord.DateTimeVal);
            Assert.True(retRecord.DecimalVal == oneRecord.DecimalVal);
            Assert.True(retRecord.DoubleVal == oneRecord.DoubleVal);
            Assert.True(retRecord.FloatVal == oneRecord.FloatVal);
            Assert.True(retRecord.GuidVal == oneRecord.GuidVal);
            Assert.True(retRecord.IntVal == oneRecord.IntVal);
            Assert.True(retRecord.LongVal == oneRecord.LongVal);
            Assert.True(retRecord.SByteVal == oneRecord.SByteVal);
            Assert.True(retRecord.ShortVal == oneRecord.ShortVal);
            Assert.True(retRecord.SingleVal == oneRecord.SingleVal);
            Assert.True(retRecord.StringVal == oneRecord.StringVal);
            Assert.True(retRecord.TimeSpanVal == oneRecord.TimeSpanVal);
            Assert.True(retRecord.UInt16Val == oneRecord.UInt16Val);
            Assert.True(retRecord.UInt32Val != oneRecord.UInt32Val);
            Assert.True(retRecord.UInt64Val != oneRecord.UInt64Val);

            // 20 select
            serviceQuery = ServiceQueryRequestBuilder.New()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .Select(nameof(TestClass.BoolVal))
                .Select(nameof(TestClass.ByteArrayVal))
                .Select(nameof(TestClass.ByteVal))
                .Select(nameof(TestClass.CharVal))
                .Select(nameof(TestClass.DateTimeOffsetVal))
                .Select(nameof(TestClass.DateTimeVal))
                .Select(nameof(TestClass.DecimalVal))
                .Select(nameof(TestClass.DoubleVal))
                .Select(nameof(TestClass.FloatVal))
                .Select(nameof(TestClass.GuidVal))
                .Select(nameof(TestClass.IntVal))
                .Select(nameof(TestClass.LongVal))
                .Select(nameof(TestClass.SByteVal))
                .Select(nameof(TestClass.ShortVal))
                .Select(nameof(TestClass.SingleVal))
                .Select(nameof(TestClass.StringVal))
                .Select(nameof(TestClass.TimeSpanVal))
                .Select(nameof(TestClass.UInt16Val))
                .Select(nameof(TestClass.UInt32Val))
                .Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            retRecord = result[0];
            oneRecord = new T().GetDefault1Record();
            Assert.True(retRecord.BoolVal == oneRecord.BoolVal);
            Assert.True(retRecord.ByteArrayVal[0] == oneRecord.ByteArrayVal[0]);
            Assert.True(retRecord.ByteVal == oneRecord.ByteVal);
            Assert.True(retRecord.CharVal == oneRecord.CharVal);
            Assert.True(retRecord.DateTimeOffsetVal == oneRecord.DateTimeOffsetVal);
            Assert.True(retRecord.DateTimeVal == oneRecord.DateTimeVal);
            Assert.True(retRecord.DecimalVal == oneRecord.DecimalVal);
            Assert.True(retRecord.DoubleVal == oneRecord.DoubleVal);
            Assert.True(retRecord.FloatVal == oneRecord.FloatVal);
            Assert.True(retRecord.GuidVal == oneRecord.GuidVal);
            Assert.True(retRecord.IntVal == oneRecord.IntVal);
            Assert.True(retRecord.LongVal == oneRecord.LongVal);
            Assert.True(retRecord.SByteVal == oneRecord.SByteVal);
            Assert.True(retRecord.ShortVal == oneRecord.ShortVal);
            Assert.True(retRecord.SingleVal == oneRecord.SingleVal);
            Assert.True(retRecord.StringVal == oneRecord.StringVal);
            Assert.True(retRecord.TimeSpanVal == oneRecord.TimeSpanVal);
            Assert.True(retRecord.UInt16Val == oneRecord.UInt16Val);
            Assert.True(retRecord.UInt32Val == oneRecord.UInt32Val);
            Assert.True(retRecord.UInt64Val != oneRecord.UInt64Val);

            // 21 select
            serviceQuery = ServiceQueryRequestBuilder.New()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .Select(nameof(TestClass.BoolVal))
                .Select(nameof(TestClass.ByteArrayVal))
                .Select(nameof(TestClass.ByteVal))
                .Select(nameof(TestClass.CharVal))
                .Select(nameof(TestClass.DateTimeOffsetVal))
                .Select(nameof(TestClass.DateTimeVal))
                .Select(nameof(TestClass.DecimalVal))
                .Select(nameof(TestClass.DoubleVal))
                .Select(nameof(TestClass.FloatVal))
                .Select(nameof(TestClass.GuidVal))
                .Select(nameof(TestClass.IntVal))
                .Select(nameof(TestClass.LongVal))
                .Select(nameof(TestClass.SByteVal))
                .Select(nameof(TestClass.ShortVal))
                .Select(nameof(TestClass.SingleVal))
                .Select(nameof(TestClass.StringVal))
                .Select(nameof(TestClass.TimeSpanVal))
                .Select(nameof(TestClass.UInt16Val))
                .Select(nameof(TestClass.UInt32Val))
                .Select(nameof(TestClass.UInt64Val))
                .Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            retRecord = result[0];
            oneRecord = new T().GetDefault1Record();
            Assert.True(retRecord.BoolVal == oneRecord.BoolVal);
            Assert.True(retRecord.ByteArrayVal[0] == oneRecord.ByteArrayVal[0]);
            Assert.True(retRecord.ByteVal == oneRecord.ByteVal);
            Assert.True(retRecord.CharVal == oneRecord.CharVal);
            Assert.True(retRecord.DateTimeOffsetVal == oneRecord.DateTimeOffsetVal);
            Assert.True(retRecord.DateTimeVal == oneRecord.DateTimeVal);
            Assert.True(retRecord.DecimalVal == oneRecord.DecimalVal);
            Assert.True(retRecord.DoubleVal == oneRecord.DoubleVal);
            Assert.True(retRecord.FloatVal == oneRecord.FloatVal);
            Assert.True(retRecord.GuidVal == oneRecord.GuidVal);
            Assert.True(retRecord.IntVal == oneRecord.IntVal);
            Assert.True(retRecord.LongVal == oneRecord.LongVal);
            Assert.True(retRecord.SByteVal == oneRecord.SByteVal);
            Assert.True(retRecord.ShortVal == oneRecord.ShortVal);
            Assert.True(retRecord.SingleVal == oneRecord.SingleVal);
            Assert.True(retRecord.StringVal == oneRecord.StringVal);
            Assert.True(retRecord.TimeSpanVal == oneRecord.TimeSpanVal);
            Assert.True(retRecord.UInt16Val == oneRecord.UInt16Val);
            Assert.True(retRecord.UInt32Val == oneRecord.UInt32Val);
            Assert.True(retRecord.UInt64Val == oneRecord.UInt64Val);
        }

        [Fact]
        public async Task NullSelectStandardTests()
        {
            var sourceQueryable = GetTestList();
            IQueryable<T> testQueryable;
            IServiceQuery serviceQuery;
            List<T> result;
            T retRecord;
            ITestClass oneRecord;

            // No select
            serviceQuery = ServiceQueryBuilder.New()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            retRecord = result[0];
            oneRecord = new T().GetDefault1Record();
            Assert.True(retRecord.NullBoolVal == oneRecord.NullBoolVal);
            Assert.True(retRecord.NullByteArrayVal == oneRecord.NullByteArrayVal);
            Assert.True(retRecord.NullByteVal == oneRecord.NullByteVal);
            Assert.True(retRecord.NullCharVal == oneRecord.NullCharVal);
            Assert.True(retRecord.NullDateTimeOffsetVal == oneRecord.NullDateTimeOffsetVal);
            Assert.True(retRecord.NullDateTimeVal == oneRecord.NullDateTimeVal);
            Assert.True(retRecord.NullDecimalVal == oneRecord.NullDecimalVal);
            Assert.True(retRecord.NullDoubleVal == oneRecord.NullDoubleVal);
            Assert.True(retRecord.NullFloatVal == oneRecord.NullFloatVal);
            Assert.True(retRecord.NullGuidVal == oneRecord.NullGuidVal);
            Assert.True(retRecord.NullIntVal == oneRecord.NullIntVal);
            Assert.True(retRecord.NullLongVal == oneRecord.NullLongVal);
            Assert.True(retRecord.NullSByteVal == oneRecord.NullSByteVal);
            Assert.True(retRecord.NullShortVal == oneRecord.NullShortVal);
            Assert.True(retRecord.NullSingleVal == oneRecord.NullSingleVal);
            Assert.True(retRecord.NullStringVal == oneRecord.NullStringVal);
            Assert.True(retRecord.NullTimeSpanVal == oneRecord.NullTimeSpanVal);
            Assert.True(retRecord.NullUInt16Val == oneRecord.NullUInt16Val);
            Assert.True(retRecord.NullUInt32Val == oneRecord.NullUInt32Val);
            Assert.True(retRecord.NullUInt64Val == oneRecord.NullUInt64Val);

            // 1 select
            serviceQuery = ServiceQueryBuilder.New()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .Select(nameof(TestClass.NullBoolVal))
                .Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            retRecord = result[0];
            oneRecord = new T().GetDefault1Record();
            Assert.True(retRecord.NullBoolVal == oneRecord.NullBoolVal);
            Assert.True(retRecord.NullByteArrayVal == oneRecord.NullByteArrayVal);
            Assert.True(retRecord.NullByteVal == oneRecord.NullByteVal);
            Assert.True(retRecord.NullCharVal == oneRecord.NullCharVal);
            Assert.True(retRecord.NullDateTimeOffsetVal == oneRecord.NullDateTimeOffsetVal);
            Assert.True(retRecord.NullDateTimeVal == oneRecord.NullDateTimeVal);
            Assert.True(retRecord.NullDecimalVal == oneRecord.NullDecimalVal);
            Assert.True(retRecord.NullDoubleVal == oneRecord.NullDoubleVal);
            Assert.True(retRecord.NullFloatVal == oneRecord.NullFloatVal);
            Assert.True(retRecord.NullGuidVal == oneRecord.NullGuidVal);
            Assert.True(retRecord.NullIntVal == oneRecord.NullIntVal);
            Assert.True(retRecord.NullLongVal == oneRecord.NullLongVal);
            Assert.True(retRecord.NullSByteVal == oneRecord.NullSByteVal);
            Assert.True(retRecord.NullShortVal == oneRecord.NullShortVal);
            Assert.True(retRecord.NullSingleVal == oneRecord.NullSingleVal);
            Assert.True(retRecord.NullStringVal == oneRecord.NullStringVal);
            Assert.True(retRecord.NullTimeSpanVal == oneRecord.NullTimeSpanVal);
            Assert.True(retRecord.NullUInt16Val == oneRecord.NullUInt16Val);
            Assert.True(retRecord.NullUInt32Val == oneRecord.NullUInt32Val);
            Assert.True(retRecord.NullUInt64Val == oneRecord.NullUInt64Val);

            // 2 select
            serviceQuery = ServiceQueryBuilder.New()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .Select(nameof(TestClass.NullBoolVal))
                .Select(nameof(TestClass.NullByteArrayVal))
                .Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            retRecord = result[0];
            oneRecord = new T().GetDefault1Record();
            Assert.True(retRecord.NullBoolVal == oneRecord.NullBoolVal);
            Assert.True(retRecord.NullByteArrayVal == oneRecord.NullByteArrayVal);
            Assert.True(retRecord.NullByteVal == oneRecord.NullByteVal);
            Assert.True(retRecord.NullCharVal == oneRecord.NullCharVal);
            Assert.True(retRecord.NullDateTimeOffsetVal == oneRecord.NullDateTimeOffsetVal);
            Assert.True(retRecord.NullDateTimeVal == oneRecord.NullDateTimeVal);
            Assert.True(retRecord.NullDecimalVal == oneRecord.NullDecimalVal);
            Assert.True(retRecord.NullDoubleVal == oneRecord.NullDoubleVal);
            Assert.True(retRecord.NullFloatVal == oneRecord.NullFloatVal);
            Assert.True(retRecord.NullGuidVal == oneRecord.NullGuidVal);
            Assert.True(retRecord.NullIntVal == oneRecord.NullIntVal);
            Assert.True(retRecord.NullLongVal == oneRecord.NullLongVal);
            Assert.True(retRecord.NullSByteVal == oneRecord.NullSByteVal);
            Assert.True(retRecord.NullShortVal == oneRecord.NullShortVal);
            Assert.True(retRecord.NullSingleVal == oneRecord.NullSingleVal);
            Assert.True(retRecord.NullStringVal == oneRecord.NullStringVal);
            Assert.True(retRecord.NullTimeSpanVal == oneRecord.NullTimeSpanVal);
            Assert.True(retRecord.NullUInt16Val == oneRecord.NullUInt16Val);
            Assert.True(retRecord.NullUInt32Val == oneRecord.NullUInt32Val);
            Assert.True(retRecord.NullUInt64Val == oneRecord.NullUInt64Val);

            // 3 select
            serviceQuery = ServiceQueryBuilder.New()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .Select(nameof(TestClass.NullBoolVal))
                .Select(nameof(TestClass.NullByteArrayVal))
                .Select(nameof(TestClass.NullByteVal))
                .Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            retRecord = result[0];
            oneRecord = new T().GetDefault1Record();
            Assert.True(retRecord.NullBoolVal == oneRecord.NullBoolVal);
            Assert.True(retRecord.NullByteArrayVal == oneRecord.NullByteArrayVal);
            Assert.True(retRecord.NullByteVal == oneRecord.NullByteVal);
            Assert.True(retRecord.NullCharVal == oneRecord.NullCharVal);
            Assert.True(retRecord.NullDateTimeOffsetVal == oneRecord.NullDateTimeOffsetVal);
            Assert.True(retRecord.NullDateTimeVal == oneRecord.NullDateTimeVal);
            Assert.True(retRecord.NullDecimalVal == oneRecord.NullDecimalVal);
            Assert.True(retRecord.NullDoubleVal == oneRecord.NullDoubleVal);
            Assert.True(retRecord.NullFloatVal == oneRecord.NullFloatVal);
            Assert.True(retRecord.NullGuidVal == oneRecord.NullGuidVal);
            Assert.True(retRecord.NullIntVal == oneRecord.NullIntVal);
            Assert.True(retRecord.NullLongVal == oneRecord.NullLongVal);
            Assert.True(retRecord.NullSByteVal == oneRecord.NullSByteVal);
            Assert.True(retRecord.NullShortVal == oneRecord.NullShortVal);
            Assert.True(retRecord.NullSingleVal == oneRecord.NullSingleVal);
            Assert.True(retRecord.NullStringVal == oneRecord.NullStringVal);
            Assert.True(retRecord.NullTimeSpanVal == oneRecord.NullTimeSpanVal);
            Assert.True(retRecord.NullUInt16Val == oneRecord.NullUInt16Val);
            Assert.True(retRecord.NullUInt32Val == oneRecord.NullUInt32Val);
            Assert.True(retRecord.NullUInt64Val == oneRecord.NullUInt64Val);

            // 4 select
            serviceQuery = ServiceQueryBuilder.New()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .Select(nameof(TestClass.NullBoolVal))
                .Select(nameof(TestClass.NullByteArrayVal))
                .Select(nameof(TestClass.NullByteVal))
                .Select(nameof(TestClass.NullCharVal))
                .Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            retRecord = result[0];
            oneRecord = new T().GetDefault1Record();
            Assert.True(retRecord.NullBoolVal == oneRecord.NullBoolVal);
            Assert.True(retRecord.NullByteArrayVal == oneRecord.NullByteArrayVal);
            Assert.True(retRecord.NullByteVal == oneRecord.NullByteVal);
            Assert.True(retRecord.NullCharVal == oneRecord.NullCharVal);
            Assert.True(retRecord.NullDateTimeOffsetVal == oneRecord.NullDateTimeOffsetVal);
            Assert.True(retRecord.NullDateTimeVal == oneRecord.NullDateTimeVal);
            Assert.True(retRecord.NullDecimalVal == oneRecord.NullDecimalVal);
            Assert.True(retRecord.NullDoubleVal == oneRecord.NullDoubleVal);
            Assert.True(retRecord.NullFloatVal == oneRecord.NullFloatVal);
            Assert.True(retRecord.NullGuidVal == oneRecord.NullGuidVal);
            Assert.True(retRecord.NullIntVal == oneRecord.NullIntVal);
            Assert.True(retRecord.NullLongVal == oneRecord.NullLongVal);
            Assert.True(retRecord.NullSByteVal == oneRecord.NullSByteVal);
            Assert.True(retRecord.NullShortVal == oneRecord.NullShortVal);
            Assert.True(retRecord.NullSingleVal == oneRecord.NullSingleVal);
            Assert.True(retRecord.NullStringVal == oneRecord.NullStringVal);
            Assert.True(retRecord.NullTimeSpanVal == oneRecord.NullTimeSpanVal);
            Assert.True(retRecord.NullUInt16Val == oneRecord.NullUInt16Val);
            Assert.True(retRecord.NullUInt32Val == oneRecord.NullUInt32Val);
            Assert.True(retRecord.NullUInt64Val == oneRecord.NullUInt64Val);

            // 5 select
            serviceQuery = ServiceQueryBuilder.New()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .Select(nameof(TestClass.NullBoolVal))
                .Select(nameof(TestClass.NullByteArrayVal))
                .Select(nameof(TestClass.NullByteVal))
                .Select(nameof(TestClass.NullCharVal))
                .Select(nameof(TestClass.NullDateTimeOffsetVal))
                .Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            retRecord = result[0];
            oneRecord = new T().GetDefault1Record();
            Assert.True(retRecord.NullBoolVal == oneRecord.NullBoolVal);
            Assert.True(retRecord.NullByteArrayVal == oneRecord.NullByteArrayVal);
            Assert.True(retRecord.NullByteVal == oneRecord.NullByteVal);
            Assert.True(retRecord.NullCharVal == oneRecord.NullCharVal);
            Assert.True(retRecord.NullDateTimeOffsetVal == oneRecord.NullDateTimeOffsetVal);
            Assert.True(retRecord.NullDateTimeVal == oneRecord.NullDateTimeVal);
            Assert.True(retRecord.NullDecimalVal == oneRecord.NullDecimalVal);
            Assert.True(retRecord.NullDoubleVal == oneRecord.NullDoubleVal);
            Assert.True(retRecord.NullFloatVal == oneRecord.NullFloatVal);
            Assert.True(retRecord.NullGuidVal == oneRecord.NullGuidVal);
            Assert.True(retRecord.NullIntVal == oneRecord.NullIntVal);
            Assert.True(retRecord.NullLongVal == oneRecord.NullLongVal);
            Assert.True(retRecord.NullSByteVal == oneRecord.NullSByteVal);
            Assert.True(retRecord.NullShortVal == oneRecord.NullShortVal);
            Assert.True(retRecord.NullSingleVal == oneRecord.NullSingleVal);
            Assert.True(retRecord.NullStringVal == oneRecord.NullStringVal);
            Assert.True(retRecord.NullTimeSpanVal == oneRecord.NullTimeSpanVal);
            Assert.True(retRecord.NullUInt16Val == oneRecord.NullUInt16Val);
            Assert.True(retRecord.NullUInt32Val == oneRecord.NullUInt32Val);
            Assert.True(retRecord.NullUInt64Val == oneRecord.NullUInt64Val);

            // 6 select
            serviceQuery = ServiceQueryBuilder.New()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .Select(nameof(TestClass.NullBoolVal))
                .Select(nameof(TestClass.NullByteArrayVal))
                .Select(nameof(TestClass.NullByteVal))
                .Select(nameof(TestClass.NullCharVal))
                .Select(nameof(TestClass.NullDateTimeOffsetVal))
                .Select(nameof(TestClass.NullDateTimeVal))
                .Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            retRecord = result[0];
            oneRecord = new T().GetDefault1Record();
            Assert.True(retRecord.NullBoolVal == oneRecord.NullBoolVal);
            Assert.True(retRecord.NullByteArrayVal == oneRecord.NullByteArrayVal);
            Assert.True(retRecord.NullByteVal == oneRecord.NullByteVal);
            Assert.True(retRecord.NullCharVal == oneRecord.NullCharVal);
            Assert.True(retRecord.NullDateTimeOffsetVal == oneRecord.NullDateTimeOffsetVal);
            Assert.True(retRecord.NullDateTimeVal == oneRecord.NullDateTimeVal);
            Assert.True(retRecord.NullDecimalVal == oneRecord.NullDecimalVal);
            Assert.True(retRecord.NullDoubleVal == oneRecord.NullDoubleVal);
            Assert.True(retRecord.NullFloatVal == oneRecord.NullFloatVal);
            Assert.True(retRecord.NullGuidVal == oneRecord.NullGuidVal);
            Assert.True(retRecord.NullIntVal == oneRecord.NullIntVal);
            Assert.True(retRecord.NullLongVal == oneRecord.NullLongVal);
            Assert.True(retRecord.NullSByteVal == oneRecord.NullSByteVal);
            Assert.True(retRecord.NullShortVal == oneRecord.NullShortVal);
            Assert.True(retRecord.NullSingleVal == oneRecord.NullSingleVal);
            Assert.True(retRecord.NullStringVal == oneRecord.NullStringVal);
            Assert.True(retRecord.NullTimeSpanVal == oneRecord.NullTimeSpanVal);
            Assert.True(retRecord.NullUInt16Val == oneRecord.NullUInt16Val);
            Assert.True(retRecord.NullUInt32Val == oneRecord.NullUInt32Val);
            Assert.True(retRecord.NullUInt64Val == oneRecord.NullUInt64Val);

            // 7 select
            serviceQuery = ServiceQueryBuilder.New()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .Select(nameof(TestClass.NullBoolVal))
                .Select(nameof(TestClass.NullByteArrayVal))
                .Select(nameof(TestClass.NullByteVal))
                .Select(nameof(TestClass.NullCharVal))
                .Select(nameof(TestClass.NullDateTimeOffsetVal))
                .Select(nameof(TestClass.NullDateTimeVal))
                .Select(nameof(TestClass.NullDecimalVal))
                .Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            retRecord = result[0];
            oneRecord = new T().GetDefault1Record();
            Assert.True(retRecord.NullBoolVal == oneRecord.NullBoolVal);
            Assert.True(retRecord.NullByteArrayVal == oneRecord.NullByteArrayVal);
            Assert.True(retRecord.NullByteVal == oneRecord.NullByteVal);
            Assert.True(retRecord.NullCharVal == oneRecord.NullCharVal);
            Assert.True(retRecord.NullDateTimeOffsetVal == oneRecord.NullDateTimeOffsetVal);
            Assert.True(retRecord.NullDateTimeVal == oneRecord.NullDateTimeVal);
            Assert.True(retRecord.NullDecimalVal == oneRecord.NullDecimalVal);
            Assert.True(retRecord.NullDoubleVal == oneRecord.NullDoubleVal);
            Assert.True(retRecord.NullFloatVal == oneRecord.NullFloatVal);
            Assert.True(retRecord.NullGuidVal == oneRecord.NullGuidVal);
            Assert.True(retRecord.NullIntVal == oneRecord.NullIntVal);
            Assert.True(retRecord.NullLongVal == oneRecord.NullLongVal);
            Assert.True(retRecord.NullSByteVal == oneRecord.NullSByteVal);
            Assert.True(retRecord.NullShortVal == oneRecord.NullShortVal);
            Assert.True(retRecord.NullSingleVal == oneRecord.NullSingleVal);
            Assert.True(retRecord.NullStringVal == oneRecord.NullStringVal);
            Assert.True(retRecord.NullTimeSpanVal == oneRecord.NullTimeSpanVal);
            Assert.True(retRecord.NullUInt16Val == oneRecord.NullUInt16Val);
            Assert.True(retRecord.NullUInt32Val == oneRecord.NullUInt32Val);
            Assert.True(retRecord.NullUInt64Val == oneRecord.NullUInt64Val);

            // 8 select
            serviceQuery = ServiceQueryBuilder.New()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .Select(nameof(TestClass.NullBoolVal))
                .Select(nameof(TestClass.NullByteArrayVal))
                .Select(nameof(TestClass.NullByteVal))
                .Select(nameof(TestClass.NullCharVal))
                .Select(nameof(TestClass.NullDateTimeOffsetVal))
                .Select(nameof(TestClass.NullDateTimeVal))
                .Select(nameof(TestClass.NullDecimalVal))
                .Select(nameof(TestClass.NullDoubleVal))
                .Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            retRecord = result[0];
            oneRecord = new T().GetDefault1Record();
            Assert.True(retRecord.NullBoolVal == oneRecord.NullBoolVal);
            Assert.True(retRecord.NullByteArrayVal == oneRecord.NullByteArrayVal);
            Assert.True(retRecord.NullByteVal == oneRecord.NullByteVal);
            Assert.True(retRecord.NullCharVal == oneRecord.NullCharVal);
            Assert.True(retRecord.NullDateTimeOffsetVal == oneRecord.NullDateTimeOffsetVal);
            Assert.True(retRecord.NullDateTimeVal == oneRecord.NullDateTimeVal);
            Assert.True(retRecord.NullDecimalVal == oneRecord.NullDecimalVal);
            Assert.True(retRecord.NullDoubleVal == oneRecord.NullDoubleVal);
            Assert.True(retRecord.NullFloatVal == oneRecord.NullFloatVal);
            Assert.True(retRecord.NullGuidVal == oneRecord.NullGuidVal);
            Assert.True(retRecord.NullIntVal == oneRecord.NullIntVal);
            Assert.True(retRecord.NullLongVal == oneRecord.NullLongVal);
            Assert.True(retRecord.NullSByteVal == oneRecord.NullSByteVal);
            Assert.True(retRecord.NullShortVal == oneRecord.NullShortVal);
            Assert.True(retRecord.NullSingleVal == oneRecord.NullSingleVal);
            Assert.True(retRecord.NullStringVal == oneRecord.NullStringVal);
            Assert.True(retRecord.NullTimeSpanVal == oneRecord.NullTimeSpanVal);
            Assert.True(retRecord.NullUInt16Val == oneRecord.NullUInt16Val);
            Assert.True(retRecord.NullUInt32Val == oneRecord.NullUInt32Val);
            Assert.True(retRecord.NullUInt64Val == oneRecord.NullUInt64Val);

            // 9 select
            serviceQuery = ServiceQueryBuilder.New()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .Select(nameof(TestClass.NullBoolVal))
                .Select(nameof(TestClass.NullByteArrayVal))
                .Select(nameof(TestClass.NullByteVal))
                .Select(nameof(TestClass.NullCharVal))
                .Select(nameof(TestClass.NullDateTimeOffsetVal))
                .Select(nameof(TestClass.NullDateTimeVal))
                .Select(nameof(TestClass.NullDecimalVal))
                .Select(nameof(TestClass.NullDoubleVal))
                .Select(nameof(TestClass.NullFloatVal))
                .Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            retRecord = result[0];
            oneRecord = new T().GetDefault1Record();
            Assert.True(retRecord.NullBoolVal == oneRecord.NullBoolVal);
            Assert.True(retRecord.NullByteArrayVal == oneRecord.NullByteArrayVal);
            Assert.True(retRecord.NullByteVal == oneRecord.NullByteVal);
            Assert.True(retRecord.NullCharVal == oneRecord.NullCharVal);
            Assert.True(retRecord.NullDateTimeOffsetVal == oneRecord.NullDateTimeOffsetVal);
            Assert.True(retRecord.NullDateTimeVal == oneRecord.NullDateTimeVal);
            Assert.True(retRecord.NullDecimalVal == oneRecord.NullDecimalVal);
            Assert.True(retRecord.NullDoubleVal == oneRecord.NullDoubleVal);
            Assert.True(retRecord.NullFloatVal == oneRecord.NullFloatVal);
            Assert.True(retRecord.NullGuidVal == oneRecord.NullGuidVal);
            Assert.True(retRecord.NullIntVal == oneRecord.NullIntVal);
            Assert.True(retRecord.NullLongVal == oneRecord.NullLongVal);
            Assert.True(retRecord.NullSByteVal == oneRecord.NullSByteVal);
            Assert.True(retRecord.NullShortVal == oneRecord.NullShortVal);
            Assert.True(retRecord.NullSingleVal == oneRecord.NullSingleVal);
            Assert.True(retRecord.NullStringVal == oneRecord.NullStringVal);
            Assert.True(retRecord.NullTimeSpanVal == oneRecord.NullTimeSpanVal);
            Assert.True(retRecord.NullUInt16Val == oneRecord.NullUInt16Val);
            Assert.True(retRecord.NullUInt32Val == oneRecord.NullUInt32Val);
            Assert.True(retRecord.NullUInt64Val == oneRecord.NullUInt64Val);

            // 10 select
            serviceQuery = ServiceQueryBuilder.New()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .Select(nameof(TestClass.NullBoolVal))
                .Select(nameof(TestClass.NullByteArrayVal))
                .Select(nameof(TestClass.NullByteVal))
                .Select(nameof(TestClass.NullCharVal))
                .Select(nameof(TestClass.NullDateTimeOffsetVal))
                .Select(nameof(TestClass.NullDateTimeVal))
                .Select(nameof(TestClass.NullDecimalVal))
                .Select(nameof(TestClass.NullDoubleVal))
                .Select(nameof(TestClass.NullFloatVal))
                .Select(nameof(TestClass.NullGuidVal))
                .Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            retRecord = result[0];
            oneRecord = new T().GetDefault1Record();
            Assert.True(retRecord.NullBoolVal == oneRecord.NullBoolVal);
            Assert.True(retRecord.NullByteArrayVal == oneRecord.NullByteArrayVal);
            Assert.True(retRecord.NullByteVal == oneRecord.NullByteVal);
            Assert.True(retRecord.NullCharVal == oneRecord.NullCharVal);
            Assert.True(retRecord.NullDateTimeOffsetVal == oneRecord.NullDateTimeOffsetVal);
            Assert.True(retRecord.NullDateTimeVal == oneRecord.NullDateTimeVal);
            Assert.True(retRecord.NullDecimalVal == oneRecord.NullDecimalVal);
            Assert.True(retRecord.NullDoubleVal == oneRecord.NullDoubleVal);
            Assert.True(retRecord.NullFloatVal == oneRecord.NullFloatVal);
            Assert.True(retRecord.NullGuidVal == oneRecord.NullGuidVal);
            Assert.True(retRecord.NullIntVal == oneRecord.NullIntVal);
            Assert.True(retRecord.NullLongVal == oneRecord.NullLongVal);
            Assert.True(retRecord.NullSByteVal == oneRecord.NullSByteVal);
            Assert.True(retRecord.NullShortVal == oneRecord.NullShortVal);
            Assert.True(retRecord.NullSingleVal == oneRecord.NullSingleVal);
            Assert.True(retRecord.NullStringVal == oneRecord.NullStringVal);
            Assert.True(retRecord.NullTimeSpanVal == oneRecord.NullTimeSpanVal);
            Assert.True(retRecord.NullUInt16Val == oneRecord.NullUInt16Val);
            Assert.True(retRecord.NullUInt32Val == oneRecord.NullUInt32Val);
            Assert.True(retRecord.NullUInt64Val == oneRecord.NullUInt64Val);

            // 11 select
            serviceQuery = ServiceQueryBuilder.New()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .Select(nameof(TestClass.NullBoolVal))
                .Select(nameof(TestClass.NullByteArrayVal))
                .Select(nameof(TestClass.NullByteVal))
                .Select(nameof(TestClass.NullCharVal))
                .Select(nameof(TestClass.NullDateTimeOffsetVal))
                .Select(nameof(TestClass.NullDateTimeVal))
                .Select(nameof(TestClass.NullDecimalVal))
                .Select(nameof(TestClass.NullDoubleVal))
                .Select(nameof(TestClass.NullFloatVal))
                .Select(nameof(TestClass.NullGuidVal))
                .Select(nameof(TestClass.IntVal))
                .Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            retRecord = result[0];
            oneRecord = new T().GetDefault1Record();
            Assert.True(retRecord.NullBoolVal == oneRecord.NullBoolVal);
            Assert.True(retRecord.NullByteArrayVal == oneRecord.NullByteArrayVal);
            Assert.True(retRecord.NullByteVal == oneRecord.NullByteVal);
            Assert.True(retRecord.NullCharVal == oneRecord.NullCharVal);
            Assert.True(retRecord.NullDateTimeOffsetVal == oneRecord.NullDateTimeOffsetVal);
            Assert.True(retRecord.NullDateTimeVal == oneRecord.NullDateTimeVal);
            Assert.True(retRecord.NullDecimalVal == oneRecord.NullDecimalVal);
            Assert.True(retRecord.NullDoubleVal == oneRecord.NullDoubleVal);
            Assert.True(retRecord.NullFloatVal == oneRecord.NullFloatVal);
            Assert.True(retRecord.NullGuidVal == oneRecord.NullGuidVal);
            Assert.True(retRecord.NullIntVal == oneRecord.NullIntVal);
            Assert.True(retRecord.NullLongVal == oneRecord.NullLongVal);
            Assert.True(retRecord.NullSByteVal == oneRecord.NullSByteVal);
            Assert.True(retRecord.NullShortVal == oneRecord.NullShortVal);
            Assert.True(retRecord.NullSingleVal == oneRecord.NullSingleVal);
            Assert.True(retRecord.NullStringVal == oneRecord.NullStringVal);
            Assert.True(retRecord.NullTimeSpanVal == oneRecord.NullTimeSpanVal);
            Assert.True(retRecord.NullUInt16Val == oneRecord.NullUInt16Val);
            Assert.True(retRecord.NullUInt32Val == oneRecord.NullUInt32Val);
            Assert.True(retRecord.NullUInt64Val == oneRecord.NullUInt64Val);

            // 12 select
            serviceQuery = ServiceQueryBuilder.New()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .Select(nameof(TestClass.NullBoolVal))
                .Select(nameof(TestClass.NullByteArrayVal))
                .Select(nameof(TestClass.NullByteVal))
                .Select(nameof(TestClass.NullCharVal))
                .Select(nameof(TestClass.NullDateTimeOffsetVal))
                .Select(nameof(TestClass.NullDateTimeVal))
                .Select(nameof(TestClass.NullDecimalVal))
                .Select(nameof(TestClass.NullDoubleVal))
                .Select(nameof(TestClass.NullFloatVal))
                .Select(nameof(TestClass.NullGuidVal))
                .Select(nameof(TestClass.IntVal))
                .Select(nameof(TestClass.NullLongVal))
                .Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            retRecord = result[0];
            oneRecord = new T().GetDefault1Record();
            Assert.True(retRecord.NullBoolVal == oneRecord.NullBoolVal);
            Assert.True(retRecord.NullByteArrayVal == oneRecord.NullByteArrayVal);
            Assert.True(retRecord.NullByteVal == oneRecord.NullByteVal);
            Assert.True(retRecord.NullCharVal == oneRecord.NullCharVal);
            Assert.True(retRecord.NullDateTimeOffsetVal == oneRecord.NullDateTimeOffsetVal);
            Assert.True(retRecord.NullDateTimeVal == oneRecord.NullDateTimeVal);
            Assert.True(retRecord.NullDecimalVal == oneRecord.NullDecimalVal);
            Assert.True(retRecord.NullDoubleVal == oneRecord.NullDoubleVal);
            Assert.True(retRecord.NullFloatVal == oneRecord.NullFloatVal);
            Assert.True(retRecord.NullGuidVal == oneRecord.NullGuidVal);
            Assert.True(retRecord.NullIntVal == oneRecord.NullIntVal);
            Assert.True(retRecord.NullLongVal == oneRecord.NullLongVal);
            Assert.True(retRecord.NullSByteVal == oneRecord.NullSByteVal);
            Assert.True(retRecord.NullShortVal == oneRecord.NullShortVal);
            Assert.True(retRecord.NullSingleVal == oneRecord.NullSingleVal);
            Assert.True(retRecord.NullStringVal == oneRecord.NullStringVal);
            Assert.True(retRecord.NullTimeSpanVal == oneRecord.NullTimeSpanVal);
            Assert.True(retRecord.NullUInt16Val == oneRecord.NullUInt16Val);
            Assert.True(retRecord.NullUInt32Val == oneRecord.NullUInt32Val);
            Assert.True(retRecord.NullUInt64Val == oneRecord.NullUInt64Val);

            // 13 select
            serviceQuery = ServiceQueryBuilder.New()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .Select(nameof(TestClass.NullBoolVal))
                .Select(nameof(TestClass.NullByteArrayVal))
                .Select(nameof(TestClass.NullByteVal))
                .Select(nameof(TestClass.NullCharVal))
                .Select(nameof(TestClass.NullDateTimeOffsetVal))
                .Select(nameof(TestClass.NullDateTimeVal))
                .Select(nameof(TestClass.NullDecimalVal))
                .Select(nameof(TestClass.NullDoubleVal))
                .Select(nameof(TestClass.NullFloatVal))
                .Select(nameof(TestClass.NullGuidVal))
                .Select(nameof(TestClass.IntVal))
                .Select(nameof(TestClass.NullLongVal))
                .Select(nameof(TestClass.NullSByteVal))
                .Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            retRecord = result[0];
            oneRecord = new T().GetDefault1Record();
            Assert.True(retRecord.NullBoolVal == oneRecord.NullBoolVal);
            Assert.True(retRecord.NullByteArrayVal == oneRecord.NullByteArrayVal);
            Assert.True(retRecord.NullByteVal == oneRecord.NullByteVal);
            Assert.True(retRecord.NullCharVal == oneRecord.NullCharVal);
            Assert.True(retRecord.NullDateTimeOffsetVal == oneRecord.NullDateTimeOffsetVal);
            Assert.True(retRecord.NullDateTimeVal == oneRecord.NullDateTimeVal);
            Assert.True(retRecord.NullDecimalVal == oneRecord.NullDecimalVal);
            Assert.True(retRecord.NullDoubleVal == oneRecord.NullDoubleVal);
            Assert.True(retRecord.NullFloatVal == oneRecord.NullFloatVal);
            Assert.True(retRecord.NullGuidVal == oneRecord.NullGuidVal);
            Assert.True(retRecord.NullIntVal == oneRecord.NullIntVal);
            Assert.True(retRecord.NullLongVal == oneRecord.NullLongVal);
            Assert.True(retRecord.NullSByteVal == oneRecord.NullSByteVal);
            Assert.True(retRecord.NullShortVal == oneRecord.NullShortVal);
            Assert.True(retRecord.NullSingleVal == oneRecord.NullSingleVal);
            Assert.True(retRecord.NullStringVal == oneRecord.NullStringVal);
            Assert.True(retRecord.NullTimeSpanVal == oneRecord.NullTimeSpanVal);
            Assert.True(retRecord.NullUInt16Val == oneRecord.NullUInt16Val);
            Assert.True(retRecord.NullUInt32Val == oneRecord.NullUInt32Val);
            Assert.True(retRecord.NullUInt64Val == oneRecord.NullUInt64Val);

            // 14 select
            serviceQuery = ServiceQueryBuilder.New()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .Select(nameof(TestClass.NullBoolVal))
                .Select(nameof(TestClass.NullByteArrayVal))
                .Select(nameof(TestClass.NullByteVal))
                .Select(nameof(TestClass.NullCharVal))
                .Select(nameof(TestClass.NullDateTimeOffsetVal))
                .Select(nameof(TestClass.NullDateTimeVal))
                .Select(nameof(TestClass.NullDecimalVal))
                .Select(nameof(TestClass.NullDoubleVal))
                .Select(nameof(TestClass.NullFloatVal))
                .Select(nameof(TestClass.NullGuidVal))
                .Select(nameof(TestClass.IntVal))
                .Select(nameof(TestClass.NullLongVal))
                .Select(nameof(TestClass.NullSByteVal))
                .Select(nameof(TestClass.NullShortVal))
                .Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            retRecord = result[0];
            oneRecord = new T().GetDefault1Record();
            Assert.True(retRecord.NullBoolVal == oneRecord.NullBoolVal);
            Assert.True(retRecord.NullByteArrayVal == oneRecord.NullByteArrayVal);
            Assert.True(retRecord.NullByteVal == oneRecord.NullByteVal);
            Assert.True(retRecord.NullCharVal == oneRecord.NullCharVal);
            Assert.True(retRecord.NullDateTimeOffsetVal == oneRecord.NullDateTimeOffsetVal);
            Assert.True(retRecord.NullDateTimeVal == oneRecord.NullDateTimeVal);
            Assert.True(retRecord.NullDecimalVal == oneRecord.NullDecimalVal);
            Assert.True(retRecord.NullDoubleVal == oneRecord.NullDoubleVal);
            Assert.True(retRecord.NullFloatVal == oneRecord.NullFloatVal);
            Assert.True(retRecord.NullGuidVal == oneRecord.NullGuidVal);
            Assert.True(retRecord.NullIntVal == oneRecord.NullIntVal);
            Assert.True(retRecord.NullLongVal == oneRecord.NullLongVal);
            Assert.True(retRecord.NullSByteVal == oneRecord.NullSByteVal);
            Assert.True(retRecord.NullShortVal == oneRecord.NullShortVal);
            Assert.True(retRecord.NullSingleVal == oneRecord.NullSingleVal);
            Assert.True(retRecord.NullStringVal == oneRecord.NullStringVal);
            Assert.True(retRecord.NullTimeSpanVal == oneRecord.NullTimeSpanVal);
            Assert.True(retRecord.NullUInt16Val == oneRecord.NullUInt16Val);
            Assert.True(retRecord.NullUInt32Val == oneRecord.NullUInt32Val);
            Assert.True(retRecord.NullUInt64Val == oneRecord.NullUInt64Val);

            // 15 select
            serviceQuery = ServiceQueryBuilder.New()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .Select(nameof(TestClass.NullBoolVal))
                .Select(nameof(TestClass.NullByteArrayVal))
                .Select(nameof(TestClass.NullByteVal))
                .Select(nameof(TestClass.NullCharVal))
                .Select(nameof(TestClass.NullDateTimeOffsetVal))
                .Select(nameof(TestClass.NullDateTimeVal))
                .Select(nameof(TestClass.NullDecimalVal))
                .Select(nameof(TestClass.NullDoubleVal))
                .Select(nameof(TestClass.NullFloatVal))
                .Select(nameof(TestClass.NullGuidVal))
                .Select(nameof(TestClass.IntVal))
                .Select(nameof(TestClass.NullLongVal))
                .Select(nameof(TestClass.NullSByteVal))
                .Select(nameof(TestClass.NullShortVal))
                .Select(nameof(TestClass.NullSingleVal))
                .Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            retRecord = result[0];
            oneRecord = new T().GetDefault1Record();
            Assert.True(retRecord.NullBoolVal == oneRecord.NullBoolVal);
            Assert.True(retRecord.NullByteArrayVal == oneRecord.NullByteArrayVal);
            Assert.True(retRecord.NullByteVal == oneRecord.NullByteVal);
            Assert.True(retRecord.NullCharVal == oneRecord.NullCharVal);
            Assert.True(retRecord.NullDateTimeOffsetVal == oneRecord.NullDateTimeOffsetVal);
            Assert.True(retRecord.NullDateTimeVal == oneRecord.NullDateTimeVal);
            Assert.True(retRecord.NullDecimalVal == oneRecord.NullDecimalVal);
            Assert.True(retRecord.NullDoubleVal == oneRecord.NullDoubleVal);
            Assert.True(retRecord.NullFloatVal == oneRecord.NullFloatVal);
            Assert.True(retRecord.NullGuidVal == oneRecord.NullGuidVal);
            Assert.True(retRecord.NullIntVal == oneRecord.NullIntVal);
            Assert.True(retRecord.NullLongVal == oneRecord.NullLongVal);
            Assert.True(retRecord.NullSByteVal == oneRecord.NullSByteVal);
            Assert.True(retRecord.NullShortVal == oneRecord.NullShortVal);
            Assert.True(retRecord.NullSingleVal == oneRecord.NullSingleVal);
            Assert.True(retRecord.NullStringVal == oneRecord.NullStringVal);
            Assert.True(retRecord.NullTimeSpanVal == oneRecord.NullTimeSpanVal);
            Assert.True(retRecord.NullUInt16Val == oneRecord.NullUInt16Val);
            Assert.True(retRecord.NullUInt32Val == oneRecord.NullUInt32Val);
            Assert.True(retRecord.NullUInt64Val == oneRecord.NullUInt64Val);

            // 16 select
            serviceQuery = ServiceQueryBuilder.New()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .Select(nameof(TestClass.NullBoolVal))
                .Select(nameof(TestClass.NullByteArrayVal))
                .Select(nameof(TestClass.NullByteVal))
                .Select(nameof(TestClass.NullCharVal))
                .Select(nameof(TestClass.NullDateTimeOffsetVal))
                .Select(nameof(TestClass.NullDateTimeVal))
                .Select(nameof(TestClass.NullDecimalVal))
                .Select(nameof(TestClass.NullDoubleVal))
                .Select(nameof(TestClass.NullFloatVal))
                .Select(nameof(TestClass.NullGuidVal))
                .Select(nameof(TestClass.IntVal))
                .Select(nameof(TestClass.NullLongVal))
                .Select(nameof(TestClass.NullSByteVal))
                .Select(nameof(TestClass.NullShortVal))
                .Select(nameof(TestClass.NullSingleVal))
                .Select(nameof(TestClass.NullStringVal))
                .Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            retRecord = result[0];
            oneRecord = new T().GetDefault1Record();
            Assert.True(retRecord.NullBoolVal == oneRecord.NullBoolVal);
            Assert.True(retRecord.NullByteArrayVal == oneRecord.NullByteArrayVal);
            Assert.True(retRecord.NullByteVal == oneRecord.NullByteVal);
            Assert.True(retRecord.NullCharVal == oneRecord.NullCharVal);
            Assert.True(retRecord.NullDateTimeOffsetVal == oneRecord.NullDateTimeOffsetVal);
            Assert.True(retRecord.NullDateTimeVal == oneRecord.NullDateTimeVal);
            Assert.True(retRecord.NullDecimalVal == oneRecord.NullDecimalVal);
            Assert.True(retRecord.NullDoubleVal == oneRecord.NullDoubleVal);
            Assert.True(retRecord.NullFloatVal == oneRecord.NullFloatVal);
            Assert.True(retRecord.NullGuidVal == oneRecord.NullGuidVal);
            Assert.True(retRecord.NullIntVal == oneRecord.NullIntVal);
            Assert.True(retRecord.NullLongVal == oneRecord.NullLongVal);
            Assert.True(retRecord.NullSByteVal == oneRecord.NullSByteVal);
            Assert.True(retRecord.NullShortVal == oneRecord.NullShortVal);
            Assert.True(retRecord.NullSingleVal == oneRecord.NullSingleVal);
            Assert.True(retRecord.NullStringVal == oneRecord.NullStringVal);
            Assert.True(retRecord.NullTimeSpanVal == oneRecord.NullTimeSpanVal);
            Assert.True(retRecord.NullUInt16Val == oneRecord.NullUInt16Val);
            Assert.True(retRecord.NullUInt32Val == oneRecord.NullUInt32Val);
            Assert.True(retRecord.NullUInt64Val == oneRecord.NullUInt64Val);

            // 17 select
            serviceQuery = ServiceQueryBuilder.New()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .Select(nameof(TestClass.NullBoolVal))
                .Select(nameof(TestClass.NullByteArrayVal))
                .Select(nameof(TestClass.NullByteVal))
                .Select(nameof(TestClass.NullCharVal))
                .Select(nameof(TestClass.NullDateTimeOffsetVal))
                .Select(nameof(TestClass.NullDateTimeVal))
                .Select(nameof(TestClass.NullDecimalVal))
                .Select(nameof(TestClass.NullDoubleVal))
                .Select(nameof(TestClass.NullFloatVal))
                .Select(nameof(TestClass.NullGuidVal))
                .Select(nameof(TestClass.IntVal))
                .Select(nameof(TestClass.NullLongVal))
                .Select(nameof(TestClass.NullSByteVal))
                .Select(nameof(TestClass.NullShortVal))
                .Select(nameof(TestClass.NullSingleVal))
                .Select(nameof(TestClass.NullStringVal))
                .Select(nameof(TestClass.NullTimeSpanVal))
                .Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            retRecord = result[0];
            oneRecord = new T().GetDefault1Record();
            Assert.True(retRecord.NullBoolVal == oneRecord.NullBoolVal);
            Assert.True(retRecord.NullByteArrayVal == oneRecord.NullByteArrayVal);
            Assert.True(retRecord.NullByteVal == oneRecord.NullByteVal);
            Assert.True(retRecord.NullCharVal == oneRecord.NullCharVal);
            Assert.True(retRecord.NullDateTimeOffsetVal == oneRecord.NullDateTimeOffsetVal);
            Assert.True(retRecord.NullDateTimeVal == oneRecord.NullDateTimeVal);
            Assert.True(retRecord.NullDecimalVal == oneRecord.NullDecimalVal);
            Assert.True(retRecord.NullDoubleVal == oneRecord.NullDoubleVal);
            Assert.True(retRecord.NullFloatVal == oneRecord.NullFloatVal);
            Assert.True(retRecord.NullGuidVal == oneRecord.NullGuidVal);
            Assert.True(retRecord.NullIntVal == oneRecord.NullIntVal);
            Assert.True(retRecord.NullLongVal == oneRecord.NullLongVal);
            Assert.True(retRecord.NullSByteVal == oneRecord.NullSByteVal);
            Assert.True(retRecord.NullShortVal == oneRecord.NullShortVal);
            Assert.True(retRecord.NullSingleVal == oneRecord.NullSingleVal);
            Assert.True(retRecord.NullStringVal == oneRecord.NullStringVal);
            Assert.True(retRecord.NullTimeSpanVal == oneRecord.NullTimeSpanVal);
            Assert.True(retRecord.NullUInt16Val == oneRecord.NullUInt16Val);
            Assert.True(retRecord.NullUInt32Val == oneRecord.NullUInt32Val);
            Assert.True(retRecord.NullUInt64Val == oneRecord.NullUInt64Val);

            // 18 select
            serviceQuery = ServiceQueryBuilder.New()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .Select(nameof(TestClass.NullBoolVal))
                .Select(nameof(TestClass.NullByteArrayVal))
                .Select(nameof(TestClass.NullByteVal))
                .Select(nameof(TestClass.NullCharVal))
                .Select(nameof(TestClass.NullDateTimeOffsetVal))
                .Select(nameof(TestClass.NullDateTimeVal))
                .Select(nameof(TestClass.NullDecimalVal))
                .Select(nameof(TestClass.NullDoubleVal))
                .Select(nameof(TestClass.NullFloatVal))
                .Select(nameof(TestClass.NullGuidVal))
                .Select(nameof(TestClass.IntVal))
                .Select(nameof(TestClass.NullLongVal))
                .Select(nameof(TestClass.NullSByteVal))
                .Select(nameof(TestClass.NullShortVal))
                .Select(nameof(TestClass.NullSingleVal))
                .Select(nameof(TestClass.NullStringVal))
                .Select(nameof(TestClass.NullTimeSpanVal))
                .Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            retRecord = result[0];
            oneRecord = new T().GetDefault1Record();
            Assert.True(retRecord.NullBoolVal == oneRecord.NullBoolVal);
            Assert.True(retRecord.NullByteArrayVal == oneRecord.NullByteArrayVal);
            Assert.True(retRecord.NullByteVal == oneRecord.NullByteVal);
            Assert.True(retRecord.NullCharVal == oneRecord.NullCharVal);
            Assert.True(retRecord.NullDateTimeOffsetVal == oneRecord.NullDateTimeOffsetVal);
            Assert.True(retRecord.NullDateTimeVal == oneRecord.NullDateTimeVal);
            Assert.True(retRecord.NullDecimalVal == oneRecord.NullDecimalVal);
            Assert.True(retRecord.NullDoubleVal == oneRecord.NullDoubleVal);
            Assert.True(retRecord.NullFloatVal == oneRecord.NullFloatVal);
            Assert.True(retRecord.NullGuidVal == oneRecord.NullGuidVal);
            Assert.True(retRecord.NullIntVal == oneRecord.NullIntVal);
            Assert.True(retRecord.NullLongVal == oneRecord.NullLongVal);
            Assert.True(retRecord.NullSByteVal == oneRecord.NullSByteVal);
            Assert.True(retRecord.NullShortVal == oneRecord.NullShortVal);
            Assert.True(retRecord.NullSingleVal == oneRecord.NullSingleVal);
            Assert.True(retRecord.NullStringVal == oneRecord.NullStringVal);
            Assert.True(retRecord.NullTimeSpanVal == oneRecord.NullTimeSpanVal);
            Assert.True(retRecord.NullUInt16Val == oneRecord.NullUInt16Val);
            Assert.True(retRecord.NullUInt32Val == oneRecord.NullUInt32Val);
            Assert.True(retRecord.NullUInt64Val == oneRecord.NullUInt64Val);

            // 19 select
            serviceQuery = ServiceQueryBuilder.New()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .Select(nameof(TestClass.NullBoolVal))
                .Select(nameof(TestClass.NullByteArrayVal))
                .Select(nameof(TestClass.NullByteVal))
                .Select(nameof(TestClass.NullCharVal))
                .Select(nameof(TestClass.NullDateTimeOffsetVal))
                .Select(nameof(TestClass.NullDateTimeVal))
                .Select(nameof(TestClass.NullDecimalVal))
                .Select(nameof(TestClass.NullDoubleVal))
                .Select(nameof(TestClass.NullFloatVal))
                .Select(nameof(TestClass.NullGuidVal))
                .Select(nameof(TestClass.IntVal))
                .Select(nameof(TestClass.NullLongVal))
                .Select(nameof(TestClass.NullSByteVal))
                .Select(nameof(TestClass.NullShortVal))
                .Select(nameof(TestClass.NullSingleVal))
                .Select(nameof(TestClass.NullStringVal))
                .Select(nameof(TestClass.NullTimeSpanVal))
                .Select(nameof(TestClass.NullUInt16Val))
                .Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            retRecord = result[0];
            oneRecord = new T().GetDefault1Record();
            Assert.True(retRecord.NullBoolVal == oneRecord.NullBoolVal);
            Assert.True(retRecord.NullByteArrayVal == oneRecord.NullByteArrayVal);
            Assert.True(retRecord.NullByteVal == oneRecord.NullByteVal);
            Assert.True(retRecord.NullCharVal == oneRecord.NullCharVal);
            Assert.True(retRecord.NullDateTimeOffsetVal == oneRecord.NullDateTimeOffsetVal);
            Assert.True(retRecord.NullDateTimeVal == oneRecord.NullDateTimeVal);
            Assert.True(retRecord.NullDecimalVal == oneRecord.NullDecimalVal);
            Assert.True(retRecord.NullDoubleVal == oneRecord.NullDoubleVal);
            Assert.True(retRecord.NullFloatVal == oneRecord.NullFloatVal);
            Assert.True(retRecord.NullGuidVal == oneRecord.NullGuidVal);
            Assert.True(retRecord.NullIntVal == oneRecord.NullIntVal);
            Assert.True(retRecord.NullLongVal == oneRecord.NullLongVal);
            Assert.True(retRecord.NullSByteVal == oneRecord.NullSByteVal);
            Assert.True(retRecord.NullShortVal == oneRecord.NullShortVal);
            Assert.True(retRecord.NullSingleVal == oneRecord.NullSingleVal);
            Assert.True(retRecord.NullStringVal == oneRecord.NullStringVal);
            Assert.True(retRecord.NullTimeSpanVal == oneRecord.NullTimeSpanVal);
            Assert.True(retRecord.NullUInt16Val == oneRecord.NullUInt16Val);
            Assert.True(retRecord.NullUInt32Val == oneRecord.NullUInt32Val);
            Assert.True(retRecord.NullUInt64Val == oneRecord.NullUInt64Val);

            // 20 select
            serviceQuery = ServiceQueryBuilder.New()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .Select(nameof(TestClass.NullBoolVal))
                .Select(nameof(TestClass.NullByteArrayVal))
                .Select(nameof(TestClass.NullByteVal))
                .Select(nameof(TestClass.NullCharVal))
                .Select(nameof(TestClass.NullDateTimeOffsetVal))
                .Select(nameof(TestClass.NullDateTimeVal))
                .Select(nameof(TestClass.NullDecimalVal))
                .Select(nameof(TestClass.NullDoubleVal))
                .Select(nameof(TestClass.NullFloatVal))
                .Select(nameof(TestClass.NullGuidVal))
                .Select(nameof(TestClass.IntVal))
                .Select(nameof(TestClass.NullLongVal))
                .Select(nameof(TestClass.NullSByteVal))
                .Select(nameof(TestClass.NullShortVal))
                .Select(nameof(TestClass.NullSingleVal))
                .Select(nameof(TestClass.NullStringVal))
                .Select(nameof(TestClass.NullTimeSpanVal))
                .Select(nameof(TestClass.NullUInt16Val))
                .Select(nameof(TestClass.NullUInt32Val))
                .Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            retRecord = result[0];
            oneRecord = new T().GetDefault1Record();
            Assert.True(retRecord.NullBoolVal == oneRecord.NullBoolVal);
            Assert.True(retRecord.NullByteArrayVal == oneRecord.NullByteArrayVal);
            Assert.True(retRecord.NullByteVal == oneRecord.NullByteVal);
            Assert.True(retRecord.NullCharVal == oneRecord.NullCharVal);
            Assert.True(retRecord.NullDateTimeOffsetVal == oneRecord.NullDateTimeOffsetVal);
            Assert.True(retRecord.NullDateTimeVal == oneRecord.NullDateTimeVal);
            Assert.True(retRecord.NullDecimalVal == oneRecord.NullDecimalVal);
            Assert.True(retRecord.NullDoubleVal == oneRecord.NullDoubleVal);
            Assert.True(retRecord.NullFloatVal == oneRecord.NullFloatVal);
            Assert.True(retRecord.NullGuidVal == oneRecord.NullGuidVal);
            Assert.True(retRecord.NullIntVal == oneRecord.NullIntVal);
            Assert.True(retRecord.NullLongVal == oneRecord.NullLongVal);
            Assert.True(retRecord.NullSByteVal == oneRecord.NullSByteVal);
            Assert.True(retRecord.NullShortVal == oneRecord.NullShortVal);
            Assert.True(retRecord.NullSingleVal == oneRecord.NullSingleVal);
            Assert.True(retRecord.NullStringVal == oneRecord.NullStringVal);
            Assert.True(retRecord.NullTimeSpanVal == oneRecord.NullTimeSpanVal);
            Assert.True(retRecord.NullUInt16Val == oneRecord.NullUInt16Val);
            Assert.True(retRecord.NullUInt32Val == oneRecord.NullUInt32Val);
            Assert.True(retRecord.NullUInt64Val == oneRecord.NullUInt64Val);

            // 21 select
            serviceQuery = ServiceQueryBuilder.New()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .Select(nameof(TestClass.NullBoolVal))
                .Select(nameof(TestClass.NullByteArrayVal))
                .Select(nameof(TestClass.NullByteVal))
                .Select(nameof(TestClass.NullCharVal))
                .Select(nameof(TestClass.NullDateTimeOffsetVal))
                .Select(nameof(TestClass.NullDateTimeVal))
                .Select(nameof(TestClass.NullDecimalVal))
                .Select(nameof(TestClass.NullDoubleVal))
                .Select(nameof(TestClass.NullFloatVal))
                .Select(nameof(TestClass.NullGuidVal))
                .Select(nameof(TestClass.IntVal))
                .Select(nameof(TestClass.NullLongVal))
                .Select(nameof(TestClass.NullSByteVal))
                .Select(nameof(TestClass.NullShortVal))
                .Select(nameof(TestClass.NullSingleVal))
                .Select(nameof(TestClass.NullStringVal))
                .Select(nameof(TestClass.NullTimeSpanVal))
                .Select(nameof(TestClass.NullUInt16Val))
                .Select(nameof(TestClass.NullUInt32Val))
                .Select(nameof(TestClass.NullUInt64Val))
                .Build();
            testQueryable = serviceQuery.Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            retRecord = result[0];
            oneRecord = new T().GetDefault1Record();
            Assert.True(retRecord.NullBoolVal == oneRecord.NullBoolVal);
            Assert.True(retRecord.NullByteArrayVal == oneRecord.NullByteArrayVal);
            Assert.True(retRecord.NullByteVal == oneRecord.NullByteVal);
            Assert.True(retRecord.NullCharVal == oneRecord.NullCharVal);
            Assert.True(retRecord.NullDateTimeOffsetVal == oneRecord.NullDateTimeOffsetVal);
            Assert.True(retRecord.NullDateTimeVal == oneRecord.NullDateTimeVal);
            Assert.True(retRecord.NullDecimalVal == oneRecord.NullDecimalVal);
            Assert.True(retRecord.NullDoubleVal == oneRecord.NullDoubleVal);
            Assert.True(retRecord.NullFloatVal == oneRecord.NullFloatVal);
            Assert.True(retRecord.NullGuidVal == oneRecord.NullGuidVal);
            Assert.True(retRecord.NullIntVal == oneRecord.NullIntVal);
            Assert.True(retRecord.NullLongVal == oneRecord.NullLongVal);
            Assert.True(retRecord.NullSByteVal == oneRecord.NullSByteVal);
            Assert.True(retRecord.NullShortVal == oneRecord.NullShortVal);
            Assert.True(retRecord.NullSingleVal == oneRecord.NullSingleVal);
            Assert.True(retRecord.NullStringVal == oneRecord.NullStringVal);
            Assert.True(retRecord.NullTimeSpanVal == oneRecord.NullTimeSpanVal);
            Assert.True(retRecord.NullUInt16Val == oneRecord.NullUInt16Val);
            Assert.True(retRecord.NullUInt32Val == oneRecord.NullUInt32Val);
            Assert.True(retRecord.NullUInt64Val == oneRecord.NullUInt64Val);
        }

        [Fact]
        public async Task NullSelectRequestTests()
        {
            var sourceQueryable = GetTestList();
            IQueryable<T> testQueryable;
            IServiceQueryRequest serviceQuery;
            List<T> result;
            T retRecord;
            ITestClass oneRecord;

            // No select
            serviceQuery = ServiceQueryRequestBuilder.New()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            retRecord = result[0];
            oneRecord = new T().GetDefault1Record();
            Assert.True(retRecord.NullBoolVal == oneRecord.NullBoolVal);
            Assert.True(retRecord.NullByteArrayVal == oneRecord.NullByteArrayVal);
            Assert.True(retRecord.NullByteVal == oneRecord.NullByteVal);
            Assert.True(retRecord.NullCharVal == oneRecord.NullCharVal);
            Assert.True(retRecord.NullDateTimeOffsetVal == oneRecord.NullDateTimeOffsetVal);
            Assert.True(retRecord.NullDateTimeVal == oneRecord.NullDateTimeVal);
            Assert.True(retRecord.NullDecimalVal == oneRecord.NullDecimalVal);
            Assert.True(retRecord.NullDoubleVal == oneRecord.NullDoubleVal);
            Assert.True(retRecord.NullFloatVal == oneRecord.NullFloatVal);
            Assert.True(retRecord.NullGuidVal == oneRecord.NullGuidVal);
            Assert.True(retRecord.NullIntVal == oneRecord.NullIntVal);
            Assert.True(retRecord.NullLongVal == oneRecord.NullLongVal);
            Assert.True(retRecord.NullSByteVal == oneRecord.NullSByteVal);
            Assert.True(retRecord.NullShortVal == oneRecord.NullShortVal);
            Assert.True(retRecord.NullSingleVal == oneRecord.NullSingleVal);
            Assert.True(retRecord.NullStringVal == oneRecord.NullStringVal);
            Assert.True(retRecord.NullTimeSpanVal == oneRecord.NullTimeSpanVal);
            Assert.True(retRecord.NullUInt16Val == oneRecord.NullUInt16Val);
            Assert.True(retRecord.NullUInt32Val == oneRecord.NullUInt32Val);
            Assert.True(retRecord.NullUInt64Val == oneRecord.NullUInt64Val);

            // 1 select
            serviceQuery = ServiceQueryRequestBuilder.New()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .Select(nameof(TestClass.NullBoolVal))
                .Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            retRecord = result[0];
            oneRecord = new T().GetDefault1Record();
            Assert.True(retRecord.NullBoolVal == oneRecord.NullBoolVal);
            Assert.True(retRecord.NullByteArrayVal == oneRecord.NullByteArrayVal);
            Assert.True(retRecord.NullByteVal == oneRecord.NullByteVal);
            Assert.True(retRecord.NullCharVal == oneRecord.NullCharVal);
            Assert.True(retRecord.NullDateTimeOffsetVal == oneRecord.NullDateTimeOffsetVal);
            Assert.True(retRecord.NullDateTimeVal == oneRecord.NullDateTimeVal);
            Assert.True(retRecord.NullDecimalVal == oneRecord.NullDecimalVal);
            Assert.True(retRecord.NullDoubleVal == oneRecord.NullDoubleVal);
            Assert.True(retRecord.NullFloatVal == oneRecord.NullFloatVal);
            Assert.True(retRecord.NullGuidVal == oneRecord.NullGuidVal);
            Assert.True(retRecord.NullIntVal == oneRecord.NullIntVal);
            Assert.True(retRecord.NullLongVal == oneRecord.NullLongVal);
            Assert.True(retRecord.NullSByteVal == oneRecord.NullSByteVal);
            Assert.True(retRecord.NullShortVal == oneRecord.NullShortVal);
            Assert.True(retRecord.NullSingleVal == oneRecord.NullSingleVal);
            Assert.True(retRecord.NullStringVal == oneRecord.NullStringVal);
            Assert.True(retRecord.NullTimeSpanVal == oneRecord.NullTimeSpanVal);
            Assert.True(retRecord.NullUInt16Val == oneRecord.NullUInt16Val);
            Assert.True(retRecord.NullUInt32Val == oneRecord.NullUInt32Val);
            Assert.True(retRecord.NullUInt64Val == oneRecord.NullUInt64Val);

            // 2 select
            serviceQuery = ServiceQueryRequestBuilder.New()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .Select(nameof(TestClass.NullBoolVal))
                .Select(nameof(TestClass.NullByteArrayVal))
                .Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            retRecord = result[0];
            oneRecord = new T().GetDefault1Record();
            Assert.True(retRecord.NullBoolVal == oneRecord.NullBoolVal);
            Assert.True(retRecord.NullByteArrayVal == oneRecord.NullByteArrayVal);
            Assert.True(retRecord.NullByteVal == oneRecord.NullByteVal);
            Assert.True(retRecord.NullCharVal == oneRecord.NullCharVal);
            Assert.True(retRecord.NullDateTimeOffsetVal == oneRecord.NullDateTimeOffsetVal);
            Assert.True(retRecord.NullDateTimeVal == oneRecord.NullDateTimeVal);
            Assert.True(retRecord.NullDecimalVal == oneRecord.NullDecimalVal);
            Assert.True(retRecord.NullDoubleVal == oneRecord.NullDoubleVal);
            Assert.True(retRecord.NullFloatVal == oneRecord.NullFloatVal);
            Assert.True(retRecord.NullGuidVal == oneRecord.NullGuidVal);
            Assert.True(retRecord.NullIntVal == oneRecord.NullIntVal);
            Assert.True(retRecord.NullLongVal == oneRecord.NullLongVal);
            Assert.True(retRecord.NullSByteVal == oneRecord.NullSByteVal);
            Assert.True(retRecord.NullShortVal == oneRecord.NullShortVal);
            Assert.True(retRecord.NullSingleVal == oneRecord.NullSingleVal);
            Assert.True(retRecord.NullStringVal == oneRecord.NullStringVal);
            Assert.True(retRecord.NullTimeSpanVal == oneRecord.NullTimeSpanVal);
            Assert.True(retRecord.NullUInt16Val == oneRecord.NullUInt16Val);
            Assert.True(retRecord.NullUInt32Val == oneRecord.NullUInt32Val);
            Assert.True(retRecord.NullUInt64Val == oneRecord.NullUInt64Val);

            // 3 select
            serviceQuery = ServiceQueryRequestBuilder.New()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .Select(nameof(TestClass.NullBoolVal))
                .Select(nameof(TestClass.NullByteArrayVal))
                .Select(nameof(TestClass.NullByteVal))
                .Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            retRecord = result[0];
            oneRecord = new T().GetDefault1Record();
            Assert.True(retRecord.NullBoolVal == oneRecord.NullBoolVal);
            Assert.True(retRecord.NullByteArrayVal == oneRecord.NullByteArrayVal);
            Assert.True(retRecord.NullByteVal == oneRecord.NullByteVal);
            Assert.True(retRecord.NullCharVal == oneRecord.NullCharVal);
            Assert.True(retRecord.NullDateTimeOffsetVal == oneRecord.NullDateTimeOffsetVal);
            Assert.True(retRecord.NullDateTimeVal == oneRecord.NullDateTimeVal);
            Assert.True(retRecord.NullDecimalVal == oneRecord.NullDecimalVal);
            Assert.True(retRecord.NullDoubleVal == oneRecord.NullDoubleVal);
            Assert.True(retRecord.NullFloatVal == oneRecord.NullFloatVal);
            Assert.True(retRecord.NullGuidVal == oneRecord.NullGuidVal);
            Assert.True(retRecord.NullIntVal == oneRecord.NullIntVal);
            Assert.True(retRecord.NullLongVal == oneRecord.NullLongVal);
            Assert.True(retRecord.NullSByteVal == oneRecord.NullSByteVal);
            Assert.True(retRecord.NullShortVal == oneRecord.NullShortVal);
            Assert.True(retRecord.NullSingleVal == oneRecord.NullSingleVal);
            Assert.True(retRecord.NullStringVal == oneRecord.NullStringVal);
            Assert.True(retRecord.NullTimeSpanVal == oneRecord.NullTimeSpanVal);
            Assert.True(retRecord.NullUInt16Val == oneRecord.NullUInt16Val);
            Assert.True(retRecord.NullUInt32Val == oneRecord.NullUInt32Val);
            Assert.True(retRecord.NullUInt64Val == oneRecord.NullUInt64Val);

            // 4 select
            serviceQuery = ServiceQueryRequestBuilder.New()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .Select(nameof(TestClass.NullBoolVal))
                .Select(nameof(TestClass.NullByteArrayVal))
                .Select(nameof(TestClass.NullByteVal))
                .Select(nameof(TestClass.NullCharVal))
                .Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            retRecord = result[0];
            oneRecord = new T().GetDefault1Record();
            Assert.True(retRecord.NullBoolVal == oneRecord.NullBoolVal);
            Assert.True(retRecord.NullByteArrayVal == oneRecord.NullByteArrayVal);
            Assert.True(retRecord.NullByteVal == oneRecord.NullByteVal);
            Assert.True(retRecord.NullCharVal == oneRecord.NullCharVal);
            Assert.True(retRecord.NullDateTimeOffsetVal == oneRecord.NullDateTimeOffsetVal);
            Assert.True(retRecord.NullDateTimeVal == oneRecord.NullDateTimeVal);
            Assert.True(retRecord.NullDecimalVal == oneRecord.NullDecimalVal);
            Assert.True(retRecord.NullDoubleVal == oneRecord.NullDoubleVal);
            Assert.True(retRecord.NullFloatVal == oneRecord.NullFloatVal);
            Assert.True(retRecord.NullGuidVal == oneRecord.NullGuidVal);
            Assert.True(retRecord.NullIntVal == oneRecord.NullIntVal);
            Assert.True(retRecord.NullLongVal == oneRecord.NullLongVal);
            Assert.True(retRecord.NullSByteVal == oneRecord.NullSByteVal);
            Assert.True(retRecord.NullShortVal == oneRecord.NullShortVal);
            Assert.True(retRecord.NullSingleVal == oneRecord.NullSingleVal);
            Assert.True(retRecord.NullStringVal == oneRecord.NullStringVal);
            Assert.True(retRecord.NullTimeSpanVal == oneRecord.NullTimeSpanVal);
            Assert.True(retRecord.NullUInt16Val == oneRecord.NullUInt16Val);
            Assert.True(retRecord.NullUInt32Val == oneRecord.NullUInt32Val);
            Assert.True(retRecord.NullUInt64Val == oneRecord.NullUInt64Val);

            // 5 select
            serviceQuery = ServiceQueryRequestBuilder.New()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .Select(nameof(TestClass.NullBoolVal))
                .Select(nameof(TestClass.NullByteArrayVal))
                .Select(nameof(TestClass.NullByteVal))
                .Select(nameof(TestClass.NullCharVal))
                .Select(nameof(TestClass.NullDateTimeOffsetVal))
                .Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            retRecord = result[0];
            oneRecord = new T().GetDefault1Record();
            Assert.True(retRecord.NullBoolVal == oneRecord.NullBoolVal);
            Assert.True(retRecord.NullByteArrayVal == oneRecord.NullByteArrayVal);
            Assert.True(retRecord.NullByteVal == oneRecord.NullByteVal);
            Assert.True(retRecord.NullCharVal == oneRecord.NullCharVal);
            Assert.True(retRecord.NullDateTimeOffsetVal == oneRecord.NullDateTimeOffsetVal);
            Assert.True(retRecord.NullDateTimeVal == oneRecord.NullDateTimeVal);
            Assert.True(retRecord.NullDecimalVal == oneRecord.NullDecimalVal);
            Assert.True(retRecord.NullDoubleVal == oneRecord.NullDoubleVal);
            Assert.True(retRecord.NullFloatVal == oneRecord.NullFloatVal);
            Assert.True(retRecord.NullGuidVal == oneRecord.NullGuidVal);
            Assert.True(retRecord.NullIntVal == oneRecord.NullIntVal);
            Assert.True(retRecord.NullLongVal == oneRecord.NullLongVal);
            Assert.True(retRecord.NullSByteVal == oneRecord.NullSByteVal);
            Assert.True(retRecord.NullShortVal == oneRecord.NullShortVal);
            Assert.True(retRecord.NullSingleVal == oneRecord.NullSingleVal);
            Assert.True(retRecord.NullStringVal == oneRecord.NullStringVal);
            Assert.True(retRecord.NullTimeSpanVal == oneRecord.NullTimeSpanVal);
            Assert.True(retRecord.NullUInt16Val == oneRecord.NullUInt16Val);
            Assert.True(retRecord.NullUInt32Val == oneRecord.NullUInt32Val);
            Assert.True(retRecord.NullUInt64Val == oneRecord.NullUInt64Val);

            // 6 select
            serviceQuery = ServiceQueryRequestBuilder.New()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .Select(nameof(TestClass.NullBoolVal))
                .Select(nameof(TestClass.NullByteArrayVal))
                .Select(nameof(TestClass.NullByteVal))
                .Select(nameof(TestClass.NullCharVal))
                .Select(nameof(TestClass.NullDateTimeOffsetVal))
                .Select(nameof(TestClass.NullDateTimeVal))
                .Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            retRecord = result[0];
            oneRecord = new T().GetDefault1Record();
            Assert.True(retRecord.NullBoolVal == oneRecord.NullBoolVal);
            Assert.True(retRecord.NullByteArrayVal == oneRecord.NullByteArrayVal);
            Assert.True(retRecord.NullByteVal == oneRecord.NullByteVal);
            Assert.True(retRecord.NullCharVal == oneRecord.NullCharVal);
            Assert.True(retRecord.NullDateTimeOffsetVal == oneRecord.NullDateTimeOffsetVal);
            Assert.True(retRecord.NullDateTimeVal == oneRecord.NullDateTimeVal);
            Assert.True(retRecord.NullDecimalVal == oneRecord.NullDecimalVal);
            Assert.True(retRecord.NullDoubleVal == oneRecord.NullDoubleVal);
            Assert.True(retRecord.NullFloatVal == oneRecord.NullFloatVal);
            Assert.True(retRecord.NullGuidVal == oneRecord.NullGuidVal);
            Assert.True(retRecord.NullIntVal == oneRecord.NullIntVal);
            Assert.True(retRecord.NullLongVal == oneRecord.NullLongVal);
            Assert.True(retRecord.NullSByteVal == oneRecord.NullSByteVal);
            Assert.True(retRecord.NullShortVal == oneRecord.NullShortVal);
            Assert.True(retRecord.NullSingleVal == oneRecord.NullSingleVal);
            Assert.True(retRecord.NullStringVal == oneRecord.NullStringVal);
            Assert.True(retRecord.NullTimeSpanVal == oneRecord.NullTimeSpanVal);
            Assert.True(retRecord.NullUInt16Val == oneRecord.NullUInt16Val);
            Assert.True(retRecord.NullUInt32Val == oneRecord.NullUInt32Val);
            Assert.True(retRecord.NullUInt64Val == oneRecord.NullUInt64Val);

            // 7 select
            serviceQuery = ServiceQueryRequestBuilder.New()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .Select(nameof(TestClass.NullBoolVal))
                .Select(nameof(TestClass.NullByteArrayVal))
                .Select(nameof(TestClass.NullByteVal))
                .Select(nameof(TestClass.NullCharVal))
                .Select(nameof(TestClass.NullDateTimeOffsetVal))
                .Select(nameof(TestClass.NullDateTimeVal))
                .Select(nameof(TestClass.NullDecimalVal))
                .Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            retRecord = result[0];
            oneRecord = new T().GetDefault1Record();
            Assert.True(retRecord.NullBoolVal == oneRecord.NullBoolVal);
            Assert.True(retRecord.NullByteArrayVal == oneRecord.NullByteArrayVal);
            Assert.True(retRecord.NullByteVal == oneRecord.NullByteVal);
            Assert.True(retRecord.NullCharVal == oneRecord.NullCharVal);
            Assert.True(retRecord.NullDateTimeOffsetVal == oneRecord.NullDateTimeOffsetVal);
            Assert.True(retRecord.NullDateTimeVal == oneRecord.NullDateTimeVal);
            Assert.True(retRecord.NullDecimalVal == oneRecord.NullDecimalVal);
            Assert.True(retRecord.NullDoubleVal == oneRecord.NullDoubleVal);
            Assert.True(retRecord.NullFloatVal == oneRecord.NullFloatVal);
            Assert.True(retRecord.NullGuidVal == oneRecord.NullGuidVal);
            Assert.True(retRecord.NullIntVal == oneRecord.NullIntVal);
            Assert.True(retRecord.NullLongVal == oneRecord.NullLongVal);
            Assert.True(retRecord.NullSByteVal == oneRecord.NullSByteVal);
            Assert.True(retRecord.NullShortVal == oneRecord.NullShortVal);
            Assert.True(retRecord.NullSingleVal == oneRecord.NullSingleVal);
            Assert.True(retRecord.NullStringVal == oneRecord.NullStringVal);
            Assert.True(retRecord.NullTimeSpanVal == oneRecord.NullTimeSpanVal);
            Assert.True(retRecord.NullUInt16Val == oneRecord.NullUInt16Val);
            Assert.True(retRecord.NullUInt32Val == oneRecord.NullUInt32Val);
            Assert.True(retRecord.NullUInt64Val == oneRecord.NullUInt64Val);

            // 8 select
            serviceQuery = ServiceQueryRequestBuilder.New()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .Select(nameof(TestClass.NullBoolVal))
                .Select(nameof(TestClass.NullByteArrayVal))
                .Select(nameof(TestClass.NullByteVal))
                .Select(nameof(TestClass.NullCharVal))
                .Select(nameof(TestClass.NullDateTimeOffsetVal))
                .Select(nameof(TestClass.NullDateTimeVal))
                .Select(nameof(TestClass.NullDecimalVal))
                .Select(nameof(TestClass.NullDoubleVal))
                .Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            retRecord = result[0];
            oneRecord = new T().GetDefault1Record();
            Assert.True(retRecord.NullBoolVal == oneRecord.NullBoolVal);
            Assert.True(retRecord.NullByteArrayVal == oneRecord.NullByteArrayVal);
            Assert.True(retRecord.NullByteVal == oneRecord.NullByteVal);
            Assert.True(retRecord.NullCharVal == oneRecord.NullCharVal);
            Assert.True(retRecord.NullDateTimeOffsetVal == oneRecord.NullDateTimeOffsetVal);
            Assert.True(retRecord.NullDateTimeVal == oneRecord.NullDateTimeVal);
            Assert.True(retRecord.NullDecimalVal == oneRecord.NullDecimalVal);
            Assert.True(retRecord.NullDoubleVal == oneRecord.NullDoubleVal);
            Assert.True(retRecord.NullFloatVal == oneRecord.NullFloatVal);
            Assert.True(retRecord.NullGuidVal == oneRecord.NullGuidVal);
            Assert.True(retRecord.NullIntVal == oneRecord.NullIntVal);
            Assert.True(retRecord.NullLongVal == oneRecord.NullLongVal);
            Assert.True(retRecord.NullSByteVal == oneRecord.NullSByteVal);
            Assert.True(retRecord.NullShortVal == oneRecord.NullShortVal);
            Assert.True(retRecord.NullSingleVal == oneRecord.NullSingleVal);
            Assert.True(retRecord.NullStringVal == oneRecord.NullStringVal);
            Assert.True(retRecord.NullTimeSpanVal == oneRecord.NullTimeSpanVal);
            Assert.True(retRecord.NullUInt16Val == oneRecord.NullUInt16Val);
            Assert.True(retRecord.NullUInt32Val == oneRecord.NullUInt32Val);
            Assert.True(retRecord.NullUInt64Val == oneRecord.NullUInt64Val);

            // 9 select
            serviceQuery = ServiceQueryRequestBuilder.New()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .Select(nameof(TestClass.NullBoolVal))
                .Select(nameof(TestClass.NullByteArrayVal))
                .Select(nameof(TestClass.NullByteVal))
                .Select(nameof(TestClass.NullCharVal))
                .Select(nameof(TestClass.NullDateTimeOffsetVal))
                .Select(nameof(TestClass.NullDateTimeVal))
                .Select(nameof(TestClass.NullDecimalVal))
                .Select(nameof(TestClass.NullDoubleVal))
                .Select(nameof(TestClass.NullFloatVal))
                .Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            retRecord = result[0];
            oneRecord = new T().GetDefault1Record();
            Assert.True(retRecord.NullBoolVal == oneRecord.NullBoolVal);
            Assert.True(retRecord.NullByteArrayVal == oneRecord.NullByteArrayVal);
            Assert.True(retRecord.NullByteVal == oneRecord.NullByteVal);
            Assert.True(retRecord.NullCharVal == oneRecord.NullCharVal);
            Assert.True(retRecord.NullDateTimeOffsetVal == oneRecord.NullDateTimeOffsetVal);
            Assert.True(retRecord.NullDateTimeVal == oneRecord.NullDateTimeVal);
            Assert.True(retRecord.NullDecimalVal == oneRecord.NullDecimalVal);
            Assert.True(retRecord.NullDoubleVal == oneRecord.NullDoubleVal);
            Assert.True(retRecord.NullFloatVal == oneRecord.NullFloatVal);
            Assert.True(retRecord.NullGuidVal == oneRecord.NullGuidVal);
            Assert.True(retRecord.NullIntVal == oneRecord.NullIntVal);
            Assert.True(retRecord.NullLongVal == oneRecord.NullLongVal);
            Assert.True(retRecord.NullSByteVal == oneRecord.NullSByteVal);
            Assert.True(retRecord.NullShortVal == oneRecord.NullShortVal);
            Assert.True(retRecord.NullSingleVal == oneRecord.NullSingleVal);
            Assert.True(retRecord.NullStringVal == oneRecord.NullStringVal);
            Assert.True(retRecord.NullTimeSpanVal == oneRecord.NullTimeSpanVal);
            Assert.True(retRecord.NullUInt16Val == oneRecord.NullUInt16Val);
            Assert.True(retRecord.NullUInt32Val == oneRecord.NullUInt32Val);
            Assert.True(retRecord.NullUInt64Val == oneRecord.NullUInt64Val);

            // 10 select
            serviceQuery = ServiceQueryRequestBuilder.New()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .Select(nameof(TestClass.NullBoolVal))
                .Select(nameof(TestClass.NullByteArrayVal))
                .Select(nameof(TestClass.NullByteVal))
                .Select(nameof(TestClass.NullCharVal))
                .Select(nameof(TestClass.NullDateTimeOffsetVal))
                .Select(nameof(TestClass.NullDateTimeVal))
                .Select(nameof(TestClass.NullDecimalVal))
                .Select(nameof(TestClass.NullDoubleVal))
                .Select(nameof(TestClass.NullFloatVal))
                .Select(nameof(TestClass.NullGuidVal))
                .Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            retRecord = result[0];
            oneRecord = new T().GetDefault1Record();
            Assert.True(retRecord.NullBoolVal == oneRecord.NullBoolVal);
            Assert.True(retRecord.NullByteArrayVal == oneRecord.NullByteArrayVal);
            Assert.True(retRecord.NullByteVal == oneRecord.NullByteVal);
            Assert.True(retRecord.NullCharVal == oneRecord.NullCharVal);
            Assert.True(retRecord.NullDateTimeOffsetVal == oneRecord.NullDateTimeOffsetVal);
            Assert.True(retRecord.NullDateTimeVal == oneRecord.NullDateTimeVal);
            Assert.True(retRecord.NullDecimalVal == oneRecord.NullDecimalVal);
            Assert.True(retRecord.NullDoubleVal == oneRecord.NullDoubleVal);
            Assert.True(retRecord.NullFloatVal == oneRecord.NullFloatVal);
            Assert.True(retRecord.NullGuidVal == oneRecord.NullGuidVal);
            Assert.True(retRecord.NullIntVal == oneRecord.NullIntVal);
            Assert.True(retRecord.NullLongVal == oneRecord.NullLongVal);
            Assert.True(retRecord.NullSByteVal == oneRecord.NullSByteVal);
            Assert.True(retRecord.NullShortVal == oneRecord.NullShortVal);
            Assert.True(retRecord.NullSingleVal == oneRecord.NullSingleVal);
            Assert.True(retRecord.NullStringVal == oneRecord.NullStringVal);
            Assert.True(retRecord.NullTimeSpanVal == oneRecord.NullTimeSpanVal);
            Assert.True(retRecord.NullUInt16Val == oneRecord.NullUInt16Val);
            Assert.True(retRecord.NullUInt32Val == oneRecord.NullUInt32Val);
            Assert.True(retRecord.NullUInt64Val == oneRecord.NullUInt64Val);

            // 11 select
            serviceQuery = ServiceQueryRequestBuilder.New()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .Select(nameof(TestClass.NullBoolVal))
                .Select(nameof(TestClass.NullByteArrayVal))
                .Select(nameof(TestClass.NullByteVal))
                .Select(nameof(TestClass.NullCharVal))
                .Select(nameof(TestClass.NullDateTimeOffsetVal))
                .Select(nameof(TestClass.NullDateTimeVal))
                .Select(nameof(TestClass.NullDecimalVal))
                .Select(nameof(TestClass.NullDoubleVal))
                .Select(nameof(TestClass.NullFloatVal))
                .Select(nameof(TestClass.NullGuidVal))
                .Select(nameof(TestClass.IntVal))
                .Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            retRecord = result[0];
            oneRecord = new T().GetDefault1Record();
            Assert.True(retRecord.NullBoolVal == oneRecord.NullBoolVal);
            Assert.True(retRecord.NullByteArrayVal == oneRecord.NullByteArrayVal);
            Assert.True(retRecord.NullByteVal == oneRecord.NullByteVal);
            Assert.True(retRecord.NullCharVal == oneRecord.NullCharVal);
            Assert.True(retRecord.NullDateTimeOffsetVal == oneRecord.NullDateTimeOffsetVal);
            Assert.True(retRecord.NullDateTimeVal == oneRecord.NullDateTimeVal);
            Assert.True(retRecord.NullDecimalVal == oneRecord.NullDecimalVal);
            Assert.True(retRecord.NullDoubleVal == oneRecord.NullDoubleVal);
            Assert.True(retRecord.NullFloatVal == oneRecord.NullFloatVal);
            Assert.True(retRecord.NullGuidVal == oneRecord.NullGuidVal);
            Assert.True(retRecord.NullIntVal == oneRecord.NullIntVal);
            Assert.True(retRecord.NullLongVal == oneRecord.NullLongVal);
            Assert.True(retRecord.NullSByteVal == oneRecord.NullSByteVal);
            Assert.True(retRecord.NullShortVal == oneRecord.NullShortVal);
            Assert.True(retRecord.NullSingleVal == oneRecord.NullSingleVal);
            Assert.True(retRecord.NullStringVal == oneRecord.NullStringVal);
            Assert.True(retRecord.NullTimeSpanVal == oneRecord.NullTimeSpanVal);
            Assert.True(retRecord.NullUInt16Val == oneRecord.NullUInt16Val);
            Assert.True(retRecord.NullUInt32Val == oneRecord.NullUInt32Val);
            Assert.True(retRecord.NullUInt64Val == oneRecord.NullUInt64Val);

            // 12 select
            serviceQuery = ServiceQueryRequestBuilder.New()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .Select(nameof(TestClass.NullBoolVal))
                .Select(nameof(TestClass.NullByteArrayVal))
                .Select(nameof(TestClass.NullByteVal))
                .Select(nameof(TestClass.NullCharVal))
                .Select(nameof(TestClass.NullDateTimeOffsetVal))
                .Select(nameof(TestClass.NullDateTimeVal))
                .Select(nameof(TestClass.NullDecimalVal))
                .Select(nameof(TestClass.NullDoubleVal))
                .Select(nameof(TestClass.NullFloatVal))
                .Select(nameof(TestClass.NullGuidVal))
                .Select(nameof(TestClass.IntVal))
                .Select(nameof(TestClass.NullLongVal))
                .Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            retRecord = result[0];
            oneRecord = new T().GetDefault1Record();
            Assert.True(retRecord.NullBoolVal == oneRecord.NullBoolVal);
            Assert.True(retRecord.NullByteArrayVal == oneRecord.NullByteArrayVal);
            Assert.True(retRecord.NullByteVal == oneRecord.NullByteVal);
            Assert.True(retRecord.NullCharVal == oneRecord.NullCharVal);
            Assert.True(retRecord.NullDateTimeOffsetVal == oneRecord.NullDateTimeOffsetVal);
            Assert.True(retRecord.NullDateTimeVal == oneRecord.NullDateTimeVal);
            Assert.True(retRecord.NullDecimalVal == oneRecord.NullDecimalVal);
            Assert.True(retRecord.NullDoubleVal == oneRecord.NullDoubleVal);
            Assert.True(retRecord.NullFloatVal == oneRecord.NullFloatVal);
            Assert.True(retRecord.NullGuidVal == oneRecord.NullGuidVal);
            Assert.True(retRecord.NullIntVal == oneRecord.NullIntVal);
            Assert.True(retRecord.NullLongVal == oneRecord.NullLongVal);
            Assert.True(retRecord.NullSByteVal == oneRecord.NullSByteVal);
            Assert.True(retRecord.NullShortVal == oneRecord.NullShortVal);
            Assert.True(retRecord.NullSingleVal == oneRecord.NullSingleVal);
            Assert.True(retRecord.NullStringVal == oneRecord.NullStringVal);
            Assert.True(retRecord.NullTimeSpanVal == oneRecord.NullTimeSpanVal);
            Assert.True(retRecord.NullUInt16Val == oneRecord.NullUInt16Val);
            Assert.True(retRecord.NullUInt32Val == oneRecord.NullUInt32Val);
            Assert.True(retRecord.NullUInt64Val == oneRecord.NullUInt64Val);

            // 13 select
            serviceQuery = ServiceQueryRequestBuilder.New()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .Select(nameof(TestClass.NullBoolVal))
                .Select(nameof(TestClass.NullByteArrayVal))
                .Select(nameof(TestClass.NullByteVal))
                .Select(nameof(TestClass.NullCharVal))
                .Select(nameof(TestClass.NullDateTimeOffsetVal))
                .Select(nameof(TestClass.NullDateTimeVal))
                .Select(nameof(TestClass.NullDecimalVal))
                .Select(nameof(TestClass.NullDoubleVal))
                .Select(nameof(TestClass.NullFloatVal))
                .Select(nameof(TestClass.NullGuidVal))
                .Select(nameof(TestClass.IntVal))
                .Select(nameof(TestClass.NullLongVal))
                .Select(nameof(TestClass.NullSByteVal))
                .Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            retRecord = result[0];
            oneRecord = new T().GetDefault1Record();
            Assert.True(retRecord.NullBoolVal == oneRecord.NullBoolVal);
            Assert.True(retRecord.NullByteArrayVal == oneRecord.NullByteArrayVal);
            Assert.True(retRecord.NullByteVal == oneRecord.NullByteVal);
            Assert.True(retRecord.NullCharVal == oneRecord.NullCharVal);
            Assert.True(retRecord.NullDateTimeOffsetVal == oneRecord.NullDateTimeOffsetVal);
            Assert.True(retRecord.NullDateTimeVal == oneRecord.NullDateTimeVal);
            Assert.True(retRecord.NullDecimalVal == oneRecord.NullDecimalVal);
            Assert.True(retRecord.NullDoubleVal == oneRecord.NullDoubleVal);
            Assert.True(retRecord.NullFloatVal == oneRecord.NullFloatVal);
            Assert.True(retRecord.NullGuidVal == oneRecord.NullGuidVal);
            Assert.True(retRecord.NullIntVal == oneRecord.NullIntVal);
            Assert.True(retRecord.NullLongVal == oneRecord.NullLongVal);
            Assert.True(retRecord.NullSByteVal == oneRecord.NullSByteVal);
            Assert.True(retRecord.NullShortVal == oneRecord.NullShortVal);
            Assert.True(retRecord.NullSingleVal == oneRecord.NullSingleVal);
            Assert.True(retRecord.NullStringVal == oneRecord.NullStringVal);
            Assert.True(retRecord.NullTimeSpanVal == oneRecord.NullTimeSpanVal);
            Assert.True(retRecord.NullUInt16Val == oneRecord.NullUInt16Val);
            Assert.True(retRecord.NullUInt32Val == oneRecord.NullUInt32Val);
            Assert.True(retRecord.NullUInt64Val == oneRecord.NullUInt64Val);

            // 14 select
            serviceQuery = ServiceQueryRequestBuilder.New()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .Select(nameof(TestClass.NullBoolVal))
                .Select(nameof(TestClass.NullByteArrayVal))
                .Select(nameof(TestClass.NullByteVal))
                .Select(nameof(TestClass.NullCharVal))
                .Select(nameof(TestClass.NullDateTimeOffsetVal))
                .Select(nameof(TestClass.NullDateTimeVal))
                .Select(nameof(TestClass.NullDecimalVal))
                .Select(nameof(TestClass.NullDoubleVal))
                .Select(nameof(TestClass.NullFloatVal))
                .Select(nameof(TestClass.NullGuidVal))
                .Select(nameof(TestClass.IntVal))
                .Select(nameof(TestClass.NullLongVal))
                .Select(nameof(TestClass.NullSByteVal))
                .Select(nameof(TestClass.NullShortVal))
                .Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            retRecord = result[0];
            oneRecord = new T().GetDefault1Record();
            Assert.True(retRecord.NullBoolVal == oneRecord.NullBoolVal);
            Assert.True(retRecord.NullByteArrayVal == oneRecord.NullByteArrayVal);
            Assert.True(retRecord.NullByteVal == oneRecord.NullByteVal);
            Assert.True(retRecord.NullCharVal == oneRecord.NullCharVal);
            Assert.True(retRecord.NullDateTimeOffsetVal == oneRecord.NullDateTimeOffsetVal);
            Assert.True(retRecord.NullDateTimeVal == oneRecord.NullDateTimeVal);
            Assert.True(retRecord.NullDecimalVal == oneRecord.NullDecimalVal);
            Assert.True(retRecord.NullDoubleVal == oneRecord.NullDoubleVal);
            Assert.True(retRecord.NullFloatVal == oneRecord.NullFloatVal);
            Assert.True(retRecord.NullGuidVal == oneRecord.NullGuidVal);
            Assert.True(retRecord.NullIntVal == oneRecord.NullIntVal);
            Assert.True(retRecord.NullLongVal == oneRecord.NullLongVal);
            Assert.True(retRecord.NullSByteVal == oneRecord.NullSByteVal);
            Assert.True(retRecord.NullShortVal == oneRecord.NullShortVal);
            Assert.True(retRecord.NullSingleVal == oneRecord.NullSingleVal);
            Assert.True(retRecord.NullStringVal == oneRecord.NullStringVal);
            Assert.True(retRecord.NullTimeSpanVal == oneRecord.NullTimeSpanVal);
            Assert.True(retRecord.NullUInt16Val == oneRecord.NullUInt16Val);
            Assert.True(retRecord.NullUInt32Val == oneRecord.NullUInt32Val);
            Assert.True(retRecord.NullUInt64Val == oneRecord.NullUInt64Val);

            // 15 select
            serviceQuery = ServiceQueryRequestBuilder.New()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .Select(nameof(TestClass.NullBoolVal))
                .Select(nameof(TestClass.NullByteArrayVal))
                .Select(nameof(TestClass.NullByteVal))
                .Select(nameof(TestClass.NullCharVal))
                .Select(nameof(TestClass.NullDateTimeOffsetVal))
                .Select(nameof(TestClass.NullDateTimeVal))
                .Select(nameof(TestClass.NullDecimalVal))
                .Select(nameof(TestClass.NullDoubleVal))
                .Select(nameof(TestClass.NullFloatVal))
                .Select(nameof(TestClass.NullGuidVal))
                .Select(nameof(TestClass.IntVal))
                .Select(nameof(TestClass.NullLongVal))
                .Select(nameof(TestClass.NullSByteVal))
                .Select(nameof(TestClass.NullShortVal))
                .Select(nameof(TestClass.NullSingleVal))
                .Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            retRecord = result[0];
            oneRecord = new T().GetDefault1Record();
            Assert.True(retRecord.NullBoolVal == oneRecord.NullBoolVal);
            Assert.True(retRecord.NullByteArrayVal == oneRecord.NullByteArrayVal);
            Assert.True(retRecord.NullByteVal == oneRecord.NullByteVal);
            Assert.True(retRecord.NullCharVal == oneRecord.NullCharVal);
            Assert.True(retRecord.NullDateTimeOffsetVal == oneRecord.NullDateTimeOffsetVal);
            Assert.True(retRecord.NullDateTimeVal == oneRecord.NullDateTimeVal);
            Assert.True(retRecord.NullDecimalVal == oneRecord.NullDecimalVal);
            Assert.True(retRecord.NullDoubleVal == oneRecord.NullDoubleVal);
            Assert.True(retRecord.NullFloatVal == oneRecord.NullFloatVal);
            Assert.True(retRecord.NullGuidVal == oneRecord.NullGuidVal);
            Assert.True(retRecord.NullIntVal == oneRecord.NullIntVal);
            Assert.True(retRecord.NullLongVal == oneRecord.NullLongVal);
            Assert.True(retRecord.NullSByteVal == oneRecord.NullSByteVal);
            Assert.True(retRecord.NullShortVal == oneRecord.NullShortVal);
            Assert.True(retRecord.NullSingleVal == oneRecord.NullSingleVal);
            Assert.True(retRecord.NullStringVal == oneRecord.NullStringVal);
            Assert.True(retRecord.NullTimeSpanVal == oneRecord.NullTimeSpanVal);
            Assert.True(retRecord.NullUInt16Val == oneRecord.NullUInt16Val);
            Assert.True(retRecord.NullUInt32Val == oneRecord.NullUInt32Val);
            Assert.True(retRecord.NullUInt64Val == oneRecord.NullUInt64Val);

            // 16 select
            serviceQuery = ServiceQueryRequestBuilder.New()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .Select(nameof(TestClass.NullBoolVal))
                .Select(nameof(TestClass.NullByteArrayVal))
                .Select(nameof(TestClass.NullByteVal))
                .Select(nameof(TestClass.NullCharVal))
                .Select(nameof(TestClass.NullDateTimeOffsetVal))
                .Select(nameof(TestClass.NullDateTimeVal))
                .Select(nameof(TestClass.NullDecimalVal))
                .Select(nameof(TestClass.NullDoubleVal))
                .Select(nameof(TestClass.NullFloatVal))
                .Select(nameof(TestClass.NullGuidVal))
                .Select(nameof(TestClass.IntVal))
                .Select(nameof(TestClass.NullLongVal))
                .Select(nameof(TestClass.NullSByteVal))
                .Select(nameof(TestClass.NullShortVal))
                .Select(nameof(TestClass.NullSingleVal))
                .Select(nameof(TestClass.NullStringVal))
                .Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            retRecord = result[0];
            oneRecord = new T().GetDefault1Record();
            Assert.True(retRecord.NullBoolVal == oneRecord.NullBoolVal);
            Assert.True(retRecord.NullByteArrayVal == oneRecord.NullByteArrayVal);
            Assert.True(retRecord.NullByteVal == oneRecord.NullByteVal);
            Assert.True(retRecord.NullCharVal == oneRecord.NullCharVal);
            Assert.True(retRecord.NullDateTimeOffsetVal == oneRecord.NullDateTimeOffsetVal);
            Assert.True(retRecord.NullDateTimeVal == oneRecord.NullDateTimeVal);
            Assert.True(retRecord.NullDecimalVal == oneRecord.NullDecimalVal);
            Assert.True(retRecord.NullDoubleVal == oneRecord.NullDoubleVal);
            Assert.True(retRecord.NullFloatVal == oneRecord.NullFloatVal);
            Assert.True(retRecord.NullGuidVal == oneRecord.NullGuidVal);
            Assert.True(retRecord.NullIntVal == oneRecord.NullIntVal);
            Assert.True(retRecord.NullLongVal == oneRecord.NullLongVal);
            Assert.True(retRecord.NullSByteVal == oneRecord.NullSByteVal);
            Assert.True(retRecord.NullShortVal == oneRecord.NullShortVal);
            Assert.True(retRecord.NullSingleVal == oneRecord.NullSingleVal);
            Assert.True(retRecord.NullStringVal == oneRecord.NullStringVal);
            Assert.True(retRecord.NullTimeSpanVal == oneRecord.NullTimeSpanVal);
            Assert.True(retRecord.NullUInt16Val == oneRecord.NullUInt16Val);
            Assert.True(retRecord.NullUInt32Val == oneRecord.NullUInt32Val);
            Assert.True(retRecord.NullUInt64Val == oneRecord.NullUInt64Val);

            // 17 select
            serviceQuery = ServiceQueryRequestBuilder.New()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .Select(nameof(TestClass.NullBoolVal))
                .Select(nameof(TestClass.NullByteArrayVal))
                .Select(nameof(TestClass.NullByteVal))
                .Select(nameof(TestClass.NullCharVal))
                .Select(nameof(TestClass.NullDateTimeOffsetVal))
                .Select(nameof(TestClass.NullDateTimeVal))
                .Select(nameof(TestClass.NullDecimalVal))
                .Select(nameof(TestClass.NullDoubleVal))
                .Select(nameof(TestClass.NullFloatVal))
                .Select(nameof(TestClass.NullGuidVal))
                .Select(nameof(TestClass.IntVal))
                .Select(nameof(TestClass.NullLongVal))
                .Select(nameof(TestClass.NullSByteVal))
                .Select(nameof(TestClass.NullShortVal))
                .Select(nameof(TestClass.NullSingleVal))
                .Select(nameof(TestClass.NullStringVal))
                .Select(nameof(TestClass.NullTimeSpanVal))
                .Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            retRecord = result[0];
            oneRecord = new T().GetDefault1Record();
            Assert.True(retRecord.NullBoolVal == oneRecord.NullBoolVal);
            Assert.True(retRecord.NullByteArrayVal == oneRecord.NullByteArrayVal);
            Assert.True(retRecord.NullByteVal == oneRecord.NullByteVal);
            Assert.True(retRecord.NullCharVal == oneRecord.NullCharVal);
            Assert.True(retRecord.NullDateTimeOffsetVal == oneRecord.NullDateTimeOffsetVal);
            Assert.True(retRecord.NullDateTimeVal == oneRecord.NullDateTimeVal);
            Assert.True(retRecord.NullDecimalVal == oneRecord.NullDecimalVal);
            Assert.True(retRecord.NullDoubleVal == oneRecord.NullDoubleVal);
            Assert.True(retRecord.NullFloatVal == oneRecord.NullFloatVal);
            Assert.True(retRecord.NullGuidVal == oneRecord.NullGuidVal);
            Assert.True(retRecord.NullIntVal == oneRecord.NullIntVal);
            Assert.True(retRecord.NullLongVal == oneRecord.NullLongVal);
            Assert.True(retRecord.NullSByteVal == oneRecord.NullSByteVal);
            Assert.True(retRecord.NullShortVal == oneRecord.NullShortVal);
            Assert.True(retRecord.NullSingleVal == oneRecord.NullSingleVal);
            Assert.True(retRecord.NullStringVal == oneRecord.NullStringVal);
            Assert.True(retRecord.NullTimeSpanVal == oneRecord.NullTimeSpanVal);
            Assert.True(retRecord.NullUInt16Val == oneRecord.NullUInt16Val);
            Assert.True(retRecord.NullUInt32Val == oneRecord.NullUInt32Val);
            Assert.True(retRecord.NullUInt64Val == oneRecord.NullUInt64Val);

            // 18 select
            serviceQuery = ServiceQueryRequestBuilder.New()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .Select(nameof(TestClass.NullBoolVal))
                .Select(nameof(TestClass.NullByteArrayVal))
                .Select(nameof(TestClass.NullByteVal))
                .Select(nameof(TestClass.NullCharVal))
                .Select(nameof(TestClass.NullDateTimeOffsetVal))
                .Select(nameof(TestClass.NullDateTimeVal))
                .Select(nameof(TestClass.NullDecimalVal))
                .Select(nameof(TestClass.NullDoubleVal))
                .Select(nameof(TestClass.NullFloatVal))
                .Select(nameof(TestClass.NullGuidVal))
                .Select(nameof(TestClass.IntVal))
                .Select(nameof(TestClass.NullLongVal))
                .Select(nameof(TestClass.NullSByteVal))
                .Select(nameof(TestClass.NullShortVal))
                .Select(nameof(TestClass.NullSingleVal))
                .Select(nameof(TestClass.NullStringVal))
                .Select(nameof(TestClass.NullTimeSpanVal))
                .Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            retRecord = result[0];
            oneRecord = new T().GetDefault1Record();
            Assert.True(retRecord.NullBoolVal == oneRecord.NullBoolVal);
            Assert.True(retRecord.NullByteArrayVal == oneRecord.NullByteArrayVal);
            Assert.True(retRecord.NullByteVal == oneRecord.NullByteVal);
            Assert.True(retRecord.NullCharVal == oneRecord.NullCharVal);
            Assert.True(retRecord.NullDateTimeOffsetVal == oneRecord.NullDateTimeOffsetVal);
            Assert.True(retRecord.NullDateTimeVal == oneRecord.NullDateTimeVal);
            Assert.True(retRecord.NullDecimalVal == oneRecord.NullDecimalVal);
            Assert.True(retRecord.NullDoubleVal == oneRecord.NullDoubleVal);
            Assert.True(retRecord.NullFloatVal == oneRecord.NullFloatVal);
            Assert.True(retRecord.NullGuidVal == oneRecord.NullGuidVal);
            Assert.True(retRecord.NullIntVal == oneRecord.NullIntVal);
            Assert.True(retRecord.NullLongVal == oneRecord.NullLongVal);
            Assert.True(retRecord.NullSByteVal == oneRecord.NullSByteVal);
            Assert.True(retRecord.NullShortVal == oneRecord.NullShortVal);
            Assert.True(retRecord.NullSingleVal == oneRecord.NullSingleVal);
            Assert.True(retRecord.NullStringVal == oneRecord.NullStringVal);
            Assert.True(retRecord.NullTimeSpanVal == oneRecord.NullTimeSpanVal);
            Assert.True(retRecord.NullUInt16Val == oneRecord.NullUInt16Val);
            Assert.True(retRecord.NullUInt32Val == oneRecord.NullUInt32Val);
            Assert.True(retRecord.NullUInt64Val == oneRecord.NullUInt64Val);

            // 19 select
            serviceQuery = ServiceQueryRequestBuilder.New()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .Select(nameof(TestClass.NullBoolVal))
                .Select(nameof(TestClass.NullByteArrayVal))
                .Select(nameof(TestClass.NullByteVal))
                .Select(nameof(TestClass.NullCharVal))
                .Select(nameof(TestClass.NullDateTimeOffsetVal))
                .Select(nameof(TestClass.NullDateTimeVal))
                .Select(nameof(TestClass.NullDecimalVal))
                .Select(nameof(TestClass.NullDoubleVal))
                .Select(nameof(TestClass.NullFloatVal))
                .Select(nameof(TestClass.NullGuidVal))
                .Select(nameof(TestClass.IntVal))
                .Select(nameof(TestClass.NullLongVal))
                .Select(nameof(TestClass.NullSByteVal))
                .Select(nameof(TestClass.NullShortVal))
                .Select(nameof(TestClass.NullSingleVal))
                .Select(nameof(TestClass.NullStringVal))
                .Select(nameof(TestClass.NullTimeSpanVal))
                .Select(nameof(TestClass.NullUInt16Val))
                .Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            retRecord = result[0];
            oneRecord = new T().GetDefault1Record();
            Assert.True(retRecord.NullBoolVal == oneRecord.NullBoolVal);
            Assert.True(retRecord.NullByteArrayVal == oneRecord.NullByteArrayVal);
            Assert.True(retRecord.NullByteVal == oneRecord.NullByteVal);
            Assert.True(retRecord.NullCharVal == oneRecord.NullCharVal);
            Assert.True(retRecord.NullDateTimeOffsetVal == oneRecord.NullDateTimeOffsetVal);
            Assert.True(retRecord.NullDateTimeVal == oneRecord.NullDateTimeVal);
            Assert.True(retRecord.NullDecimalVal == oneRecord.NullDecimalVal);
            Assert.True(retRecord.NullDoubleVal == oneRecord.NullDoubleVal);
            Assert.True(retRecord.NullFloatVal == oneRecord.NullFloatVal);
            Assert.True(retRecord.NullGuidVal == oneRecord.NullGuidVal);
            Assert.True(retRecord.NullIntVal == oneRecord.NullIntVal);
            Assert.True(retRecord.NullLongVal == oneRecord.NullLongVal);
            Assert.True(retRecord.NullSByteVal == oneRecord.NullSByteVal);
            Assert.True(retRecord.NullShortVal == oneRecord.NullShortVal);
            Assert.True(retRecord.NullSingleVal == oneRecord.NullSingleVal);
            Assert.True(retRecord.NullStringVal == oneRecord.NullStringVal);
            Assert.True(retRecord.NullTimeSpanVal == oneRecord.NullTimeSpanVal);
            Assert.True(retRecord.NullUInt16Val == oneRecord.NullUInt16Val);
            Assert.True(retRecord.NullUInt32Val == oneRecord.NullUInt32Val);
            Assert.True(retRecord.NullUInt64Val == oneRecord.NullUInt64Val);

            // 20 select
            serviceQuery = ServiceQueryRequestBuilder.New()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .Select(nameof(TestClass.NullBoolVal))
                .Select(nameof(TestClass.NullByteArrayVal))
                .Select(nameof(TestClass.NullByteVal))
                .Select(nameof(TestClass.NullCharVal))
                .Select(nameof(TestClass.NullDateTimeOffsetVal))
                .Select(nameof(TestClass.NullDateTimeVal))
                .Select(nameof(TestClass.NullDecimalVal))
                .Select(nameof(TestClass.NullDoubleVal))
                .Select(nameof(TestClass.NullFloatVal))
                .Select(nameof(TestClass.NullGuidVal))
                .Select(nameof(TestClass.IntVal))
                .Select(nameof(TestClass.NullLongVal))
                .Select(nameof(TestClass.NullSByteVal))
                .Select(nameof(TestClass.NullShortVal))
                .Select(nameof(TestClass.NullSingleVal))
                .Select(nameof(TestClass.NullStringVal))
                .Select(nameof(TestClass.NullTimeSpanVal))
                .Select(nameof(TestClass.NullUInt16Val))
                .Select(nameof(TestClass.NullUInt32Val))
                .Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            retRecord = result[0];
            oneRecord = new T().GetDefault1Record();
            Assert.True(retRecord.NullBoolVal == oneRecord.NullBoolVal);
            Assert.True(retRecord.NullByteArrayVal == oneRecord.NullByteArrayVal);
            Assert.True(retRecord.NullByteVal == oneRecord.NullByteVal);
            Assert.True(retRecord.NullCharVal == oneRecord.NullCharVal);
            Assert.True(retRecord.NullDateTimeOffsetVal == oneRecord.NullDateTimeOffsetVal);
            Assert.True(retRecord.NullDateTimeVal == oneRecord.NullDateTimeVal);
            Assert.True(retRecord.NullDecimalVal == oneRecord.NullDecimalVal);
            Assert.True(retRecord.NullDoubleVal == oneRecord.NullDoubleVal);
            Assert.True(retRecord.NullFloatVal == oneRecord.NullFloatVal);
            Assert.True(retRecord.NullGuidVal == oneRecord.NullGuidVal);
            Assert.True(retRecord.NullIntVal == oneRecord.NullIntVal);
            Assert.True(retRecord.NullLongVal == oneRecord.NullLongVal);
            Assert.True(retRecord.NullSByteVal == oneRecord.NullSByteVal);
            Assert.True(retRecord.NullShortVal == oneRecord.NullShortVal);
            Assert.True(retRecord.NullSingleVal == oneRecord.NullSingleVal);
            Assert.True(retRecord.NullStringVal == oneRecord.NullStringVal);
            Assert.True(retRecord.NullTimeSpanVal == oneRecord.NullTimeSpanVal);
            Assert.True(retRecord.NullUInt16Val == oneRecord.NullUInt16Val);
            Assert.True(retRecord.NullUInt32Val == oneRecord.NullUInt32Val);
            Assert.True(retRecord.NullUInt64Val == oneRecord.NullUInt64Val);

            // 21 select
            serviceQuery = ServiceQueryRequestBuilder.New()
                .IsEqual(nameof(TestClass.IntVal), "1")
                .Select(nameof(TestClass.NullBoolVal))
                .Select(nameof(TestClass.NullByteArrayVal))
                .Select(nameof(TestClass.NullByteVal))
                .Select(nameof(TestClass.NullCharVal))
                .Select(nameof(TestClass.NullDateTimeOffsetVal))
                .Select(nameof(TestClass.NullDateTimeVal))
                .Select(nameof(TestClass.NullDecimalVal))
                .Select(nameof(TestClass.NullDoubleVal))
                .Select(nameof(TestClass.NullFloatVal))
                .Select(nameof(TestClass.NullGuidVal))
                .Select(nameof(TestClass.IntVal))
                .Select(nameof(TestClass.NullLongVal))
                .Select(nameof(TestClass.NullSByteVal))
                .Select(nameof(TestClass.NullShortVal))
                .Select(nameof(TestClass.NullSingleVal))
                .Select(nameof(TestClass.NullStringVal))
                .Select(nameof(TestClass.NullTimeSpanVal))
                .Select(nameof(TestClass.NullUInt16Val))
                .Select(nameof(TestClass.NullUInt32Val))
                .Select(nameof(TestClass.NullUInt64Val))
                .Build();
            testQueryable = serviceQuery.GetServiceQuery().Apply(sourceQueryable);
            result = testQueryable.ToList();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            result = await testQueryable.ToListAsync();
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            retRecord = result[0];
            oneRecord = new T().GetDefault1Record();
            Assert.True(retRecord.NullBoolVal == oneRecord.NullBoolVal);
            Assert.True(retRecord.NullByteArrayVal == oneRecord.NullByteArrayVal);
            Assert.True(retRecord.NullByteVal == oneRecord.NullByteVal);
            Assert.True(retRecord.NullCharVal == oneRecord.NullCharVal);
            Assert.True(retRecord.NullDateTimeOffsetVal == oneRecord.NullDateTimeOffsetVal);
            Assert.True(retRecord.NullDateTimeVal == oneRecord.NullDateTimeVal);
            Assert.True(retRecord.NullDecimalVal == oneRecord.NullDecimalVal);
            Assert.True(retRecord.NullDoubleVal == oneRecord.NullDoubleVal);
            Assert.True(retRecord.NullFloatVal == oneRecord.NullFloatVal);
            Assert.True(retRecord.NullGuidVal == oneRecord.NullGuidVal);
            Assert.True(retRecord.NullIntVal == oneRecord.NullIntVal);
            Assert.True(retRecord.NullLongVal == oneRecord.NullLongVal);
            Assert.True(retRecord.NullSByteVal == oneRecord.NullSByteVal);
            Assert.True(retRecord.NullShortVal == oneRecord.NullShortVal);
            Assert.True(retRecord.NullSingleVal == oneRecord.NullSingleVal);
            Assert.True(retRecord.NullStringVal == oneRecord.NullStringVal);
            Assert.True(retRecord.NullTimeSpanVal == oneRecord.NullTimeSpanVal);
            Assert.True(retRecord.NullUInt16Val == oneRecord.NullUInt16Val);
            Assert.True(retRecord.NullUInt32Val == oneRecord.NullUInt32Val);
            Assert.True(retRecord.NullUInt64Val == oneRecord.NullUInt64Val);
        }
    }
}
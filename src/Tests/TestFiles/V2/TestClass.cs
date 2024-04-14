﻿namespace ServiceQuery.Xunit
{
    public class TestClass : ITestClass
    {
        //cosmos key
        public Guid CosmosKey { get; set; }

        //db key
        public virtual int DatabaseKey { get; set; }

        //For use with shared record testing
        public virtual int SharedParentKey { get; set; }

        public virtual string StringVal { get; set; }
#pragma warning disable CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
        public virtual string? NullStringVal { get; set; }
#pragma warning restore CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.

        public virtual byte ByteVal { get; set; }
        public virtual byte? NullByteVal { get; set; }

        public virtual byte[] ByteArrayVal { get; set; }
        public virtual byte?[] NullByteArrayVal { get; set; }

        public virtual char CharVal { get; set; }
        public virtual char? NullCharVal { get; set; }

        public virtual double DoubleVal { get; set; }
        public virtual double? NullDoubleVal { get; set; }

        public virtual bool BoolVal { get; set; }
        public virtual bool? NullBoolVal { get; set; }

        public virtual DateOnly DateOnlyVal { get; set; }
        public virtual DateOnly? NullDateOnlyVal { get; set; }

        public virtual DateTime DateTimeVal { get; set; }
        public virtual DateTime? NullDateTimeVal { get; set; }

        public virtual decimal DecimalVal { get; set; }
        public virtual decimal? NullDecimalVal { get; set; }

        public virtual Guid GuidVal { get; set; }
        public virtual Guid? NullGuidVal { get; set; }

        public virtual DateTimeOffset DateTimeOffsetVal { get; set; }
        public virtual DateTimeOffset? NullDateTimeOffsetVal { get; set; }

        public virtual short ShortVal { get; set; }
        public virtual short? NullShortVal { get; set; }

        public virtual int IntVal { get; set; }
        public virtual int? NullIntVal { get; set; }

        public virtual long LongVal { get; set; }
        public virtual long? NullLongVal { get; set; }

        public virtual sbyte SByteVal { get; set; }
        public virtual sbyte? NullSByteVal { get; set; }

        public virtual Single SingleVal { get; set; }
        public virtual Single? NullSingleVal { get; set; }

        public virtual TimeOnly TimeOnlyVal { get; set; }
        public virtual TimeOnly? NullTimeOnlyVal { get; set; }

        public virtual TimeSpan TimeSpanVal { get; set; }
        public virtual TimeSpan? NullTimeSpanVal { get; set; }

        public virtual UInt16 UInt16Val { get; set; }
        public virtual UInt16? NullUInt16Val { get; set; }

        public virtual UInt32 UInt32Val { get; set; }
        public virtual UInt32? NullUInt32Val { get; set; }

        public virtual UInt64 UInt64Val { get; set; }
        public virtual UInt64? NullUInt64Val { get; set; }

#if NET7_0_OR_GREATER
        public virtual UInt128 UInt128Val { get; set; }
        public virtual UInt128? NullUInt128Val { get; set; }
#endif
        public virtual float FloatVal { get; set; }
        public virtual float? NullFloatVal { get; set; }

        public virtual List<TestClass> GetDefaultList()
        {
            //added out of order
            return new List<TestClass>()
            {
                (TestClass)GetDefault3Record(),
                (TestClass)GetDefault0Record(),
                (TestClass)GetDefault2Record(),
                (TestClass)GetDefault1Record(),
            };
        }

        public virtual void CopyToNullVals()
        {
            NullBoolVal = BoolVal;
            NullByteArrayVal = new byte?[] { ByteArrayVal[0] };
            NullByteVal = ByteVal;
            NullCharVal = CharVal;
            NullDateOnlyVal = DateOnlyVal;
            NullDateTimeOffsetVal = DateTimeOffsetVal;
            NullDateTimeVal = DateTimeVal;
            NullDecimalVal = DecimalVal;
            NullDoubleVal = DoubleVal;
            NullFloatVal = FloatVal;
            NullGuidVal = GuidVal;
            NullIntVal = IntVal;
            NullLongVal = LongVal;
            NullShortVal = ShortVal;
            NullSByteVal = SByteVal;
            NullSingleVal = SingleVal;
            NullStringVal = StringVal;
            NullTimeOnlyVal = TimeOnlyVal;
            NullTimeSpanVal = TimeSpanVal;
            NullUInt16Val = UInt16Val;
            NullUInt32Val = UInt32Val;
            NullUInt64Val = UInt64Val;
#if NET7_0_OR_GREATER
            NullUInt128Val = UInt128Val;
#endif
        }

        public virtual ITestClass GetDefault0Record()
        {
            return new TestClass()
            {
                BoolVal = false,
                ByteArrayVal = new byte[] { 0 },
                ByteVal = 0,
                CharVal = ' ',
                DateOnlyVal = new DateOnly(2000, 1, 1),
                DateTimeOffsetVal = new DateTimeOffset(2000, 1, 1, 0, 0, 0, 0, TimeSpan.Zero),
                DateTimeVal = new DateTime(2000, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc),
                DecimalVal = 0,
                DoubleVal = 0,
                FloatVal = 0,
                GuidVal = Guid.Empty,
                IntVal = 0,
                LongVal = 0,
                ShortVal = 0,
                SByteVal = 0,
                SingleVal = 0,
                StringVal = " ",
                TimeOnlyVal = new TimeOnly(0, 0, 0),
                TimeSpanVal = TimeSpan.Zero,
                UInt16Val = 0,
                UInt32Val = 0,
                UInt64Val = 0,
#if NET7_0_OR_GREATER
                UInt128Val = 0,
#endif
                SharedParentKey = 1
            };
        }

        public virtual ITestClass GetDefault1Record()
        {
            return new TestClass()
            {
                BoolVal = true,
                ByteArrayVal = new byte[] { 1 },
                ByteVal = 1,
                CharVal = 'a',
                DateOnlyVal = new DateOnly(2001, 1, 1),
                DateTimeOffsetVal = new DateTimeOffset(2001, 1, 1, 1, 1, 1, 1, TimeSpan.Zero),
                DateTimeVal = new DateTime(2001, 1, 1, 1, 1, 1, 1, DateTimeKind.Utc),
                DecimalVal = 1,
                DoubleVal = 1,
                FloatVal = 1,
                GuidVal = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                IntVal = 1,
                LongVal = 1,
                ShortVal = 1,
                SByteVal = 1,
                SingleVal = 1,
                StringVal = "a",
                TimeOnlyVal = new TimeOnly(1, 1, 1),
                TimeSpanVal = TimeSpan.FromMilliseconds(1),
                UInt16Val = 1,
                UInt32Val = 1,
                UInt64Val = 1,
#if NET7_0_OR_GREATER
                UInt128Val = 1,
#endif
                SharedParentKey = 1
            };
        }

        public virtual ITestClass GetDefault2Record()
        {
            return new TestClass()
            {
                BoolVal = false,
                ByteArrayVal = new byte[] { 2 },
                ByteVal = 2,
                CharVal = 'b',
                DateOnlyVal = new DateOnly(2002, 2, 2),
                DateTimeOffsetVal = new DateTimeOffset(2002, 2, 2, 2, 2, 2, 2, TimeSpan.Zero),
                DateTimeVal = new DateTime(2002, 2, 2, 2, 2, 2, 2, DateTimeKind.Utc),
                DecimalVal = 2,
                DoubleVal = 2,
                FloatVal = 2,
                GuidVal = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                IntVal = 2,
                LongVal = 2,
                ShortVal = 2,
                SByteVal = 2,
                SingleVal = 2,
                StringVal = "b",
                TimeOnlyVal = new TimeOnly(2, 2, 2),
                TimeSpanVal = TimeSpan.FromMilliseconds(2),
                UInt16Val = 2,
                UInt32Val = 2,
                UInt64Val = 2,
#if NET7_0_OR_GREATER
                UInt128Val = 2,
#endif
                SharedParentKey = 1
            };
        }

        public virtual ITestClass GetDefault3Record()
        {
            return new TestClass()
            {
                BoolVal = true,
                ByteArrayVal = new byte[] { 3 },
                ByteVal = 3,
                CharVal = 'c',
                DateOnlyVal = new DateOnly(2003, 3, 3),
                DateTimeOffsetVal = new DateTimeOffset(2003, 3, 3, 3, 3, 3, 3, TimeSpan.Zero),
                DateTimeVal = new DateTime(2003, 3, 3, 3, 3, 3, 3, DateTimeKind.Utc),
                DecimalVal = 3,
                DoubleVal = 3,
                FloatVal = 3,
                GuidVal = Guid.Parse("33333333-3333-3333-3333-333333333333"),
                IntVal = 3,
                LongVal = 3,
                ShortVal = 3,
                SByteVal = 3,
                SingleVal = 3,
                StringVal = "c",
                TimeOnlyVal = new TimeOnly(3, 3, 3),
                TimeSpanVal = TimeSpan.FromMilliseconds(3),
                UInt16Val = 3,
                UInt32Val = 3,
                UInt64Val = 3,
#if NET7_0_OR_GREATER
                UInt128Val = 3,
#endif
                SharedParentKey = 1
            };
        }
    }
}
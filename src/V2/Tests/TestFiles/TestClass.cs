namespace ServiceQuery.Xunit
{
    public class TestNullCopyClass : TestClass, ITestClass
    {
        public override List<TestClass> GetDefaultList()
        {
            //added out of order
            var list = new List<TestClass>()
            {
                (TestNullCopyClass)GetDefault3Record(new TestNullCopyClass()),
                (TestNullCopyClass)GetDefault0Record(new TestNullCopyClass()),
                (TestNullCopyClass)GetDefault2Record(new TestNullCopyClass()),
                (TestNullCopyClass)GetDefault1Record(new TestNullCopyClass()),
            };
            foreach (var item in list)
                item.CopyToNullVals();
            return list;
        }
    }

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
                (TestClass)GetDefault3Record(new TestClass()),
                (TestClass)GetDefault0Record(new TestClass()),
                (TestClass)GetDefault2Record(new TestClass()),
                (TestClass)GetDefault1Record(new TestClass()),
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

        public virtual ITestClass GetDefault0Record(ITestClass testClass)
        {
            testClass.BoolVal = false;
            testClass.ByteArrayVal = new byte[] { 0 };
            testClass.ByteVal = 0;
            testClass.CharVal = ' ';
            testClass.DateOnlyVal = new DateOnly(2000, 1, 1);
            testClass.DateTimeOffsetVal = new DateTimeOffset(2000, 1, 1, 0, 0, 0, 0, TimeSpan.Zero);
            testClass.DateTimeVal = new DateTime(2000, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            testClass.DecimalVal = 0;
            testClass.DoubleVal = 0;
            testClass.FloatVal = 0;
            testClass.GuidVal = Guid.Empty;
            testClass.IntVal = 0;
            testClass.LongVal = 0;
            testClass.ShortVal = 0;
            testClass.SByteVal = 0;
            testClass.SingleVal = 0;
            testClass.StringVal = " ";
            testClass.TimeOnlyVal = new TimeOnly(0, 0, 0);
            testClass.TimeSpanVal = TimeSpan.Zero;
            testClass.UInt16Val = 0;
            testClass.UInt32Val = 0;
            testClass.UInt64Val = 0;
#if NET7_0_OR_GREATER
            testClass.UInt128Val = 0;
#endif
            testClass.SharedParentKey = 1;
            return testClass;
        }

        public virtual ITestClass GetDefault1Record(ITestClass testClass)
        {
            testClass.BoolVal = true;
            testClass.ByteArrayVal = new byte[] { 1 };
            testClass.ByteVal = 1;
            testClass.CharVal = 'a';
            testClass.DateOnlyVal = new DateOnly(2001, 1, 1);
            testClass.DateTimeOffsetVal = new DateTimeOffset(2001, 1, 1, 1, 1, 1, 1, TimeSpan.Zero);
            testClass.DateTimeVal = new DateTime(2001, 1, 1, 1, 1, 1, 1, DateTimeKind.Utc);
            testClass.DecimalVal = 1;
            testClass.DoubleVal = 1;
            testClass.FloatVal = 1;
            testClass.GuidVal = Guid.Parse("11111111-1111-1111-1111-111111111111");
            testClass.IntVal = 1;
            testClass.LongVal = 1;
            testClass.ShortVal = 1;
            testClass.SByteVal = 1;
            testClass.SingleVal = 1;
            testClass.StringVal = "a";
            testClass.TimeOnlyVal = new TimeOnly(1, 1, 1);
            testClass.TimeSpanVal = TimeSpan.FromMilliseconds(1);
            testClass.UInt16Val = 1;
            testClass.UInt32Val = 1;
            testClass.UInt64Val = 1;
#if NET7_0_OR_GREATER
            testClass.UInt128Val = 1;
#endif
            testClass.SharedParentKey = 1;
            return testClass;
        }

        public virtual ITestClass GetDefault2Record(ITestClass testClass)
        {
            testClass.BoolVal = false;
            testClass.ByteArrayVal = new byte[] { 2 };
            testClass.ByteVal = 2;
            testClass.CharVal = 'b';
            testClass.DateOnlyVal = new DateOnly(2002, 2, 2);
            testClass.DateTimeOffsetVal = new DateTimeOffset(2002, 2, 2, 2, 2, 2, 2, TimeSpan.Zero);
            testClass.DateTimeVal = new DateTime(2002, 2, 2, 2, 2, 2, 2, DateTimeKind.Utc);
            testClass.DecimalVal = 2;
            testClass.DoubleVal = 2;
            testClass.FloatVal = 2;
            testClass.GuidVal = Guid.Parse("22222222-2222-2222-2222-222222222222");
            testClass.IntVal = 2;
            testClass.LongVal = 2;
            testClass.ShortVal = 2;
            testClass.SByteVal = 2;
            testClass.SingleVal = 2;
            testClass.StringVal = "b";
            testClass.TimeOnlyVal = new TimeOnly(2, 2, 2);
            testClass.TimeSpanVal = TimeSpan.FromMilliseconds(2);
            testClass.UInt16Val = 2;
            testClass.UInt32Val = 2;
            testClass.UInt64Val = 2;
#if NET7_0_OR_GREATER
            testClass.UInt128Val = 2;
#endif
            testClass.SharedParentKey = 1;
            return testClass;
        }

        public virtual ITestClass GetDefault3Record(ITestClass testClass)
        {
            testClass.BoolVal = true;
            testClass.ByteArrayVal = new byte[] { 3 };
            testClass.ByteVal = 3;
            testClass.CharVal = 'c';
            testClass.DateOnlyVal = new DateOnly(2003, 3, 3);
            testClass.DateTimeOffsetVal = new DateTimeOffset(2003, 3, 3, 3, 3, 3, 3, TimeSpan.Zero);
            testClass.DateTimeVal = new DateTime(2003, 3, 3, 3, 3, 3, 3, DateTimeKind.Utc);
            testClass.DecimalVal = 3;
            testClass.DoubleVal = 3;
            testClass.FloatVal = 3;
            testClass.GuidVal = Guid.Parse("33333333-3333-3333-3333-333333333333");
            testClass.IntVal = 3;
            testClass.LongVal = 3;
            testClass.ShortVal = 3;
            testClass.SByteVal = 3;
            testClass.SingleVal = 3;
            testClass.StringVal = "c";
            testClass.TimeOnlyVal = new TimeOnly(3, 3, 3);
            testClass.TimeSpanVal = TimeSpan.FromMilliseconds(3);
            testClass.UInt16Val = 3;
            testClass.UInt32Val = 3;
            testClass.UInt64Val = 3;
#if NET7_0_OR_GREATER
            testClass.UInt128Val = 3;
#endif
            testClass.SharedParentKey = 1;
            return testClass;
        }
    }
}
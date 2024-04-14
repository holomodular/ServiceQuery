namespace ServiceQuery.Xunit
{
    public interface ITestClass
    {
        bool BoolVal { get; set; }
        bool? NullBoolVal { get; set; }
        byte[] ByteArrayVal { get; set; }
        byte?[] NullByteArrayVal { get; set; }
        byte ByteVal { get; set; }
        byte? NullByteVal { get; set; }
        char CharVal { get; set; }
        char? NullCharVal { get; set; }
#if NET6_0_OR_GREATER
        DateOnly DateOnlyVal { get; set; }
        DateOnly? NullDateOnlyVal { get; set; }

        TimeOnly TimeOnlyVal { get; set; }
        TimeOnly? NullTimeOnlyVal { get; set; }
#endif
        DateTimeOffset DateTimeOffsetVal { get; set; }
        DateTimeOffset? NullDateTimeOffsetVal { get; set; }
        DateTime DateTimeVal { get; set; }
        DateTime? NullDateTimeVal { get; set; }
        decimal DecimalVal { get; set; }
        decimal? NullDecimalVal { get; set; }
        double DoubleVal { get; set; }
        double? NullDoubleVal { get; set; }
        float FloatVal { get; set; }
        float? NullFloatVal { get; set; }
        Guid GuidVal { get; set; }
        Guid? NullGuidVal { get; set; }
        int IntVal { get; set; }
        int? NullIntVal { get; set; }
        long LongVal { get; set; }
        long? NullLongVal { get; set; }

        sbyte SByteVal { get; set; }
        sbyte? NullSByteVal { get; set; }
        int SharedParentKey { get; set; }
        short ShortVal { get; set; }
        short? NullShortVal { get; set; }
        float SingleVal { get; set; }
        float? NullSingleVal { get; set; }
        string StringVal { get; set; }
        string NullStringVal { get; set; }
        TimeSpan TimeSpanVal { get; set; }
        TimeSpan? NullTimeSpanVal { get; set; }
        UInt16 UInt16Val { get; set; }
        UInt16? NullUInt16Val { get; set; }
        UInt32 UInt32Val { get; set; }
        UInt32? NullUInt32Val { get; set; }
        UInt64 UInt64Val { get; set; }
        UInt64? NullUInt64Val { get; set; }
#if NET7_0_OR_GREATER
        UInt128 UInt128Val { get; set; }
        UInt128? NullUInt128Val { get; set; }
#endif

        void CopyToNullVals();

        ITestClass GetDefault0Record();

        ITestClass GetDefault1Record();

        ITestClass GetDefault2Record();

        ITestClass GetDefault3Record();

        List<TestClass> GetDefaultList();
    }
}
namespace ServiceQuery
{
    /// <summary>
    /// This interface allows you to create a ServiceQuery object.
    /// </summary>
    public partial interface IServiceQueryBuilder
    {
        /// <summary>
        /// Build the ServiceQuery object.
        /// </summary>
        /// <returns></returns>
        ServiceQuery Build();

        /// <summary>
        /// Create an expression.
        /// </summary>
        /// <returns></returns>
        IServiceQueryBuilder Expression(ServiceQueryExpressionType expressionType);

        /// <summary>
        /// Begin a grouping expression.
        /// </summary>
        /// <returns></returns>
        IServiceQueryBuilder BeginExpression();

        /// <summary>
        /// Begin a grouping expression.
        /// </summary>
        /// <returns></returns>
        IServiceQueryBuilder Begin();

        /// <summary>
        /// End a grouping expression.
        /// </summary>
        /// <returns></returns>
        IServiceQueryBuilder EndExpression();

        /// <summary>
        /// End a grouping expression.
        /// </summary>
        /// <returns></returns>
        IServiceQueryBuilder End();

        /// <summary>
        /// Inclusive grouping expression.
        /// </summary>
        /// <returns></returns>
        IServiceQueryBuilder And();

        /// <summary>
        /// Exclusive grouping expression.
        /// </summary>
        /// <returns></returns>
        IServiceQueryBuilder Or();

        /// <summary>
        /// Add properties to be selected.
        /// </summary>
        /// <param name="propertyNames"></param>
        /// <returns></returns>
        IServiceQueryBuilder Select(params string[] propertyNames);

        /// <summary>
        /// Add distinct modifier.
        /// </summary>
        /// <returns></returns>
        IServiceQueryBuilder Distinct();

        /// <summary>
        /// Add a between expression.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="lowValue"></param>
        /// <param name="highValue"></param>
        /// <returns></returns>
        IServiceQueryBuilder Between(string propertyName, string lowValue, string highValue);

        /// <summary>
        /// Add a string comparison expression that contains the value.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        IServiceQueryBuilder Contains(string propertyName, string value);

        /// <summary>
        /// Add a string comparison expression that starts with the value.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        IServiceQueryBuilder StartsWith(string propertyName, string value);

        /// <summary>
        /// Add a string comparison expression that ends with the value.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        IServiceQueryBuilder EndsWith(string propertyName, string value);

        /// <summary>
        /// Add an equality expression.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        IServiceQueryBuilder IsEqual(string propertyName, string value);

        /// <summary>
        /// Add an inequality expression.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        IServiceQueryBuilder IsNotEqual(string propertyName, string value);

        /// <summary>
        /// Add a greater than expression.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        IServiceQueryBuilder IsGreaterThan(string propertyName, string value);

        /// <summary>
        /// Add a greater than or equal to expression.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        IServiceQueryBuilder IsGreaterThanOrEqual(string propertyName, string value);

        /// <summary>
        /// Add a less than expression.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        IServiceQueryBuilder IsLessThan(string propertyName, string value);

        /// <summary>
        /// Add a less than or equal to expression.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        IServiceQueryBuilder IsLessThanOrEqual(string propertyName, string value);

        /// <summary>
        /// Add a comparison expression.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="comparisonType"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        IServiceQueryBuilder Compare(string propertyName, ServiceQueryCompareType comparisonType, string value);

        ///// <summary>
        ///// Add a property comparison expression.
        ///// </summary>
        ///// <param name="propertyNameLeft"></param>
        ///// <param name="comparisonType"></param>
        ///// <param name="propertyNameRight"></param>
        ///// <returns></returns>
        //IServiceQueryBuilder PropertyCompare(string propertyNameLeft, ServiceQueryCompareType comparisonType, string propertyNameRight);

        /// <summary>
        /// Add a is null expression.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        IServiceQueryBuilder IsNull(string propertyName);

        /// <summary>
        /// Add a null expression.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="isNull"></param>
        /// <returns></returns>
        IServiceQueryBuilder Null(string propertyName, bool isNull);

        /// <summary>
        /// Add a is not null expression.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        IServiceQueryBuilder IsNotNull(string propertyName);

        /// <summary>
        /// Add a in set expression.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        IServiceQueryBuilder IsInSet(string propertyName, params string[] values);

        /// <summary>
        /// Add a set operation.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="inSet"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        IServiceQueryBuilder Set(string propertyName, bool inSet, params string[] values);

        /// <summary>
        /// Add a not in set operation.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        IServiceQueryBuilder IsNotInSet(string propertyName, params string[] values);

        /// <summary>
        /// Add a sort expression.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="sortAscending"></param>
        /// <returns></returns>
        IServiceQueryBuilder Sort(string propertyName, bool sortAscending);

        /// <summary>
        /// Add a sort expression ascending.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        IServiceQueryBuilder SortAsc(string propertyName);

        /// <summary>
        /// Add a sort expression descending.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        IServiceQueryBuilder SortDesc(string propertyName);

        /// <summary>
        /// Add an averate aggregate expression.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        IServiceQueryBuilder Average(string propertyName);

        ///// <summary>
        ///// Add a binary checksum aggregate expression.
        ///// </summary>
        ///// <param name="propertyName"></param>
        ///// <returns></returns>
        ////IServiceQueryBuilder BinaryChecksum(string propertyName);

        ///// <summary>
        ///// Add a checksum aggregate expression.
        ///// </summary>
        ///// <param name="propertyName"></param>
        ///// <returns></returns>
        ////IServiceQueryBuilder Checksum(string propertyName);

        /// <summary>
        /// Add a count aggregate expression.
        /// </summary>
        /// <returns></returns>
        IServiceQueryBuilder Count();

        /// <summary>
        /// Add a maximum aggregate expression.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        IServiceQueryBuilder Maximum(string propertyName);

        /// <summary>
        /// Add a minumum aggregate expression.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        IServiceQueryBuilder Minimum(string propertyName);

        /// <summary>
        /// Add a sum aggregate expression.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        IServiceQueryBuilder Sum(string propertyName);

        /// <summary>
        /// Add an aggregate expression.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="aggregateType"></param>
        /// <returns></returns>
        IServiceQueryBuilder Aggregate(string propertyName, ServiceQueryAggregateType aggregateType);

        /// <summary>
        /// Setup paging.
        /// </summary>
        /// <param name="startPage"></param>
        /// <param name="numberPerPage"></param>
        /// <param name="includeCount"></param>
        /// <returns></returns>
        IServiceQueryBuilder Paging(int startPage, int numberPerPage, bool includeCount);
    }
}
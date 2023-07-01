namespace ServiceQuery
{
    /// <summary>
    /// This interface allows you to create a ServiceQueryRequest object.
    /// </summary>
    public partial interface IServiceQueryRequestBuilder
    {
        /// <summary>
        /// Build the ServiceQueryRequest object.
        /// </summary>
        /// <returns></returns>
        ServiceQueryRequest Build();

        /// <summary>
        /// Begin a grouping expression.
        /// </summary>
        /// <returns></returns>
        IServiceQueryRequestBuilder BeginExpression();

        /// <summary>
        /// Begin a grouping expression.
        /// </summary>
        /// <returns></returns>
        IServiceQueryRequestBuilder Begin();

        /// <summary>
        /// End a grouping expression.
        /// </summary>
        /// <returns></returns>
        IServiceQueryRequestBuilder EndExpression();

        /// <summary>
        /// End a grouping expression.
        /// </summary>
        /// <returns></returns>
        IServiceQueryRequestBuilder End();

        /// <summary>
        /// Inclusive grouping expression.
        /// </summary>
        /// <returns></returns>
        IServiceQueryRequestBuilder And();

        /// <summary>
        /// Exclusive grouping expression.
        /// </summary>
        /// <returns></returns>
        IServiceQueryRequestBuilder Or();

        /// <summary>
        /// Add properties to be selected.
        /// </summary>
        /// <param name="propertyNames"></param>
        /// <returns></returns>
        IServiceQueryRequestBuilder Select(params string[] propertyNames);

        /// <summary>
        /// Add distinct modifier.
        /// </summary>
        /// <returns></returns>
        IServiceQueryRequestBuilder Distinct();

        /// <summary>
        /// Add a between expression.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="lowValue"></param>
        /// <param name="highValue"></param>
        /// <returns></returns>
        IServiceQueryRequestBuilder Between(string propertyName, string lowValue, string highValue);

        /// <summary>
        /// Add a string comparison expression that contains the value.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        IServiceQueryRequestBuilder Contains(string propertyName, string value);

        /// <summary>
        /// Add a string comparison expression that starts with the value.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        IServiceQueryRequestBuilder StartsWith(string propertyName, string value);

        /// <summary>
        /// Add a string comparison expression that ends with the value.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        IServiceQueryRequestBuilder EndsWith(string propertyName, string value);

        /// <summary>
        /// Add an equality expression.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        IServiceQueryRequestBuilder IsEqual(string propertyName, string value);

        /// <summary>
        /// Add an inequality expression.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        IServiceQueryRequestBuilder IsNotEqual(string propertyName, string value);

        /// <summary>
        /// Add a greater than expression.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        IServiceQueryRequestBuilder IsGreaterThan(string propertyName, string value);

        /// <summary>
        /// Add a greater than or equal to expression.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        IServiceQueryRequestBuilder IsGreaterThanOrEqual(string propertyName, string value);

        /// <summary>
        /// Add a less than expression.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        IServiceQueryRequestBuilder IsLessThan(string propertyName, string value);

        /// <summary>
        /// Add a less than or equal to expression.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        IServiceQueryRequestBuilder IsLessThanOrEqual(string propertyName, string value);

        /// <summary>
        /// Add a is null expression.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        IServiceQueryRequestBuilder IsNull(string propertyName);

        /// <summary>
        /// Add a null expression.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="isNull"></param>
        /// <returns></returns>
        IServiceQueryRequestBuilder Null(string propertyName, bool isNull);

        /// <summary>
        /// Add a is not null expression.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        IServiceQueryRequestBuilder IsNotNull(string propertyName);

        /// <summary>
        /// Add a in set expression.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        IServiceQueryRequestBuilder IsInSet(string propertyName, params string[] values);

        /// <summary>
        /// Add a set operation.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="inSet"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        IServiceQueryRequestBuilder Set(string propertyName, bool inSet, params string[] values);

        /// <summary>
        /// Add a not in set operation.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        IServiceQueryRequestBuilder IsNotInSet(string propertyName, params string[] values);

        /// <summary>
        /// Add a sort expression.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="sortAscending"></param>
        /// <returns></returns>
        IServiceQueryRequestBuilder Sort(string propertyName, bool sortAscending);

        /// <summary>
        /// Sort an expression ascending.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        IServiceQueryRequestBuilder SortAsc(string propertyName);

        /// <summary>
        /// Sort an expression descending.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        IServiceQueryRequestBuilder SortDesc(string propertyName);

        /// <summary>
        /// Add an averate aggregate expression.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        IServiceQueryRequestBuilder Average(string propertyName);

        ///// <summary>
        ///// Add a binary checksum aggregate expression.
        ///// </summary>
        ///// <param name="propertyName"></param>
        ///// <returns></returns>
        ////IServiceQueryRequestBuilder BinaryChecksum(string propertyName);

        ///// <summary>
        ///// Add a checksum aggregate expression.
        ///// </summary>
        ///// <param name="propertyName"></param>
        ///// <returns></returns>
        ////IServiceQueryRequestBuilder Checksum(string propertyName);

        /// <summary>
        /// Add a count aggregate expression.
        /// </summary>
        /// <returns></returns>
        IServiceQueryRequestBuilder Count();

        /// <summary>
        /// Add a maximum aggregate expression.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        IServiceQueryRequestBuilder Maximum(string propertyName);

        /// <summary>
        /// Add a minumum aggregate expression.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        IServiceQueryRequestBuilder Minimum(string propertyName);

        /// <summary>
        /// Add a sum aggregate expression.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        IServiceQueryRequestBuilder Sum(string propertyName);

        /// <summary>
        /// Include the count with the query.
        /// </summary>
        /// <returns></returns>
        IServiceQueryRequestBuilder IncludeCount();

        /// <summary>
        /// Setup paging.
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <param name="includeCount"></param>
        /// <returns></returns>
        IServiceQueryRequestBuilder Paging(int pageNumber, int pageSize, bool includeCount);
    }
}
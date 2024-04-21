using System.Collections.Generic;
using System.Linq;

namespace ServiceQuery
{
    /// <summary>
    /// This builder creates a ServiceQueryRequest object used to contain a Service Query.
    /// </summary>
    public partial class ServiceQueryRequestBuilder : IServiceQueryRequestBuilder
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public ServiceQueryRequestBuilder()
        {
            Filters = new List<ServiceQueryServiceFilter>();
        }

        /// <summary>
        /// The list of filters.
        /// </summary>
        public virtual List<ServiceQueryServiceFilter> Filters { get; set; }

        /// <summary>
        /// Create a new instance of a builder object.
        /// </summary>
        /// <returns></returns>
        public static IServiceQueryRequestBuilder New()
        {
            return new ServiceQueryRequestBuilder();
        }

        /// <summary>
        /// Get the ServiceQuery Request object.
        /// </summary>
        /// <returns></returns>
        public virtual ServiceQueryRequest Build()
        {
            return new ServiceQueryRequest()
            {
                Filters = this.Filters,
            };
        }

        /// <summary>
        /// Begin a grouping expression.
        /// </summary>
        /// <returns></returns>
        public virtual IServiceQueryRequestBuilder BeginExpression()
        {
            Filters.Add(new ServiceQueryServiceFilter()
            {
                FilterType = ServiceQueryServiceFilterType.begin,
            });
            return this;
        }

        /// <summary>
        /// Begin a grouping expression.
        /// </summary>
        /// <returns></returns>
        public virtual IServiceQueryRequestBuilder Begin()
        {
            Filters.Add(new ServiceQueryServiceFilter()
            {
                FilterType = ServiceQueryServiceFilterType.begin,
            });
            return this;
        }

        /// <summary>
        /// End a grouping expression.
        /// </summary>
        /// <returns></returns>
        public virtual IServiceQueryRequestBuilder EndExpression()
        {
            Filters.Add(new ServiceQueryServiceFilter()
            {
                FilterType = ServiceQueryServiceFilterType.end,
            });
            return this;
        }

        /// <summary>
        /// End a grouping expression.
        /// </summary>
        /// <returns></returns>
        public virtual IServiceQueryRequestBuilder End()
        {
            Filters.Add(new ServiceQueryServiceFilter()
            {
                FilterType = ServiceQueryServiceFilterType.end,
            });
            return this;
        }

        /// <summary>
        /// Inclusive grouping expression.
        /// </summary>
        /// <returns></returns>
        public virtual IServiceQueryRequestBuilder And()
        {
            Filters.Add(new ServiceQueryServiceFilter()
            {
                FilterType = ServiceQueryServiceFilterType.and,
            });
            return this;
        }

        /// <summary>
        /// Seperate exclusive grouping expression.
        /// </summary>
        /// <returns></returns>
        public virtual IServiceQueryRequestBuilder Or()
        {
            Filters.Add(new ServiceQueryServiceFilter()
            {
                FilterType = ServiceQueryServiceFilterType.or,
            });
            return this;
        }

        /// <summary>
        /// Add properties to be selected.
        /// </summary>
        /// <param name="propertyNames"></param>
        /// <returns></returns>
        public virtual IServiceQueryRequestBuilder Select(params string[] propertyNames)
        {
            ServiceQueryServiceFilter filter = new ServiceQueryServiceFilter();
            filter.FilterType = ServiceQueryServiceFilterType.select;
            foreach (string prop in propertyNames)
                filter.Properties.Add(prop);
            Filters.Add(filter);
            return this;
        }

        /// <summary>
        /// Add distinct modifier.
        /// </summary>
        /// <returns></returns>
        public virtual IServiceQueryRequestBuilder Distinct()
        {
            ServiceQueryServiceFilter filter = new ServiceQueryServiceFilter();
            filter.FilterType = ServiceQueryServiceFilterType.distinct;
            Filters.Add(filter);
            return this;
        }

        /// <summary>
        /// Add a between expression.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="lowValue"></param>
        /// <param name="highValue"></param>
        /// <returns></returns>
        public virtual IServiceQueryRequestBuilder Between(string propertyName, string lowValue, string highValue)
        {
            Filters.Add(new ServiceQueryServiceFilter()
            {
                FilterType = ServiceQueryServiceFilterType.between,
                Properties = new List<string>() { propertyName },
                Values = new List<string>() { lowValue, highValue }
            });
            return this;
        }

        /// <summary>
        /// A string comparison expression that contains the value.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public virtual IServiceQueryRequestBuilder Contains(string propertyName, string value)
        {
            Filters.Add(new ServiceQueryServiceFilter()
            {
                FilterType = ServiceQueryServiceFilterType.contains,
                Properties = new List<string>() { propertyName },
                Values = new List<string>() { value }
            });
            return this;
        }

        /// <summary>
        /// A string comparison expression that starts with the value.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public virtual IServiceQueryRequestBuilder StartsWith(string propertyName, string value)
        {
            Filters.Add(new ServiceQueryServiceFilter()
            {
                FilterType = ServiceQueryServiceFilterType.startswith,
                Properties = new List<string>() { propertyName },
                Values = new List<string>() { value }
            });
            return this;
        }

        /// <summary>
        /// A string comparison expression that ends with the value.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public virtual IServiceQueryRequestBuilder EndsWith(string propertyName, string value)
        {
            Filters.Add(new ServiceQueryServiceFilter()
            {
                FilterType = ServiceQueryServiceFilterType.endswith,
                Properties = new List<string>() { propertyName },
                Values = new List<string>() { value }
            });
            return this;
        }

        /// <summary>
        /// An equality expression.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public virtual IServiceQueryRequestBuilder IsEqual(string propertyName, string value)
        {
            Filters.Add(new ServiceQueryServiceFilter()
            {
                FilterType = ServiceQueryServiceFilterType.equal,
                Properties = new List<string>() { propertyName },
                Values = new List<string>() { value }
            });
            return this;
        }

        /// <summary>
        /// Add an inequality expression.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public virtual IServiceQueryRequestBuilder IsNotEqual(string propertyName, string value)
        {
            Filters.Add(new ServiceQueryServiceFilter()
            {
                FilterType = ServiceQueryServiceFilterType.notequal,
                Properties = new List<string>() { propertyName },
                Values = new List<string>() { value }
            });
            return this;
        }

        /// <summary>
        /// Add a greater than expression.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public virtual IServiceQueryRequestBuilder IsGreaterThan(string propertyName, string value)
        {
            Filters.Add(new ServiceQueryServiceFilter()
            {
                FilterType = ServiceQueryServiceFilterType.greaterthan,
                Properties = new List<string>() { propertyName },
                Values = new List<string>() { value }
            });
            return this;
        }

        /// <summary>
        /// Add a greater than or equal to expression.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public virtual IServiceQueryRequestBuilder IsGreaterThanOrEqual(string propertyName, string value)
        {
            Filters.Add(new ServiceQueryServiceFilter()
            {
                FilterType = ServiceQueryServiceFilterType.greaterthanorequal,
                Properties = new List<string>() { propertyName },
                Values = new List<string>() { value }
            });
            return this;
        }

        /// <summary>
        /// Add a less than expression.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public virtual IServiceQueryRequestBuilder IsLessThan(string propertyName, string value)
        {
            Filters.Add(new ServiceQueryServiceFilter()
            {
                FilterType = ServiceQueryServiceFilterType.lessthan,
                Properties = new List<string>() { propertyName },
                Values = new List<string>() { value }
            });
            return this;
        }

        /// <summary>
        /// Add a less than or equal to expression.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public virtual IServiceQueryRequestBuilder IsLessThanOrEqual(string propertyName, string value)
        {
            Filters.Add(new ServiceQueryServiceFilter()
            {
                FilterType = ServiceQueryServiceFilterType.lessthanorequal,
                Properties = new List<string>() { propertyName },
                Values = new List<string>() { value }
            });
            return this;
        }

        /// <summary>
        /// Add a is null expression.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public virtual IServiceQueryRequestBuilder IsNull(string propertyName)
        {
            return Null(propertyName, true);
        }

        /// <summary>
        /// Add a null expression.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="isNull"></param>
        /// <returns></returns>
        public virtual IServiceQueryRequestBuilder Null(string propertyName, bool isNull)
        {
            ServiceQueryServiceFilter filter = new ServiceQueryServiceFilter();
            filter.Properties.Add(propertyName);
            if (isNull)
                filter.FilterType = ServiceQueryServiceFilterType.isnull;
            else
                filter.FilterType = ServiceQueryServiceFilterType.isnotnull;
            Filters.Add(filter);
            return this;
        }

        /// <summary>
        /// Add a is not null expression.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public virtual IServiceQueryRequestBuilder IsNotNull(string propertyName)
        {
            return Null(propertyName, false);
        }

        /// <summary>
        /// Add a in set expression.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public virtual IServiceQueryRequestBuilder IsInSet(string propertyName, params string[] values)
        {
            return Set(propertyName, true, values);
        }

        /// <summary>
        /// Add a set operation.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="inSet"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public virtual IServiceQueryRequestBuilder Set(string propertyName, bool inSet, params string[] values)
        {
            ServiceQueryServiceFilter filter = new ServiceQueryServiceFilter();

            filter.Properties.Add(propertyName);
            if (inSet)
                filter.FilterType = ServiceQueryServiceFilterType.inset;
            else
                filter.FilterType = ServiceQueryServiceFilterType.notinset;
            if (values == null)
                filter.Values.Add(null);
            else
            {
                foreach (string value in values)
                    filter.Values.Add(value);
            }
            Filters.Add(filter);
            return this;
        }

        /// <summary>
        /// Add a not in set operation.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public virtual IServiceQueryRequestBuilder IsNotInSet(string propertyName, params string[] values)
        {
            return Set(propertyName, false, values);
        }

        /// <summary>
        /// Add a sort expression.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="sortAscending"></param>
        /// <returns></returns>
        public virtual IServiceQueryRequestBuilder Sort(string propertyName, bool sortAscending)
        {
            ServiceQueryServiceFilter filter = new ServiceQueryServiceFilter();
            filter.Properties.Add(propertyName);
            if (sortAscending)
                filter.FilterType = ServiceQueryServiceFilterType.sortasc;
            else
                filter.FilterType = ServiceQueryServiceFilterType.sortdesc;
            Filters.Add(filter);
            return this;
        }

        /// <summary>
        /// Sort ascending.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public virtual IServiceQueryRequestBuilder SortAsc(string propertyName)
        {
            ServiceQueryServiceFilter filter = new ServiceQueryServiceFilter();
            filter.Properties.Add(propertyName);
            filter.FilterType = ServiceQueryServiceFilterType.sortasc;
            Filters.Add(filter);
            return this;
        }

        /// <summary>
        /// Sort descending.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public virtual IServiceQueryRequestBuilder SortDesc(string propertyName)
        {
            ServiceQueryServiceFilter filter = new ServiceQueryServiceFilter();
            filter.Properties.Add(propertyName);
            filter.FilterType = ServiceQueryServiceFilterType.sortdesc;
            Filters.Add(filter);
            return this;
        }

        /// <summary>
        /// Add an averate aggregate expression.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public virtual IServiceQueryRequestBuilder Average(string propertyName)
        {
            Filters.Add(new ServiceQueryServiceFilter()
            {
                FilterType = ServiceQueryServiceFilterType.average,
                Properties = new List<string> { propertyName }
            });
            return this;
        }

        /// <summary>
        /// Add a count aggregate expression.
        /// </summary>
        /// <returns></returns>
        public virtual IServiceQueryRequestBuilder Count()
        {
            Filters.Add(new ServiceQueryServiceFilter()
            {
                FilterType = ServiceQueryServiceFilterType.count
            });
            return this;
        }

        /// <summary>
        /// Add a maximum aggregate expression.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public virtual IServiceQueryRequestBuilder Maximum(string propertyName)
        {
            Filters.Add(new ServiceQueryServiceFilter()
            {
                FilterType = ServiceQueryServiceFilterType.maximum,
                Properties = new List<string> { propertyName }
            });
            return this;
        }

        /// <summary>
        /// Add a minumum aggregate expression.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public virtual IServiceQueryRequestBuilder Minimum(string propertyName)
        {
            Filters.Add(new ServiceQueryServiceFilter()
            {
                FilterType = ServiceQueryServiceFilterType.minimum,
                Properties = new List<string> { propertyName }
            });
            return this;
        }

        /// <summary>
        /// Add a sum aggregate expression.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public virtual IServiceQueryRequestBuilder Sum(string propertyName)
        {
            Filters.Add(new ServiceQueryServiceFilter()
            {
                FilterType = ServiceQueryServiceFilterType.sum,
                Properties = new List<string> { propertyName }
            });
            return this;
        }

        /// <summary>
        /// Include the Count() of records with the response.
        /// </summary>
        /// <returns></returns>
        public virtual IServiceQueryRequestBuilder IncludeCount()
        {
            var found = Filters.Where(x => x.FilterType == ServiceQueryServiceFilterType.includecount).FirstOrDefault();
            if (found != null)
                found.Values = new List<string>() { "true" };
            else
            {
                Filters.Add(new ServiceQueryServiceFilter()
                {
                    FilterType = ServiceQueryServiceFilterType.includecount,
                    Values = new List<string> { "true" }
                });
            }
            return this;
        }

        /// <summary>
        /// Setup paging.
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <param name="includeCount"></param>
        /// <returns></returns>
        public virtual IServiceQueryRequestBuilder Paging(int pageNumber, int pageSize, bool includeCount)
        {
            Filters.Add(new ServiceQueryServiceFilter()
            {
                FilterType = ServiceQueryServiceFilterType.pagenumber,
                Values = new List<string> { pageNumber.ToString() }
            });
            Filters.Add(new ServiceQueryServiceFilter()
            {
                FilterType = ServiceQueryServiceFilterType.pagesize,
                Values = new List<string> { pageSize.ToString() }
            });
            var found = Filters.Where(x => x.FilterType == ServiceQueryServiceFilterType.includecount).FirstOrDefault();
            if (found != null)
                found.Values = new List<string>() { includeCount.ToString() };
            else
            {
                Filters.Add(new ServiceQueryServiceFilter()
                {
                    FilterType = ServiceQueryServiceFilterType.includecount,
                    Values = new List<string> { includeCount.ToString() }
                });
            }
            return this;
        }
    }
}
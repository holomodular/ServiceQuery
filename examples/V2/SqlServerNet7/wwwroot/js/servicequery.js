class ServiceQueryServiceFilter {
    constructor() {
        this.filterType = '';
        this.properties = [];
        this.values = [];
    }
}

class ServiceQueryRequest {
    constructor() {
        this.filters = [];
    }
}

class ServiceQueryRequestBuilder {
    constructor() {
        this.filters = [];
    }

    static New() {
        return new ServiceQueryBuilder();
    }

    Build() {
        var req = new ServiceQueryRequest();
        req.filters = this.filters
        return req;
    }

    Begin() {
        var filter = new ServiceQueryServiceFilter();
        filter.filterType = 'begin';
        this.filters.push(filter);
        return this;
    }

    BeginExpression() {
        var filter = new ServiceQueryServiceFilter();
        filter.filterType = 'begin';
        this.filters.push(filter);
        return this;
    }

    End() {
        var filter = new ServiceQueryServiceFilter();
        filter.filterType = 'end';
        this.filters.push(filter);
        return this;
    }

    EndExpression() {
        var filter = new ServiceQueryServiceFilter();
        filter.filterType = 'end';
        this.filters.push(filter);
        return this;
    }

    And() {
        var filter = new ServiceQueryServiceFilter();
        filter.filterType = 'and';
        this.filters.push(filter);
        return this;
    }

    Or() {
        var filter = new ServiceQueryServiceFilter();
        filter.filterType = 'or';
        this.filters.push(filter);
        return this;
    }

    Select(...props) {
        var filter = new ServiceQueryServiceFilter();
        filter.filterType = 'select';
        for (var i = 0; i < props.length; i++) {
            filter.properties.push(props[i]);
        }
        this.filters.push(filter);
        return this;
    }

    Distinct() {
        var filter = new ServiceQueryServiceFilter();
        filter.filterType = 'distinct';
        this.filters.push(filter);
        return this;
    }

    Between(propertyName, lowValue, highValue) {
        var filter = new ServiceQueryServiceFilter();
        filter.filterType = 'between';
        filter.properties.push(propertyName);
        filter.values.push(lowValue);
        filter.values.push(highValue);
        this.filters.push(filter);
        return this;
    }

    Contains(propertyName, val) {
        var filter = new ServiceQueryServiceFilter();
        filter.filterType = 'contains';
        filter.properties.push(propertyName);
        filter.values.push(val);
        this.filters.push(filter);
        return this;
    }

    StartsWith(propertyName, val) {
        var filter = new ServiceQueryServiceFilter();
        filter.filterType = 'startswith';
        filter.properties.push(propertyName);
        filter.values.push(val);
        this.filters.push(filter);
        return this;
    }

    EndsWith(propertyName, val) {
        var filter = new ServiceQueryServiceFilter();
        filter.filterType = 'endswith';
        filter.properties.push(propertyName);
        filter.values.push(val);
        this.filters.push(filter);
        return this;
    }

    IsEqual(propertyName, val) {
        var filter = new ServiceQueryServiceFilter();
        filter.filterType = 'equal';
        filter.properties.push(propertyName);
        filter.values.push(val);
        this.filters.push(filter);
        return this;
    }

    IsNotEqual(propertyName, val) {
        var filter = new ServiceQueryServiceFilter();
        filter.filterType = 'notequal';
        filter.properties.push(propertyName);
        filter.values.push(val);
        this.filters.push(filter);
        return this;
    }

    IsGreaterThan(propertyName, val) {
        var filter = new ServiceQueryServiceFilter();
        filter.filterType = 'greaterthan';
        filter.properties.push(propertyName);
        filter.values.push(val);
        this.filters.push(filter);
        return this;
    }

    IsGreaterThanOrEqual(propertyName, val) {
        var filter = new ServiceQueryServiceFilter();
        filter.filterType = 'greaterthanorequal';
        filter.properties.push(propertyName);
        filter.values.push(val);
        this.filters.push(filter);
        return this;
    }

    IsLessThan(propertyName, val) {
        var filter = new ServiceQueryServiceFilter();
        filter.filterType = 'lessthan';
        filter.properties.push(propertyName);
        filter.values.push(val);
        this.filters.push(filter);
        return this;
    }

    IsLessThanOrEqual(propertyName, val) {
        var filter = new ServiceQueryServiceFilter();
        filter.filterType = 'lessthanorequal';
        filter.properties.push(propertyName);
        filter.values.push(val);
        this.filters.push(filter);
        return this;
    }

    IsNull(propertyName) {
        var filter = new ServiceQueryServiceFilter();
        filter.filterType = 'isnull';
        filter.properties.push(propertyName);
        this.filters.push(filter);
        return this;
    }

    IsNotNull(propertyName) {
        var filter = new ServiceQueryServiceFilter();
        filter.filterType = 'isnotnull';
        filter.properties.push(propertyName);
        this.filters.push(filter);
        return this;
    }

    IsInSet(propertyName, inSet, ...vals) {
        var filter = new ServiceQueryServiceFilter();
        if (inSet) {
            filter.filterType = 'inset';
        }
        else {
            filter.filterType = 'notinset';
        }
        filter.properties.push(propertyName);
        for (var i = 0; i < vals.length; i++) {
            filter.values.push(vals[i]);
        }
        this.filters.push(filter);
        return this;
    }

    IsInSet(propertyName, ...vals) {
        var filter = new ServiceQueryServiceFilter();
        filter.filterType = 'inset';
        filter.properties.push(propertyName);
        for (var i = 0; i < vals.length; i++) {
            filter.values.push(vals[i]);
        }
        this.filters.push(filter);
        return this;
    }

    IsNotInSet(propertyName, ...vals) {
        var filter = new ServiceQueryServiceFilter();
        filter.filterType = 'notinset';
        filter.properties.push(propertyName);
        for (var i = 0; i < vals.length; i++) {
            filter.values.push(vals[i]);
        }
        this.filters.push(filter);
        return this;
    }

    Sort(propertyName) {
        var filter = new ServiceQueryServiceFilter();
        filter.filterType = 'sortasc';
        filter.properties.push(propertyName);
        this.filters.push(filter);
        return this;
    }

    Sort(propertyName, sortAsc) {
        var filter = new ServiceQueryServiceFilter();
        if (sortAsc) {
            filter.filterType = 'sortasc';
        } else {
            filter.filterType = 'sortdesc';
        }
        filter.properties.push(propertyName);
        this.filters.push(filter);
        return this;
    }

    SortAsc(propertyName) {
        var filter = new ServiceQueryServiceFilter();
        filter.filterType = 'sortasc';
        filter.properties.push(propertyName);
        this.filters.push(filter);
        return this;
    }

    SortDesc(propertyName) {
        var filter = new ServiceQueryServiceFilter();
        filter.filterType = 'sortdesc';
        filter.properties.push(propertyName);
        this.filters.push(filter);
        return this;
    }

    Average(propertyName) {
        var filter = new ServiceQueryServiceFilter();
        filter.filterType = 'average';
        filter.properties.push(propertyName);
        this.filters.push(filter);
        return this;
    }

    Count() {
        var filter = new ServiceQueryServiceFilter();
        filter.filterType = 'count';
        this.filters.push(filter);
        return this;
    }

    Maximum(propertyName) {
        var filter = new ServiceQueryServiceFilter();
        filter.filterType = 'maximum';
        filter.properties.push(propertyName);
        this.filters.push(filter);
        return this;
    }

    Minimum(propertyName) {
        var filter = new ServiceQueryServiceFilter();
        filter.filterType = 'minimum';
        filter.properties.push(propertyName);
        this.filters.push(filter);
        return this;
    }

    Sum(propertyName) {
        var filter = new ServiceQueryServiceFilter();
        filter.filterType = 'sum';
        filter.properties.push(propertyName);
        this.filters.push(filter);
        return this;
    }

    IncludeCount() {
        var filter = new ServiceQueryServiceFilter();
        filter.filterType = 'includecount';
        this.filters.push(filter);
        return this;
    }

    Paging(pageNumber, pageSize, includeCount) {
        var filter = new ServiceQueryServiceFilter();
        filter.filterType = 'pagenumber';
        filter.values.push(pageNumber);
        this.filters.push(filter);
        filter = new ServiceQueryServiceFilter();
        filter.filterType = 'pagesize';
        filter.values.push(pageSize);
        this.filters.push(filter);
        filter = new ServiceQueryServiceFilter();
        filter.filterType = 'includecount';
        filter.values.push(includeCount);
        this.filters.push(filter);
        return this;
    }
}
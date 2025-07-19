<img src="https://github.com/holomodular/ServiceQuery/blob/main/Logo.png" title="ServiceQuery Logo" width="250"/>

[![NuGet version](https://badge.fury.io/nu/ServiceQuery.svg)](https://badge.fury.io/nu/ServiceQuery)
![badge](https://img.shields.io/endpoint?url=https://gist.githubusercontent.com/holomodular-support/5a0fab9a9341bb94e27da49a1e89fd03/raw/servicequery-codecoverage.json)
[![License: MIT](https://img.shields.io/badge/License-MIT-389DA0.svg)](https://opensource.org/licenses/MIT)

# ServiceQuery: Dynamic Data Querying Over REST APIs
## Overview
Welcome to [ServiceQuery](https://ServiceQuery.com), the open-source library designed to revolutionize your data querying over REST APIs. Similar to OData and GraphQL, ServiceQuery harnesses the power of an expression builder and a straightforward model to serialize query instructions across service boundaries. It seamlessly supports a wide array of popular relational (SQL), document (NoSQL), cloud and embedded database engines, as well as in-memory lists. Front-end applications gain unprecedented querying capabilities through a standardized endpoint supporting polyglot data access across all database providers. 

## Get Started
### Installation is simple:
Install the NuGet Package **ServiceQuery**

## Why ServiceQuery?
* **Powerful:** Provides front-end and back-end applications unprecedented dynamic querying capabilities with ease.
* **Secure:** Utilizing the IQueryable interface, it builds dynamic LINQ expressions using individually mapped functions and parsed data, eliminating injection attacks.
* **Versatile:** Supports numerous database engines including Azure Data Tables, Cosmos DB, MongoDB, MySQL, SQLite, SQL Server, PostgreSQL, Oracle, and many more.

## Examples
Explore our [Examples Repository](https://github.com/holomodular/ServiceQuery-Examples) for detailed implementations using the most popular database storage providers.

## We Value Your Feedback
Join our discussion board to post any questions or issues. Don't forget to star our repository. For direct support, reach us at: Support@HoloModular.com

## Dynamic Querying Made Easy
Here's how you can dynamically query data using JavaScript:
Make sure to include the following [ServiceQuery.js](https://github.com/holomodular/ServiceQuery/blob/main/src/V2/javascript/servicequery.js) javascript file to quickly build request queries in javascript.
```javascript
<script src="/js/servicequery.js"></script>
<script type="text/javascript">

  function GetById() {

    // Build the request where id = 123
    var request = new ServiceQueryRequestBuilder().IsEqual("Id","123").Build();

    // Send ajax request to REST Controller
    $.ajax({
        url: '/ExampleServiceQuery',
        data: JSON.stringify(request),
        type: "POST",
        dataType: 'json',
        headers: { 'Content-Type': 'application/json' },
        success: function (result) {

          // Output the response
          alert(result.list.length + ' records returned');
        }
    });
  }
</script>
```
On the server side, convert the request into IQueryable expressions and return the result (sync or async):
```csharp
using ServiceQuery;

[HttpPost]
[Route("ExampleServiceQuery")]
public ServiceQueryResponse<ExampleTable> ExampleServiceQuery(ServiceQueryRequest request)
{
  var queryable = databaseContext.ExampleTable.AsQueryable();
  return request.Execute(queryable);
}

[HttpPost]
[Route("ExampleServiceQueryAsync")]
public async Task<ServiceQueryResponse<ExampleTable>> ExampleServiceQueryAsync(ServiceQueryRequest request)
{
  var queryable = databaseContext.ExampleTable.AsQueryable();
  return await request.ExecuteAsync(queryable);
}
```

## Documentation
Comprehensive documentation is available on our website at http://ServiceQuery.com including tables for supported data types and operations by .NET Framework version and database engine.

## ServiceQuery.AzureDataTables
Azure Data Tables has certain limitations, such as lack of support for aggregate functions, string comparisons and ordering. 
Our companion NuGet package <b>ServiceQuery.AzureDataTables</b> provides a solution to these limitations, allowing you to use standard operations and execute requests seamlessly. The solution is to download all records and then perform the query using an internal list. See our example project for more information.

## Building and Executing a Query
Construct queries using the ServiceQueryRequestBuilder object:
```csharp
using ServiceQuery;

public void Example()
{
  var request = new ServiceQueryRequestBuilder().Build();
  var queryable = databaseContext.ExampleTable.AsQueryable();
  
  var response = request.Execute(queryable); // sync support  
  var responseasync = await request.ExecuteAsync(queryable); // async support

  List<ExampleTable> list = response.List; // contains the list of objects returned from the query
  int? count = response.Count; // returns the count of the query (if requested)
  double? aggregate = response.Aggregate; // returns the aggregate value (if requested)
}
```

## Supported Operations
### Aggregate Functions
* Average
* Count
* Maximum
* Minimum
* Sum

### Comparison Functions
* Between
* Equal
* Not Equal
* Less Than
* Less Than or Equal
* Greater Than
* Greater Than or Equal
* In Set
* Not In Set

### Comparison Functions (string)
* Contains
* StartsWith
* EndsWith

### Grouping Functions
* And
* Or
* Begin
* End

### Nullability Functions
* Null
* Not Null

### Paging Functions
* Page Number
* Page Size
* Include Count

### Selecting Functions
* Distinct
* Select

### Sorting Functions
* Sort Ascending
* Sort Descending


## Using Query Operations
If you are using javascript, make sure to download the [ServiceQuery.js](https://github.com/holomodular/ServiceQuery/blob/main/src/V2/javascript/servicequery.js) javascript file. This allows you to use the same syntax as the .NET code below!
```csharp
  using ServiceQuery;

  var request = new ServiceQueryRequestBuilder().Build();

  // This is the same as just a new object
  request = new ServiceQueryRequestBuilder()
    .Paging(1, 1000, false)
    .Build();

  // Include the count of records with the response
  request = new ServiceQueryRequestBuilder()
    .IsGreaterThan("id","10")
    .IncludeCount()
    .Build();

  // Select only the properties you want
  request = new ServiceQueryRequestBuilder()
    .Select("Id","FirstName","LastName")
    .Build();
  
  // Build AND expressions 
  request = new ServiceQueryRequestBuilder()
    .IsEqual("Id","1")
    .And()
    .StartsWith("FirstName", "John")    
    .Build();

  // Build OR expressions
  request = new ServiceQueryRequestBuilder()
    .Between("Id","1", "5")
    .Or()
    .Contains("LastName", "Smith")    
    .Build();

  // Group expressions with BEGIN, END, AND and OR. Nest as deeply as needed.
  request = new ServiceQueryRequestBuilder()
    .Begin()
      .IsEqual("Id","1")
      .And()
      .IsInSet("Status", "Created", "Open", "InProcess")
    .End()
    .Or()
    .Begin()
      .IsLessThanOrEqual("BirthDate","1/1/2000")
      .And()
      .IsNull("CloseDate")
    .End()
    .Build();

  // Sorting
  request = new ServiceQueryRequestBuilder()
    .IsEqual("Age", "21")
    .SortAsc("FirstName")
    .Build();

  // Aggregate functions
  request = new ServiceQueryRequestBuilder()
    .IsLessThan("Id", "200")
    .Sum("Price")
    .Build();

```

## ServiceQuery Options
Customize server-side query processing with ServiceQueryOptions object:
```csharp
    public class ServiceQueryOptions
    {
        public Dictionary<string, string> PropertyNameMappings { get; set; }
        public bool PropertyNameCaseSensitive { get; set; }
        public bool AllowMissingExpressions { get; set; }
    }
```

## Advanced Usage Scenarios

### Manipulate queries before executing them
Add to, change or remove filters from incoming queries for business reasons.

### Restrict properties by user roles
Adjust property mappings based on user role for security.

### Sharding data
Add expressions to queries to target specific data segments, ensuring efficient data retrieval and enhanced security.

## About
Authored by https://www.linkedin.com/in/danlogsdon Visit https://HoloModular.com or https://ServiceQuery.com to learn more.

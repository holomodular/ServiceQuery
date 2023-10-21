<img src="https://github.com/holomodular/ServiceQuery/blob/main/examples/InMemory7/wwwroot/Logo.png" title="ServiceQuery Logo" width="250"/>

# Service Query Allows Querying Data Over REST APIs
Service Query (http://ServiceQuery.com) is an open-source library that allows dynamically querying database information over REST APIs. It leverages the power of an expressions builder and a simple model that is capable of serializing query instructions over service boundaries, similar to how ODATA works but better. It supports numerous popular relational (SQL) and document (NoSQL) database engines that expose an IQueryable interface. This provides clients and front end applications unprecedented queryability as well as a standardized endpoint for a microservice-based architecture supporting polyglot data access to system database data.

# Installation Instructions
Install the NuGet Package <b>ServiceQuery</b>

# Examples
We have numerous examples built using the most popular database storage providers, such as Azure Data Tables, Cosmos DB, MongoDB, MySQL, SQLite, SQL Server, PostgreSQL, Oracle and more! 
View all our examples in the [examples](https://github.com/holomodular/ServiceQuery/blob/main/examples) folder in this project.

# Feedback
We want to hear from our users. Please feel free to post any issues or questions on our discussion board. You can star our repository or you can also reach us at: Support@HoloModular.com

# Simple Example - Dynamic Querying Using Javascript
Modeled based on LINQ, on your client or web page, you create a simple request object that is sent over a REST API endpoint to your web server. Make sure to include the following [ServiceQuery.js](https://servicequery.com/js/servicequery.js) javascript file to quickly build request queries in javascript.
```javascript
<script src="/js/servicequery.js"></script>
<script type="text/javascript">
  function GetAll() {
    var request = new ServiceQueryRequestBuilder().Build();
    $.ajax({
        url: '/api/MyAPI/ExampleServiceQuery',
        data: JSON.stringify(request),
        type: "POST",
        dataType: 'json',
        headers: { 'Content-Type': 'application/json' },
        success: function (result) {
          alert(result.list.length + ' records returned');
        }
    });
  }
</script>
```
On the web server, the request is safely converted into IQueryable expressions that can be executed against several different database engines.
The following exposes a REST API POST method that the client will call.
```csharp
using ServiceQuery;

[HttpPost]
[Route("ExampleServiceQuery")]
public ServiceQueryResponse<ExampleTable> ExampleServiceQuery(ServiceQueryRequest request)
{
  var queryable = _context.ExampleTable.AsQueryable(); //_context is your database context
  return request.Execute(queryable);
}
```

# Documentation
Documentation is located on our website at (http://ServiceQuery.com) as well as a simplified version below. The website also contains tables for supported data types and operations by .NET Framework version and database engine.

## ServiceQuery.AzureDataTables
AzureDataTables does not support several things out of the box, such as string comparisons and ordering (solved by downloading all records). We have built a companion NuGet package <b>ServiceQuery.AzureDataTables</b> that provides workarounds to get around these limitations and paging (supporting more than 1000 records), as well as a simple wrapper to execute the request in one line of code. See our example project for more information.

## Building and Executing a Query
Building a query is accomplish using the ServiceQueryRequestBuilder object to create the request.
```csharp
using ServiceQuery;

public void Example()
{
  var request = new ServiceQueryRequestBuilder().Build();
  var queryable = context.ExampleTable.AsQueryable();
  var response = request.Execute(queryable);

  List<ExampleTable> list = response.List; // contains the list of objects returned from the query
  long? count = response.Count; // returns the count of the query (if requested)
  double? aggregate = response.Aggregate; // returns the aggregate (if requested)
}
```

## Supported Operations
Aggregate Functions
* Average
* Count
* Maximum
* Minimum
* Sum

Comparison Functions
* Between
* Equal
* Not Equal
* Less Than
* Less Than or Equal
* Greater Than
* Greater Than or Equal
* In Set
* Not In Set

Grouping Functions
* And
* Or
* Begin
* End

String Comparison Functions
* Contains
* StartsWith
* EndsWith

Nullability Functions
* Null
* Not Null

Sorting Functions
* Sort Ascending
* Sort Descending

Selecting Functions
* Distinct
* Select

## Using Query Operations
If you are using javascript, make sure to download the [ServiceQuery.js](https://servicequery.com/js/servicequery.js) javascript file. This allows you to use the same syntax as the .NET code below!
```csharp
  using ServiceQuery;

  var request = new ServiceQueryRequestBuilder().Build();

  // This is the same as just a new object
  request = new ServiceQueryRequestBuilder()
    .Paging(1, 1000, false)
    .Build();

  request = new ServiceQueryRequestBuilder()
    .IsGreaterThan("id","1")
    .Build();

  request = new ServiceQueryRequestBuilder()
    .IsEqual("id","1")
    .Select("Id","FirstName","LastName")
    .Build();
  
  request = new ServiceQueryRequestBuilder()
    .IsEqual("Id","1")
    .And() // You can also comment this out, an And() will be added by default
    .StartsWith("FirstName", "John")    
    .Build();

  request = new ServiceQueryRequestBuilder()
    .Between("Id","1", "5")
    .Or()
    .Contains("LastName", "Smith")    
    .Build();

  request = new ServiceQueryRequestBuilder()
    .Begin()
      .IsEqual("Id","1")
      .And()
      .IsInSet("Status", "Created", "Open", "InProcess")
    .End()
    .Or()
    .Begin()
      .LessThanOrEqual("BirthDate","1/1/2000")
      .And()
      .IsNull("CloseDate")
    .End()
    .Build();

  request = new ServiceQueryRequestBuilder()
    .IsEqual("Age", "21")
    .Count()
    .Build();

  request = new ServiceQueryRequestBuilder()
    .IsLessThan("Id", "200")
    .Sum("Age")
    .Build();

```

<img src="https://github.com/holomodular/ServiceQuery/blob/main/Logo.png" title="ServiceQuery Logo" width="250"/>

# ServiceQuery Allows Querying Data Over REST APIs
ServiceQuery, https://ServiceQuery.com, is an open-source library that allows dynamically querying database information over REST APIs. Similar to how OData and GraphQL work, it leverages the power of an expressions builder and a simple model that is capable of serializing query instructions over service boundaries. It supports numerous popular relational (SQL) and document (NoSQL) database engines that expose an IQueryable interface. ServiceQuery builds LINQ expressions using individually mapped functions and parsed data that eliminates injection attacks, so querying your data is safe and secure. This library provides clients and front end applications unprecedented queryability using a standardized endpoint supporting polyglot data access. 

# Installation Instructions
Install the NuGet Package <b>ServiceQuery</b>

# Examples
We have numerous examples built using the most popular database storage providers, such as Azure Data Tables, Cosmos DB, MongoDB, MySQL, SQLite, SQL Server, PostgreSQL, Oracle and more! 
View all our examples in the [examples](https://github.com/holomodular/ServiceQuery/blob/main/examples) folder in this project.

# Feedback
We want to hear from our users. Please feel free to post any issues or questions on our discussion board. You can star our repository or you can also reach us at: Support@HoloModular.com

# Simple Example - Dynamic Querying Using Javascript
Modeled based on LINQ, you create a simple request object that is sent over a REST API endpoint to your web server. Make sure to include the following [ServiceQuery.js](https://github.com/holomodular/ServiceQuery/blob/main/src/V2/javascript/servicequery.js) javascript file to quickly build request queries in javascript.
```javascript
<script src="/js/servicequery.js"></script>
<script type="text/javascript">

  function GetById() {

    // Build the request
    var request = new ServiceQueryRequestBuilder().IsEqual("Id","123").Build();

    // Send request to REST API
    $.ajax({
        url: '/api/MyAPI/ExampleServiceQuery',
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
On the web server, the request is safely converted into IQueryable expressions that can be executed against several different database engines.
The following exposes a REST API POST method that the client will call.
```csharp
using ServiceQuery;

[HttpPost]
[Route("ExampleServiceQuery")]
public ServiceQueryResponse<ExampleTable> ExampleServiceQuery(ServiceQueryRequest request)
{
  var queryable = databaseContext.ExampleTable.AsQueryable();
  return request.Execute(queryable);
}
```

# Documentation
Documentation is located on our website at http://ServiceQuery.com as well as a simplified version below. The website also contains tables for supported data types and operations by .NET Framework version and database engine.

## ServiceQuery.AzureDataTables
AzureDataTables does not support several things out of the box, such as aggregates, string comparisons and ordering (solved by downloading all records). We have built a companion NuGet package <b>ServiceQuery.AzureDataTables</b> that provides workarounds to these limitations so you can use all standard operations and execute the request in one line of code. See our example projects for more information.

## Building and Executing a Query
Building a query is accomplished using the ServiceQueryRequestBuilder object to create the request.
```csharp
using ServiceQuery;

public void Example()
{
  var request = new ServiceQueryRequestBuilder().Build();
  var queryable = databaseContext.ExampleTable.AsQueryable();
  var response = request.Execute(queryable);

  List<ExampleTable> list = response.List; // contains the list of objects returned from the query
  int? count = response.Count; // returns the count of the query (if requested)
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

Comparison Functions (string)
* Contains
* StartsWith
* EndsWith

Grouping Functions
* And
* Or
* Begin
* End

Nullability Functions
* Null
* Not Null

Paging Functions
* Page Number
* Page Size
* Include Count

Selecting Functions
* Distinct
* Select

Sorting Functions
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
    .Sum("Age")
    .Build();

```

## ServiceQuery Options
We currently provide the following server-side options when processing queries.
```csharp
    public class ServiceQueryOptions
    {
        /// <summary>
        /// Dictionary list of property name mappings.
        /// Exposed Class -> Internal Class
        /// Default will use all queryable class property names
        /// </summary>
        public Dictionary<string, string> PropertyNameMappings { get; set; }

        /// <summary>
        /// Determine whether property names must be case sensitive or throw an exception.
        /// Default is false.
        /// </summary>
        public bool PropertyNameCaseSensitive { get; set; }
        
	/// <summary>
	/// Determine if missing BEGIN/END/AND/OR expressions throw an exception
	/// or try to add them missing ones automatiically.
	/// </summary>
        public bool AllowMissingExpressions { get; set; }
    }
```

# Roadmap
There are several new features planned and currently in development. Visit the Issues page at the top to view the current list. Let us know if you have any requests.

# About
I am a business executive and software architect with over 26 years professional experience. You can reach me via www.linkedin.com/in/danlogsdon or https://HoloModular.com

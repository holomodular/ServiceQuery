# Welcome to Service Query

Service Query allows dynamic querying of data over service boundaries.
Visit http://ServiceQuery.com to learn more.

## Overview

Service Query provides a light-weight model for building complex query expressions that can be serialized for REST API usage.
It is a thin abstraction layer that is storage provider agnostic.
It dynamically builds Linq expressions and functions supporting IQueryable integration.
The model allows mapping of property names so you can perform any translations needed quickly.

## Usage

1) Use the ServiceQueryRequestBuilder object to create a request.
2) Get a IQueryable interface from your data store
3) Execute the request and return the response

var request = ServiceQueryBuilder.New().Build();
var queryable = databaseContext.ExampleTable.AsQueryable();
var response = request.Execute(queryable);


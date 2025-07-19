# 2.2.2
- Added new package ServiceQuery.EntityFrameworkCore8 to specifically target EFC 8 since base library uses EFC 9 for .NET 8 runtime
- Update MongoDb driver to 3.4.1
- Update test project references to latest versions. Note, NET6 and NET7 no longer work with updated test references and must use SuppressTfmSupportBuildErrors since out of support
- Expose GetFilterSet method and fix exception thrown
- Update readme

# 2.2.1
- Added target frameworks for Net46, Net47, Net471, Net472, Net48, Net481
- Relaxed ServiceQuery.EntityFrameworkCore references for Net 6, 7, 8 and 9 to be base 0 versions
- Changed ServiceQuery.EntityFrameworkCore Net8 to target EntityFrameworkCore 9 
- Updated MongoDb driver to 3.2.1
- Fixes for compiler warnings in tests

# 2.2.0
- Support async operations with an inmemory provider
- Added new NuGet packages for ServiceQuery.EntityFrameworkCore and ServiceQuery.MongoDb for async support

# 2.1.1
- Support sorting for nullable datatypes
- Update project structure for future release versions

# 2.1.0
- Support for datatypes DateOnly, TimeOnly and UInt128
- Unit test code coverage at 95%

# 2.0.1
- Change ServiceQueryServiceFilter.FilterType enum to a string for easier serialization

# 2.0.0
- Refactor library to contain no external nuget package references

# 1.0.8
- Support .NET 8.0

# 1.0.6
- DateTime parsing using System.Globalization.DateTimeStyles.RoundtripKind

# 1.0.5
- Full source code release with unit and integration tests


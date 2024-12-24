using Microsoft.EntityFrameworkCore;

namespace ServiceQuery.Xunit
{
    public class PagingTestsAsyncEfc : BaseTest
    {
        [Fact]
        public async Task NegativePagingRequestTest()
        {
            var sourceQueryable = GetTestList();
            IServiceQueryRequest serviceQuery;
            ServiceQueryResponse<TestClass> result;

            serviceQuery = ServiceQueryRequestBuilder.New().Paging(0, 0, false).Build();
            serviceQuery.Filters.Where(x =>
            x.FilterType == "pagenumber").First().Values = new List<string>() { "a" };
            Assert.Throws<ServiceQueryException>(() =>
            {
                result = serviceQuery.Execute(sourceQueryable);
            });
            Assert.Throws<ServiceQueryException>(() =>
            {
                result = serviceQuery.Execute<TestClass>(sourceQueryable);
            });

            serviceQuery = ServiceQueryRequestBuilder.New().Paging(0, 0, false).Build();
            serviceQuery.Filters.Where(x =>
            x.FilterType == "pagesize").First().Values = new List<string>() { "a" };
            Assert.Throws<ServiceQueryException>(() =>
            {
                result = serviceQuery.Execute(sourceQueryable);
            });

            serviceQuery = ServiceQueryRequestBuilder.New().Paging(0, 0, false).Build();
            serviceQuery.Filters.Where(x =>
            x.FilterType == "includecount").First().Values = new List<string>() { "a" };
            Assert.Throws<ServiceQueryException>(() =>
            {
                result = serviceQuery.Execute(sourceQueryable);
            });
        }

        [Fact]
        public async Task PagingStandardTest()
        {
            var sourceQueryable = GetTestList();
            IServiceQuery serviceQuery;
            ServiceQueryResponse<TestClass> result;

            serviceQuery = ServiceQueryBuilder.New().Paging(0, 0, false).Build();
            result = serviceQuery.Execute(sourceQueryable);
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
            Assert.True(!result.Aggregate.HasValue);
            Assert.NotNull(result.List);
            Assert.True(result.List.Count == 0);

            serviceQuery = ServiceQueryBuilder.New().Paging(0, 0, true).Build();
            result = serviceQuery.Execute(sourceQueryable);
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
            Assert.True(!result.Aggregate.HasValue);
            Assert.NotNull(result.List);
            Assert.True(result.List.Count == 0);
            result = serviceQuery.Execute(sourceQueryable);
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
            Assert.True(!result.Aggregate.HasValue);
            Assert.NotNull(result.List);
            Assert.True(result.List.Count == 0);

            serviceQuery = ServiceQueryBuilder.New().Paging(1, 1, true).Build();
            result = serviceQuery.Execute(sourceQueryable);
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
            Assert.True(!result.Aggregate.HasValue);
            Assert.NotNull(result.List);
            Assert.True(result.List.Count == 1);

            serviceQuery = ServiceQueryBuilder.New().Paging(1, 2, true).Build();
            result = serviceQuery.Execute(sourceQueryable);
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
            Assert.True(!result.Aggregate.HasValue);
            Assert.NotNull(result.List);
            Assert.True(result.List.Count == 2);

            serviceQuery = ServiceQueryBuilder.New().Paging(1, 3, true).Build();
            result = serviceQuery.Execute(sourceQueryable);
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
            Assert.True(!result.Aggregate.HasValue);
            Assert.NotNull(result.List);
            Assert.True(result.List.Count == 3);

            serviceQuery = ServiceQueryBuilder.New().Paging(2, 1, true).Build();
            result = serviceQuery.Execute(sourceQueryable);
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
            Assert.True(!result.Aggregate.HasValue);
            Assert.NotNull(result.List);
            Assert.True(result.List.Count == 1);

            serviceQuery = ServiceQueryBuilder.New().Paging(2, 2, true).Build();
            result = serviceQuery.Execute(sourceQueryable);
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
            Assert.True(!result.Aggregate.HasValue);
            Assert.NotNull(result.List);
            Assert.True(result.List.Count == 2);

            serviceQuery = ServiceQueryBuilder.New().Paging(2, 3, true).Build();
            result = serviceQuery.Execute(sourceQueryable);
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
            Assert.True(!result.Aggregate.HasValue);
            Assert.NotNull(result.List);
            Assert.True(result.List.Count == 1);

            serviceQuery = ServiceQueryBuilder.New().Paging(3, 3, true).Build();
            result = serviceQuery.Execute(sourceQueryable);
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
            Assert.True(!result.Aggregate.HasValue);
            Assert.NotNull(result.List);
            Assert.True(result.List.Count == 0);

            serviceQuery = ServiceQueryBuilder.New().Paging(3, 1, true).Build();
            result = serviceQuery.Execute(sourceQueryable);
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
            Assert.True(!result.Aggregate.HasValue);
            Assert.NotNull(result.List);
            Assert.True(result.List.Count == 1);

            serviceQuery = ServiceQueryBuilder.New().Paging(3, 2, true).Build();
            result = serviceQuery.Execute(sourceQueryable);
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
            Assert.True(!result.Aggregate.HasValue);
            Assert.NotNull(result.List);
            Assert.True(result.List.Count == 0);

            serviceQuery = ServiceQueryBuilder.New().Paging(10, 10, true).Build();
            result = serviceQuery.Execute(sourceQueryable);
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
            Assert.True(!result.Aggregate.HasValue);
            Assert.NotNull(result.List);
            Assert.True(result.List.Count == 0);

            serviceQuery = ServiceQueryBuilder.New().IsEqual(nameof(TestClass.SharedParentKey), "1").Paging(1, 2, true).Build();
            result = serviceQuery.Execute(sourceQueryable);
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
            Assert.True(!result.Aggregate.HasValue);
            Assert.NotNull(result.List);
            Assert.True(result.List.Count == 2);

            serviceQuery = ServiceQueryBuilder.New().IsEqual(nameof(TestClass.IntVal), "1").Paging(1, 2, true).Build();
            result = serviceQuery.Execute(sourceQueryable);
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            Assert.True(!result.Aggregate.HasValue);
            Assert.NotNull(result.List);
            Assert.True(result.List.Count == 1);

            serviceQuery = ServiceQueryBuilder.New().IsEqual(nameof(TestClass.BoolVal), "true").Paging(1, 2, true).Build();
            result = serviceQuery.Execute(sourceQueryable);
            Assert.NotNull(result);
            Assert.True(result.Count == 2);
            Assert.True(!result.Aggregate.HasValue);
            Assert.NotNull(result.List);
            Assert.True(result.List.Count == 2);

            serviceQuery = ServiceQueryBuilder.New().IsEqual(nameof(TestClass.BoolVal), "true").Paging(1, 2, false).Build();
            result = serviceQuery.Execute(sourceQueryable);
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
            Assert.True(!result.Aggregate.HasValue);
            Assert.NotNull(result.List);
            Assert.True(result.List.Count == 2);
        }

        [Fact]
        public async Task PagingRequestTest()
        {
            var sourceQueryable = GetTestList();
            IServiceQueryRequest serviceQuery;
            ServiceQueryResponse<TestClass> result;

            serviceQuery = ServiceQueryRequestBuilder.New().Paging(0, 0, false).Build();
            result = serviceQuery.GetServiceQuery().Execute(sourceQueryable);
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
            Assert.True(!result.Aggregate.HasValue);
            Assert.NotNull(result.List);
            Assert.True(result.List.Count == 0);
            result = serviceQuery.GetServiceQuery().Execute(sourceQueryable);
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
            Assert.True(!result.Aggregate.HasValue);
            Assert.NotNull(result.List);
            Assert.True(result.List.Count == 0);

            serviceQuery = ServiceQueryRequestBuilder.New().Paging(0, 0, true).Build();
            result = serviceQuery.GetServiceQuery().Execute(sourceQueryable);
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
            Assert.True(!result.Aggregate.HasValue);
            Assert.NotNull(result.List);
            Assert.True(result.List.Count == 0);

            serviceQuery = ServiceQueryRequestBuilder.New().Paging(1, 1, true).Build();
            result = serviceQuery.GetServiceQuery().Execute(sourceQueryable);
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
            Assert.True(!result.Aggregate.HasValue);
            Assert.NotNull(result.List);
            Assert.True(result.List.Count == 1);

            serviceQuery = ServiceQueryRequestBuilder.New().Paging(1, 2, true).Build();
            result = serviceQuery.GetServiceQuery().Execute(sourceQueryable);
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
            Assert.True(!result.Aggregate.HasValue);
            Assert.NotNull(result.List);
            Assert.True(result.List.Count == 2);

            serviceQuery = ServiceQueryRequestBuilder.New().Paging(1, 3, true).Build();
            result = serviceQuery.GetServiceQuery().Execute(sourceQueryable);
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
            Assert.True(!result.Aggregate.HasValue);
            Assert.NotNull(result.List);
            Assert.True(result.List.Count == 3);

            serviceQuery = ServiceQueryRequestBuilder.New().Paging(2, 1, true).Build();
            result = serviceQuery.GetServiceQuery().Execute(sourceQueryable);
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
            Assert.True(!result.Aggregate.HasValue);
            Assert.NotNull(result.List);
            Assert.True(result.List.Count == 1);

            serviceQuery = ServiceQueryRequestBuilder.New().Paging(2, 2, true).Build();
            result = serviceQuery.GetServiceQuery().Execute(sourceQueryable);
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
            Assert.True(!result.Aggregate.HasValue);
            Assert.NotNull(result.List);
            Assert.True(result.List.Count == 2);

            serviceQuery = ServiceQueryRequestBuilder.New().Paging(2, 3, true).Build();
            result = serviceQuery.GetServiceQuery().Execute(sourceQueryable);
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
            Assert.True(!result.Aggregate.HasValue);
            Assert.NotNull(result.List);
            Assert.True(result.List.Count == 1);

            serviceQuery = ServiceQueryRequestBuilder.New().Paging(3, 3, true).Build();
            result = serviceQuery.GetServiceQuery().Execute(sourceQueryable);
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
            Assert.True(!result.Aggregate.HasValue);
            Assert.NotNull(result.List);
            Assert.True(result.List.Count == 0);

            serviceQuery = ServiceQueryRequestBuilder.New().Paging(3, 1, true).Build();
            result = serviceQuery.GetServiceQuery().Execute(sourceQueryable);
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
            Assert.True(!result.Aggregate.HasValue);
            Assert.NotNull(result.List);
            Assert.True(result.List.Count == 1);

            serviceQuery = ServiceQueryRequestBuilder.New().Paging(3, 2, true).Build();
            result = serviceQuery.GetServiceQuery().Execute(sourceQueryable);
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
            Assert.True(!result.Aggregate.HasValue);
            Assert.NotNull(result.List);
            Assert.True(result.List.Count == 0);

            serviceQuery = ServiceQueryRequestBuilder.New().Paging(10, 10, true).Build();
            result = serviceQuery.GetServiceQuery().Execute(sourceQueryable);
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
            Assert.True(!result.Aggregate.HasValue);
            Assert.NotNull(result.List);
            Assert.True(result.List.Count == 0);

            serviceQuery = ServiceQueryRequestBuilder.New().IsEqual(nameof(TestClass.SharedParentKey), "1").Paging(1, 2, true).Build();
            result = serviceQuery.GetServiceQuery().Execute(sourceQueryable);
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
            Assert.True(!result.Aggregate.HasValue);
            Assert.NotNull(result.List);
            Assert.True(result.List.Count == 2);

            serviceQuery = ServiceQueryRequestBuilder.New().IsEqual(nameof(TestClass.IntVal), "1").Paging(1, 2, true).Build();
            result = serviceQuery.GetServiceQuery().Execute(sourceQueryable);
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            Assert.True(!result.Aggregate.HasValue);
            Assert.NotNull(result.List);
            Assert.True(result.List.Count == 1);

            serviceQuery = ServiceQueryRequestBuilder.New().IsEqual(nameof(TestClass.BoolVal), "true").Paging(1, 2, true).Build();
            result = serviceQuery.GetServiceQuery().Execute(sourceQueryable);
            Assert.NotNull(result);
            Assert.True(result.Count == 2);
            Assert.True(!result.Aggregate.HasValue);
            Assert.NotNull(result.List);
            Assert.True(result.List.Count == 2);

            serviceQuery = ServiceQueryRequestBuilder.New().IsEqual(nameof(TestClass.BoolVal), "true").Paging(1, 2, false).Build();
            result = serviceQuery.GetServiceQuery().Execute(sourceQueryable);
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
            Assert.True(!result.Aggregate.HasValue);
            Assert.NotNull(result.List);
            Assert.True(result.List.Count == 2);
        }
    }
}
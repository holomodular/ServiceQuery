using Microsoft.EntityFrameworkCore;

namespace ServiceQuery.Xunit
{
    public class PagingTestsAsyncEfc : BaseTest
    {
        [Fact]
        public async Task NegativePagingRequestTest()
        {
            var sourceQueryable = await GetTestListAsync();
            IServiceQueryRequest serviceQuery;
            ServiceQueryResponse<TestClass> result;

            serviceQuery = ServiceQueryRequestBuilder.New().Paging(0, 0, false).Build();
            serviceQuery.Filters.Where(x =>
            x.FilterType == "pagenumber").First().Values = new List<string>() { "a" };
            await Assert.ThrowsAsync<ServiceQueryException>(async () =>
            {
                result = await serviceQuery.ExecuteAsync(sourceQueryable);
            });
            await Assert.ThrowsAsync<ServiceQueryException>(async () =>
            {
                result = await serviceQuery.ExecuteAsync<TestClass>(sourceQueryable);
            });

            serviceQuery = ServiceQueryRequestBuilder.New().Paging(0, 0, false).Build();
            serviceQuery.Filters.Where(x =>
            x.FilterType == "pagesize").First().Values = new List<string>() { "a" };
            await Assert.ThrowsAsync<ServiceQueryException>(async () =>
            {
                result = await serviceQuery.ExecuteAsync(sourceQueryable);
            });

            serviceQuery = ServiceQueryRequestBuilder.New().Paging(0, 0, false).Build();
            serviceQuery.Filters.Where(x =>
            x.FilterType == "includecount").First().Values = new List<string>() { "a" };
            await Assert.ThrowsAsync<ServiceQueryException>(async () =>
            {
                result = await serviceQuery.ExecuteAsync(sourceQueryable);
            });
        }

        [Fact]
        public async Task PagingStandardTest()
        {
            var sourceQueryable = await GetTestListAsync();
            IServiceQuery serviceQuery;
            ServiceQueryResponse<TestClass> result;

            serviceQuery = ServiceQueryBuilder.New().Paging(0, 0, false).Build();
            result = await serviceQuery.ExecuteAsync(sourceQueryable);
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
            Assert.True(!result.Aggregate.HasValue);
            Assert.NotNull(result.List);
            Assert.True(result.List.Count == 0);

            serviceQuery = ServiceQueryBuilder.New().Paging(0, 0, true).Build();
            result = await serviceQuery.ExecuteAsync(sourceQueryable);
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
            Assert.True(!result.Aggregate.HasValue);
            Assert.NotNull(result.List);
            Assert.True(result.List.Count == 0);

            serviceQuery = ServiceQueryBuilder.New().Paging(1, 1, true).Build();
            result = await serviceQuery.ExecuteAsync(sourceQueryable);
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
            Assert.True(!result.Aggregate.HasValue);
            Assert.NotNull(result.List);
            Assert.True(result.List.Count == 1);

            serviceQuery = ServiceQueryBuilder.New().Paging(1, 2, true).Build();
            result = await serviceQuery.ExecuteAsync(sourceQueryable);
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
            Assert.True(!result.Aggregate.HasValue);
            Assert.NotNull(result.List);
            Assert.True(result.List.Count == 2);

            serviceQuery = ServiceQueryBuilder.New().Paging(1, 3, true).Build();
            result = await serviceQuery.ExecuteAsync(sourceQueryable);
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
            Assert.True(!result.Aggregate.HasValue);
            Assert.NotNull(result.List);
            Assert.True(result.List.Count == 3);

            serviceQuery = ServiceQueryBuilder.New().Paging(2, 1, true).Build();
            result = await serviceQuery.ExecuteAsync(sourceQueryable);
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
            Assert.True(!result.Aggregate.HasValue);
            Assert.NotNull(result.List);
            Assert.True(result.List.Count == 1);

            serviceQuery = ServiceQueryBuilder.New().Paging(2, 2, true).Build();
            result = await serviceQuery.ExecuteAsync(sourceQueryable);
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
            Assert.True(!result.Aggregate.HasValue);
            Assert.NotNull(result.List);
            Assert.True(result.List.Count == 2);

            serviceQuery = ServiceQueryBuilder.New().Paging(2, 3, true).Build();
            result = await serviceQuery.ExecuteAsync(sourceQueryable);
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
            Assert.True(!result.Aggregate.HasValue);
            Assert.NotNull(result.List);
            Assert.True(result.List.Count == 1);

            serviceQuery = ServiceQueryBuilder.New().Paging(3, 3, true).Build();
            result = await serviceQuery.ExecuteAsync(sourceQueryable);
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
            Assert.True(!result.Aggregate.HasValue);
            Assert.NotNull(result.List);
            Assert.True(result.List.Count == 0);

            serviceQuery = ServiceQueryBuilder.New().Paging(3, 1, true).Build();
            result = await serviceQuery.ExecuteAsync(sourceQueryable);
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
            Assert.True(!result.Aggregate.HasValue);
            Assert.NotNull(result.List);
            Assert.True(result.List.Count == 1);

            serviceQuery = ServiceQueryBuilder.New().Paging(3, 2, true).Build();
            result = await serviceQuery.ExecuteAsync(sourceQueryable);
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
            Assert.True(!result.Aggregate.HasValue);
            Assert.NotNull(result.List);
            Assert.True(result.List.Count == 0);

            serviceQuery = ServiceQueryBuilder.New().Paging(10, 10, true).Build();
            result = await serviceQuery.ExecuteAsync(sourceQueryable);
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
            Assert.True(!result.Aggregate.HasValue);
            Assert.NotNull(result.List);
            Assert.True(result.List.Count == 0);

            serviceQuery = ServiceQueryBuilder.New().IsEqual(nameof(TestClass.SharedParentKey), "1").Paging(1, 2, true).Build();
            result = await serviceQuery.ExecuteAsync(sourceQueryable);
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
            Assert.True(!result.Aggregate.HasValue);
            Assert.NotNull(result.List);
            Assert.True(result.List.Count == 2);

            serviceQuery = ServiceQueryBuilder.New().IsEqual(nameof(TestClass.IntVal), "1").Paging(1, 2, true).Build();
            result = await serviceQuery.ExecuteAsync(sourceQueryable);
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            Assert.True(!result.Aggregate.HasValue);
            Assert.NotNull(result.List);
            Assert.True(result.List.Count == 1);

            serviceQuery = ServiceQueryBuilder.New().IsEqual(nameof(TestClass.BoolVal), "true").Paging(1, 2, true).Build();
            result = await serviceQuery.ExecuteAsync(sourceQueryable);
            Assert.NotNull(result);
            Assert.True(result.Count == 2);
            Assert.True(!result.Aggregate.HasValue);
            Assert.NotNull(result.List);
            Assert.True(result.List.Count == 2);

            serviceQuery = ServiceQueryBuilder.New().IsEqual(nameof(TestClass.BoolVal), "true").Paging(1, 2, false).Build();
            result = await serviceQuery.ExecuteAsync(sourceQueryable);
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
            Assert.True(!result.Aggregate.HasValue);
            Assert.NotNull(result.List);
            Assert.True(result.List.Count == 2);
        }

        [Fact]
        public async Task PagingRequestTest()
        {
            var sourceQueryable = await GetTestListAsync();
            IServiceQueryRequest serviceQuery;
            ServiceQueryResponse<TestClass> result;

            serviceQuery = ServiceQueryRequestBuilder.New().Paging(0, 0, false).Build();
            result = await serviceQuery.GetServiceQuery().ExecuteAsync(sourceQueryable);
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
            Assert.True(!result.Aggregate.HasValue);
            Assert.NotNull(result.List);
            Assert.True(result.List.Count == 0);

            serviceQuery = ServiceQueryRequestBuilder.New().Paging(0, 0, true).Build();
            result = await serviceQuery.GetServiceQuery().ExecuteAsync(sourceQueryable);
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
            Assert.True(!result.Aggregate.HasValue);
            Assert.NotNull(result.List);
            Assert.True(result.List.Count == 0);

            serviceQuery = ServiceQueryRequestBuilder.New().Paging(1, 1, true).Build();
            result = await serviceQuery.GetServiceQuery().ExecuteAsync(sourceQueryable);
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
            Assert.True(!result.Aggregate.HasValue);
            Assert.NotNull(result.List);
            Assert.True(result.List.Count == 1);

            serviceQuery = ServiceQueryRequestBuilder.New().Paging(1, 2, true).Build();
            result = await serviceQuery.GetServiceQuery().ExecuteAsync(sourceQueryable);
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
            Assert.True(!result.Aggregate.HasValue);
            Assert.NotNull(result.List);
            Assert.True(result.List.Count == 2);

            serviceQuery = ServiceQueryRequestBuilder.New().Paging(1, 3, true).Build();
            result = await serviceQuery.GetServiceQuery().ExecuteAsync(sourceQueryable);
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
            Assert.True(!result.Aggregate.HasValue);
            Assert.NotNull(result.List);
            Assert.True(result.List.Count == 3);

            serviceQuery = ServiceQueryRequestBuilder.New().Paging(2, 1, true).Build();
            result = await serviceQuery.GetServiceQuery().ExecuteAsync(sourceQueryable);
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
            Assert.True(!result.Aggregate.HasValue);
            Assert.NotNull(result.List);
            Assert.True(result.List.Count == 1);

            serviceQuery = ServiceQueryRequestBuilder.New().Paging(2, 2, true).Build();
            result = await serviceQuery.GetServiceQuery().ExecuteAsync(sourceQueryable);
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
            Assert.True(!result.Aggregate.HasValue);
            Assert.NotNull(result.List);
            Assert.True(result.List.Count == 2);

            serviceQuery = ServiceQueryRequestBuilder.New().Paging(2, 3, true).Build();
            result = await serviceQuery.GetServiceQuery().ExecuteAsync(sourceQueryable);
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
            Assert.True(!result.Aggregate.HasValue);
            Assert.NotNull(result.List);
            Assert.True(result.List.Count == 1);

            serviceQuery = ServiceQueryRequestBuilder.New().Paging(3, 3, true).Build();
            result = await serviceQuery.GetServiceQuery().ExecuteAsync(sourceQueryable);
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
            Assert.True(!result.Aggregate.HasValue);
            Assert.NotNull(result.List);
            Assert.True(result.List.Count == 0);

            serviceQuery = ServiceQueryRequestBuilder.New().Paging(3, 1, true).Build();
            result = await serviceQuery.GetServiceQuery().ExecuteAsync(sourceQueryable);
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
            Assert.True(!result.Aggregate.HasValue);
            Assert.NotNull(result.List);
            Assert.True(result.List.Count == 1);

            serviceQuery = ServiceQueryRequestBuilder.New().Paging(3, 2, true).Build();
            result = await serviceQuery.GetServiceQuery().ExecuteAsync(sourceQueryable);
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
            Assert.True(!result.Aggregate.HasValue);
            Assert.NotNull(result.List);
            Assert.True(result.List.Count == 0);

            serviceQuery = ServiceQueryRequestBuilder.New().Paging(10, 10, true).Build();
            result = await serviceQuery.GetServiceQuery().ExecuteAsync(sourceQueryable);
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
            Assert.True(!result.Aggregate.HasValue);
            Assert.NotNull(result.List);
            Assert.True(result.List.Count == 0);

            serviceQuery = ServiceQueryRequestBuilder.New().IsEqual(nameof(TestClass.SharedParentKey), "1").Paging(1, 2, true).Build();
            result = await serviceQuery.GetServiceQuery().ExecuteAsync(sourceQueryable);
            Assert.NotNull(result);
            Assert.True(result.Count == 4);
            Assert.True(!result.Aggregate.HasValue);
            Assert.NotNull(result.List);
            Assert.True(result.List.Count == 2);

            serviceQuery = ServiceQueryRequestBuilder.New().IsEqual(nameof(TestClass.IntVal), "1").Paging(1, 2, true).Build();
            result = await serviceQuery.GetServiceQuery().ExecuteAsync(sourceQueryable);
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            Assert.True(!result.Aggregate.HasValue);
            Assert.NotNull(result.List);
            Assert.True(result.List.Count == 1);

            serviceQuery = ServiceQueryRequestBuilder.New().IsEqual(nameof(TestClass.BoolVal), "true").Paging(1, 2, true).Build();
            result = await serviceQuery.GetServiceQuery().ExecuteAsync(sourceQueryable);
            Assert.NotNull(result);
            Assert.True(result.Count == 2);
            Assert.True(!result.Aggregate.HasValue);
            Assert.NotNull(result.List);
            Assert.True(result.List.Count == 2);

            serviceQuery = ServiceQueryRequestBuilder.New().IsEqual(nameof(TestClass.BoolVal), "true").Paging(1, 2, false).Build();
            result = await serviceQuery.GetServiceQuery().ExecuteAsync(sourceQueryable);
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
            Assert.True(!result.Aggregate.HasValue);
            Assert.NotNull(result.List);
            Assert.True(result.List.Count == 2);
        }
    }
}
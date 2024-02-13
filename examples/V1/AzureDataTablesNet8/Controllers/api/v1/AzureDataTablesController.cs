using Asp.Versioning;
using AutoMapper;
using Azure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceQuery;
using System.Net;
using WebApp.Database;
using WebApp.Mapping;
using WebApp.Model;

namespace WebApp.Controllers.api.v1
{
    [AllowAnonymous]
    [ApiController]
    [ApiVersion("1.0")]
    [Produces("application/json", "application/problem+json")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class AzureDataTablesController : Controller
    {
        private readonly AzureDataTablesDatabaseContext _context;
        private IMapper _mapper;

        public AzureDataTablesController(
            AzureDataTablesDatabaseContext exampleDatabaseContext,
            IMapper mapper)
        {
            _context = exampleDatabaseContext;
            _mapper = mapper;
        }

        [HttpPost]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ServiceQueryResponse<ExampleClass>), (int)HttpStatusCode.OK)]
        [Route("ServiceQuery")]
        public ServiceQueryResponse<ExampleClass> ServiceQuery([FromBody] ServiceQueryRequest serviceQueryRequest)
        {
            if (serviceQueryRequest == null)
                return null;

            var tableClient = _context.ExampleClassesTableClient();
            var serviceQuery = serviceQueryRequest.GetServiceQuery();
            var selectProperties = serviceQuery.GetSelectProperties<ExampleClass>();
            var predicate = serviceQuery.BuildWhereExpression<ExampleClass>();

            Azure.Pageable<ExampleClass> result = null;
            if (predicate == null)
                result = tableClient.Query<ExampleClass>(maxPerPage: serviceQuery.PageSize, select: selectProperties);
            else
                result = tableClient.Query<ExampleClass>(predicate, serviceQuery.PageSize, selectProperties);

            ServiceQueryResponse<ExampleClass> response = new ServiceQueryResponse<ExampleClass>();
            int recordCount = 0;
            int startRecordCount = (serviceQuery.PageNumber - 1) * serviceQuery.PageSize;
            int endRecordCount = serviceQuery.PageNumber * serviceQuery.PageSize;
            foreach (Page<ExampleClass> page in result.AsPages())
            {
                foreach (var item in page.Values)
                {
                    recordCount++;
                    if (recordCount > startRecordCount && recordCount <= endRecordCount)
                        response.List.Add(item);

                    if (recordCount > endRecordCount)
                        break;
                }
            }

            return response;
        }

        [HttpPost]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ServiceQueryResponse<ExampleClass>), (int)HttpStatusCode.OK)]
        [Route("ServiceQueryAsync")]
        public async Task<ServiceQueryResponse<ExampleClass>> ServiceQueryAsync([FromBody] ServiceQueryRequest serviceQueryRequest)
        {
            if (serviceQueryRequest == null)
                return null;

            var tableClient = _context.ExampleClassesTableClient();
            var serviceQuery = serviceQueryRequest.GetServiceQuery();
            var selectProperties = serviceQuery.GetSelectProperties<ExampleClass>();
            var predicate = serviceQuery.BuildWhereExpression<ExampleClass>();

            Azure.AsyncPageable<ExampleClass> asyncresult = null;
            if (predicate == null)
                asyncresult = tableClient.QueryAsync<ExampleClass>(maxPerPage: serviceQuery.PageSize, select: selectProperties);
            else
                asyncresult = tableClient.QueryAsync<ExampleClass>(predicate, serviceQuery.PageSize, selectProperties);

            ServiceQueryResponse<ExampleClass> response = new ServiceQueryResponse<ExampleClass>();
            int recordCount = 0;
            int startRecordCount = (serviceQuery.PageNumber - 1) * serviceQuery.PageSize;
            int endRecordCount = serviceQuery.PageNumber * serviceQuery.PageSize;
            await foreach (Page<ExampleClass> page in asyncresult.AsPages())
            {
                foreach (var item in page.Values)
                {
                    recordCount++;
                    if (recordCount > startRecordCount && recordCount <= endRecordCount)
                        response.List.Add(item);

                    if (recordCount > endRecordCount)
                        break;
                }
            }

            return response;
        }

        [HttpPost]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ServiceQueryResponse<CustomExampleClassDto>), (int)HttpStatusCode.OK)]
        [Route("CustomExampleServiceQuery")]
        public ServiceQueryResponse<CustomExampleClassDto> CustomExampleServiceQuery([FromBody] ServiceQueryRequest serviceQueryRequest)
        {
            if (serviceQueryRequest == null)
                return null;

            // Property Mappings from CustomExampleClassDto -> ExampleClass
            var serviceQueryOptions = new ServiceQueryOptions()
            {
                PropertyNameMappings = CustomExampleClassMappingProfile.ServiceQueryMappings
            };

            var tableClient = _context.ExampleClassesTableClient();
            var serviceQuery = serviceQueryRequest.GetServiceQuery();
            var selectProperties = serviceQuery.GetSelectProperties<ExampleClass>(serviceQueryOptions);
            var predicate = serviceQuery.BuildWhereExpression<ExampleClass>(serviceQueryOptions);

            Azure.Pageable<ExampleClass> result = null;
            if (predicate == null)
                result = tableClient.Query<ExampleClass>(maxPerPage: serviceQuery.PageSize, select: selectProperties);
            else
                result = tableClient.Query<ExampleClass>(predicate, serviceQuery.PageSize, selectProperties);

            ServiceQueryResponse<ExampleClass> response = new ServiceQueryResponse<ExampleClass>();
            int recordCount = 0;
            int startRecordCount = (serviceQuery.PageNumber - 1) * serviceQuery.PageSize;
            int endRecordCount = serviceQuery.PageNumber * serviceQuery.PageSize;
            foreach (Page<ExampleClass> page in result.AsPages())
            {
                foreach (var item in page.Values)
                {
                    recordCount++;
                    if (recordCount > startRecordCount && recordCount <= endRecordCount)
                        response.List.Add(item);

                    if (recordCount > endRecordCount)
                        break;
                }
            }

            return new ServiceQueryResponse<CustomExampleClassDto>()
            {
                List = _mapper.Map<List<CustomExampleClassDto>>(response.List)
            };
        }
    }
}
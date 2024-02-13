using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceQuery;
using System.Net;
using WebApp.Database;
using WebApp.Mapping;
using WebApp.Model;
using Asp.Versioning;

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

            var azureDataTablesOptions = new AzureDataTablesOptions()
            {
                DownloadAllRecordsForAggregate = true,
                DownloadAllRecordsForCount = true,
                DownloadAllRecordsForSort = true,
                DownloadAllRecordsForStringComparison = true,
            };
            var tableClient = _context.ExampleClassesTableClient();
            return serviceQueryRequest.Execute<ExampleClass>(
                tableClient,
                null,
                azureDataTablesOptions);
        }

        [HttpPost]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ServiceQueryResponse<ExampleClass>), (int)HttpStatusCode.OK)]
        [Route("ServiceQueryAsync")]
        public async Task<ServiceQueryResponse<ExampleClass>> ServiceQueryAsync([FromBody] ServiceQueryRequest serviceQueryRequest)
        {
            if (serviceQueryRequest == null)
                return null;

            var azureDataTablesOptions = new AzureDataTablesOptions()
            {
                DownloadAllRecordsForAggregate = true,
                DownloadAllRecordsForCount = true,
                DownloadAllRecordsForSort = true,
                DownloadAllRecordsForStringComparison = true,
            };
            var tableClient = _context.ExampleClassesTableClient();
            return await serviceQueryRequest.ExecuteAsync<ExampleClass>(
                tableClient,
                null,
                azureDataTablesOptions);
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
            var azureDataTablesOptions = new AzureDataTablesOptions()
            {
                DownloadAllRecordsForAggregate = true,
                DownloadAllRecordsForCount = true,
                DownloadAllRecordsForSort = true,
                DownloadAllRecordsForStringComparison = true,
            };

            var tableClient = _context.ExampleClassesTableClient();
            var response = serviceQueryRequest.Execute<ExampleClass>(
                tableClient,
                serviceQueryOptions,
                azureDataTablesOptions);

            return new ServiceQueryResponse<CustomExampleClassDto>()
            {
                Count = response.Count,
                Aggregate = response.Aggregate,
                List = _mapper.Map<List<CustomExampleClassDto>>(response.List)
            };
        }
    }
}
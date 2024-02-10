using Asp.Versioning;
using AutoMapper;
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
    public class ExampleController : ControllerBase
    {
        private readonly IMapper _mapper;

        public ExampleController(IMapper mapper)
        {
            _mapper = mapper;
        }

        protected virtual IQueryable<ExampleClass> GetQueryable()
        {
            // Just objects in a list
            return SeedExampleDatabase.GetExamples().AsQueryable();
        }

        [HttpPost]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ServiceQueryResponse<ExampleClass>), (int)HttpStatusCode.OK)]
        [Route("ServiceQuery")]
        public ServiceQueryResponse<ExampleClass> ServiceQuery([FromBody] ServiceQueryRequest serviceQueryRequest)
        {
            if (serviceQueryRequest == null)
                return null;

            var queryable = GetQueryable();
            return serviceQueryRequest.Execute(queryable);
        }

        [HttpPost]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ServiceQueryResponse<ExampleClass>), (int)HttpStatusCode.OK)]
        [Route("ServiceQueryAsync")]
        public async Task<ServiceQueryResponse<ExampleClass>> ServiceQueryAsync([FromBody] ServiceQueryRequest serviceQueryRequest)
        {
            if (serviceQueryRequest == null)
                return null;

            var queryable = GetQueryable();
            return await serviceQueryRequest.ExecuteAsync(queryable);
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

            // Execute the ServiceQuery
            var queryable = GetQueryable();
            var result = serviceQueryRequest.Execute(queryable, serviceQueryOptions);

            // Map from ExampleClass -> CustomExampleClassDto
            return new ServiceQueryResponse<CustomExampleClassDto>()
            {
                Aggregate = result.Aggregate,
                Count = result.Count,
                List = _mapper.Map<List<CustomExampleClassDto>>(result.List)
            };
        }
    }
}
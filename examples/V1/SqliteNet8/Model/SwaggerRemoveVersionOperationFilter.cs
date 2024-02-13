using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace WebApp.Model
{
    public class SwaggerRemoveVersionOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var version = operation.Parameters.Where(x => x.Name == "version").FirstOrDefault();
            if (version != null)
                operation.Parameters.Remove(version);
        }
    }
}
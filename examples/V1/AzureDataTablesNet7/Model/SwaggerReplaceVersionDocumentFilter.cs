using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace WebApp.Model
{
    public class SwaggerReplaceVersionDocumentFilter : IDocumentFilter
    {
        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            var paths = new OpenApiPaths();
            foreach (var path in swaggerDoc.Paths)
                paths.Add(path.Key.Replace("{version}", swaggerDoc.Info.Version), path.Value);
            swaggerDoc.Paths = paths;
        }
    }
}
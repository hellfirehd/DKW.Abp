using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace DKW.Abp.Microservices;

public class VoloDocumentFilter : IDocumentFilter
{
    public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
    {
        foreach (var schema in swaggerDoc.Components.Schemas)
        {
            if (schema.Key.StartsWith("Volo"))
            {
                swaggerDoc.Components.Schemas.Remove(schema.Key);
            }
            else
            {
                var title = schema.Key;
                var firstIndex = title.IndexOf('`');
                if (firstIndex > 0)
                {
                    title = title[..firstIndex];
                }

                schema.Value.Title = title[(title.LastIndexOf('.') + 1)..];
            }
        }
    }
}
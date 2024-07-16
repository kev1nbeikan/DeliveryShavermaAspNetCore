using System.Reflection;
using System.Xml.XPath;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace OrderService.Api.Extensions;

/// <summary>
/// Расширение для добавляения комментариев из XML документации Swagger
/// </summary>
public static class AddSwaggerCommentsFromXml
{
    /// <summary>
    /// Включить описание эндпоинтов для Swagger из XML документации (из summary).
    /// </summary>
    /// <param name="options"></param>
    /// <exception cref="FileNotFoundException"></exception>
    public static void IncludeSwaggerCommentsFromXml(SwaggerGenOptions options)
    {
        options.IncludeXmlComments(() =>
        {
            var assembly = Assembly.GetExecutingAssembly();

            if (assembly?.GetManifestResourceNames().FirstOrDefault(x => x.Contains("swagger.xml"))
                is not { } swaggerResourceName)
            {
                throw new FileNotFoundException("Не найден файл с XML документацией для Swagger");
            }

            using var resource = assembly.GetManifestResourceStream(swaggerResourceName);
            return resource is null
                ? throw new FileNotFoundException("Не найден файл с XML документацией для Swagger")
                : new XPathDocument(resource);
        }, true);
    }
}
using ContentCleaner.Services;
using ContentCleaner.ViewModels;
using EPiServer;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;

namespace ContentCleaner.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    [Authorize(Policy = Constants.AuthorizationPolicy)]
    public class MissingPropertiesApiController : Controller
    {
        private readonly IContentService _contentService;
        private readonly IContentRepository _contentRepository;
        private readonly IPropertyDefinitionRepository _propertyDefinitionRepository;

        public MissingPropertiesApiController(IContentService contentService,
            IContentRepository contentRepository,
            IPropertyDefinitionRepository propertyDefinitionRepository)
        {
            _contentService = contentService;
            _contentRepository = contentRepository;
            _propertyDefinitionRepository = propertyDefinitionRepository;
        }

        [HttpGet]
        [Route("/missing.properties/api/[action]")]
        public IActionResult GetMissingProperties()
        {
            try
            {
                var missingProperties = _contentService.GetMissingProperties().ToList();

                var model = new MissingPropertiesViewModel(1, missingProperties.Count, missingProperties.Count, missingProperties);

                var serializationOptions = new JsonSerializerOptions(JsonSerializerDefaults.Web)
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                };

                var content = JsonSerializer.Serialize(model, serializationOptions);

                return new ContentResult
                {
                    StatusCode = (int)HttpStatusCode.OK,
                    ContentType = "application/json",
                    Content = content
                };
            }
            catch (Exception ex)
            {
                return new ContentResult
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError,
                    ContentType = "application/json",
                    Content = $"Error - {ex.Message}"
                };
            }
        }

        [HttpDelete]
        [Route("/missing.properties/api/[action]")]
        public IActionResult Delete(string id)
        {
            try
            {
                DeleteItem(id);

                return new OkResult();
            }
            catch (Exception ex)
            {
                return new ContentResult
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError,
                    ContentType = "application/json",
                    Content = $"Error - {ex.Message}"
                };
            }
        }

        private void DeleteItem(string id)
        {
            var propertyDefinition = _propertyDefinitionRepository.Load(int.Parse(id));

            var writable = propertyDefinition.CreateWritableClone();

            _propertyDefinitionRepository.Delete(writable);
        }
    }
}
using ContentCleaner;
using ContentCleaner.Services;
using ContentCleaner.ViewModels;
using EPiServer;
using EPiServer.Core;
using EPiServer.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;

namespace ContentCleaner.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    [Authorize(Policy = Constants.AuthorizationPolicy)]
    public class ContentCleanerApiController : Controller
    {
        private readonly IContentService _contentTypeservice;
        private readonly IContentRepository _contentRepository;

        public ContentCleanerApiController(IContentService contentTypeservice, IContentRepository contentRepository)
        {
            _contentTypeservice = contentTypeservice;
            _contentRepository = contentRepository;
        }

        [HttpGet]
        [Route("/content.cleaner/api/[action]")]
        public IActionResult GetContentTypeUsage(int contentTypeId)
        {
            try
            {
                var contentTypeUsage = _contentTypeservice.GetContentTypeUsage(contentTypeId);

                var model = new ContentUsageViewModel(1, contentTypeUsage.Count, contentTypeUsage.Count, contentTypeUsage);

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
        [Route("/content.cleaner/api/[action]")]
        public IActionResult Delete(string contentRef)
        {
            try
            {
                DeleteItem(contentRef);

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

        private void DeleteItem(string contentRef)
        {
            _contentRepository.Delete(ContentReference.Parse(contentRef), forceDelete: false, access: AccessLevel.NoAccess);
        }
    }
}
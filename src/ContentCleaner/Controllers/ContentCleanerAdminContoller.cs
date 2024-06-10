using ContentCleaner.Services;
using ContentCleaner.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ContentCleaner.Controllers
{
    public class ContentCleanerAdminContoller : Controller
    {
        private readonly IContentService _contentService;

        public ContentCleanerAdminContoller(IContentService contentService)
        {
            _contentService = contentService;
        }

        [HttpGet]
        [Route("/content.cleaner/admin")]
        public IActionResult Index()
        {
            var model = new ContentCleanerViewModel()
            {
                ContentItems = _contentService.GetContentTypes()
            };

            return View("~/Views/ContentCleaner/Index.cshtml", model);
        }
    }
}
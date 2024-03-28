using ContentCleaner.ViewModels;
using ContentCleaner.Services;
using Microsoft.AspNetCore.Mvc;

namespace ContentCleaner.Controllers
{
    public class ContentCleanerAdminContoller : Controller
    {
        private readonly IContentService _contentTypeService;

        public ContentCleanerAdminContoller(IContentService contentTypeService)
        {
            _contentTypeService = contentTypeService;
        }

        [HttpGet]
        [Route("/content.cleaner/admin")]
        public IActionResult Index()
        {
            var model = new ContentCleanerViewModel()
            {
                ContentItems = _contentTypeService.GetContentTypes()
            };

            return View("~/Views/ContentCleaner/Index.cshtml", model);
        }
    }
}
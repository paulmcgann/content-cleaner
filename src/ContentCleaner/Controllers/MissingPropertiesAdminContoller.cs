using ContentCleaner.Services;
using Microsoft.AspNetCore.Mvc;

namespace ContentCleaner.Controllers
{
    public class MissingPropertiesAdminContoller : Controller
    {

        public MissingPropertiesAdminContoller(IContentService contentService)
        {
        }

        [HttpGet]
        [Route("/missing.properties/admin")]
        public IActionResult Index()
        {
            return View("~/Views/MissingProperties/Index.cshtml");
        }
    }
}
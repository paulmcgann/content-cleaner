using Microsoft.AspNetCore.Mvc.Rendering;

namespace ContentCleaner.ViewModels
{
    public class ContentCleanerViewModel
    {
        public IEnumerable<SelectListItem> ContentItems { get; set; } = new List<SelectListItem>();
    }
}
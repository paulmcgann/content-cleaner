using Microsoft.AspNetCore.Mvc.Rendering;

namespace ContentCleaner.ViewModels
{
    public class ContentCleanerViewModel
    {
        public IEnumerable<SelectListItem> ContentItems { get; set; } = new List<SelectListItem>();

        public IEnumerable<ContentUsageDataViewModel> ContentUsages { get; set; } = new List<ContentUsageDataViewModel>();

        public int ContentTypeId { get; set; }
    }
}
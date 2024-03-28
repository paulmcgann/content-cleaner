using ContentCleaner.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ContentCleaner.Services
{
    public interface IContentService
    {
        IEnumerable<SelectListItem> GetContentTypes();

        List<ContentUsageDataViewModel> GetContentTypeUsage(int selectedTypeID);
    }
}
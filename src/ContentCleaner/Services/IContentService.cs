using ContentCleaner.ViewModels;
using EPiServer.DataAbstraction;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ContentCleaner.Services
{
    public interface IContentService
    {
        IEnumerable<MissingPropertiesDataViewModel> GetMissingProperties();

        IEnumerable<SelectListItem> GetContentTypes();

        IEnumerable<ContentUsageDataViewModel> GetContentTypeUsage(int selectedTypeID);
    }
}
using ContentCleaner.ViewModels;
using EPiServer;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text;
using EditUrlResolver = EPiServer.Web.Routing.EditUrlResolver;

namespace ContentCleaner.Services
{
    public class ContentService : IContentService
    {
        private readonly IContentLoader _contentLoader;
        private readonly IContentTypeRepository _contentTypeRepository;
        private readonly IContentModelUsage _contentModelUsage;
        private readonly EditUrlResolver _editUrlResolver;

        public ContentService(IContentLoader contentLoader, IContentTypeRepository contentTypeRepository, IContentModelUsage contentModelUsage, EditUrlResolver editUrlResolver)
        {
            _contentLoader = contentLoader;
            _contentTypeRepository = contentTypeRepository;
            _contentModelUsage = contentModelUsage;
            _editUrlResolver = editUrlResolver;
        }

        public IEnumerable<SelectListItem> GetContentTypes()
        {
            var contentTypes = _contentTypeRepository.List();

            var exclusions = new List<int>() { 1, 2, 3, 4 };

            var result = contentTypes.Where(p => !exclusions.Exists(p2 => p2 == p.ID)).OrderBy(x => x.Name);

            return result.Select(x => new SelectListItem
            {
                Value = x.ID.ToString(),
                Text = x.Name
            }).ToList();
        }

        public List<ContentUsageDataViewModel> GetContentTypeUsage(int selectedTypeID)
        {
            var contentType = _contentTypeRepository.Load(selectedTypeID);

            var usage = _contentModelUsage.ListContentOfContentType(contentType)
                .Select(x => new ContentUsageDataViewModel()
                {
                    Id = x.ContentLink.ID,
                    TypeId = selectedTypeID,
                    EditUrl = GetEditUrl(x.ContentLink),
                    Location = GetLocation(x.ContentLink),
                })
                .DistinctBy(x => x.Id)
                .ToList();

            return usage;
        }

        public string GetEditUrl(ContentReference cReference)
        {
            return _editUrlResolver.GetEditViewUrl(cReference).ToString();
        }

        public string GetLocation(ContentReference cReference)
        {
            if (cReference == null)
                return string.Empty;

            var ancestor = _contentLoader.GetAncestors(cReference).ToList();
            var content = _contentLoader.Get<IContent>(cReference);

            var path = new StringBuilder();

            for (var x = ancestor.Count - 1; x >= 0; x--)
            {
                path.Append($"\\{ancestor[x].Name}");
            }

            return $"{path}\\{content.Name}".Trim("\\".ToCharArray());
        }
    }
}
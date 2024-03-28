using ContentCleaner;
using EPiServer.Shell.Navigation;

namespace ContentCleaner.Presentation
{
    [MenuProvider]
    public class ContentCleanerMenuProvider : IMenuProvider
    {
        public IEnumerable<MenuItem> GetMenuItems()
        {
            var contentCleaner = new UrlMenuItem("Content Cleaner", "/global/cms/content.cleaner", "/content.cleaner/admin")
            {
                IsAvailable = _ => true,
                SortIndex = SortIndex.Last + 1,
                AuthorizationPolicy = Constants.AuthorizationPolicy
            };

            return new List<MenuItem> { contentCleaner };
        }
    }
}
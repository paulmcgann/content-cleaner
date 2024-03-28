using ContentCleaner.Web.Models.Pages;
using EPiServer.Web;
using Microsoft.AspNetCore.Html;
using System.ComponentModel.DataAnnotations;

namespace ContentCleaner.Web.Models.ViewModels
{
    public class ContactBlockModel
    {
        [UIHint(UIHint.Image)]
        public ContentReference Image { get; set; }

        public string Heading { get; set; }

        public string LinkText { get; set; }

        public IHtmlContent LinkUrl { get; set; }

        public bool ShowLink { get; set; }

        public ContactPage ContactPage { get; set; }
    }
}

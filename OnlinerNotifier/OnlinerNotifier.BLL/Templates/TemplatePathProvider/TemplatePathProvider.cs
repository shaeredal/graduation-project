using System.IO;
using System.Web;

namespace OnlinerNotifier.BLL.Templates.TemplatePathProvider
{
    public class TemplatePathProvider : ITemplatePathProvider
    {
        public string GetEmailTemplatePath()
        {
            return Path.Combine(HttpRuntime.AppDomainAppPath,
                "..\\OnlinerNotifier.BLL\\Templates\\EmailTemplate.cshtml");
        }
    }
}

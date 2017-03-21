using System.Collections.Generic;
using System.IO;
using OnlinerNotifier.BLL.Models.NotificationModels;
using OnlinerNotifier.BLL.Templates.TemplatePathProvider;
using RazorEngine;

namespace OnlinerNotifier.BLL.Templates.Builders
{
    public class RazorPriceChangesEmailBuilder : IPriceChangesEmailBuilder
    {
        private readonly ITemplatePathProvider templatePathProvider;

        public RazorPriceChangesEmailBuilder(ITemplatePathProvider templatePathProvider)
        {
            this.templatePathProvider = templatePathProvider;
        }
        
        public string GetMailBody(List<NotificationProductChangesModel> priceChanges)
        {
            var templatePath = templatePathProvider.GetEmailTemplatePath();
            var template = File.ReadAllText(templatePath);
            return Razor.Parse(template, priceChanges);
        }
    }
}

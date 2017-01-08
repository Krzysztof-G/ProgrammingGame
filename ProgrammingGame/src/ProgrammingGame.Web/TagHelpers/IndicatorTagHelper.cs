using System;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace ProgrammingGame.Web.TagHelpers
{
    [HtmlTargetElement("div", Attributes = Attributes)]
    public class IndicatorTagHelper : TagHelper
    {
        private const string ValueAttributeName = "indicator-value";
        private const string MinAttributeName = "indicator-min";
        private const string MaxAttributeName = "indicator-max";
        private const string Comma = ",";
        private const string Attributes = ValueAttributeName + Comma + MinAttributeName + Comma + MaxAttributeName;

        [HtmlAttributeName(ValueAttributeName)]
        public decimal Value { get; set; }

        [HtmlAttributeName(MinAttributeName)]
        public decimal Min { get; set; }

        [HtmlAttributeName(MaxAttributeName)]
        public decimal Max { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var progressTotal = Max - Min;
            var progressPercentage = Math.Round(((Value - Min) / progressTotal) * 100, 4);

            string progressBarContent =
                 $@"<div class='progress-bar' role='progressbar' aria-valuenow='{Value}' aria-valuemin='{Min}' aria-valuemax='{Max}' style='width: {progressPercentage}%;'> 
                        <span>{Value}/{Max}</span>
                    </div>";

            output.Content.AppendHtml(progressBarContent);

            var classValue = output.Attributes.Contains(new TagHelperAttribute("class")) ? $"{output.Attributes["class"]} progress" : "progress";

            output.Attributes.SetAttribute("class", classValue);
        }
    }
}

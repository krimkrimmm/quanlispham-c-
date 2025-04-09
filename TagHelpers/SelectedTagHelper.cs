using Microsoft.AspNetCore.Razor.TagHelpers;

namespace TMDT.TagHelpers
{
    [HtmlTargetElement("option", Attributes = "asp-selected")]
    public class SelectedTagHelper : TagHelper
    {
        [HtmlAttributeName("asp-selected")]
        public bool Selected { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (Selected)
            {
                output.Attributes.SetAttribute("selected", "selected");
            }
        }
    }
}

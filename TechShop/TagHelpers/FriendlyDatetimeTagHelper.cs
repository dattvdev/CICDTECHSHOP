using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Text;

namespace TechShop.TagHelpers
{
    public class FriendlyDatetimeTagHelper : TagHelper
    {
        public DateTime takeTime { get; set; }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "span";
            output.TagMode = TagMode.StartTagAndEndTag;

            DateTime currentTime = DateTime.Now;

            int yearTakeTime = takeTime.Year;
            int monthTakeTime = takeTime.Month;
            int dayTakeTime = takeTime.Day;
            int hourTakeTime = takeTime.Hour;
            int minuteTakeTime = takeTime.Minute;

            int yearCurrentTime = currentTime.Year;
            int monthCurrentTime = currentTime.Month;
            int dayCurrentTime = currentTime.Day;
            int hourCurrentTime = currentTime.Hour;
            int minuteCurrentTime = currentTime.Minute;

            var sb = new StringBuilder();

            if (yearCurrentTime == yearTakeTime)
            {
                if (monthCurrentTime == monthTakeTime)
                {
                    if (dayCurrentTime == dayTakeTime)
                    {
                        if (hourCurrentTime == hourTakeTime)
                        {
                            if (minuteCurrentTime == minuteTakeTime)
                                sb.Append("Just now");
                            else
                                sb.Append($"{(minuteCurrentTime - minuteTakeTime)} minutes ago");
                        }
                        else
                        {
                            if (hourCurrentTime - 1 == hourTakeTime)
                                sb.Append("An hour ago");
                            else
                                sb.Append($"{(hourCurrentTime - hourTakeTime)} hours ago");
                        }
                    }
                    else
                    {
                        if (dayCurrentTime - 1 == dayTakeTime)
                            sb.Append("Yesterday");
                        else
                            sb.Append($"{(dayCurrentTime - dayTakeTime)} days ago");
                    }
                }
                else
                {
                    sb.Append($"{(monthCurrentTime - monthTakeTime)} months ago");
                }
            }
            else
            {
                sb.Append($"{(yearCurrentTime - yearTakeTime)} years ago");
            }

            output.Content.SetContent(sb.ToString());
        }
    }
}

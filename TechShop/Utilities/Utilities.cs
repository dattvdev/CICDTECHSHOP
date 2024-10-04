using System.Globalization;
using System.Text.RegularExpressions;
using System.Text;

namespace TechShop.Utilities
{
	public class Utilities
	{
		public static string ConvertToSlug(string input)
		{
			// Step 1: Normalize the Unicode characters to decompose accents
			string normalizedString = input.Normalize(NormalizationForm.FormD);

			// Step 2: Use a StringBuilder to build the result without diacritics
			var stringBuilder = new StringBuilder();
			foreach (var c in normalizedString)
			{
				var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
				if (unicodeCategory != UnicodeCategory.NonSpacingMark)
				{
					stringBuilder.Append(c);
				}
			}

			// Step 3: Normalize again to FormC (composed form)
			string slug = stringBuilder.ToString().Normalize(NormalizationForm.FormC);

			// Step 4: Replace spaces with hyphens and convert to lowercase
			slug = Regex.Replace(slug, @"\s+", "-").ToLower();

			return slug;
		}

        public static string GetPublicIdFromUrl(string url)
        {
            var uri = new Uri(url);
            var fileName = uri.Segments.Last().Split('.').First();
            return fileName;
        }

    }
}

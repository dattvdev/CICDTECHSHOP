using System.Drawing;

namespace TechShop.Helper
{
    public static class ColorHelper
    {
        public static (int R, int G, int B) HexToRgb(string hex)
        {
            if (hex.StartsWith("#"))
                hex = hex.Substring(1);

            int r = int.Parse(hex.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
            int g = int.Parse(hex.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);
            int b = int.Parse(hex.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);

            return (r, g, b);
        }

        public static double ColorDistance((int R, int G, int B) color1, (int R, int G, int B) color2)
        {
            return Math.Sqrt(
                Math.Pow(color1.R - color2.R, 2) +
                Math.Pow(color1.G - color2.G, 2) +
                Math.Pow(color1.B - color2.B, 2)
            );
        }

        public static bool IsSimilarColor(string hexColor1, string hexColor2, double tolerance = 20)
        {
            var hc1 = HexToRgb(hexColor1);
            var hc2 = HexToRgb(hexColor2);

            if (ColorDistance(hc1, hc2) <= tolerance)
                return true;

            return false;
        }
    }
}

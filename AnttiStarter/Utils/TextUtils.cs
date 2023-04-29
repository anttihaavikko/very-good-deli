using System.Globalization;

namespace AnttiStarter.Utils
{
    public class TextUtils
    {
        public static string NumberWithSign(int number)
        {
            return number > 0 ? $"+{number}" : number.ToString();
        }
        
        public static string NumberWithSpaces(long number)
        {
            var nfi = (NumberFormatInfo)CultureInfo.InvariantCulture.NumberFormat.Clone();
            nfi.NumberGroupSeparator = " ";
            return number.ToString("#,0", nfi);
        }
    }
}

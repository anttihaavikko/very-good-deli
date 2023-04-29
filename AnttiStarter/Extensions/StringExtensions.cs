using System.Text.RegularExpressions;

// using AnttiStarterKit.Utils;

namespace AnttiStarter.Extensions
{
    public static class StringExtensions
    {
        public static int NthIndexOf(this string target, string value, int n)
        {
            var m = Regex.Match(target, "((" + Regex.Escape(value) + ").*?){" + n + "}");

            if (m.Success)
                return m.Groups[2].Captures[n - 1].Index;
            
            return -1;
        }
    }
}
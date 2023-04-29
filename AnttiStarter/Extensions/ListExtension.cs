using System.Collections.Generic;
using System.Linq;
using AnttiStarter.Utils;

namespace AnttiStarter.Extensions
{
    public static class ListExtension
    {
        public static T Random<T>(this IList<T> list)
        {
            return list.Any() ? list[Rng.Range(0, list.Count - 1)] : default;
        }
        
        public static IEnumerable<T> RandomOrder<T>(this IEnumerable<T> list)
        {
            return list.OrderBy(_ => Rng.Value);
        }
    }
}
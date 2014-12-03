using System;
using System.Collections.Generic;
using System.Linq;

namespace ALPACA.Utility
{
    public static class Extensions
    {
        public static IList<string> SplitAndTrim(this string s, params string[] delimiters)
        {
            return s.SplitAndTrim(StringSplitOptions.RemoveEmptyEntries, delimiters);
        }

        public static IList<string> SplitAndTrim(this string s, StringSplitOptions options, params string[] delimiters)
        {
            if (s == null)
            {
                return null;
            }
            var query = s.Split(delimiters, StringSplitOptions.None).Select(x => x.Trim());
            if (options == StringSplitOptions.RemoveEmptyEntries)
            {
                query = query.Where(x => x.Trim() != string.Empty);
            }
            return query.ToList();
        }

        public static void RemoveAll<T>(this IList<T> source, Predicate<T> predicate)
        {
            var list = source as List<T>;
            if (list != null)
            {
                list.RemoveAll(predicate);
                return;
            }

            for (int i = source.Count - 1; i >= 0; i--)
                if (predicate(source[i]))
                    source.RemoveAt(i);
        }
    }
}

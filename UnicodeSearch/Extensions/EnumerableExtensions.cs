using System;
using System.Collections.Generic;

namespace UnicodeSearch
{
    public static class EnumerableExtensions
    {
        public static IEnumerable<IEnumerable<T>> Buffer<T>(this IEnumerable<T> source, int count)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (count <= 0)
                throw new ArgumentOutOfRangeException(nameof(count));
            return BufferImpl(source, count);
            static IEnumerable<IEnumerable<T>> BufferImpl(IEnumerable<T> source, int count)
            {
                using var enumerable = source.GetEnumerator();
                while (enumerable.MoveNext())
                {
                    var array = new List<T>(count);
                    for (var i = 0; i < count; i++)
                    {
                        array.Add(enumerable.Current);
                        if (!enumerable.MoveNext())
                            break;
                    }
                    if (array.Count > 0)
                        yield return array;
                }
            }
        }
    }
}

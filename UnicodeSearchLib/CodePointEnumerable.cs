using System.Collections;
using System.Collections.Generic;

namespace UnicodeSearch
{
    /// <summary>
    /// string to CodePoint
    /// </summary>
    public class CodePointEnumerable : IEnumerable<int>
    {
        readonly string Text;
        public CodePointEnumerable(string Text)
            => this.Text = Text;
        public IEnumerator<int> GetEnumerator()
        {
            var chars = Text.ToCharArray();
            var Enumerator = chars.GetEnumerator();
            while (Enumerator.MoveNext())
            {
                var Current = (char)Enumerator.Current;
                if (!char.IsSurrogate(Current))
                {
                    yield return Current;
                    continue;
                }
                if (Enumerator.MoveNext())
                {
                    var Current2 = (char)Enumerator.Current;
                    var CodePoint = char.ConvertToUtf32(Current, Current2);
                    yield return CodePoint;
                    continue;
                }
                yield return Current;
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}

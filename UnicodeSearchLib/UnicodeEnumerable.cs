using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Unicode;

namespace UnicodeSearch
{
    /// <summary>
    /// string to UnicodeCharInfo
    /// </summary>
    public class UnicodeEnumerable : IEnumerable<UnicodeCharInfo>
    {
        readonly string Text;
        public UnicodeEnumerable(string Text)
            => this.Text = Text;
        public IEnumerator<UnicodeCharInfo> GetEnumerator()
        {
            foreach (var CodePoint in new CodePointEnumerable(Text))
                yield return UnicodeInfo.GetCharInfo(CodePoint);
        }
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}

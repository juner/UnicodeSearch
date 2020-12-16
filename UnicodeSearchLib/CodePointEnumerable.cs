using System.Collections;
using System.Collections.Generic;

namespace UnicodeSearch
{
    /// <summary>
    /// string to CodePoint enumerable.
    /// </summary>
    public class CodePointEnumerable : IEnumerable<int>
    {
        readonly string Text;
        public CodePointEnumerable(string Text)
            => this.Text = Text;
        public CodePointEnumerator GetEnumerator() => new CodePointEnumerator(Text);
        IEnumerator<int> IEnumerable<int>.GetEnumerator() => GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public override string ToString() => Text;
    }
}

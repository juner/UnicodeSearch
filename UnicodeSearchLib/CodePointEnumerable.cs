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
        public IEnumerator<int> GetEnumerator() => new CodePointEnumerator(Text);

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public override string ToString() => Text;
    }
}

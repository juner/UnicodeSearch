using System;
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
        public IEnumerator<int> GetEnumerator() => new CodePointEnumerator(Text);

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        struct CodePointEnumerator : IEnumerator<int>
        {
            public CodePointEnumerator(string Text)
                => (this.Text, this.index) = (Text, -1);
            int index;
            string Text;
            bool IsEnd => index >= Text.Length;
            bool CurrentIsSurrogatePair => index < 0 || IsEnd ? false : char.IsSurrogate(Text[index]);
            public int Current
            {
                get
                {
                    if (IsEnd)
                        throw new InvalidOperationException("this enumerator is end.");
                    var Current = Text[index];
                    if (!CurrentIsSurrogatePair)
                        return Current;
                    var Current2 = Text[index + 1];
                    return char.ConvertToUtf32(Current, Current2);
                }
            }

            object IEnumerator.Current => Current;

            public void Dispose() { }

            public bool MoveNext()
            {
                var CurrentIsSurrogatePair = this.CurrentIsSurrogatePair;
                index++;
                if (CurrentIsSurrogatePair)
                    index++;
                return !IsEnd;
            }

            public void Reset()
            {
                index = -1;
            }
        }
    }
}

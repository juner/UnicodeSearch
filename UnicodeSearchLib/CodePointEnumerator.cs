using System;
using System.Collections;
using System.Collections.Generic;

namespace UnicodeSearch
{
    /// <summary>
    /// string to CodePoint enumerator.
    /// </summary>
    public struct CodePointEnumerator : IEnumerator<int>
    {
        public CodePointEnumerator(string Text)
            => (text, this.index) = (Text, -1);
        int index;
        readonly string text;
        public int Index => index;
        public bool IsNoStart => index < 0;
        public bool IsEnd => !(index < text.Length);
        public bool CurrentIsSurrogatePair => IsNoStart || IsEnd ? false : char.IsSurrogate(text, index);
        public int Current
        {
            get
            {
                if (IsNoStart)
                    throw new InvalidOperationException("this enumerator is no MoveNext().");
                if (IsEnd)
                    throw new InvalidOperationException("this enumerator is end.");
                return char.ConvertToUtf32(text, index);
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

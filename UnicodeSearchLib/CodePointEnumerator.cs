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
            => (text, this.index) = (Text, 0);
        int index;
        readonly string text;
        public readonly int Index => index - 1;
        public readonly bool IsNoStart => Index < 0;
        public readonly bool IsEnd => !(Index < text.Length);
        public readonly bool CurrentIsSurrogatePair => IsNoStart || IsEnd ? false : char.IsSurrogate(text, Index);
        public int Current
        {
            get
            {
                if (IsNoStart)
                    throw new InvalidOperationException("this enumerator is no MoveNext().");
                if (IsEnd)
                    throw new InvalidOperationException("this enumerator is end.");
                return char.ConvertToUtf32(text, Index);
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
            index = 0;
        }
        public override string ToString() => $"{text},{Index}";
    }
}

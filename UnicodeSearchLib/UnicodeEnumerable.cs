using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Unicode;

namespace UnicodeSearch
{
    public class UnicodeEnumerable : IEnumerable<UnicodeCharInfo>
    {
        readonly string Text;
        public UnicodeEnumerable(string Text)
            => this.Text = Text;
        public IEnumerator<UnicodeCharInfo> GetEnumerator()
        {
            var Enumerator = StringInfo.GetTextElementEnumerator(Text);
            while (Enumerator.MoveNext())
            {
                var Current = Enumerator.Current.ToString();
                var CodePoint = char.ConvertToUtf32(Current, 0);
                var CharInfo = UnicodeInfo.GetCharInfo(CodePoint);
                yield return CharInfo;
                try
                {
                    CodePoint = char.ConvertToUtf32(Current, 1);
                    CharInfo = UnicodeInfo.GetCharInfo(CodePoint);
                }
                catch (Exception)
                {
                    continue;
                }
                yield return CharInfo;
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}

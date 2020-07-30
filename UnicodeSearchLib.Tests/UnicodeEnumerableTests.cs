using System;
using System.Collections.Generic;
using System.Linq;
using System.Unicode;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnicodeSearch.Tests
{
    [TestClass]
    public class UnicodeEnumerableTests
    {
        static IEnumerable<object[]> GetEnumeratorTestData
        {
            get
            {
                yield return GetEnumeratorTestData("test",
                    UnicodeInfo.GetCharInfo(char.ConvertToUtf32("t", 0)),
                    UnicodeInfo.GetCharInfo(char.ConvertToUtf32("e", 0)),
                    UnicodeInfo.GetCharInfo(char.ConvertToUtf32("s", 0)),
                    UnicodeInfo.GetCharInfo(char.ConvertToUtf32("t", 0))
                );
                static object[] GetEnumeratorTestData(string Text, params UnicodeCharInfo[] Expected)
                    => new object[] { Text, Expected };
            }
        }
        [TestMethod]
        [DynamicData(nameof(GetEnumeratorTestData))]
        public void GetEnumeratorTest(string Text, UnicodeCharInfo[] Expected)
        {
            Console.WriteLine($"FROM TEXT: {Text}");
            Console.WriteLine("EXPECTED:");
            foreach (var e in Expected)
                Console.WriteLine($"U+{e.CodePoint:X4}: {e.GetDisplayText()}");
            var Actual = new UnicodeEnumerable(Text).ToArray();
            Console.WriteLine("EXPECTED:");
            foreach (var a in Actual)
                Console.WriteLine($"U+{a.CodePoint:X4}: {a.GetDisplayText()}");
            CollectionAssert.AreEqual(Expected, Actual);
        }
    }
}

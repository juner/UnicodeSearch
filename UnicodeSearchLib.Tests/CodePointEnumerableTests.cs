using System;
using System.Collections.Generic;
using System.Linq;
using System.Unicode;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnicodeSearch.Tests
{
    [TestClass]
    public class CodePointEnumerableTests
    {
        static IEnumerable<object[]> GetEnumeratorTestData
        {
            get
            {
                yield return GetEnumeratorTestData("test",
                    char.ConvertToUtf32("t", 0),
                    char.ConvertToUtf32("e", 0),
                    char.ConvertToUtf32("s", 0),
                    char.ConvertToUtf32("t", 0)
                );
                yield return GetEnumeratorTestData("ㇷ゚",
                    char.ConvertToUtf32("ㇷ", 0),
                    char.ConvertToUtf32("゚", 0)
                );
                static object[] GetEnumeratorTestData(string Text, params int[] Expected)
                    => new object[] { Text, Expected };
            }
        }
        [TestMethod]
        [DynamicData(nameof(GetEnumeratorTestData))]
        public void GetEnumeratorTest(string Text, int[] Expected)
        {
            Console.WriteLine($"FROM TEXT: {Text}");
            Console.WriteLine("EXPECTED:");
            foreach (var e in Expected)
                Console.WriteLine($"U+{e:X4}");
            Console.WriteLine("ACTUAL:");
            var Actual = new List<int>();
            foreach (var a in new CodePointEnumerable(Text))
            {
                Console.WriteLine($"U+{a:X4}");
                Actual.Add(a);
            }
            CollectionAssert.AreEqual(Expected, Actual);
        }
    }
}

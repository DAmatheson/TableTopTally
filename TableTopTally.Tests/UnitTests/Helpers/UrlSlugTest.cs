using MongoDB.Bson;
using NUnit.Framework;
using TableTopTally.Helpers;

namespace TableTopTally.Tests.UnitTests.Helpers
{
    [TestFixture]
    public class UrlSlugTest
    {
        [TestCase("")]
        [TestCase(null)]
        [TestCase("      ")]
        public void URLFriendly_EmptyInputs_ReturnEmptyString(string inputTitle)
        {
            string result = UrlSlug.URLFriendly(inputTitle);

            StringAssert.AreEqualIgnoringCase(string.Empty, result);
        }

        [Test]
        public void GenerateSlug_StringWithSpaces_ReturnsTrimmedString()
        {
            string phrase = " aa ";

            string result = UrlSlug.URLFriendly(phrase);

            StringAssert.AreEqualIgnoringCase("aa", result);
        }

        [Test]
        public void GenerateSlug_StringContainingUppers_ReturnsLoweredString()
        {
            string phrase = "aAa";

            string result = UrlSlug.URLFriendly(phrase);

            StringAssert.AreEqualIgnoringCase("aaa", result);
        }

        [Test]
        public void GenerateSlug_StringWithSingleSpace_ReturnsHyphenatedString()
        {
            string phrase = "a a";

            string result = UrlSlug.URLFriendly(phrase);

            StringAssert.AreEqualIgnoringCase("a-a", result);
        }

        [Test]
        public void GenerateSlug_StringWithMultipleSpace_ReturnsSingleHyphenString()
        {
            string phrase = "a  a";

            string result = UrlSlug.URLFriendly(phrase);

            StringAssert.AreEqualIgnoringCase("a-a", result);
        }

        [Test]
        public void URLFriendly_EndingHyphen_IsRemoved()
        {
            string title = "a-";

            string result = UrlSlug.URLFriendly(title);

            StringAssert.AreEqualIgnoringCase("a", result);
        }

        [Test]
        public void GenerateSlug_StringWithValidCharacters_ReturnsSameString()
        {
            string phrase = "abcdefghijklmnopqrstuvwxyz1234567890-a";

            string result = UrlSlug.URLFriendly(phrase);

            StringAssert.AreEqualIgnoringCase(phrase, result);
        }

        [Test]
        public void GenerateSlug_LongString_Returns45CharacterString()
        {
            string phrase = "aaaaaaaaa1aaaaaaaaa2aaaaaaaaa3aaaaaaaaa4aaaaaaaaa5";

            string result = UrlSlug.URLFriendly(phrase);

            Assert.That(result.Length, Is.EqualTo(45));
        }

        [Test]
        public void GenerateSlug_SpaceAs45thChar_IsCutTo45AndTrimmed()
        {
            string phrase = "aaaaaaaaa1aaaaaaaaa2aaaaaaaaa3aaaaaaaaa4aaaa aaaa5";

            string result = UrlSlug.URLFriendly(phrase);

            Assert.That(result.Length, Is.EqualTo(44));
        }

        [TestCase("`~!@#$%^&*a", "a")]
        [TestCase("[](){}a", "a")]
        [TestCase("<>a", "a")]
        [TestCase("+a", "a")]
        [TestCase("|a", "a")]
        [TestCase("?a", "a")]
        [TestCase(";a", "a")]
        [TestCase(":a", "a")]
        [TestCase("'a", "a")] // Single quote literal
        [TestCase(@"""a", "a")] // Double quote literal
        [TestCase("_a", "a")]
        [TestCase("=a", "a")]
        [TestCase("/a", "a")]
        [TestCase(",a", "a")]
        [TestCase(".a", "a")]
        [TestCase("-a", "a")]
        [TestCase(@"\a", "a")] // Backslash literal
        public void URLFriendly_StartsWithInvalidCharacters_ReturnsCleanString(string input, string expected)
        {
            string result = UrlSlug.URLFriendly(input);

            StringAssert.AreEqualIgnoringCase(expected, result);
        }

        [TestCase("a`~!@#$%^&*a", "aa")]
        [TestCase("a[](){}a", "aa")]
        [TestCase("a<>a", "aa")]
        [TestCase("a+a", "aa")]
        [TestCase("a|a", "aa")]
        [TestCase("a?a", "aa")]
        [TestCase("a;a", "aa")]
        [TestCase("a:a", "aa")]
        [TestCase("a'a", "aa")] // Single quote literal
        [TestCase(@"a""a", "aa")] // Double quote literal
        [TestCase("a_a", "a-a")]
        [TestCase("a=a", "a-a")]
        [TestCase("a/a", "a-a")]
        [TestCase("a,a", "a-a")]
        [TestCase("a.a", "a-a")]
        [TestCase("a-a", "a-a")]
        [TestCase(@"a\a", "a-a")] // Backslash literal
        public void URLFriendly_ContainsInvalidCharacters_ReturnsCleanString(string input, string expected)
        {
            string result = UrlSlug.URLFriendly(input);

            StringAssert.AreEqualIgnoringCase(expected, result);
        }

        [TestCase("a`~!@#$%^&*", "a")]
        [TestCase("a[](){}", "a")]
        [TestCase("a<>", "a")]
        [TestCase("a+", "a")]
        [TestCase("a|", "a")]
        [TestCase("a?", "a")]
        [TestCase("a;", "a")]
        [TestCase("a:", "a")]
        [TestCase("a'", "a")] // Single quote literal
        [TestCase(@"a""", "a")] // Double quote literal
        [TestCase("a_", "a")]
        [TestCase("a=", "a")]
        [TestCase("a/", "a")]
        [TestCase("a,", "a")]
        [TestCase("a.", "a")]
        [TestCase("a-", "a")]
        [TestCase(@"a\", "a")] // Backslash literal
        public void URLFriendly_EndWithInvalidCharacters_ReturnsCleanString(string input, string expected)
        {
            string result = UrlSlug.URLFriendly(input);

            StringAssert.AreEqualIgnoringCase(expected, result);
        }

        [Test]
        public void URLFriendly_NonASCIICharacters_ReturnsConvertedString()
        {
            string title = "œ";

            string result = UrlSlug.URLFriendly(title);

            StringAssert.AreEqualIgnoringCase("oe", result);
        }

        [Test]
        public void URLFriendly_ObjectIdOverload_AppendsHexIdentifier()
        {
            string title = "a";
            ObjectId id = new ObjectId("54a07c8a4a91a323e83d78d2");

            string result = UrlSlug.URLFriendly(title, id);

            StringAssert.AreEqualIgnoringCase("a-8D1F10042DCA100", result);
        }

        [TestCase("àåáâäãåą", "aaaaaaaa")]
        [TestCase("èéêëę", "eeeee")]
        [TestCase("ìíîïı", "iiiii")]
        [TestCase("òóôõöøőð", "oooooooo")]
        [TestCase("ùúûüŭů", "uuuuuu")]
        [TestCase("çćčĉ", "cccc")]
        [TestCase("żźž", "zzz")]
        [TestCase("śşšŝ", "ssss")]
        [TestCase("ñń", "nn")]
        [TestCase("ýÿ", "yy")]
        [TestCase("ğĝ", "gg")]
        [TestCase("ř", "r")]
        [TestCase("ł", "l")]
        [TestCase("đ", "d")]
        [TestCase("ß", "ss")]
        [TestCase("Þ", "th")]
        [TestCase("ĥ", "h")]
        [TestCase("ĵ", "j")]
        [TestCase("æ", "ae")]
        [TestCase("œ", "oe")]
        [TestCase("µ", "")] // Anything not covered returns empty string
        public void RemapInternationalCharToAscii_InternationalCharacters_ReturnsConvertedString(string input, string expected)
        {
            string result = UrlSlug.URLFriendly(input);

            StringAssert.AreEqualIgnoringCase(expected, result);
        }
    }
}
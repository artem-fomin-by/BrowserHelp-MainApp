using NUnit.Framework;
using Logic;

namespace MainProgramTests;

internal class WildcardTests
{

    [Test]
    public void ConvertingTest()
    {
        var regexPattern = @".*\.youtube\.com/.*";
        var wildcard = @"*.youtube.com/*";
        var wildcardRegex = WildcardUtilities.WildcardToRegex(wildcard);
        Assert.AreEqual(regexPattern, wildcardRegex.ToString());
    }

    [TestCase("https://www.youtube.com/watch?v=cZhD6JNM3Go", "*.youtube.com/*", ExpectedResult = true)]
    [TestCase("https://www.youtube.com/", "*.youtube.com/*", ExpectedResult = true)]
    [TestCase("https://www.youtube.com", "*.youtube.com/*", ExpectedResult = true)]
    [TestCase("https://www.youtube.com/watch?v=cZhD6JNM3Go", "*.youtube.com", ExpectedResult = true)]
    [TestCase("https://www.youtube.com/", "*.youtube.com", ExpectedResult = true)]
    [TestCase("https://www.youtube.com", "*.youtube.com", ExpectedResult = true)]
    [TestCase("https://www.youtube.com/watch?v=cZhD6JNM3Go", "*.youtube.com/", ExpectedResult = false)]
    [TestCase("https://www.youtube.com/watch?v=cZhD6JNM3Go", ".youtube.com/", ExpectedResult = false)]
    [TestCase("https://www.youtube.com/", ".youtube.com/", ExpectedResult = false)]
    [TestCase("https://www.youtube.com", ".youtube.com/", ExpectedResult = false)]
    public bool MatchTest(string link, string pattern)
    {
        var browser = new Browser{ UrlPatterns = new[]{ pattern } };

        return browser.IsLinkMatch(link);
    }
}

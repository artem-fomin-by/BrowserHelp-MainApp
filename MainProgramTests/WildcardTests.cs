using NUnit.Framework;
using NUnit.Framework.Interfaces;
using Logic;

namespace MainProgramTests;

internal class WildcardTests
{
    private static bool HasPreviousTestPassed = true;

    [SetUp]
    public void SetUp()
    {
        Assume.That(HasPreviousTestPassed, Is.True);
    }

    [TearDown]
    public void TearDown()
    {
        HasPreviousTestPassed = 
            HasPreviousTestPassed && TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Passed;
    }

    [Test, Order(1)]
    public void ConvertingTest()
    {
        var regexPattern = @".*\.youtube\.com/.*";
        var wildcard = @"*.youtube.com/*";
        var wildcardRegex = WildcardUtilities.WildcardToRegex(wildcard);
        Assert.AreEqual(regexPattern, wildcardRegex.ToString());
    }

    [Test, Order(2)]
    public void MatchTest()
    {
        var browser = new Browser{ UrlPatterns = new[]{ @"*.youtube.com/*" } };

        var link1 = @"https://www.youtube.com/watch?v=cZhD6JNM3Go";
        var link2 = @"https://www.youtube.com/";

        Assert.That(browser.IsLinkMatch(link1));
        Assert.That(browser.IsLinkMatch(link2));
    }
}

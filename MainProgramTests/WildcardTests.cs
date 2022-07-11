

using Logic;
using NUnit.Framework;
using NUnit.Framework.Interfaces;

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
        if(HasPreviousTestPassed)
        {
            HasPreviousTestPassed = TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Passed;
        }
    }

    [Test]
    public void ConvertingTest()
    {
        var regexPattern = @".*\.youtube.com/.*";
        var wildcard = @"*.youtube.com/*";
        var wildcardRegex = WildcardUtilities.WildcardToRegex(wildcard);
        Assert.AreEqual(regexPattern, wildcardRegex.ToString());
    }
}
namespace Ultima.Extensions.Primitives.Tests;

using System.Text.Json;
using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public sealed class YearMonthTests
{
    [TestMethod]
    public void Constructor_FromJson_ShouldMapPropertiesCorrectly()
    {
        var subject = JsonSerializer.Deserialize<YearMonth>("{ \"Year\": 2023, \"Month\": 7 }");

        Assert.AreEqual(2023, subject.Year);
        Assert.AreEqual(7, subject.Month);
    }
}

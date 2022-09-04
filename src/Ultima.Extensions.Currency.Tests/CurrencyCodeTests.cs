namespace Ultima.Extensions.Currency.Tests;

using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public sealed class CurrencyCodeTests
{
    [TestMethod]
    [DataRow("THB", "TH")]
    [DataRow("EUR", null)]
    public void Parse_WithValidCode_ShouldReturnCorrespondingValue(string code, string? country)
    {
        var result = CurrencyCode.Parse(code);

        Assert.AreEqual(country, result.Country?.Name);
        Assert.AreEqual(code, result.Value);
    }

    [TestMethod]
    public void Parse_WithInvalidCode_ShouldThrow()
    {
        Assert.ThrowsException<FormatException>(() => CurrencyCode.Parse("USB"));
    }

    [TestMethod]
    public void CompareTo_WithEuroAndBaht_ShouldReturnNegativeNumber()
    {
        var eur = CurrencyCode.Parse("EUR");
        var thb = CurrencyCode.Parse("THB");

        Assert.IsTrue(eur.CompareTo(thb) < 0);
    }

    [TestMethod]
    public void CompareTo_WithEuroAndEuro_ShouldReturnZero()
    {
        var left = CurrencyCode.Parse("EUR");
        var right = CurrencyCode.Parse("EUR");

        Assert.IsTrue(left.CompareTo(right) == 0);
    }

    [TestMethod]
    public void CompareTo_WithNull_ShouldReturnPositive()
    {
        var eur = CurrencyCode.Parse("EUR");

        Assert.IsTrue(eur.CompareTo(null) > 0);
    }

    [TestMethod]
    public void Equals_WithEuroAndBaht_ShouldReturnFalse()
    {
        var eur = CurrencyCode.Parse("EUR");
        var thb = CurrencyCode.Parse("THB");

        Assert.IsFalse(eur.Equals(thb));
    }

    [TestMethod]
    public void Equals_WithEuroAndEuro_ShouldReturnTrue()
    {
        var left = CurrencyCode.Parse("EUR");
        var right = CurrencyCode.Parse("EUR");

        Assert.IsTrue(left.Equals(right));
    }

    [TestMethod]
    public void Equals_WithNull_ShouldReturnFalse()
    {
        var eur = CurrencyCode.Parse("EUR");

        Assert.IsFalse(eur.Equals(null));
    }
}

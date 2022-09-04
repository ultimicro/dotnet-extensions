namespace Ultima.Extensions.Currency.Tests;

using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public sealed class CurrencyCodeTests
{
    [TestMethod]
    [DataRow("THB", "TH", false, 0x00424854)]
    [DataRow("EUR", null, true, 0x00525545)]
    public void Parse_WithValidCode_ShouldReturnCorrespondingValue(string code, string? country, bool euro, int hash)
    {
        var result = CurrencyCode.Parse(code);

        Assert.AreEqual(country, result.Country?.Name);
        Assert.AreEqual(code, result.Value);

        if (BitConverter.IsLittleEndian)
        {
            Assert.AreEqual(hash, result.GetHashCode());
        }
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

        Assert.IsTrue(eur.CompareTo(CurrencyCode.Null) > 0);
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

        Assert.IsFalse(eur.Equals(CurrencyCode.Null));
    }

    [TestMethod]
    public void GetHashCode_WithNull_ShouldReturnZero()
    {
        Assert.AreEqual(0, CurrencyCode.Null.GetHashCode());
    }
}

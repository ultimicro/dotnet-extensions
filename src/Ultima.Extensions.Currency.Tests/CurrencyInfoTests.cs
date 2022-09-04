namespace Ultima.Extensions.Currency.Tests;

using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public sealed class CurrencyInfoTests
{
    [TestMethod]
    [DataRow(0, "0:0", "0.5:0", "1.5:2")]
    [DataRow(1, "0:0", "0.55:0.6", "0.65:0.6")]
    public void Round_WithNonOverflowValue_ShouldReturnBankersRounding(int digits, params string[] cases)
    {
        var subject = TestCurrency.Instance;

        subject.SetFractionalDigits(digits);

        for (var i = 0; i < cases.Length; i++)
        {
            var @case = cases[i].Split(':');
            var result = subject.Round(decimal.Parse(@case[0]));

            Assert.AreEqual(decimal.Parse(@case[1]), result);
        }
    }

    [TestMethod]
    [DataRow(0, "0", "-1", "1")]
    [DataRow(1, "0", "-1", "1", "0.1", "-0.1", "1.1", "-1.1")]
    [DataRow(2, "0", "-1", "1", "0.1", "-0.1", "1.1", "-1.1", "0.01", "-0.01", "1.01", "-1.01", "0.11", "-0.11", "1.11", "-1.11")]
    public void IsValidAmount_WithValidMinorUnit_ShouldReturnTrueOnValidAmount(int digits, params string[] amounts)
    {
        var subject = TestCurrency.Instance;

        subject.SetFractionalDigits(digits);

        for (var i = 0; i < amounts.Length; i++)
        {
            var amount = decimal.Parse(amounts[i]);
            var result = subject.IsValidAmount(amount);

            Assert.IsTrue(result);
        }
    }

    [TestMethod]
    [DataRow(0, "0.1", "-1.1", "1.1")]
    [DataRow(1, "0.11", "-0.11", "1.11", "-1.11")]
    [DataRow(2, "0.111", "-0.111", "1.111", "-1.111")]
    public void IsValidAmount_WithValidMinorUnit_ShouldReturnFalseOnInvalidAmount(int digits, params string[] amounts)
    {
        var subject = TestCurrency.Instance;

        subject.SetFractionalDigits(digits);

        for (var i = 0; i < amounts.Length; i++)
        {
            var amount = decimal.Parse(amounts[i]);
            var result = subject.IsValidAmount(amount);

            Assert.IsFalse(result);
        }
    }

    [TestMethod]
    [DataRow(0, "0:0", "-1:-1", "1:1")]
    [DataRow(1, "0:0", "-1:-10", "1:10", "0.1:1", "-0.1:-1", "1.1:11", "-1.1:-11")]
    [DataRow(2, "0:0", "-1:-100", "1:100", "0.1:10", "-0.1:-10", "1.1:110", "-1.1:-110", "0.01:1", "-0.01:-1", "1.01:101", "-1.01:-101", "0.11:11", "-0.11:-11", "1.11:111", "-1.11:-111")]
    public void GetMinorUnitAmount_WithValidAmount_ShouldReturnCorrectMinorUnit(int digits, params string[] cases)
    {
        var subject = TestCurrency.Instance;

        subject.SetFractionalDigits(digits);

        for (var i = 0; i < cases.Length; i++)
        {
            var @case = cases[i].Split(':');
            var result = subject.GetMinorUnitAmount(decimal.Parse(@case[0]));

            Assert.AreEqual(long.Parse(@case[1]), result);
        }
    }

    [TestMethod]
    [DataRow(0, "0.1", "-1.1", "1.1")]
    [DataRow(1, "0.11", "-0.11", "1.11", "-1.11")]
    [DataRow(2, "0.111", "-0.111", "1.111", "-1.111")]
    public void GetMinorUnitAmount_WithInvalidAmount_ShouldThrow(int minor, params string[] amounts)
    {
        var subject = TestCurrency.Instance;

        subject.SetFractionalDigits(minor);

        for (var i = 0; i < amounts.Length; i++)
        {
            var amount = decimal.Parse(amounts[i]);

            Assert.ThrowsException<ArgumentException>(() => subject.GetMinorUnitAmount(amount));
        }
    }
}

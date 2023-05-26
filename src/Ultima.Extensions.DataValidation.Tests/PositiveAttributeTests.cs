namespace Ultima.Extensions.DataValidation.Tests;

using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public sealed class PositiveAttributeTests
{
    private readonly PositiveAttribute subject;

    public PositiveAttributeTests()
    {
        this.subject = new();
    }

    [TestMethod]
    [DataRow(1U)]
    [DataRow(uint.MaxValue)]
    public void IsValid_WithValidUIntPtr_ShouldReturnTrue(uint value)
    {
        Assert.IsTrue(this.subject.IsValid(new UIntPtr(value)));
    }

    [TestMethod]
    public void IsValid_WithInvalidUIntPtr_ShouldReturnFalse()
    {
        Assert.IsFalse(this.subject.IsValid(UIntPtr.Zero));
    }
}

namespace Ultima.Extensions.DataValidation;

using System.Numerics;

/// <summary>
/// Specifies that the number must be greater or equal than zero.
/// </summary>
[AttributeUsage(AttributeTargets.Parameter | AttributeTargets.Property)]
public sealed class NonNegativeAttribute : NumberValidationAttribute
{
    protected override bool IsValid(sbyte value) => value >= 0;

    protected override bool IsValid(byte value) => true;

    protected override bool IsValid(short value) => value >= 0;

    protected override bool IsValid(ushort value) => true;

    protected override bool IsValid(int value) => value >= 0;

    protected override bool IsValid(uint value) => true;

    protected override bool IsValid(long value) => value >= 0;

    protected override bool IsValid(ulong value) => true;

    protected override bool IsValid(nint value) => value >= 0;

    protected override bool IsValid(UIntPtr value) => true;

    protected override bool IsValid(BigInteger value) => value >= BigInteger.Zero;

    protected override bool IsValid(decimal value) => value >= 0m;
}

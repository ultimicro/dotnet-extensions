namespace Ultima.Extensions.DataValidation;

using System.Numerics;

/// <summary>
/// Specifies that the number must be greater than zero.
/// </summary>
[AttributeUsage(AttributeTargets.Parameter | AttributeTargets.Property)]
public sealed class PositiveAttribute : NumberValidationAttribute
{
    protected override bool IsValid(sbyte value) => value > 0;

    protected override bool IsValid(byte value) => value > 0;

    protected override bool IsValid(short value) => value > 0;

    protected override bool IsValid(ushort value) => value > 0;

    protected override bool IsValid(int value) => value > 0;

    protected override bool IsValid(uint value) => value > 0;

    protected override bool IsValid(long value) => value > 0;

    protected override bool IsValid(ulong value) => value > 0;

    protected override bool IsValid(nint value) => value > 0;

    protected override bool IsValid(UIntPtr value) => value.CompareTo(UIntPtr.Zero) > 0;

    protected override bool IsValid(BigInteger value) => value > BigInteger.Zero;

    protected override bool IsValid(decimal value) => value > 0m;
}

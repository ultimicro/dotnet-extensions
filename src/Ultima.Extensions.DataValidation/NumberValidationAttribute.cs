namespace Ultima.Extensions.DataValidation;

using System.ComponentModel.DataAnnotations;
using System.Numerics;

/// <summary>
/// Base class for numeric validation.
/// </summary>
/// <remarks>
/// The supported types are <see cref="sbyte"/>, <see cref="short"/>, <see cref="int"/>, <see cref="long"/>, <see cref="nint"/>, <see cref="BigInteger"/> and
/// <see cref="decimal"/>.
/// </remarks>
public abstract class NumberValidationAttribute : ValidationAttribute
{
    /// <summary>
    /// Gets or sets a value indicating whether unsupported type is allowed.
    /// </summary>
    /// <value>
    /// <see langword="true"/> if unsupported type is allowed; otherwise, <see langword="false"/>. The default value is <see langword="false"/>.
    /// </value>
    public bool AllowUnsupportedType { get; set; }

    public override bool IsValid(object? value) => value switch
    {
        sbyte v => this.IsValid(v),
        byte v => this.IsValid(v),
        short v => this.IsValid(v),
        ushort v => this.IsValid(v),
        int v => this.IsValid(v),
        uint v => this.IsValid(v),
        long v => this.IsValid(v),
        ulong v => this.IsValid(v),
        nint v => this.IsValid(v),
        UIntPtr v => this.IsValid(v),
        BigInteger v => this.IsValid(v),
        decimal v => this.IsValid(v),
        null => true,
        _ => this.AllowUnsupportedType,
    };

    protected abstract bool IsValid(sbyte value);

    protected abstract bool IsValid(byte value);

    protected abstract bool IsValid(short value);

    protected abstract bool IsValid(ushort value);

    protected abstract bool IsValid(int value);

    protected abstract bool IsValid(uint value);

    protected abstract bool IsValid(long value);

    protected abstract bool IsValid(ulong value);

    protected abstract bool IsValid(nint value);

    protected abstract bool IsValid(UIntPtr value);

    protected abstract bool IsValid(BigInteger value);

    protected abstract bool IsValid(decimal value);
}

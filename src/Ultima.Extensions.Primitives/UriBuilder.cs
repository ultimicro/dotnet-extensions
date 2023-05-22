namespace Ultima.Extensions.Primitives;

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Runtime.CompilerServices;
using System.Text;

/// <summary>
/// A class for building a <see cref="Uri"/>.
/// </summary>
/// <remarks>
/// This class is more powerful and easier to use than <see cref="System.UriBuilder"/>.
/// </remarks>
public sealed class UriBuilder
{
    private static readonly Rune ExclamationMark = new('!');
    private static readonly Rune DollarSign = new('$');
    private static readonly Rune Ampersand = new('&');
    private static readonly Rune Apostrphe = new('\'');
    private static readonly Rune LeftParenthesis = new('(');
    private static readonly Rune RightParenthesis = new(')');
    private static readonly Rune Asterisk = new('*');
    private static readonly Rune PlusSign = new('+');
    private static readonly Rune Comma = new(',');
    private static readonly Rune Hyphen = new('-');
    private static readonly Rune FullStop = new('.');
    private static readonly Rune Solidus = new('/');
    private static readonly Rune DigitZero = new('0');
    private static readonly Rune DigitNine = new('9');
    private static readonly Rune Colon = new(':');
    private static readonly Rune Semicolon = new(';');
    private static readonly Rune EqualSign = new('=');
    private static readonly Rune QuestionMark = new('?');
    private static readonly Rune CommercialAt = new('@');
    private static readonly Rune LatinCapitalLetterA = new('A');
    private static readonly Rune LatinCapitalLetterZ = new('Z');
    private static readonly Rune LatinSmallLetterA = new('a');
    private static readonly Rune LatinSmallLetterZ = new('z');
    private static readonly Rune LowLine = new('_');
    private static readonly Rune Tilde = new('~');

    private readonly List<string> paths;
    private readonly NameValueCollection queries;
    private Uri @base;

    private UriBuilder(Uri @base)
    {
        this.paths = new();
        this.queries = new(StringComparer.Ordinal);
        this.@base = @base;
    }

    /// <summary>
    /// Gets or sets the base URI to use.
    /// </summary>
    /// <exception cref="ArgumentException">
    /// Attempt to set the property with non-absolute URI, has some query or has some fragment.
    /// </exception>
    public Uri Base
    {
        get => this.@base;
        set
        {
            CheckBase(value);
            this.@base = value;
        }
    }

    /// <summary>
    /// Create an <see cref="UriBuilder"/> with a specified base URI.
    /// </summary>
    /// <param name="base">
    /// Base URI to use.
    /// </param>
    /// <returns>
    /// An <see cref="UriBuilder"/> with <see cref="Base"/> equal to <paramref name="base"/>.
    /// </returns>
    /// <exception cref="ArgumentException">
    /// <paramref name="base"/> is non-absolute URI, has some query or has some fragment.
    /// </exception>
    public static UriBuilder For(Uri @base)
    {
        CheckBase(@base);
        return new(@base);
    }

    /// <summary>
    /// Create an <see cref="UriBuilder"/> with a specified base URI.
    /// </summary>
    /// <param name="base">
    /// Base URI to use.
    /// </param>
    /// <returns>
    /// An <see cref="UriBuilder"/> with <see cref="Base"/> equal to <paramref name="base"/>.
    /// </returns>
    /// <exception cref="ArgumentException">
    /// <paramref name="base"/> is not a valid URI, non-absolute, has some query or has some fragment.
    /// </exception>
    public static UriBuilder For(string @base)
    {
        Uri uri;

        try
        {
            uri = new(@base);
        }
        catch (FormatException ex)
        {
            throw new ArgumentException("The value is not a valid URI.", nameof(@base), ex);
        }

        CheckBase(uri, nameof(@base));

        return new(uri);
    }

    /// <summary>
    /// Append path with a specified value.
    /// </summary>
    /// <param name="path">
    /// Path to append.
    /// </param>
    /// <returns>
    /// The current <see cref="UriBuilder"/>.
    /// </returns>
    /// <exception cref="ArgumentException">
    /// <paramref name="path"/> is empty, contains invalid unicode characters, start or end with /.
    /// </exception>
    /// <remarks>
    /// This method will encode each path component in <paramref name="path"/> with percent-encoding automatically.
    /// </remarks>
    public UriBuilder AppendPath(string path)
    {
        // Simple check.
        if (path.Length == 0)
        {
            throw new ArgumentException("The value is empty.", nameof(path));
        }
        else if (path[0] == '/')
        {
            throw new ArgumentException("The value start with '/'.", nameof(path));
        }
        else if (path[^1] == '/')
        {
            throw new ArgumentException("The value end with '/'.", nameof(path));
        }

        // Encode.
        var encoded = new StringBuilder();
        var i = 0;

        while (i < path.Length)
        {
            if (!Rune.TryGetRuneAt(path, i, out var r))
            {
                throw new ArgumentException("The value contains unknown characters.", nameof(path));
            }

            var length = r.Utf16SequenceLength;
            var chars = path.AsSpan(i, length);

            // https://stackoverflow.com/a/4669755/1829232
            if (IsAllowedInPath(r) || r == Solidus)
            {
                encoded.Append(chars);
            }
            else
            {
                PercentEncode(encoded, chars);
            }

            i += length;
        }

        this.paths.Add(encoded.ToString());

        return this;
    }

    /// <summary>
    /// Append the query string.
    /// </summary>
    /// <param name="name">
    /// Name of the query.
    /// </param>
    /// <param name="value">
    /// Value of the query.
    /// </param>
    /// <returns>
    /// The current <see cref="UriBuilder"/>.
    /// </returns>
    /// <exception cref="ArgumentException">
    /// <paramref name="name"/> or <paramref name="value"/> is empty or contains invalid unicode characters.
    /// </exception>
    /// <remarks>
    /// <para>
    /// <paramref name="name"/> is not unique so you can add multiple pair with the same name.
    /// </para>
    /// <para>
    /// This method will automatically encode <paramref name="name"/> and <paramref name="value"/> with percent-encoding.
    /// </para>
    /// </remarks>
    public UriBuilder AppendQuery(string name, string value)
    {
        if (name.Length == 0)
        {
            throw new ArgumentException("The value is empty.", nameof(name));
        }

        if (value.Length == 0)
        {
            throw new ArgumentException("The value is empty.", nameof(value));
        }

        var encodedName = EncodeQuery(name) ?? throw new ArgumentException("The value contains unknown characters.", nameof(name));
        var encodedValue = EncodeQuery(value) ?? throw new ArgumentException("The value contains unknown characters.", nameof(value));

        this.queries.Add(encodedName, encodedValue);

        return this;
    }

    public Uri BuildUri() => new(this.Build());

    public string Build()
    {
        var result = new StringBuilder();

        // Base.
        var @base = this.@base.AbsoluteUri;

        if (@base[^1] == '/')
        {
            result.Append(@base.AsSpan()[..^1]);
        }
        else
        {
            result.Append(@base);
        }

        // Path.
        foreach (var path in this.paths)
        {
            result.Append('/');
            result.Append(path);
        }

        // Query.
        if (this.queries.Count > 0)
        {
            var appended = false;

            result.Append('?');

            foreach (string name in this.queries)
            {
                foreach (var value in this.queries.GetValues(name)!)
                {
                    if (appended)
                    {
                        result.Append('&');
                    }

                    result.Append(name);
                    result.Append('=');
                    result.Append(value);

                    appended = true;
                }
            }
        }

        return result.ToString();
    }

    private static void CheckBase(Uri value, [CallerArgumentExpression("value")] string? paramName = null)
    {
        if (!value.IsAbsoluteUri)
        {
            throw new ArgumentException("The value is not an absolute URI.", paramName);
        }
        else if (value.Query.Length != 0)
        {
            throw new ArgumentException("The value has query string.", paramName);
        }
        else if (value.Fragment.Length != 0)
        {
            throw new ArgumentException("The value has fragment.", paramName);
        }
    }

    private static string? EncodeQuery(string input)
    {
        var encoded = new StringBuilder();
        var i = 0;

        while (i < input.Length)
        {
            if (!Rune.TryGetRuneAt(input, i, out var r))
            {
                return null;
            }

            var length = r.Utf16SequenceLength;
            var chars = input.AsSpan(i, length);

            // https://stackoverflow.com/a/2375597/1829232
            // https://stackoverflow.com/a/2924187/1829232
            if (r != EqualSign && r != Ampersand && (IsAllowedInPath(r) || r == Solidus || r == QuestionMark))
            {
                encoded.Append(chars);
            }
            else
            {
                PercentEncode(encoded, chars);
            }

            i += length;
        }

        return encoded.ToString();
    }

    private static void PercentEncode(StringBuilder output, ReadOnlySpan<char> input)
    {
        Span<byte> utf8 = stackalloc byte[4];
        Span<char> hex = stackalloc char[2];
        var length = Encoding.UTF8.GetBytes(input, utf8);

        for (var i = 0; i < length; i++)
        {
            var b = utf8[i];

            hex[0] = ToHex((byte)(b >> 4));
            hex[1] = ToHex((byte)(b & 0xF));

            output.Append('%');
            output.Append(hex);
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static char ToHex(byte lower) => lower switch
    {
        0x0 => '0',
        0x1 => '1',
        0x2 => '2',
        0x3 => '3',
        0x4 => '4',
        0x5 => '5',
        0x6 => '6',
        0x7 => '7',
        0x8 => '8',
        0x9 => '9',
        0xA => 'A',
        0xB => 'B',
        0xC => 'C',
        0xD => 'D',
        0xE => 'E',
        0xF => 'F',
        _ => throw new ArgumentOutOfRangeException(nameof(lower)),
    };

    private static bool IsAllowedInPath(Rune r) => IsUnreserved(r) || IsSubDelims(r) || r == Colon || r == CommercialAt;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static bool IsUnreserved(Rune r) =>
        IsCapitalLetter(r) ||
        IsSmallLetter(r) ||
        IsDigit(r) ||
        r == Hyphen ||
        r == FullStop ||
        r == LowLine ||
        r == Tilde;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static bool IsSubDelims(Rune r) =>
        r == ExclamationMark ||
        r == DollarSign ||
        r == Ampersand ||
        r == Apostrphe ||
        r == LeftParenthesis ||
        r == RightParenthesis ||
        r == Asterisk ||
        r == PlusSign ||
        r == Comma ||
        r == Semicolon ||
        r == EqualSign;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static bool IsDigit(Rune r) => r >= DigitZero && r <= DigitNine;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static bool IsCapitalLetter(Rune r) => r >= LatinCapitalLetterA && r <= LatinCapitalLetterZ;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static bool IsSmallLetter(Rune r) => r >= LatinSmallLetterA && r <= LatinSmallLetterZ;
}

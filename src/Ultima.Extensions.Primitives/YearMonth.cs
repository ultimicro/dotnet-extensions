namespace Ultima.Extensions.Primitives;

using System;

/// <summary>
/// Represents year and month without day.
/// </summary>
public readonly struct YearMonth
{
    public const int MinYear = 1;
    public const int MaxYear = 9999;
    public const int MinMonth = 1;
    public const int MaxMonth = 12;

    private readonly int month; // Zero based.
    private readonly int year; // Same here.

    /// <summary>
    /// Initializes a new instance of the <see cref="YearMonth"/> struct.
    /// </summary>
    /// <param name="year">
    /// The year (1 through 9999).
    /// </param>
    /// <param name="month">
    /// The month (1 through 12).
    /// </param>
    /// <exception cref="ArgumentOutOfRangeException">
    /// <paramref name="year"/> or <paramref name="month"/> is not valid.
    /// </exception>
    public YearMonth(int year, int month)
    {
        if (year < MinYear || year > MaxYear)
        {
            throw new ArgumentOutOfRangeException(nameof(year));
        }

        if (month < MinMonth || month > MaxMonth)
        {
            throw new ArgumentOutOfRangeException(nameof(month));
        }

        this.year = year - 1;
        this.month = month - 1;
    }

    /// <summary>
    /// Gets the year component of the date represented by this instance.
    /// </summary>
    public int Year => this.year + 1;

    /// <summary>
    /// Gets the month component of the date represented by this instance.
    /// </summary>
    public int Month => this.month + 1;
}

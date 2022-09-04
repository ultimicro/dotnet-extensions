# .NET Extensions

This is a collection of lightweight .NET libraries to provide some missing features.

## Packages

| Package                       | Version                                                                                                                                                      |
| ----------------------------- | ------------------------------------------------------------------------------------------------------------------------------------------------------------ |
| Ultima.Extensions.CommandLine | [![Nuget (with prereleases)](https://img.shields.io/nuget/vpre/Ultima.Extensions.CommandLine)](https://www.nuget.org/packages/Ultima.Extensions.CommandLine) |

## Ultima.Extensions.CommandLine

Provides some utilities and missing features for [System.CommandLine](https://www.nuget.org/packages/System.CommandLine).

| Type           | Description                                                                   |
| -------------- | ----------------------------------------------------------------------------- |
| CommandHandler | An implementation of `ICommandHandler` that automatic handle interupt signal. |

## Ultima.Extensions.Currency

Provides some types to handling currency.

| Type         | Description                               |
| ------------ | ----------------------------------------- |
| CurrencyCode | Provides type safety for a currency code. |
| CurrencyInfo | Provides details for a currency.          |

Available `CurrencyInfo` implementation:

| Type               | Wikipedia                                          |
| ------------------ | -------------------------------------------------- |
| Euro               | https://en.wikipedia.org/wiki/Euro                 |
| SingaporeDollar    | https://en.wikipedia.org/wiki/Singapore_dollar     |
| SouthKoreanWon     | https://en.wikipedia.org/wiki/South_Korean_won     |
| ThaiBaht           | https://en.wikipedia.org/wiki/Thai_baht            |
| UnitedStatesDollar | https://en.wikipedia.org/wiki/United_States_dollar |

**Please note that `CurrencyCode.Parse` will accept any valid currency, including a test currency with XTS code. You MUST check `CurrencyInfo.IsOfficial` when accepting a currency from the user.**

## License

MIT

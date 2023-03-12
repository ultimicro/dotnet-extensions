# .NET Extensions

This is a collection of lightweight .NET libraries providing some missing features. These libraries has been using on production environment of [Cloudsumé](https://cloudsume.com) so it is production ready.

## Ultima.Extensions.Collections
[![Nuget](https://img.shields.io/nuget/v/Ultima.Extensions.Collections)](https://www.nuget.org/packages/Ultima.Extensions.Collections)

Provides additional types and methods for handle collections.

| Type                  | Description                                                |
| --------------------- | ---------------------------------------------------------- |
| DictionaryExtensions  | A collection of extension methods for `Dictionary`.        |
| KeyedByTypeCollection | A collection to store unique items identified by its type. |
| TypeComparer          | Provides implementations of `IComparer` for `Type`.        |

## Ultima.Extensions.CommandLine
[![Nuget (with prereleases)](https://img.shields.io/nuget/vpre/Ultima.Extensions.CommandLine)](https://www.nuget.org/packages/Ultima.Extensions.CommandLine)

Provides some utilities and missing features for [System.CommandLine](https://www.nuget.org/packages/System.CommandLine).

| Type           | Description                                                                   |
| -------------- | ----------------------------------------------------------------------------- |
| CommandHandler | An implementation of `ICommandHandler` that automatic handle interupt signal. |

## Ultima.Extensions.Currency
[![Nuget](https://img.shields.io/nuget/v/Ultima.Extensions.Currency)](https://www.nuget.org/packages/Ultima.Extensions.Currency)

Provides some types to handling currency.

| Type         | Description                               |
| ------------ | ----------------------------------------- |
| CurrencyCode | Provides type safety for a currency code. |
| CurrencyInfo | Provides details for a currency.          |

Available `CurrencyInfo` implementation:

| Type               | Wikipedia                                          |
| ------------------ | -------------------------------------------------- |
| Euro               | https://en.wikipedia.org/wiki/Euro                 |
| IndianRupee        | https://en.wikipedia.org/wiki/Indian_rupee         |
| JapaneseYen        | https://en.wikipedia.org/wiki/Japanese_yen         |
| SingaporeDollar    | https://en.wikipedia.org/wiki/Singapore_dollar     |
| SouthKoreanWon     | https://en.wikipedia.org/wiki/South_Korean_won     |
| ThaiBaht           | https://en.wikipedia.org/wiki/Thai_baht            |
| UnitedStatesDollar | https://en.wikipedia.org/wiki/United_States_dollar |

**Please note that `CurrencyCode.Parse` will accept any valid currency, including a test currency with XTS code. You MUST check `CurrencyInfo.IsOfficial` when accepting a currency from the user.**

### Breaking changes

#### 1.0 to 2.0

`CurrencyCode` was changed from `struct` to `class`.

## Ultima.Extensions.Security
[![Nuget](https://img.shields.io/nuget/v/Ultima.Extensions.Security)](https://www.nuget.org/packages/Ultima.Extensions.Security)

Provides additional types and methods for types in `System.Security` namespace.

| Type                       | Description                                              |
| -------------------------- | -------------------------------------------------------- |
| ClaimsPrincipalExtensions  | A collection of extension methods for `ClaimsPrincipal`. |

## License

MIT

namespace FoodDelivery.BuildingBlocks.Domain.ValueObjects;

public sealed class Currency : IEquatable<Currency>
{
    private static readonly Dictionary<string, Currency> _supported =
        new(StringComparer.OrdinalIgnoreCase);

    public string Code { get; }

    private Currency(string code)
    {
        Code = code;
    }

    static Currency()
    {
        Register("USD");
        Register("EUR");
        Register("GBP");
        Register("JPY");
    }

    private static void Register(string code)
    {
        _supported[code] = new Currency(code);
    }

    public static Currency From(string code)
    {
        if (string.IsNullOrWhiteSpace(code))
            throw new ArgumentException("Currency code is required.", nameof(code));

        code = code.ToUpperInvariant();

        if (!_supported.TryGetValue(code, out var currency))
            throw new ArgumentException($"Unsupported currency: {code}", nameof(code));

        return currency;
    }

    public static IReadOnlyCollection<Currency> All => _supported.Values;

    public bool Equals(Currency other)
        => other is not null && Code == other.Code;

    public override bool Equals(object obj)
        => Equals(obj as Currency);

    public override int GetHashCode()
        => Code.GetHashCode(StringComparison.OrdinalIgnoreCase);

    public override string ToString() => Code;
}
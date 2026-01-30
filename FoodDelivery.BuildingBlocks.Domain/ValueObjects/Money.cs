using System.Globalization;

namespace FoodDelivery.BuildingBlocks.Domain.ValueObjects
{
    public sealed class Money : IEquatable<Money>
    {
        public decimal Amount { get; }
        public Currency Currency { get; }

        public Money(decimal amount, Currency currency)
        {
            Currency = currency ?? throw new ArgumentNullException(nameof(currency));
            Amount = decimal.Round(amount, 2, MidpointRounding.AwayFromZero);
        }

        public static Money Zero(Currency currency)
            => new Money(0m, currency);

        public Money Add(Money other)
        {
            EnsureSameCurrency(other);
            return new Money(Amount + other.Amount, Currency);
        }

        public Money Subtract(Money other)
        {
            EnsureSameCurrency(other);
            return new Money(Amount - other.Amount, Currency);
        }

        public Money Multiply(decimal factor)
            => new Money(Amount * factor, Currency);

        private void EnsureSameCurrency(Money other)
        {
            if (other is null)
                throw new ArgumentNullException(nameof(other));

            if (!Currency.Equals(other.Currency))
                throw new InvalidOperationException(
                    "Money operations require the same currency.");
        }

        public bool Equals(Money other)
            => other is not null &&
               Amount == other.Amount &&
               Currency.Equals(other.Currency);

        public override bool Equals(object obj)
            => Equals(obj as Money);

        public override int GetHashCode()
            => HashCode.Combine(Amount, Currency);

        public static bool operator ==(Money left, Money right)
            => Equals(left, right);

        public static bool operator !=(Money left, Money right)
            => !Equals(left, right);

        public static Money operator +(Money left, Money right)
            => left.Add(right);

        public static Money operator -(Money left, Money right)
            => left.Subtract(right);

        public override string ToString()
        {
            return string.Format(
                CultureInfo.InvariantCulture,
                "{0} {1:N2}",
                Currency,
                Amount);
        }
    }
}

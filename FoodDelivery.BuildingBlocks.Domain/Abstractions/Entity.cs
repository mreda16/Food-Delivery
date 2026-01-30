using System;
using System.Collections.Generic;

namespace FoodDelivery.BuildingBlocks.Domain.Abstractions
{
    public abstract class Entity<TKey, TEntity> : IEquatable<TEntity>
        where TEntity : Entity<TKey, TEntity>
    {
        private readonly List<IDomainEvent> _domainEvents = new();

        public TKey ID { get; protected set; }

        public IReadOnlyCollection<IDomainEvent> DomainEvents =>
            _domainEvents.AsReadOnly();

        protected Entity()
        {
        }

        public bool Equals(TEntity other)
        {
            if (other is null)
                return false;

            if (ReferenceEquals(this, other))
                return true;

            if (IsTransient() || other.IsTransient())
                return false;

            return EqualityComparer<TKey>.Default.Equals(ID, other.ID);
        }

        public override bool Equals(object obj)
        {
            return obj is TEntity other && Equals(other);
        }

        public override int GetHashCode()
        {
            return IsTransient()
                ? base.GetHashCode()
                : EqualityComparer<TKey>.Default.GetHashCode(ID);
        }

        protected bool IsTransient()
        {
            return EqualityComparer<TKey>.Default.Equals(ID, default);
        }

        protected void AddDomainEvent(IDomainEvent domainEvent)
        {
            if (domainEvent is null)
                throw new ArgumentNullException(nameof(domainEvent));

            _domainEvents.Add(domainEvent);
        }

        public void ClearDomainEvents()
        {
            _domainEvents.Clear();
        }

        public static bool operator ==(
            Entity<TKey, TEntity> left,
            Entity<TKey, TEntity> right)
        {
            if (left is null && right is null)
                return true;

            if (left is null || right is null)
                return false;

            return left.Equals((TEntity)right);
        }

        public static bool operator !=(
            Entity<TKey, TEntity> left,
            Entity<TKey, TEntity> right)
        {
            return !(left == right);
        }
    }
}
